namespace GeezYerScreensavers.Classes
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;

    public class FileSaver : IFileSaver
    {
        private readonly ILog logger;

        public FileSaver(ILog logger)
        {
            this.logger = logger;
        }

        public void SaveFiles(List<ImageFile> imageFiles, string filePath)
        {
            string oldFileName;
            string newFileName;

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            foreach (ImageFile imageFile in imageFiles)
            {
                oldFileName = imageFile.BaseSaveLocation + imageFile.FileName;
                newFileName = filePath + imageFile.FileName + ".jpg";

                logger.Debug(string.Format("Copying {0} to {1}", imageFile.FileName, filePath));

                if(!File.Exists(newFileName))
                    File.Copy(oldFileName, newFileName);
            }
        }
    }
}
