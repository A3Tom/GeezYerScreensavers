using System;
using System.IO;
using Microsoft.Extensions.Configuration;

const string SAVE_TARGET_DIR_KEY = "ConfigLocations:SaveImageToLocation";
const string SPOTLIGHT_IMAGE_DIR_KEY = "ConfigLocations:SpotlightImageLocation";

IConfigurationRoot _config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
    .AddJsonFile("appsettings.json", false)
    .Build();

var saveTargetDir = Environment.ExpandEnvironmentVariables(_config.GetSection(SAVE_TARGET_DIR_KEY).Value);
var spotlightImageDir = Environment.ExpandEnvironmentVariables(_config.GetSection(SPOTLIGHT_IMAGE_DIR_KEY).Value);























Console.WriteLine("Remo");

Console.Read();