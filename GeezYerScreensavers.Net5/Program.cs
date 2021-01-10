using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
IConfigurationRoot _config = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetParent(System.AppContext.BaseDirectory).FullName).AddJsonFile("appsettings.json", false).Build();
var saveTargetDir = System.Environment.ExpandEnvironmentVariables(_config.GetSection("ConfigLocations:SaveImageToLocation").Value);
var spotlightImageDir = System.Environment.ExpandEnvironmentVariables(_config.GetSection("ConfigLocations:SpotlightImageLocation").Value);
System.IO.Directory.CreateDirectory(saveTargetDir);
foreach (var file in Directory.GetFiles(spotlightImageDir))
{
    var _fullyQualifiedFilePath = $"{saveTargetDir}/{Path.GetFileName(file)}.jpg";
    if (!File.Exists(_fullyQualifiedFilePath))
        File.Copy(file, _fullyQualifiedFilePath);
}
foreach (var file in Directory.GetFiles(saveTargetDir))
{
    Microsoft.WindowsAPICodePack.Shell.ShellObject shellObj = Microsoft.WindowsAPICodePack.Shell.ShellObject.FromParsingName(file);
    int.TryParse(shellObj.Properties.GetProperty(SystemProperties.System.Image.HorizontalSize)?.ValueAsObject?.ToString(), out int _width);
    int.TryParse(shellObj.Properties.GetProperty(SystemProperties.System.Image.VerticalSize)?.ValueAsObject?.ToString(), out int _height);
    var _fileName = shellObj.Properties.GetProperty(SystemProperties.System.Title)?.ValueAsObject?.ToString() ?? $"{System.DateTime.Now:ddMMyy HHmmssffff}";
    _fileName += ".jpg";
    var sortFile = new SortFile(_width, _height);
    var destinationFolder = sortFile switch { { Width: >= 1920 } and { Height: >= 1080 } => "Wallpapers", { Width: >= 1080 } and { Height: >= 1920 } => "Portrait", _ => "Other" };
    var sortedFQDir = Path.Combine(Directory.GetParent(file).FullName, destinationFolder, _fileName);
    if (!Directory.Exists(Directory.GetParent(sortedFQDir).FullName))
        Directory.CreateDirectory(Directory.GetParent(sortedFQDir).FullName);
    if (!File.Exists(sortedFQDir))
        File.Copy(file, sortedFQDir);
    File.Delete(file);
}
record SortFile(int Width, int Height);