namespace GeezYerScreensavers.Classes
{
    using Interfaces;
using Models;
using System;
using System.Collections.Generic;

    public class App : IApp
    {
        private readonly ILog logger;
        private readonly IFileFinder fileFinder;
        private readonly IFileSaver fileSaver;
        private readonly IOrganizeFiles fileOrganizer;
        private readonly ICleanseFilePaths filePathCleanser;

        public App(ILog logger, IFileFinder fileFinder, IFileSaver fileSaver, IOrganizeFiles fileOrganizer, ICleanseFilePaths filePathCleanser)
        {
            this.logger = logger;
            this.fileFinder = fileFinder;
            this.fileSaver = fileSaver;
            this.fileOrganizer = fileOrganizer;
            this.filePathCleanser = filePathCleanser;
        }

        public void Run()
        {
            Console.WriteLine("Spotlight Image Finder Started.");

            List<ImageFile> imageFilesToBeCopied = new List<ImageFile>();
            List<ImageFile> imageFilesToBeOrganized = new List<ImageFile>();

            string spotlightFilePath = filePathCleanser.GeezMaFilePath("SpotlightImageLocation");
            string newLocalPath = filePathCleanser.GeezMaFilePath("SaveImageToLocation");

            imageFilesToBeCopied = fileFinder.FindImages(spotlightFilePath);
            fileSaver.SaveFiles(imageFilesToBeCopied, newLocalPath);

            imageFilesToBeCopied = fileFinder.FindImages(newLocalPath);
            fileOrganizer.OrganizeFiles(imageFilesToBeCopied);

            Console.WriteLine("Spotlight Image Finder Finished.");
            Console.ReadLine();
        }
    }
}
