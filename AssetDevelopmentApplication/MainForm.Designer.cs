namespace sample3dscan.cs
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.sidePanel = new System.Windows.Forms.Panel();
            this.lblProjectName = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.buttonStorage = new System.Windows.Forms.Button();
            this.buttonViewer = new System.Windows.Forms.Button();
            this.buttonStore = new System.Windows.Forms.Button();
            this.buttonScan = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.panelStore = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelViewer = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.Landmarks = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtProject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Status2 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.flopPreviewImage = new System.Windows.Forms.CheckBox();
            this.OptionsLabel = new System.Windows.Forms.Label();
            this.ModeLabel = new System.Windows.Forms.Label();
            this.OutOfRange = new System.Windows.Forms.Label();
            this.TrackingLost = new System.Windows.Forms.Label();
            this.TooFar = new System.Windows.Forms.Label();
            this.TooClose = new System.Windows.Forms.Label();
            this.Solid = new System.Windows.Forms.CheckBox();
            this.ScanningArea = new System.Windows.Forms.ComboBox();
            this.Textured = new System.Windows.Forms.CheckBox();
            this.Reconstruct = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.PictureBox();
            this.Start = new System.Windows.Forms.Button();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.DeviceMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ColorNone = new System.Windows.Forms.ToolStripMenuItem();
            this.DepthMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DepthNone = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeLive = new System.Windows.Forms.ToolStripMenuItem();
            this.ModePlayback = new System.Windows.Forms.ToolStripMenuItem();
            this.ModeRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.panelExport = new System.Windows.Forms.Panel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.FaceNotDetected = new System.Windows.Forms.Label();
            this.listBoxMsg = new System.Windows.Forms.ListBox();
            this.sidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelStore.SuspendLayout();
            this.panelViewer.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Status2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPanel)).BeginInit();
            this.MainMenu.SuspendLayout();
            this.panelExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // sidePanel
            // 
            this.sidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(39)))), ((int)(((byte)(40)))));
            this.sidePanel.Controls.Add(this.lblProjectName);
            this.sidePanel.Controls.Add(this.label6);
            this.sidePanel.Controls.Add(this.pictureBox1);
            this.sidePanel.Controls.Add(this.buttonPanel);
            this.sidePanel.Controls.Add(this.buttonStorage);
            this.sidePanel.Controls.Add(this.buttonViewer);
            this.sidePanel.Controls.Add(this.buttonStore);
            this.sidePanel.Controls.Add(this.buttonScan);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 0);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(212, 655);
            this.sidePanel.TabIndex = 1;
            // 
            // lblProjectName
            // 
            this.lblProjectName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblProjectName.BackColor = System.Drawing.Color.Transparent;
            this.lblProjectName.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjectName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblProjectName.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblProjectName.Location = new System.Drawing.Point(44, 161);
            this.lblProjectName.Name = "lblProjectName";
            this.lblProjectName.Size = new System.Drawing.Size(123, 22);
            this.lblProjectName.TabIndex = 14;
            this.lblProjectName.Text = "Hello World";
            this.lblProjectName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(42, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 22);
            this.label6.TabIndex = 4;
            this.label6.Text = "Project Name";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(179, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // buttonPanel
            // 
            this.buttonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.buttonPanel.Location = new System.Drawing.Point(1, 216);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(10, 54);
            this.buttonPanel.TabIndex = 5;
            // 
            // buttonStorage
            // 
            this.buttonStorage.FlatAppearance.BorderSize = 0;
            this.buttonStorage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStorage.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStorage.ForeColor = System.Drawing.Color.White;
            this.buttonStorage.Image = ((System.Drawing.Image)(resources.GetObject("buttonStorage.Image")));
            this.buttonStorage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStorage.Location = new System.Drawing.Point(12, 382);
            this.buttonStorage.Name = "buttonStorage";
            this.buttonStorage.Size = new System.Drawing.Size(197, 54);
            this.buttonStorage.TabIndex = 8;
            this.buttonStorage.Text = "           Information";
            this.buttonStorage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStorage.UseVisualStyleBackColor = true;
            this.buttonStorage.Click += new System.EventHandler(this.buttonStorage_Click);
            // 
            // buttonViewer
            // 
            this.buttonViewer.FlatAppearance.BorderSize = 0;
            this.buttonViewer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonViewer.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonViewer.ForeColor = System.Drawing.Color.White;
            this.buttonViewer.Image = ((System.Drawing.Image)(resources.GetObject("buttonViewer.Image")));
            this.buttonViewer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonViewer.Location = new System.Drawing.Point(12, 322);
            this.buttonViewer.Name = "buttonViewer";
            this.buttonViewer.Size = new System.Drawing.Size(197, 69);
            this.buttonViewer.TabIndex = 10;
            this.buttonViewer.Text = "      3D Library Web";
            this.buttonViewer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonViewer.UseVisualStyleBackColor = true;
            this.buttonViewer.Click += new System.EventHandler(this.buttonViewer_Click);
            // 
            // buttonStore
            // 
            this.buttonStore.FlatAppearance.BorderSize = 0;
            this.buttonStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStore.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStore.ForeColor = System.Drawing.Color.White;
            this.buttonStore.Image = ((System.Drawing.Image)(resources.GetObject("buttonStore.Image")));
            this.buttonStore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStore.Location = new System.Drawing.Point(12, 268);
            this.buttonStore.Name = "buttonStore";
            this.buttonStore.Size = new System.Drawing.Size(197, 54);
            this.buttonStore.TabIndex = 11;
            this.buttonStore.Text = "          Asset Store";
            this.buttonStore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonStore.UseVisualStyleBackColor = true;
            this.buttonStore.Click += new System.EventHandler(this.buttonStore_Click);
            // 
            // buttonScan
            // 
            this.buttonScan.FlatAppearance.BorderSize = 0;
            this.buttonScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonScan.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonScan.ForeColor = System.Drawing.Color.White;
            this.buttonScan.Image = ((System.Drawing.Image)(resources.GetObject("buttonScan.Image")));
            this.buttonScan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonScan.Location = new System.Drawing.Point(12, 214);
            this.buttonScan.Name = "buttonScan";
            this.buttonScan.Size = new System.Drawing.Size(197, 54);
            this.buttonScan.TabIndex = 12;
            this.buttonScan.Text = "      3D\r\n      Reconstruction";
            this.buttonScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonScan.UseVisualStyleBackColor = true;
            this.buttonScan.Click += new System.EventHandler(this.buttonScan_Click);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(212, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(970, 22);
            this.topPanel.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Century Schoolbook", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(212, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(970, 41);
            this.label4.TabIndex = 5;
            this.label4.Text = "ASSETS DEVELOPMENT APPLICATION";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.FlatAppearance.BorderSize = 0;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.ForeColor = System.Drawing.Color.White;
            this.button13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button13.Location = new System.Drawing.Point(1140, 28);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(32, 35);
            this.button13.TabIndex = 6;
            this.button13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button13.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.ForeColor = System.Drawing.Color.White;
            this.button12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button12.Location = new System.Drawing.Point(1091, 28);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(32, 35);
            this.button12.TabIndex = 7;
            this.button12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.FlatAppearance.BorderSize = 0;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button11.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.ForeColor = System.Drawing.Color.White;
            this.button11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button11.Location = new System.Drawing.Point(1053, 28);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(32, 35);
            this.button11.TabIndex = 8;
            this.button11.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button11.UseVisualStyleBackColor = true;
            // 
            // panelStore
            // 
            this.panelStore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStore.Controls.Add(this.label2);
            this.panelStore.Controls.Add(this.panel1);
            this.panelStore.Location = new System.Drawing.Point(212, 82);
            this.panelStore.Name = "panelStore";
            this.panelStore.Size = new System.Drawing.Size(970, 561);
            this.panelStore.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(970, 23);
            this.label2.TabIndex = 17;
            this.label2.Text = "Sketchfab Asset Store";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(3, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 520);
            this.panel1.TabIndex = 16;
            // 
            // panelViewer
            // 
            this.panelViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelViewer.Controls.Add(this.label3);
            this.panelViewer.Controls.Add(this.panel2);
            this.panelViewer.Location = new System.Drawing.Point(212, 79);
            this.panelViewer.Name = "panelViewer";
            this.panelViewer.Size = new System.Drawing.Size(970, 572);
            this.panelViewer.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(970, 26);
            this.label3.TabIndex = 21;
            this.label3.Text = "3D Library Web";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Location = new System.Drawing.Point(3, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(964, 533);
            this.panel2.TabIndex = 16;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomPanel.Controls.Add(this.Landmarks);
            this.bottomPanel.Controls.Add(this.comboBox1);
            this.bottomPanel.Controls.Add(this.groupBox1);
            this.bottomPanel.Controls.Add(this.Status2);
            this.bottomPanel.Controls.Add(this.flopPreviewImage);
            this.bottomPanel.Controls.Add(this.OptionsLabel);
            this.bottomPanel.Controls.Add(this.ModeLabel);
            this.bottomPanel.Controls.Add(this.FaceNotDetected);
            this.bottomPanel.Controls.Add(this.OutOfRange);
            this.bottomPanel.Controls.Add(this.TrackingLost);
            this.bottomPanel.Controls.Add(this.TooFar);
            this.bottomPanel.Controls.Add(this.TooClose);
            this.bottomPanel.Controls.Add(this.Solid);
            this.bottomPanel.Controls.Add(this.ScanningArea);
            this.bottomPanel.Controls.Add(this.Textured);
            this.bottomPanel.Controls.Add(this.Reconstruct);
            this.bottomPanel.Controls.Add(this.MainPanel);
            this.bottomPanel.Controls.Add(this.Start);
            this.bottomPanel.Controls.Add(this.MainMenu);
            this.bottomPanel.Controls.Add(this.txtMsg);
            this.bottomPanel.Controls.Add(this.txtStatus);
            this.bottomPanel.Controls.Add(this.listBoxMsg);
            this.bottomPanel.Location = new System.Drawing.Point(212, 85);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(970, 570);
            this.bottomPanel.TabIndex = 9;
            // 
            // Landmarks
            // 
            this.Landmarks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Landmarks.AutoSize = true;
            this.Landmarks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.Landmarks.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Landmarks.Enabled = false;
            this.Landmarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Landmarks.ForeColor = System.Drawing.Color.White;
            this.Landmarks.Location = new System.Drawing.Point(830, 121);
            this.Landmarks.Name = "Landmarks";
            this.Landmarks.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Landmarks.Size = new System.Drawing.Size(107, 24);
            this.Landmarks.TabIndex = 167;
            this.Landmarks.Text = "Landmarks";
            this.Landmarks.UseVisualStyleBackColor = false;
            this.Landmarks.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Choose Arduino Port"});
            this.comboBox1.Location = new System.Drawing.Point(338, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(201, 32);
            this.comboBox1.TabIndex = 166;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.txtProject);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(21, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 151);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Project";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(208, 108);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(76, 108);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtProject
            // 
            this.txtProject.Location = new System.Drawing.Point(59, 73);
            this.txtProject.Name = "txtProject";
            this.txtProject.Size = new System.Drawing.Size(238, 21);
            this.txtProject.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(122, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 22);
            this.label5.TabIndex = 0;
            this.label5.Text = "Project Name";
            // 
            // Status2
            // 
            this.Status2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.Status2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.Status2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.Status2.Location = new System.Drawing.Point(0, 570);
            this.Status2.Name = "Status2";
            this.Status2.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.Status2.Size = new System.Drawing.Size(970, 0);
            this.Status2.TabIndex = 164;
            this.Status2.Text = "OK";
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.Gray;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(140, 0, 0, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 0);
            // 
            // flopPreviewImage
            // 
            this.flopPreviewImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flopPreviewImage.AutoSize = true;
            this.flopPreviewImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.flopPreviewImage.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.flopPreviewImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flopPreviewImage.ForeColor = System.Drawing.Color.White;
            this.flopPreviewImage.Location = new System.Drawing.Point(830, 306);
            this.flopPreviewImage.Name = "flopPreviewImage";
            this.flopPreviewImage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flopPreviewImage.Size = new System.Drawing.Size(117, 24);
            this.flopPreviewImage.TabIndex = 163;
            this.flopPreviewImage.Text = "Flop Preview";
            this.flopPreviewImage.UseVisualStyleBackColor = false;
            // 
            // OptionsLabel
            // 
            this.OptionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsLabel.AutoSize = true;
            this.OptionsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.OptionsLabel.Location = new System.Drawing.Point(822, 38);
            this.OptionsLabel.Name = "OptionsLabel";
            this.OptionsLabel.Size = new System.Drawing.Size(71, 20);
            this.OptionsLabel.TabIndex = 162;
            this.OptionsLabel.Text = "Options";
            // 
            // ModeLabel
            // 
            this.ModeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ModeLabel.AutoSize = true;
            this.ModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModeLabel.Location = new System.Drawing.Point(817, 156);
            this.ModeLabel.Name = "ModeLabel";
            this.ModeLabel.Size = new System.Drawing.Size(127, 20);
            this.ModeLabel.TabIndex = 161;
            this.ModeLabel.Text = "Scanning Area";
            // 
            // OutOfRange
            // 
            this.OutOfRange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutOfRange.AutoSize = true;
            this.OutOfRange.BackColor = System.Drawing.Color.Red;
            this.OutOfRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.OutOfRange.ForeColor = System.Drawing.Color.White;
            this.OutOfRange.Location = new System.Drawing.Point(849, 486);
            this.OutOfRange.Name = "OutOfRange";
            this.OutOfRange.Size = new System.Drawing.Size(98, 20);
            this.OutOfRange.TabIndex = 149;
            this.OutOfRange.Text = "Out of range";
            this.OutOfRange.Visible = false;
            // 
            // TrackingLost
            // 
            this.TrackingLost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TrackingLost.AutoSize = true;
            this.TrackingLost.BackColor = System.Drawing.Color.Red;
            this.TrackingLost.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrackingLost.ForeColor = System.Drawing.Color.White;
            this.TrackingLost.Location = new System.Drawing.Point(611, 517);
            this.TrackingLost.Name = "TrackingLost";
            this.TrackingLost.Size = new System.Drawing.Size(349, 20);
            this.TrackingLost.TabIndex = 147;
            this.TrackingLost.Text = "TRACKING LOST...align images to recover";
            this.TrackingLost.Visible = false;
            // 
            // TooFar
            // 
            this.TooFar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TooFar.AutoSize = true;
            this.TooFar.BackColor = System.Drawing.Color.Red;
            this.TooFar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TooFar.ForeColor = System.Drawing.Color.White;
            this.TooFar.Location = new System.Drawing.Point(501, 486);
            this.TooFar.Name = "TooFar";
            this.TooFar.Size = new System.Drawing.Size(104, 20);
            this.TooFar.TabIndex = 146;
            this.TooFar.Text = "Move forward";
            this.TooFar.Visible = false;
            // 
            // TooClose
            // 
            this.TooClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TooClose.AutoSize = true;
            this.TooClose.BackColor = System.Drawing.Color.Red;
            this.TooClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TooClose.ForeColor = System.Drawing.Color.White;
            this.TooClose.Location = new System.Drawing.Point(503, 517);
            this.TooClose.Name = "TooClose";
            this.TooClose.Size = new System.Drawing.Size(85, 20);
            this.TooClose.TabIndex = 144;
            this.TooClose.Text = "Move back";
            this.TooClose.Visible = false;
            // 
            // Solid
            // 
            this.Solid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Solid.AutoSize = true;
            this.Solid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.Solid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Solid.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Solid.Checked = true;
            this.Solid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Solid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Solid.ForeColor = System.Drawing.Color.White;
            this.Solid.Location = new System.Drawing.Point(830, 61);
            this.Solid.Name = "Solid";
            this.Solid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Solid.Size = new System.Drawing.Size(63, 24);
            this.Solid.TabIndex = 145;
            this.Solid.Text = "Solid";
            this.Solid.UseVisualStyleBackColor = false;
            // 
            // ScanningArea
            // 
            this.ScanningArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ScanningArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ScanningArea.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScanningArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScanningArea.Items.AddRange(new object[] {
            "Object",
            "Face",
            "Head",
            "Body",
            "Full"});
            this.ScanningArea.Location = new System.Drawing.Point(825, 183);
            this.ScanningArea.Name = "ScanningArea";
            this.ScanningArea.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ScanningArea.Size = new System.Drawing.Size(124, 33);
            this.ScanningArea.TabIndex = 142;
            // 
            // Textured
            // 
            this.Textured.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Textured.AutoSize = true;
            this.Textured.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.Textured.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Textured.Checked = true;
            this.Textured.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Textured.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Textured.ForeColor = System.Drawing.Color.White;
            this.Textured.Location = new System.Drawing.Point(830, 91);
            this.Textured.Name = "Textured";
            this.Textured.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Textured.Size = new System.Drawing.Size(81, 24);
            this.Textured.TabIndex = 148;
            this.Textured.Text = "Texture";
            this.Textured.UseVisualStyleBackColor = false;
            // 
            // Reconstruct
            // 
            this.Reconstruct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Reconstruct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.Reconstruct.Enabled = false;
            this.Reconstruct.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reconstruct.ForeColor = System.Drawing.Color.White;
            this.Reconstruct.Location = new System.Drawing.Point(821, 338);
            this.Reconstruct.Name = "Reconstruct";
            this.Reconstruct.Size = new System.Drawing.Size(132, 78);
            this.Reconstruct.TabIndex = 140;
            this.Reconstruct.Text = "Start Scanning";
            this.Reconstruct.UseVisualStyleBackColor = false;
            this.Reconstruct.Click += new System.EventHandler(this.Reconstruct_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPanel.ErrorImage = null;
            this.MainPanel.InitialImage = null;
            this.MainPanel.Location = new System.Drawing.Point(10, 36);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(789, 427);
            this.MainPanel.TabIndex = 141;
            this.MainPanel.TabStop = false;
            // 
            // Start
            // 
            this.Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.Start.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.ForeColor = System.Drawing.Color.White;
            this.Start.Location = new System.Drawing.Point(821, 230);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(134, 70);
            this.Start.TabIndex = 139;
            this.Start.Text = "Start Camera";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeviceMenu,
            this.ColorMenu,
            this.DepthMenu,
            this.ModeMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MainMenu.Size = new System.Drawing.Size(261, 33);
            this.MainMenu.TabIndex = 138;
            this.MainMenu.Text = "MainMenu";
            // 
            // DeviceMenu
            // 
            this.DeviceMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceMenu.Name = "DeviceMenu";
            this.DeviceMenu.Size = new System.Drawing.Size(90, 29);
            this.DeviceMenu.Text = "Device";
            // 
            // ColorMenu
            // 
            this.ColorMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ColorNone});
            this.ColorMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ColorMenu.Name = "ColorMenu";
            this.ColorMenu.Size = new System.Drawing.Size(75, 29);
            this.ColorMenu.Text = "Color";
            // 
            // ColorNone
            // 
            this.ColorNone.Name = "ColorNone";
            this.ColorNone.Size = new System.Drawing.Size(135, 30);
            this.ColorNone.Text = "None";
            // 
            // DepthMenu
            // 
            this.DepthMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DepthNone});
            this.DepthMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DepthMenu.Name = "DepthMenu";
            this.DepthMenu.Size = new System.Drawing.Size(81, 29);
            this.DepthMenu.Text = "Depth";
            // 
            // DepthNone
            // 
            this.DepthNone.Name = "DepthNone";
            this.DepthNone.Size = new System.Drawing.Size(135, 30);
            this.DepthNone.Text = "None";
            // 
            // ModeMenu
            // 
            this.ModeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModeLive,
            this.ModePlayback,
            this.ModeRecord});
            this.ModeMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModeMenu.Name = "ModeMenu";
            this.ModeMenu.Size = new System.Drawing.Size(78, 29);
            this.ModeMenu.Text = "Mode";
            // 
            // ModeLive
            // 
            this.ModeLive.Checked = true;
            this.ModeLive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ModeLive.Name = "ModeLive";
            this.ModeLive.Size = new System.Drawing.Size(172, 30);
            this.ModeLive.Text = "Live";
            // 
            // ModePlayback
            // 
            this.ModePlayback.Name = "ModePlayback";
            this.ModePlayback.Size = new System.Drawing.Size(172, 30);
            this.ModePlayback.Text = "Playback";
            // 
            // ModeRecord
            // 
            this.ModeRecord.Name = "ModeRecord";
            this.ModeRecord.Size = new System.Drawing.Size(172, 30);
            this.ModeRecord.Text = "Record";
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(70, 428);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(100, 21);
            this.txtMsg.TabIndex = 23;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(70, 384);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(100, 21);
            this.txtStatus.TabIndex = 22;
            // 
            // panelExport
            // 
            this.panelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelExport.Controls.Add(this.listBox2);
            this.panelExport.Controls.Add(this.label1);
            this.panelExport.Location = new System.Drawing.Point(212, 79);
            this.panelExport.Name = "panelExport";
            this.panelExport.Size = new System.Drawing.Size(970, 575);
            this.panelExport.TabIndex = 21;
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 21;
            this.listBox2.Location = new System.Drawing.Point(55, 52);
            this.listBox2.Name = "listBox2";
            this.listBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listBox2.Size = new System.Drawing.Size(864, 378);
            this.listBox2.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(970, 33);
            this.label1.TabIndex = 17;
            this.label1.Text = "Information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM5";
            this.serialPort1.ReadTimeout = 5000;
            this.serialPort1.WriteTimeout = 500;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FaceNotDetected
            // 
            this.FaceNotDetected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FaceNotDetected.AutoSize = true;
            this.FaceNotDetected.BackColor = System.Drawing.Color.Red;
            this.FaceNotDetected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FaceNotDetected.ForeColor = System.Drawing.Color.White;
            this.FaceNotDetected.Location = new System.Drawing.Point(660, 486);
            this.FaceNotDetected.Name = "FaceNotDetected";
            this.FaceNotDetected.Size = new System.Drawing.Size(139, 20);
            this.FaceNotDetected.TabIndex = 150;
            this.FaceNotDetected.Text = "Face not detected";
            this.FaceNotDetected.Visible = false;
            // 
            // listBoxMsg
            // 
            this.listBoxMsg.FormattingEnabled = true;
            this.listBoxMsg.ItemHeight = 15;
            this.listBoxMsg.Location = new System.Drawing.Point(21, 468);
            this.listBoxMsg.Name = "listBoxMsg";
            this.listBoxMsg.Size = new System.Drawing.Size(184, 94);
            this.listBoxMsg.TabIndex = 165;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1182, 655);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.sidePanel);
            this.Controls.Add(this.panelStore);
            this.Controls.Add(this.panelViewer);
            this.Controls.Add(this.panelExport);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "3D Development Application";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.sidePanel.ResumeLayout(false);
            this.sidePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelStore.ResumeLayout(false);
            this.panelViewer.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Status2.ResumeLayout(false);
            this.Status2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPanel)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.panelExport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void ColorMenu_Click(object sender, System.EventArgs e)
        {
            this.ColorMenu.ShowDropDown();
        }

        #endregion
        private System.Windows.Forms.Panel sidePanel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button buttonStorage;
        private System.Windows.Forms.Button buttonViewer;
        private System.Windows.Forms.Button buttonStore;
        private System.Windows.Forms.Button buttonScan;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panelStore;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelViewer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.StatusStrip Status2;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.CheckBox flopPreviewImage;
        private System.Windows.Forms.Label OptionsLabel;
        private System.Windows.Forms.Label ModeLabel;
        public System.Windows.Forms.Label OutOfRange;
        private System.Windows.Forms.Label TrackingLost;
        private System.Windows.Forms.Label TooFar;
        private System.Windows.Forms.Label TooClose;
        private System.Windows.Forms.CheckBox Solid;
        private System.Windows.Forms.ComboBox ScanningArea;
        private System.Windows.Forms.CheckBox Textured;
        private System.Windows.Forms.Button Reconstruct;
        private System.Windows.Forms.PictureBox MainPanel;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem DeviceMenu;
        private System.Windows.Forms.ToolStripMenuItem ColorMenu;
        private System.Windows.Forms.ToolStripMenuItem ColorNone;
        private System.Windows.Forms.ToolStripMenuItem DepthMenu;
        private System.Windows.Forms.ToolStripMenuItem DepthNone;
        private System.Windows.Forms.ToolStripMenuItem ModeMenu;
        private System.Windows.Forms.ToolStripMenuItem ModeLive;
        private System.Windows.Forms.ToolStripMenuItem ModePlayback;
        private System.Windows.Forms.ToolStripMenuItem ModeRecord;
        private System.Windows.Forms.Panel panelExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblProjectName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtMsg;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox Landmarks;
        public System.Windows.Forms.Label FaceNotDetected;
        private System.Windows.Forms.ListBox listBoxMsg;
    }
}