namespace GeezYerScreensavers.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface IFileSaver
    {
        void SaveFiles(List<ImageFile> imageFiles, string filePath);
    }
}
