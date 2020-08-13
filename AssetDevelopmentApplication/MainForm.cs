using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sample3dscan.cs;
using SampleDX;
using System.Threading;
using System.Threading.Tasks;

using DotNetBrowser;
using DotNetBrowser.WinForms;
using DotNetBrowser.Events;
using Ionic.Zip;

using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using EdgeJs;
using System.Net.Http;
using System.Net.Http.Headers;

namespace sample3dscan.cs
{
    public partial class MainForm : Form
    {
        private PXCMSession session;
        private volatile bool closing = false;
        private volatile bool stop = false;
        private Boolean startStop = true;
        private Boolean userFacingCamera = true;
        private Boolean scanReconstruct = true;
        public bool scan_requested = false;
        public bool reconstruct_requested = false;
        public bool scanning = false;
        private string filename = null;
        private string mesh_filename = null;
        private Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo> devices = new Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo>();
        private Dictionary<ToolStripMenuItem, int> devices_iuid = new Dictionary<ToolStripMenuItem, int>();
        private Dictionary<ToolStripMenuItem, PXCMCapture.Device.StreamProfile> profiles = new Dictionary<ToolStripMenuItem, PXCMCapture.Device.StreamProfile>();
        private D2D1Render render = new D2D1Render();
        private Color OriginalReconstructBackColor;

        System.IO.Stream myStream;
        private String mySelectedFiles;
        String[] ports;
        public SerialPort myport;

        public Browser myBrowser;
        string projectname;

        MqttClient client;
        string clientId;
        //string BrokerAddress = "192.168.43.177";
        //string BrokerAddress = "192.168.42.106";
        string BrokerAddress = "x1.hcm-lab.id";

        delegate void SetTextCallback(string text);
        string sub1;
        string sub2;
        string sub3;
        string sub4;
        string sub5;
        string sub6;
        string pub1;
        string pub2;
        string pub3;
        string appmsg1;
        string appmsg2;
        string appmsg3;
        string webmsg;
        string topicmsg;
        int counter;

        Archiving ar = new Archiving();
        bool YesDo = false;

        public enum ButtonState { 
            // LeftButtonState_RightButtonState
            SCe_SSd = 0,    // Streaming stopped (Start Camera enabled, Start Scanning disabled)
            Cd_SSd,         // Waiting for stream start (Cancel disabled, Start Scanning disabled)
            Ce_SSd,         // Start Camera pressed (Cancel enabled, Start Scanning disabled)
            Ce_SSd2,        // Streaming started (Cancel enabled, Start Scanning disabled)
            Ce_SSe,         // Scanning preconditions met (Cancel enabled, Start Scanning enabled)
            Ce_ESd,         // Start Scanning pressed (Cancel enabled, End Scanning disabled)
            Ce_ESe,         // Scanning started (Cancel enabled, End Scanning enabled)
            Cd_ESd,         // Scanning ended (Cancel disabled, End Scanning disabled)
        };
        private ButtonState buttonState = ButtonState.SCe_SSd;

        public ButtonState GetButtonState(ButtonState state)
        {
            return buttonState;
        }

        public void SetButtonState(ButtonState state)
        {
            Color RECORD_COLOR = Color.Red;
            // Consolidated logic to restore the color of the Reconstruct button after the scanning has ended.
            if (Reconstruct.BackColor == RECORD_COLOR) Reconstruct.BackColor = OriginalReconstructBackColor;

            buttonState = state;
            switch (buttonState)
            {
                case ButtonState.SCe_SSd: // Streaming stopped (Start Camera enabled, Start Scanning disabled)
                    Start.Text = "Start Camera";
                    Start.Enabled = true;
                    Start.Focus();
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = false;
                    break;
                case ButtonState.Cd_SSd: // Waiting for streaming start (Cancel disabled, Start Scanning disabled)
                    Start.Text = "Cancel";
                    Start.Enabled = false;
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = false;
                    break;
                case ButtonState.Ce_SSd: // Start Camera pressed (Cancel enabled, Start Scanning disabled)
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Start.Focus();
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = false; 
                    break;
                case ButtonState.Ce_SSd2: // Streaming started (Cancel enabled, Start Scanning disabled)
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Start.Focus();
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = false; 
                    break;
                case ButtonState.Ce_SSe: // Scanning preconditions met (Cancel enabled, Start Scanning enabled)
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Reconstruct.Text = "Start Scanning";
                    Reconstruct.Enabled = true;
                    Reconstruct.Focus();
                    break;
                case ButtonState.Ce_ESd: // Start Scanning pressed (Cancel enabled, End Scanning disabled)
                    scan_requested = true;
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Start.Focus();
                    Reconstruct.Text = "End Scanning";
                    Reconstruct.Enabled = false; 
                    break;
                case ButtonState.Ce_ESe: // Scanning started (Cancel enabled, End Scanning enabled)
                    Start.Text = "Cancel";
                    Start.Enabled = true;
                    Reconstruct.Text = "End Scanning";
                    Reconstruct.Enabled = true;
                    Reconstruct.Focus();
                    OriginalReconstructBackColor = Reconstruct.BackColor;
                    Reconstruct.BackColor = RECORD_COLOR;
                    break;
                case ButtonState.Cd_ESd: // Scanning ended (Cancel disabled, End Scanning disabled)
                    Start.Text = "Cancel";
                    Start.Enabled = false;
                    Reconstruct.Text = "End Scanning";
                    Reconstruct.Enabled = false;
                    break;
            }
            Panel_Paint(MainPanel, null);
        }

        public bool download1 = false;
        public bool GetScanRequested()
        {
            return scan_requested;
        }

        public void SetScanRequested( bool enabled )
        {
            scan_requested = enabled;
        }

        /*public string FirstName
        {
            get { return listBox1.Text; }
            set { listBox1.Text = value; }
        }*/

        /*
        public static void upload(MediaFile mediaFile)
        {
            try
            {
                StreamContent scontent = new StreamContent(mediaFile.GetStream());
                scontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = "newimage",
                    Name = "image"
                };
                scontent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.API_ROOT_URL);
                var result = client.PostAsync("api/photo", scontent).Result;
                Debug.WriteLine(result.ReasonPhrase);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }*/

        public MainForm(PXCMSession session)
        {
            InitializeComponent();

            this.session = session;
            PopulateDeviceMenu();

            FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            MainPanel.Paint += new PaintEventHandler(Panel_Paint);
            MainPanel.Resize += new EventHandler(Panel_Resize);
            render.SetHWND(MainPanel);

            buttonPanel.Height = buttonScan.Height;
            buttonPanel.Top = buttonScan.Top;
            bottomPanel.BringToFront();

            groupBox1.Left = (this.ClientSize.Width - MainPanel.Width) / 2;
            groupBox1.Top = (this.ClientSize.Height - MainPanel.Height) / 2;

            buttonScan.Enabled = false;
            buttonStore.Enabled = false;
            buttonViewer.Enabled = false;
            //buttonExport.Enabled = false;
            buttonStorage.Enabled = false;
            //buttonVE.Enabled = false;
            Start.Enabled = false;

            //////////////////////////////////////

            getAvailableComPorts();
            comboBox1.SelectedIndex = 0;
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }

            //Asset Store 
            //Browser myBrowser;
            DotNetBrowser.Helper.irDeveloper.ModifyInMemory.ActivateMemoryPatching();
            myBrowser = BrowserFactory.Create();
            BrowserView browserViewStore = new WinFormsBrowserView(myBrowser);
            Control browserWindow = (Control)browserViewStore;
            browserWindow.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(browserWindow);
            //Controls.Add((Control)browserView);
            myBrowser.LoadURL("https://sketchfab.com/search?features=downloadable&q=obj&sort_by=-pertinence&type=models");

            //Download Handler Asset Store
            //var downloadHandler = new SampleDownloadHandler();
            //myBrowser.DownloadHandler = downloadHandler;


            //Model Viewer
            BrowserView browserViewModel = new WinFormsBrowserView(BrowserFactory.Create());
            Control browserWindow2 = (Control)browserViewModel;
            browserWindow2.Dock = DockStyle.Fill;
            this.panel2.Controls.Add(browserWindow2);
            //Controls.Add((Control)browserView);
            browserViewModel.Browser.LoadURL("http://x1.hcm-lab.id:3000/");
            
            // Configure the default scanning mode.
            ScanningArea.SelectedIndex = ScanningArea.Items.IndexOf("Object");

            // Hide all alerts by default.
            HideAlerts();
        }

        /*class SampleDownloadHandler : DownloadHandler
        {
            public bool AllowDownload(DownloadItem download)
            {
                download.DestinationFile = ;
                download.DownloadEvent += delegate (object sender, DownloadEventArgs e)
                {
                    DownloadItem downloadItem = e.Item;
                    if (downloadItem.Completed)
                    {
                        Console.Out.WriteLine("Download is completed!");
                        System.Windows.MessageBox.Show("Destination file: " + download.DestinationFile, "Download Complete", System.Windows.MessageBoxButton.OK, 
                            System.Windows.MessageBoxImage.Information);
                        //System.Windows.MessageBox.Show("Download complete");
                    }
                };
                //System.Windows.MessageBox.Show("Destination file: " +
                //        download.DestinationFile);
                return true;
            }
        }*/

        class SampleDownloadHandler : DownloadHandler
        {
            private ManualResetEvent waitEvent = new ManualResetEvent(false);

            public event EventHandler DownloadUpdated;

            public bool AllowDownload(DownloadItem download)
            {
                download.DownloadEvent += delegate (object sender, DownloadEventArgs e)
                {
                    //Console.Clear();
                    download.DestinationFile = "";
                    DownloadItem downloadItem = e.Item;
                    Console.Out.WriteLine("Destination file: " +
                       download.DestinationFile);
                    if (downloadItem.Completed)
                    {
                        if (downloadItem.Canceled)
                        {
                            Console.Out.WriteLine("Download is canceled!");
                        }
                        else
                        {
                            Console.Out.WriteLine("Download is completed!");
                        }
                        waitEvent.Set();
                    }
                    else
                    {
                        Console.Out.Write("Complete: " +
                               download.PercentComplete + "%");

                        if (downloadItem.Paused)
                        {
                            Console.Out.Write(" - Download is paused");
                        }
                    }

                    if (DownloadUpdated != null)
                    {
                        DownloadUpdated.Invoke(sender, new EventArgs());
                    }

                };
                return true;
            }

            public void Wait()
            {
                waitEvent.WaitOne();
            }
        }


        private void PopulateDeviceMenu()
        {
            devices.Clear();
            devices_iuid.Clear();

            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;

            DeviceMenu.DropDownItems.Clear();

            for (int i = 0; ; i++)
            {
                PXCMSession.ImplDesc desc1;
                if (session.QueryImpl(desc, i, out desc1) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                PXCMCapture capture;
                if (session.CreateImpl<PXCMCapture>(desc1, out capture) < pxcmStatus.PXCM_STATUS_NO_ERROR) continue;
                for (int j = 0; ; j++)
                {
                    PXCMCapture.DeviceInfo dinfo;
                    if (capture.QueryDeviceInfo(j, out dinfo) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                    if (dinfo.model == PXCMCapture.DeviceModel.DEVICE_MODEL_GENERIC) continue;

                    ToolStripMenuItem sm1 = new ToolStripMenuItem(dinfo.name, null, new EventHandler(Device_Item_Click));
                    devices[sm1] = dinfo;
                    devices_iuid[sm1] = desc1.iuid;
                    DeviceMenu.DropDownItems.Add(sm1);
                }
                capture.Dispose();
            }
            if (DeviceMenu.DropDownItems.Count > 0)
            {
                (DeviceMenu.DropDownItems[0] as ToolStripMenuItem).Checked = true;
                PopulateColorMenus(DeviceMenu.DropDownItems[0] as ToolStripMenuItem);
                PopulateDepthMenus(DeviceMenu.DropDownItems[0] as ToolStripMenuItem);
            }
        }

        private bool PopulateDeviceFromFileMenu()
        {
            devices.Clear();
            devices_iuid.Clear();

            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;

            PXCMSession.ImplDesc desc1;
            PXCMCapture.Device device = null ;
            PXCMCapture.DeviceInfo dinfo;
            PXCMSenseManager pp = PXCMSenseManager.CreateInstance();
            if (pp == null)
            {
                UpdateStatus("Init Failed");
                return false;
            }
            if (pp.captureManager == null)
            {
                pp.Dispose();
                UpdateStatus("Init Failed");
                return false;
            }
            try
            {
                if (session.QueryImpl(desc, 0, out desc1) < pxcmStatus.PXCM_STATUS_NO_ERROR) throw null;
                if (pp.captureManager.SetFileName(filename, false) < pxcmStatus.PXCM_STATUS_NO_ERROR) throw null;
                if (pp.captureManager.LocateStreams() < pxcmStatus.PXCM_STATUS_NO_ERROR) throw null;
                device = pp.captureManager.QueryDevice();
                if (device != null) device.QueryDeviceInfo(out dinfo);
                else
                {
                    pp.Dispose();
                    UpdateStatus("Init Failed");
                    return false;
                }
            }
            catch
            {
                pp.Dispose();
                UpdateStatus("Init Failed");
                return false;
            }
            DeviceMenu.DropDownItems.Clear();
            ToolStripMenuItem sm1 = new ToolStripMenuItem(dinfo.name, null, new EventHandler(Device_Item_Click));
            devices[sm1] = dinfo;
            devices_iuid[sm1] = desc1.iuid;
            DeviceMenu.DropDownItems.Add(sm1);

            sm1 = new ToolStripMenuItem("Playback from the file : ", null);
            sm1.Enabled = false;
            DeviceMenu.DropDownItems.Add(sm1);
            sm1 = new ToolStripMenuItem(filename, null);
            sm1.Enabled = false;
            DeviceMenu.DropDownItems.Add(sm1);
            if (DeviceMenu.DropDownItems.Count > 0)
                (DeviceMenu.DropDownItems[0] as ToolStripMenuItem).Checked = true;

            // populate color depth menu from the file
            profiles.Clear();
            ColorMenu.DropDownItems.Clear();
            DepthMenu.DropDownItems.Clear();

            PXCMCapture.Device.StreamProfileSet profile = new PXCMCapture.Device.StreamProfileSet();
            if (dinfo.streams.HasFlag(PXCMCapture.StreamType.STREAM_TYPE_COLOR))
            {
                for (int p = 0; ; p++)
                {
                    if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_COLOR, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                    PXCMCapture.Device.StreamProfile sprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_COLOR];
                    sm1 = new ToolStripMenuItem(ProfileToString(sprofile), null, new EventHandler(Color_Item_Click));
                    profiles[sm1] = sprofile;
                    ColorMenu.DropDownItems.Add(sm1);
                }
            }

            if (((int)dinfo.streams & (int)PXCMCapture.StreamType.STREAM_TYPE_DEPTH) != 0)
            {
                for (int p = 0; ; p++)
                {
                    if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_DEPTH, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                    PXCMCapture.Device.StreamProfile sprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_DEPTH];
                    if (sprofile.options != (PXCMCapture.Device.StreamOption)0x20000) // do not show Depth Confidence
                    {
                        sm1 = new ToolStripMenuItem(ProfileToString(sprofile), null, new EventHandler(Depth_Item_Click));
                        profiles[sm1] = sprofile;
                        DepthMenu.DropDownItems.Add(sm1);
                    }
                }
            }

            GetCheckedDevice();

            pp.Close();
            pp.Dispose();
            return true;
        }

        private void PopulateColorMenus(ToolStripMenuItem device_item)
        {
            OperatingSystem os_version = Environment.OSVersion;
            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;
            desc.iuid = devices_iuid[device_item];
            desc.cuids[0] = PXCMCapture.CUID;

            profiles.Clear();
            ColorMenu.DropDownItems.Clear();
            PXCMCapture capture;
            PXCMCapture.DeviceInfo dinfo2 = GetCheckedDevice();

            PXCMSenseManager pp = session.CreateSenseManager();
            if (pp == null) return;
            if (pp.Enable3DScan() < pxcmStatus.PXCM_STATUS_NO_ERROR) return;
            PXCM3DScan s = pp.Query3DScan();
            if (s == null) return;
            PXCMVideoModule m = s.QueryInstance<PXCMVideoModule>();
            if (m == null) return;

            int count = 0;
            if (session.CreateImpl<PXCMCapture>(desc, out capture) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                PXCMCapture.Device device = capture.CreateDevice(dinfo2.didx);
                if (device != null)
                {
                    PXCMCapture.Device.StreamProfileSet profile = new PXCMCapture.Device.StreamProfileSet(); ;
                    if (dinfo2.streams.HasFlag(PXCMCapture.StreamType.STREAM_TYPE_COLOR))
                    {
                        for (int p = 0; ; p++)
                        {
                            if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_COLOR, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                            PXCMCapture.Device.StreamProfile sprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_COLOR];

                            // Only populate profiles which are supported by the module
                            bool bFound = false;
                            int i = 0;
                            PXCMVideoModule.DataDesc inputs;
                            PXCMImage.PixelFormat format = PXCMImage.PixelFormat.PIXEL_FORMAT_RGB32;
                            if (dinfo2.orientation != PXCMCapture.DeviceOrientation.DEVICE_ORIENTATION_REAR_FACING)
                            {
                                format = PXCMImage.PixelFormat.PIXEL_FORMAT_RGB24;
                            }
                            while ((m.QueryCaptureProfile(i++, out inputs) >= pxcmStatus.PXCM_STATUS_NO_ERROR))
                            {
                                if ((sprofile.imageInfo.height == inputs.streams.color.sizeMax.height)
                                    && (sprofile.imageInfo.width == inputs.streams.color.sizeMax.width)
                                    && (sprofile.frameRate.max == inputs.streams.color.frameRate.max)
                                    && (sprofile.imageInfo.format == format)
                                    && (0==(sprofile.options & PXCMCapture.Device.StreamOption.STREAM_OPTION_UNRECTIFIED)))
                                {
                                    bFound = true;
                                    if (dinfo2.orientation != PXCMCapture.DeviceOrientation.DEVICE_ORIENTATION_REAR_FACING)
                                    {   // Hide rear facing resolutions when the front facing camera is connected...
                                        if (sprofile.imageInfo.width == 640) bFound = false;
                                    }
                                    // On Windows 7, filter non-functional 30 fps modes
                                    if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1)
                                    {
                                        if (sprofile.frameRate.max == 30) bFound = false;
                                    }
                                }
                            }
                            if (bFound)
                            {
                                ToolStripMenuItem sm1 = new ToolStripMenuItem(ProfileToString(sprofile), null, new EventHandler(Color_Item_Click));
                                profiles[sm1] = sprofile;
                                ColorMenu.DropDownItems.Add(sm1);
                                count++;
                            }
                        }
                    }
                    device.Dispose();
                }
                capture.Dispose();
            }
            m.Dispose();
            pp.Dispose();
            if (count > 0) (ColorMenu.DropDownItems[ColorMenu.DropDownItems.Count - 3] as ToolStripMenuItem).Checked = true;
        }

        private void PopulateDepthMenus(ToolStripMenuItem device_item)
        {
            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;
            desc.iuid = devices_iuid[device_item];
            desc.cuids[0] = PXCMCapture.CUID;

            DepthMenu.DropDownItems.Clear();
            PXCMCapture capture;
            PXCMCapture.DeviceInfo dinfo2 = GetCheckedDevice();

            PXCMSenseManager pp = session.CreateSenseManager();
            if (pp == null) return;
            if (pp.Enable3DScan() < pxcmStatus.PXCM_STATUS_NO_ERROR) return;
            PXCM3DScan s = pp.Query3DScan();
            if (s == null) return;
            PXCMVideoModule m = s.QueryInstance<PXCMVideoModule>();
            if (m == null) return;

            if (session.CreateImpl<PXCMCapture>(desc, out capture) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                PXCMCapture.Device device = capture.CreateDevice(dinfo2.didx);
                if (device != null)
                {
                    PXCMCapture.Device.StreamProfileSet profile = new PXCMCapture.Device.StreamProfileSet(); ;
                    PXCMCapture.Device.StreamProfile color_profile = GetColorConfiguration();
                    if (((int)dinfo2.streams & (int)PXCMCapture.StreamType.STREAM_TYPE_DEPTH) != 0)
                    {
                        for (int p = 0; ; p++)
                        {
                            if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_DEPTH, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                            PXCMCapture.Device.StreamProfile sprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_DEPTH];

                            bool bFound = false;
                            int i = 0;
                            PXCMVideoModule.DataDesc inputs;
                            while ((m.QueryCaptureProfile(i++, out inputs) >= pxcmStatus.PXCM_STATUS_NO_ERROR))
                            {
                                if ((sprofile.imageInfo.height == inputs.streams.depth.sizeMax.height)
                                    && (sprofile.imageInfo.width == inputs.streams.depth.sizeMax.width)
                                    && (sprofile.frameRate.max == inputs.streams.depth.frameRate.max)
                                    && (color_profile.frameRate.max == inputs.streams.depth.frameRate.max))
                                {
                                    bFound = true;
                                }
                            }
                            if (bFound)
                            {
                                if (sprofile.options != (PXCMCapture.Device.StreamOption)0x20000) // do not show Depth Confidence
                                {
                                    ToolStripMenuItem sm1 = new ToolStripMenuItem(ProfileToString(sprofile), null, new EventHandler(Depth_Item_Click));
                                    profiles[sm1] = sprofile;
                                    DepthMenu.DropDownItems.Add(sm1);
                                }
                            }
                        }
                    }
                    device.Dispose();
                }
                capture.Dispose();
            }
            m.Dispose();
            pp.Dispose();

            if (DepthMenu.DropDownItems.Count > 0) (DepthMenu.DropDownItems[DepthMenu.DropDownItems.Count - 1] as ToolStripMenuItem).Checked = true;
        }

        private string ProfileToString(PXCMCapture.Device.StreamProfile pinfo)
        {
            string line = pinfo.imageInfo.format.ToString().Substring(13) + " " + pinfo.imageInfo.width + "x" + pinfo.imageInfo.height + " ";
            if (pinfo.frameRate.min != pinfo.frameRate.max)
            {
                line += (float)pinfo.frameRate.min + "-" +
                      (float)pinfo.frameRate.max;
            }
            else
            {
                float fps = (pinfo.frameRate.min != 0) ? pinfo.frameRate.min : pinfo.frameRate.max;
                line += fps;
            }
            if (pinfo.options.HasFlag(PXCMCapture.Device.StreamOption.STREAM_OPTION_UNRECTIFIED))
                line += " RAW";
            return line;
        }

        private void Device_Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in DeviceMenu.DropDownItems)
            {
                e1.Checked = (sender == e1);
            }
            PopulateColorMenus(sender as ToolStripMenuItem);
            PopulateDepthMenus(sender as ToolStripMenuItem);

            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            PXCMCapture.DeviceInfo dev_info = devices[(sender as ToolStripMenuItem)];
        }

        private void Color_Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in ColorMenu.DropDownItems)
                e1.Checked = (sender == e1);
            // Repopulate the depth menu in case we switched from 30 to 60 fps (or vise versa).
            foreach (ToolStripMenuItem e2 in DeviceMenu.DropDownItems)
                if (e2.Checked) PopulateDepthMenus(e2 as ToolStripMenuItem);
        }

        private void Depth_Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in DepthMenu.DropDownItems)
                e1.Checked = (sender == e1);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            SetButtonState(ButtonState.Cd_SSd);
            if (startStop)
            {
                // Wait for previous thread to exit.
                while (stop == true) System.Threading.Thread.Sleep(5);
                System.Threading.Thread thread = new System.Threading.Thread(DoRendering);
                thread.Start();
            }
            else
            {
                stop = true;
            }

            HideTrackingAlert();
            HideRangeAlerts();

            startStop ^= true;
        }

        private void EnableLandmarks(bool enable)
        {
            Landmarks.Enabled = enable; 
        }

        private void EnableControls(bool enable)
        {
            MainMenu.Enabled = enable;
            ScanningArea.Enabled = enable;
            EnableLandmarks(enable);
            Textured.Enabled = enable;
            Solid.Enabled = enable;
            flopPreviewImage.Enabled = enable;
            /*
            MaxTrianglesEnabled.Enabled = enable;
            MaxVerticesEnabled.Enabled = enable;
            MaxTriangles.Enabled = enable;
            MaxVertices.Enabled = enable;
            useMarker.Enabled = enable;
            */
        }

        delegate String DoRenderingBegin();
        delegate void DoRenderingEnd();
        private void DoRendering()
        {
            RenderStreams rs = new RenderStreams(this);

            try
            {
                rs.StreamColorDepth((String)Invoke(new DoRenderingBegin(
                    delegate
                    {
                        EnableControls(false);
                        return ScanningArea.SelectedItem.ToString();
                    }
                )));

                this.Invoke(new DoRenderingEnd(
                    delegate
                    {
                        if (closing) Close();
                        EnableControls(true);
                        SetButtonState(ButtonState.SCe_SSd);
                        startStop = true;
                        scanReconstruct = true;
                    }
                ));
            }
            catch (Exception) { }
        }

        public PXCMCapture.DeviceInfo GetCheckedDevice()
        {
            foreach (ToolStripMenuItem e in DeviceMenu.DropDownItems)
            {
                if (devices.ContainsKey(e))
                {
                    if (e.Checked)
                    {
                        PXCMCapture.DeviceInfo dev_info = devices[e];
                        userFacingCamera = (dev_info.orientation != PXCMCapture.DeviceOrientation.DEVICE_ORIENTATION_REAR_FACING);
                        if (!userFacingCamera && Landmarks.Checked) Landmarks.Checked = false;
                        // Landmarks are only supported when using front facing cameras in this release
                        try { EnableLandmarks(userFacingCamera && Solid.Enabled); }
                        catch (Exception) { }
                        return devices[e];
                    }
                }
            }
            return new PXCMCapture.DeviceInfo();
        }

        private PXCMCapture.Device.StreamProfile GetConfiguration(ToolStripMenuItem m)
        {
            foreach (ToolStripMenuItem e in m.DropDownItems)
                if (e.Checked) return profiles[e];
            return new PXCMCapture.Device.StreamProfile();
        }

        public PXCMCapture.Device.StreamProfile GetColorConfiguration()
        {
            return GetConfiguration(ColorMenu);
        }

        public PXCMCapture.Device.StreamProfile GetDepthConfiguration()
        {
            return GetConfiguration(DepthMenu);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop = true;
            e.Cancel = false;
            closing = true;
        }

        private delegate void UpdateStatusDelegate(string status);
        public void UpdateStatus(string status)
        {
            try
            {
                Status2.Invoke(new UpdateStatusDelegate(delegate(string s) { StatusLabel.Text = s; }), new object[] { status });
            }
            catch (Exception) { }
        }

        public void SetBitmap(PXCMImage image)
        {
            if (image == null) return;
            lock (this)
            {
                render.UpdatePanel(image);
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            lock (this)
            {
                render.UpdatePanel();
            }
        }

        private void Panel_Resize(object sender, EventArgs e)
        {
            lock (this)
            {
                render.ResizePanel();
            }
        }

        public string GetFileName()
        {
            return filename;
        }

        public string GetMeshFileName()
        {
            return mesh_filename;
        }

        public void ReleaseMeshFileName()
        {
            mesh_filename = null;
        }
        public bool IsModeLive()
        {
            return ModeLive.Checked;
        }

        public bool IsModeRecord()
        {
            return ModeRecord.Checked;
        }

        public void WarnOfHtmlCompatibility()
        {
            MessageBox.Show("In this release, HTML generation is disabled when Texture is used. To inspect a textured mesh, import it into a mesh viewer (e.g. meshlab.sourceforge.net).", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public bool isSolidificationSelected()
        {
            return Solid.Checked;
        }

        public bool isTextureSelected()
        {
            return Textured.Checked;
        }

        public bool isLandmarksSelected()
        {
            return Landmarks.Checked;
        }

        
        /*public bool isUseMarkerChecked()
        {
            return useMarker.Checked;
        }*/
        

        internal bool isFlopPreviewImageSelected()
        {
            return flopPreviewImage.Checked;
        }

        private void ModeLive_Click(object sender, EventArgs e)
        {
            ModeLive.Checked = true;
            ModePlayback.Checked = ModeRecord.Checked = false;
            PopulateDeviceMenu();
        }

        private void Playback()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = @"RSSDK clip|*.rssdk|All files|*.*";
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            filename = (ofd.ShowDialog() == DialogResult.OK) ? ofd.FileName : null;
            ofd.Dispose();
            if (filename == null)
            {
                ModeLive.Checked = true;
                ModePlayback.Checked = ModeRecord.Checked = false;
                PopulateDeviceMenu();
            }
            else
            {
                ModePlayback.Checked = true;
                ModeLive.Checked = ModeRecord.Checked = false;
                if (PopulateDeviceFromFileMenu() == false)
                {
                    ModeLive.Checked = true;
                    ModePlayback.Checked = ModeRecord.Checked = false;
                }
            }
        }

        private void ModePlayback_Click(object sender, EventArgs e)
        {
            Playback();
        }

        private void ModeRecord_Click(object sender, EventArgs e)
        {
            ModeRecord.Checked = true;
            ModeLive.Checked = ModePlayback.Checked = false;
            PopulateDeviceMenu();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = @"RSSDK clip|*.rssdk|All files|*.*";
            sfd.CheckPathExists = true;
            sfd.OverwritePrompt = true;
            try
            {
                filename = (sfd.ShowDialog() == DialogResult.OK) ? sfd.FileName : null;
            }
            catch
            {
                sfd.Dispose();
            }
            sfd.Dispose();
        }

        public bool GetStopState()
        {
            return stop;
        }

        public void StartScanning( bool enabled )
        {
            scanReconstruct = enabled;
        }

        private void Reconstruct_Click(object sender, System.EventArgs e)
        {
            if (scanReconstruct)
            {
                SetButtonState(ButtonState.Ce_ESd);

                //myport.DtrEnable = true;
                if (myport.IsOpen)
                {
                    myport.WriteLine("O");
                }
                //txtMsg.Text = "";
                //myport.Close();
                
                //myport.Close();
            }
            else EndScan();
            HideTrackingAlert();
            HideRangeAlerts();
            StartScanning(!scanReconstruct);
        }

        internal void EndScan()
        {
            SetButtonState(ButtonState.Cd_ESd);
            reconstruct_requested = true;
            stop = true;
            /*
            SaveFileDialog mesh_dialog = new SaveFileDialog();
            //mesh_dialog.FileName = "3DScan"; // asli
            mesh_dialog.FileName = projectname;
            //mesh_dialog.Filter = @"OBJ file|*.obj|PLY file|*.ply|STL file|*.stl"; // asli
            mesh_dialog.Filter = @"OBJ file|*.obj";
            mesh_dialog.CheckPathExists = true;
            mesh_dialog.OverwritePrompt = true;*/
            try
            {
                //mesh_filename = (mesh_dialog.ShowDialog() == DialogResult.OK) ? mesh_dialog.FileName : new String('c', 1);
                ////Coba Koding
                mesh_filename = projectname + @".obj";
                //string nameFolder = projectname;
                //var outDirName = Path.Combine(Path.GetDirectoryName(mesh_filename), nameFolder);
                //Directory.CreateDirectory(projectname);
                //string[] files = Directory.GetFiles("C:\\Users\\mobiu\\OneDrive\\Documents\\RSSDK\\Samples\\DF_3DScanAsli.cs\\bin\\x64\\Debug", "*.obj");
                
            }
            catch
            {
                //mesh_dialog.Dispose();
            }
            //mesh_dialog.Dispose();
        }

        public bool landmarksChecked()
        {
            return Landmarks.Checked;
        }

        internal void HideLabel(Label l)
        {
            l.Visible = false;
        }

        internal void HideAlerts()
        {
            HideRangeAlerts();
            HideLabel(OutOfRange);
            HideLabel(FaceNotDetected);
            //HideLabel(MoveRight);
            //HideLabel(MoveLeft);
            //HideLabel(MoveBack);
            //HideLabel(MoveForward);
            //HideLabel(TurnLeft);
            //HideLabel(TurnRight);
            //HideLabel(MoveDown);
            //HideLabel(MoveUp);
            //HideLabel(TiltDown);
            //HideLabel(TiltUp);
            //HideLabel(MarkerDetected);
            //HideLabel(MarkerNotDetected);
        }

        internal void ShowLabel(Label l)
        {
            l.Visible = true;
        }

        internal void HideALERT_INSUFFICIENT_STRUCTURE()
        {
            OutOfRange.Visible = false;
        }

        internal void ShowTooClose()
        {
            TooClose.Visible = true;
        }

        internal void ShowTooFar()
        {
            TooFar.Visible = true;
        }

        /*
        internal void ShowMarkerDetected()
        {
            MarkerDetected.Visible = true;
            MarkerNotDetected.Visible = false;
        }

        internal void ShowMarkerUnDetected()
        {
            MarkerDetected.Visible = false;
            MarkerNotDetected.Visible = true;
        }
        */

        internal void HideRangeAlerts()
        {
            TooClose.Visible = false;
            TooFar.Visible = false;
        }

        internal void ShowTrackingAlert()
        {
            TrackingLost.Visible = true;
        }

        internal void HideTrackingAlert()
        {
            TrackingLost.Visible = false;
        }

        internal void ResetStop()
        {
            stop = false;
            HideAlerts();
        }

        private void HelpClicked(object sender, System.EventArgs e)
        {
            try
            {
                bool found = false;
                string RSSDK_DIR = Environment.GetEnvironmentVariable("RSSDK_DIR");
                if (RSSDK_DIR != null)
                {
                    string helpFile = RSSDK_DIR + @"doc\CHM\sdkmanual-scan.chm";
                    if (System.IO.File.Exists(helpFile))
                    {
                        found = true;
                        System.Windows.Forms.Help.ShowHelp(
                            // The following string is found in the context menu of the chm page 
                            // (rt-click on the chm page, then select properties).
                            // The previous string should match the middle portion (chm file name).
                            this, @"file://" + helpFile, @"\Intel\RSSDK\doc\CHM\SD81FF~1.CHM::/doc_scan_3d_scanning.html");
                    }
                }

                if ( found == false )
                {
                    MessageBox.Show("Error: Help file not installed.");
                }
            }
            catch {};
        }

        public void EnableReconstruction(bool enabled)
        {
            if (Reconstruct.Enabled != enabled)
            {
                Reconstruct.Enabled = enabled;
                if (enabled) Reconstruct.Focus();
                else Start.Focus();
            }
        }

        /*
        public bool getMaxVerticesEnabledChecked()
        {
            return MaxVerticesEnabled.Checked == true;
        }

        public int getMaxVertices()
        {
            return (int)this.MaxVertices.Value;
        }

        public bool getMaxTrianglesEnabledChecked()
        {
            return MaxTrianglesEnabled.Checked == true;
        }

        public int getMaxTriangles()
        {
            return (int)this.MaxTriangles.Value;
        }
        */

        private void Landmarks_CheckedChanged(object sender, EventArgs e)
        {
            if (Landmarks.Checked == true)
            {
                if (!userFacingCamera)
                {
                    MessageBox.Show("The attached camera does not support scanning with landmarks.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    Landmarks.Checked = false;
                }
            }
        }

        private void ScanningArea_SelectedIndexChanged(object sender,
            System.EventArgs e)
        {
            if (userFacingCamera)
            {
                ComboBox comboBox = (ComboBox)sender;
/*                if (comboBox.SelectedIndex == ScanningArea.Items.IndexOf("Head"))
                {
                    MessageBox.Show("The attached camera does not have enough range for Head scanning.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else 
 */              if (comboBox.SelectedIndex == ScanningArea.Items.IndexOf("Body"))
                {
                    MessageBox.Show("The attached camera does not have enough range for Body scanning.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            buttonPanel.Height = buttonScan.Height;
            buttonPanel.Top = buttonScan.Top;
            bottomPanel.BringToFront();
        }

        private void buttonStore_Click(object sender, EventArgs e)
        {
            buttonPanel.Height = buttonStore.Height;
            buttonPanel.Top = buttonStore.Top;
            panelStore.BringToFront();
        }

        private void buttonViewer_Click(object sender, EventArgs e)
        {
            buttonPanel.Height = buttonViewer.Height;
            buttonPanel.Top = buttonViewer.Top;
            panelViewer.BringToFront();
        }

        private void buttonStorage_Click(object sender, EventArgs e)
        {
            buttonPanel.Height = buttonStorage.Height;
            buttonPanel.Top = buttonStorage.Top;
            panelExport.BringToFront();
        }

        /*private void buttonExport_Click(object sender, EventArgs e)
        {
            buttonPanel.Height = buttonExport.Height;
            buttonPanel.Top = buttonExport.Top;
            panelExport.BringToFront();
        }*/


        private void buttonRar_Click(object sender, EventArgs e)
        {
            /*
            FolderBrowserDialog Fbd = new FolderBrowserDialog();
            string path = string.Empty;
            if(Fbd.ShowDialog() == DialogResult.OK)
            {
                path = Fbd.SelectedPath;
            }
            DirectoryInfo Dinfo = new DirectoryInfo(path);
            foreach (FileInfo fInfo in Dinfo.GetFiles())
            {
                ar.Extract(fInfo);
                File.Delete(fInfo.FullName);
            }
            *

            SaveFileDialog saveFileZip = new SaveFileDialog();
            saveFileZip.Title = "Save Zip File";
            if (saveFileZip.ShowDialog() == DialogResult.OK)
            {
                ZipFile zipUtility = ZipFile.Read(mySelectedFiles);
                foreach (String file in mySelectedFiles)
                {
                    zipUtility.ExtractAll(file);
                }
                zipUtility.Save(saveFileZip.FileName);
            }

            foreach (var file in Directory.EnumerateFiles("<directory path>", "*.zip"))
            {
                using (ZipFile zip = ZipFile.Read(file))
                {
                    foreach (ZipEntry zipFiles in zip)
                    {
                        zipFiles.Extract(@"D:\DownloadFile\Extract\", true);
                    }
                }
            }
            */
                using (ZipFile zipUtility = ZipFile.Read(mySelectedFiles))
                {
                    foreach (ZipEntry file in zipUtility)
                    {
                        file.Extract(@"D:\DownloadFile\");
                    }
                }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectFileZip = new OpenFileDialog();

            selectFileZip.Title = "Select Zip File";
            selectFileZip.Multiselect = true;
            if (selectFileZip.ShowDialog() == DialogResult.OK)
            {
                //listBox1.Items.Clear();

                mySelectedFiles = selectFileZip.FileName;
                foreach (String file in selectFileZip.SafeFileNames)
                {
                    //listBox1.Items.Add(file);
                }
            }
        }

        private void capture()
        {
            // Create a PXCMSenseManager instance

            PXCMSenseManager sm = PXCMSenseManager.CreateInstance();
            // Select the color stream
            sm.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 640, 480);
            // Initialize and Stream samples
            sm.Init();

                // retrieve the sample
                PXCMCapture.Sample sample = sm.QuerySample();
                // go fetching the next sample
                //sm.ReleaseFrame();
            // Close down
            sm.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectModel = new OpenFileDialog();

            selectModel.Title = "Select Model";
            selectModel.Multiselect = true;
            if (selectModel.ShowDialog() == DialogResult.OK)
            {
                // Read the files
                foreach (String file in selectModel.SafeFileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        if ((myStream = selectModel.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                listBox2.Items.Add(file);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.listBox2.Items.Clear();
            socketIoManager();
            MessageBox.Show("Export Sucessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void socketIoManager()
        {
            /*var socket = IO.Socket("http://127.0.0.1:8888");
            socket.On(Socket.EVENT_CONNECT, () =>
            {
                UpdateStatus("Connected");
            });

            socket.On("txt", (data) => {
                String msg = data.Json.Args[0].ToString();
                if (txtMsg.InvokeRequired)
                    txtMsg.Invoke(new Action(() => txtMsg.Text += msg + Environment.NewLine));
                Console.WriteLine(msg);
            });*/
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                client = new MqttClient("x1.hcm-lab.id", 18883, false, MqttSslProtocols.None, null, null);
                // use a unique id as client id, each time we start the application

                clientId = Guid.NewGuid().ToString();
                client.Connect(clientId);

                sub1 = "vecom/scan/init";
                sub2 = "vecom/scan/start";
                sub3 = "vecom/save";
                sub4 = "vecom/upload";
                sub5 = "vecom/view";
                sub6 = "vecom/project";

                listBoxMsg.Items.Add("* Client connected");
                client.Subscribe(new string[] { sub1 }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                client.Subscribe(new string[] { sub2 }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                client.Subscribe(new string[] { sub3 }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                client.Subscribe(new string[] { sub4 }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                client.Subscribe(new string[] { sub5 }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                client.Subscribe(new string[] { sub6 }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

                listBoxMsg.Items.Add("** Subscribing to: " + sub1);
                listBoxMsg.Items.Add("** Subscribing to: " + sub2);
                listBoxMsg.Items.Add("** Subscribing to: " + sub3);
                listBoxMsg.Items.Add("** Subscribing to: " + sub4);
                listBoxMsg.Items.Add("** Subscribing to: " + sub5);
                listBoxMsg.Items.Add("** Subscribing to: " + sub6);

                client.MqttMsgPublishReceived += new MqttClient.MqttMsgPublishEventHandler(EventPublished);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public void Read()
        {
            while (myport.IsOpen)
            {
                try
                {
                    if (myport.BytesToRead > 0)
                    {
                        string message = myport.ReadLine();
                        this.SetText(message);
                        txtStatus.Text = message;
                    }
                }
                catch (TimeoutException) { }
            }
        }

        private void EventPublished(Object sender, MqttMsgPublishEventArgs e)
        {
            try
            {
                SetText("*** Received Message");
                SetText("*** Topic: " + e.Topic);
                SetText("*** Message: " + Encoding.UTF8.GetString(e.Message));
                SetText("");
                txtProject.Invoke((MethodInvoker)(() => txtProject.Text = Encoding.UTF8.GetString(e.Message)));
                txtProject.Invoke((MethodInvoker)(() => webmsg = txtProject.Text));
                topicmsg = e.Topic;
                //txtStatus.Invoke((MethodInvoker)(() => txtStatus.Text = e.Topic));
                //txtStatus.Invoke((MethodInvoker)(() => topicmsg = txtStatus.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            SetAction();
        }

        private void SetAction()
        {
            //txtProject.Invoke((MethodInvoker)(() => txtProject.Text = webmsg));
            //btnCreate.PerformClick();

            switch (topicmsg)
            {
                case "vecom/project":
                    //MessageBox.Show("PROJECT");
                    btnCreate.Invoke((MethodInvoker)(() => btnCreate.PerformClick()));  
                    break;
                case "vecom/scan/init":
                    //MessageBox.Show("INIT CAMERA");
                    Start.Invoke((MethodInvoker)(() => Start.PerformClick()));
                    break;
                case "vecom/scan/start":
                    
                    //MessageBox.Show("START CAMERA");
                    Reconstruct.Invoke((MethodInvoker)(() => Reconstruct.PerformClick()));
                    Rectangle rect = new Rectangle(475,190,1095,770);
                    using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
                    {
                        Graphics g = Graphics.FromImage(bmp);
                        g.CopyFromScreen(rect.Left,rect.Top,0,0,bmp.Size);
                        //bottomPanel.Invoke((MethodInvoker)(() => bottomPanel.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height))));
                        bmp.Save(projectname + "Viewer" + ".jpg", ImageFormat.Jpeg);
                    }
                    counter = 0;
                   
                    
                    break;
                case "vecom/save":   
                    pub1 = "vecom/save/obj";
                    counter++;

                    capture();
                    Reconstruct.Invoke((MethodInvoker)(() => Reconstruct.PerformClick()));
                    if(counter == 1)
                    {
                        MessageBox.Show("SAVING FILE . . . PLEASE WAIT STATUS UNTIL FILE SAVED");
                    }
                    if (counter == 2)
                    {
                        MessageBox.Show("PROCESSING FILE TO WEB . . .");
                        appmsg1 = "/" + projectname + ".obj" + "/" + projectname + ".mtl" + "/" + projectname + "1.png";
                       
                        client.Publish(pub1, Encoding.UTF8.GetBytes(appmsg1), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                        listBoxMsg.Invoke((MethodInvoker)(() => listBoxMsg.Items.Add("*** Publishing on: " + pub1)));
                    }
                    
                    var filteredFiles = Directory
                    .GetFiles("E:\\ProjectTA\\DF_3DScanAsli.cs\\bin\\x64\\Debug", "*.*")
                    .Where(file => file.ToLower().EndsWith("obj") || file.ToLower().EndsWith("mtl") || file.ToLower().EndsWith("png") || file.ToLower().EndsWith("jpg"))
                    .ToList();
                    //listBoxMsg.Items.Add(filteredFiles);
                    //listBoxMsg.Items.Add("D:\\ProjectTA\\DF_3DScanAsli.cs\\bin\\x64\\Debug" + "\\" + projectname);
                    foreach (string s in filteredFiles)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        string filename = System.IO.Path.GetFileName(s);
                        //listBoxMsg.Items.Add("");
                        //listBoxMsg.Items.Add(filename);
                        string sorcefile = System.IO.Path.Combine("E:\\ProjectTA\\DF_3DScanAsli.cs\\bin\\x64\\Debug", filename);
                        string destfile = System.IO.Path.Combine("E:\\ProjectTA\\FileAsset3D\\ScanResult", filename);
                        System.IO.File.Move(sorcefile,destfile);
                    }
                    break;
                case "vecom/upload":
                    MessageBox.Show("UPLOAD");
                    /*var filteredFiles2 = Directory
                    .GetFiles("D:\\ProjectTA\\DF_3DScanAsli.cs\\bin\\x64\\Debug", "*.*")
                    .Where(file => file.ToLower().EndsWith("obj") || file.ToLower().EndsWith("mtl") || file.ToLower().EndsWith("png"))
                    .ToList();
                    listBoxMsg.Items.Add(filteredFiles2);
                    //listBoxMsg.Items.Add("D:\\ProjectTA\\DF_3DScanAsli.cs\\bin\\x64\\Debug" + "\\" + projectname);
                    foreach (string s in filteredFiles2)
                    {
                        // Use static Path methods to extract only the file name from the path.
                        string filename2 = System.IO.Path.GetFileName(s);
                        listBoxMsg.Items.Add("");
                        listBoxMsg.Items.Add(filename2);
                        System.IO.File.Delete(filename2);
                    }*/
                    break;
                case "vecom/view":
                    MessageBox.Show("VIEW");

                    break;
            }
        }

        private void SetText(string text)
        {
            if (listBoxMsg.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { text });
            }
            else
            {
                listBoxMsg.Items.Add(text);
            }
        }
        
        private void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            projectname = txtProject.Text;
            lblProjectName.Text = projectname;
            groupBox1.Visible = false;

            buttonScan.Enabled = true;
            buttonStore.Enabled = true;
            buttonViewer.Enabled = true;
            //buttonExport.Enabled = true;
            buttonStorage.Enabled = true;
            //buttonVE.Enabled = true;
            Start.Enabled = true;

            string selectedPort = comboBox1.GetItemText(comboBox1.SelectedItem);
            myport = new SerialPort(selectedPort, 9600);
            //myport = new SerialPort();
            //myport.BaudRate = 9600;
            //myport.PortName = "COM8";
            myport.Open();

            timer1.Start();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonVE_Click(object sender, EventArgs e)
        {
            try
            {
                client.Disconnect();
                listBoxMsg.Items.Add("* Client disconnected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (myport.IsOpen)
            {
                try
                {
                    if (myport.BytesToRead > 0)
                    {
                        String message = myport.ReadLine().ToString();

                        
                        txtStatus.Text = message;
                        listBoxMsg.Items.Add(message);
                        
                        /*txtMsg.Text = txtStatus.Text;
                        pub2 = "vecom/scan/progres";
                        appmsg2 = txtStatus.Text;

                        try
                        {
                            client.Publish(pub2, Encoding.UTF8.GetBytes(appmsg2), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
                            listBoxMsg.Items.Add("*** Publishing on: " + pub2);
                            listBoxMsg.Items.Add(DateTime.Now.ToLongTimeString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }*/
                    }
                }
                catch (TimeoutException) { }
            }
        }
    }
}
