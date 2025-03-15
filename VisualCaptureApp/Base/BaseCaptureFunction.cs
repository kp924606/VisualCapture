using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCaptureApp.AP
{
    public class BaseCaptureFunction
    {
        #region Static
        public const string ScreenshotFullScreen = "ScreenshotFullScreen";
        public const string ScreenshotSpecifyRange = "ScreenshotSpecifyRange";

        public const string ScreenshotFullScreenImagPath = "image/ScreenshotFullScreen.png";
        public const string ScreenshotSpecifyRangeImagPath = "image/ScreenshotSpecifyRange.png";

        #endregion

        #region Property
        public string? Name { set; get; }
        public string? ImagePath { set; get; }

        public bool HideWindow { set; get; }
        #endregion


        public BaseCaptureFunction(string name, string imagepath, bool hidewindow)
        {
            this.Name = name;
            this.ImagePath = imagepath;
            this.HideWindow = hidewindow;
        }
    }
}
