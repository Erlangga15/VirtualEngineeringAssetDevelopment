using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample3dscan.cs
{
    class Archiving
    {
        public void Extract(FileInfo FileToDecompress)
        {
            using (FileStream originalStream = FileToDecompress.OpenRead())
            {
                string currentFileName = FileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - FileToDecompress.Extension.Length);
                using (FileStream DecompressionFileStream = File.Create(newFileName))
                {
                    using (GZipStream DecompressionStream = new GZipStream(originalStream, CompressionMode.Decompress))
                    {
                        if (FileToDecompress.Extension == ".zip")
                        {
                            DecompressionStream.CopyTo(DecompressionFileStream);
                        }
                    }
                }
            }
        }
    }
}
