using System;
using System.IO;
using Microsoft.Extensions.Configuration;

// consts purely because I like getting an opertunity to type in ALL CAPS
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


// Okay so what images can we find
foreach (var file in Directory.GetFiles(spotlightImageDir))
{
    Console.WriteLine(file);
}
















Console.WriteLine("Remo");

Console.Read();