using ILogger.AP;
using Judgment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VisualCaptureApp.Base
{
    public class BaseSpecifiedRange: INotifyPropertyChanged    
    {
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        const int LOGPIXELSX = 88;

        public double ScaleFactor { set; get; }

        //public double Top { set; get; }
        private double _top;
        public double Top
        {
            get => this._top;
            set
            {
                this._top = value * this.ScaleFactor;
                OnPropertyChanged(nameof(this.Top));
            }
        }

        //public double Left { set; get; }
        private double _left;
        public double Left
        {
            get => this._left;
            set
            {
                this._left = value * this.ScaleFactor;
                OnPropertyChanged(nameof(this.Left));
            }
        }

        //public double Height { set; get; }
        private double _height;
        public double Height
        {
            get => this._height;
            set
            {
                this._height = value * this.ScaleFactor;
                OnPropertyChanged(nameof(this.Height));
            }
        }

        private double _width;
        public double Width
        {
            get => this._width;
            set
            {
                this._width = value * this.ScaleFactor;
                OnPropertyChanged(nameof(this.Width));
            }
        }

        //public double BorderThickness { set; get; }
        private double _borderThickness;
        public double BorderThickness
        {
            get => this._borderThickness;
            set
            {
                if (this._borderThickness != value)
                {
                    this._borderThickness = value;
                    OnPropertyChanged(nameof(this.BorderThickness));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BaseSpecifiedRange()
        {
            try
            {
                this.ScaleFactor = GetScaleFactor();
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{MethodBase.GetCurrentMethod()!.DeclaringType!.Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{MethodBase.GetCurrentMethod()!.DeclaringType!.Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        static float GetScaleFactor()
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr desktop = g.GetHdc();
                int dpi = GetDeviceCaps(desktop, LOGPIXELSX);
                g.ReleaseHdc(desktop);

                return dpi / 96.0f; // 96 DPI = 100% 縮放
            }
        }
    }
}
