using System;
using System.Collections.Generic;
using System.IO;

namespace AutomataNETjuegos.Compilador
{
    public class TempFileManager : ITempFileManager
    {
        private List<string> tempFiles;

        public TempFileManager()
        {
            tempFiles = new List<string>();
        }

        public string Create()
        {
            var tempFilePath = Path.GetTempFileName();
            this.tempFiles.Add(tempFilePath);
            return tempFilePath;
        }

        public void Dispose()
        {
            foreach (var tempFile in tempFiles)
            {
                
                try
                {
                    File.Delete(tempFile);
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
