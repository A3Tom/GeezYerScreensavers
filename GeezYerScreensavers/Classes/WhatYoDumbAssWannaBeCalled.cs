namespace GeezYerScreensavers.Classes
{
    using Interfaces;
    using Models;
    using System.Configuration;
    using System.Collections.Generic;
    using System.IO;
    using System;
    using System.Diagnostics;
    using Microsoft.WindowsAPICodePack.Shell;
    using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

    public class WhatYoDumbAssWannaBeCalled : ICallYoDumbAssWhateverYoWant
    {

        public ImageFile ReturnCompleteMetaData(ImageFile imageFile)
        {
            if (imageFile.FileName.EndsWith(".jpg"))
            {
                ShellObject picture = ShellObject.FromParsingName(imageFile.BaseSaveLocation + imageFile.FileName);
                int picWidth = 0, picHeight = 0;

                int.TryParse(GetValue(picture.Properties.GetProperty(SystemProperties.System.Image.HorizontalSize)), out picWidth);
                int.TryParse(GetValue(picture.Properties.GetProperty(SystemProperties.System.Image.VerticalSize)), out picHeight);
                var picTitle = GetValue(picture.Properties.GetProperty(SystemProperties.System.Title));

                if(picTitle == "")
                {
                    picTitle = string.Format("{0:ddMMyy HHmmssffff}",DateTime.Now);
                }

                var formattedString = String.Format("File {0}\nHas Width {1} and Height {2}\nWith a real title of: {3}\n",
                                                    picture.ParsingName,
                                                    picWidth,
                                                    picHeight,
                                                    picTitle);

                imageFile.Width = picWidth;
                imageFile.Height = picHeight;
                imageFile.Title = picTitle;

                Console.WriteLine(formattedString);
            }
            return imageFile;
        }

        private static string GetValue(IShellProperty value)
        {
            if (value == null || value.ValueAsObject == null)
            {
                return String.Empty;
            }
            return value.ValueAsObject.ToString();
        }
    }
}
