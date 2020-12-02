using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

// consts purely because I like getting an opportunity to type in ALL CAPS
const string SAVE_TARGET_DIR_KEY = "ConfigLocations:SaveImageToLocation";
const string SPOTLIGHT_IMAGE_DIR_KEY = "ConfigLocations:SpotlightImageLocation";

// Lets build a fuckin config roooooooot
IConfigurationRoot _config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
    .AddJsonFile("appsettings.json", false)
    .Build();

// Get directories from appSettings
var saveTargetDir = Environment.ExpandEnvironmentVariables(_config.GetSection(SAVE_TARGET_DIR_KEY).Value);
var spotlightImageDir = Environment.ExpandEnvironmentVariables(_config.GetSection(SPOTLIGHT_IMAGE_DIR_KEY).Value);

// Ensure target directory exists
Directory.CreateDirectory(saveTargetDir);

// Okay so what images can we find and lets copy them to destination folder to allow us to rename them
foreach (var file in Directory.GetFiles(spotlightImageDir))
{
    var _fullyQualifiedFilePath = $"{saveTargetDir}/{Path.GetFileName(file)}.jpg";

    if (!File.Exists(_fullyQualifiedFilePath))
        File.Copy(file, _fullyQualifiedFilePath);
}

// Alright lets loop through these fucks and sort em then give them a readable name, move them to sorted directory n finish up
foreach (var file in Directory.GetFiles(saveTargetDir))
{
    ShellObject shellObj = ShellObject.FromParsingName(file);

    int.TryParse(shellObj.Properties.GetProperty(SystemProperties.System.Image.HorizontalSize)?.ValueAsObject?.ToString(), out int _width);
    int.TryParse(shellObj.Properties.GetProperty(SystemProperties.System.Image.VerticalSize)?.ValueAsObject?.ToString(), out int _height);
    var _fileName = shellObj.Properties.GetProperty(SystemProperties.System.Title)?.ValueAsObject?.ToString() ?? $"{DateTime.Now:ddMMyy HHmmssffff}";
    _fileName += ".jpg";

    var sortFile = new SortFile(_width, _height);

    var destinationFolder = sortFile switch
    {
        { Width: >= 1920 } and { Height: >= 1080 } => "Portrait",
        { Width: >= 1080 } and { Height: >= 1920 } => "Wallpapers",
        _ => "Other"
    };

    var sortedFQDir = Path.Combine(Directory.GetParent(file).FullName, destinationFolder, _fileName);

    if (!Directory.Exists(Directory.GetParent(sortedFQDir).FullName))
        Directory.CreateDirectory(Directory.GetParent(sortedFQDir).FullName);

    if (!File.Exists(sortedFQDir))
        File.Copy(file, sortedFQDir);

    File.Delete(file);
}

record SortFile(int Width, int Height);