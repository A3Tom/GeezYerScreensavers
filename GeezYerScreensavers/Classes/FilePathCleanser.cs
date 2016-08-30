namespace GeezYerScreensavers.Classes
{
    using Interfaces;
    using System;
    using System.Configuration;

    class FilePathCleanser : ICleanseFilePaths
    {
        public string GeezMaFilePath(string weeDurty)
        {
            string filePath = ConfigurationManager.AppSettings[weeDurty].ToString();
            filePath = Environment.ExpandEnvironmentVariables(filePath);

            return filePath;
        }
    }
}
