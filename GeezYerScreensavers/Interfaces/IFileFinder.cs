namespace GeezYerScreensavers.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface IFileFinder
    {
        List<ImageFile> FindImages(string filePath);
    }
}
