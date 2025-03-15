using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCaptureApp.Base
{
    public class BaseKeyboardShortcut
    {
        #region Property

        /// <summary>
        /// 鍵碼,112
        /// </summary>
        public int Code { set; get; }

        /// <summary>
        /// 描述,F11
        /// </summary>
        public string? Description { set; get; }

        /// <summary>
        /// 按下 Ctrl、Shift 或 Alt
        /// </summary>
        public bool IsModifiersHasFlag { set; get; }

        /// <summary>
        /// Ctrl、Shift 或 Alt
        /// </summary>
        public ConsoleModifiers CM { set; get; }

        #endregion
    }
}
