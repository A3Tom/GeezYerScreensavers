namespace GeezYerScreensavers.Classes
{
    using Interfaces;
    using Models;
    using System.IO;
    using System.Collections.Generic;

    class FileOrganizer : IOrganizeFiles
    {
        private readonly ICallYoDumbAssWhateverYoWant wtfYouWannaBeCalled;

        public FileOrganizer(ICallYoDumbAssWhateverYoWant wtfYouWannaBeCalled)
        {
            this.wtfYouWannaBeCalled = wtfYouWannaBeCalled;
        }

        public void OrganizeFiles(List<ImageFile> imageFiles)
        {
            
            string newDirectory, fullFileName;

            foreach (ImageFile rawImageFile in imageFiles)
            {
                ImageFile imageFile = wtfYouWannaBeCalled.ReturnCompleteMetaData(rawImageFile);

                if (imageFile.Height >= 1080 && imageFile.Width >= 1920)
                {
                    newDirectory = imageFile.BaseSaveLocation + @"\Wallpapers\";
                }
                else if(imageFile.Height >= 1920 && imageFile.Width >= 1080)
                {
                    newDirectory = imageFile.BaseSaveLocation + @"\Portrait\";
                }
                else
                {
                    newDirectory = imageFile.BaseSaveLocation + @"\Other\";
                }


                fullFileName = newDirectory + imageFile.Title + ".jpg";

                if (!Directory.Exists(newDirectory))
                    Directory.CreateDirectory(newDirectory);

                if (!File.Exists(fullFileName))
                    File.Copy(imageFile.BaseSaveLocation + imageFile.FileName, fullFileName);

                File.Delete(imageFile.BaseSaveLocation + imageFile.FileName);
            }
        }
    }
}
