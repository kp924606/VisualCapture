using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VisualCaptureApp.Base
{
    public class BaseSpecifiedRange: INotifyPropertyChanged    
    {
        //public double Top { set; get; }
        private double _top;
        public double Top
        {
            get => this._top;
            set
            {
                if (this._top != value)
                {
                    this._top = value;
                    OnPropertyChanged(nameof(this.Top));
                }
            }
        }

        //public double Left { set; get; }
        private double _left;
        public double Left
        {
            get => this._left;
            set
            {
                if (this._left != value)
                {
                    this._left = value;
                    OnPropertyChanged(nameof(this.Left));
                }
            }
        }

        //public double Height { set; get; }
        private double _height;
        public double Height
        {
            get => this._height;
            set
            {
                if (this._height != value)
                {
                    this._height = value;
                    OnPropertyChanged(nameof(this.Height));
                }
            }
        }

        private double _width;
        public double Width
        {
            get => this._width;
            set
            {
                if (this._width != value)
                {
                    this._width = value;
                    OnPropertyChanged(nameof(this.Width));
                }
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
    }
}
