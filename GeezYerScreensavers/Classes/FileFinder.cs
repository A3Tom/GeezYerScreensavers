namespace GeezYerScreensavers.Classes
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;

    public class FileFinder : IFileFinder
    {
        private readonly ILog logger;

        public FileFinder(ILog logger)
        {
            this.logger = logger;
        }

        public List<ImageFile> FindImages(string filePath)
        {
            List<ImageFile> result = new List<ImageFile>();
            string[] files = Directory.GetFiles(filePath);

            foreach (string file in files)
            {
                ImageFile imageFile = new ImageFile();
                imageFile.FileName = file.Replace(filePath,"");
                imageFile.BaseSaveLocation = filePath;
                result.Add(imageFile);
            }
            Console.WriteLine(string.Format("Found {0} images in {1}.", result.Count, filePath));
            return result;
        }
    }
}
