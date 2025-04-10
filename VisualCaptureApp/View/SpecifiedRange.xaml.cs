using ILogger.AP;
using ILogger.Enum;
using Judgment;
using ProjectLifeModuleManagement.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VisualCaptureApp.AP;
using VisualCaptureApp.Base;

namespace VisualCaptureApp.View
{
    /// <summary>
    /// SpecifiedRange.xaml 的互動邏輯
    /// </summary>
    public partial class SpecifiedRange : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// 選擇範圍邊框
        /// </summary>
        private Thickness _specifiedRangeBorderThickness = new Thickness(GenerallySize.SpecifiedRangeBorderThickness);

        /// <summary>
        /// 選擇範圍邊框
        /// </summary>
        public Thickness SpecifiedRangeBorderThickness
        {
            get => this._specifiedRangeBorderThickness;
            set
            {
                if (this._specifiedRangeBorderThickness != value)
                {
                    this._specifiedRangeBorderThickness = value;
                    OnPropertyChanged(nameof(this.SpecifiedRangeBorderThickness));
                }
            }
        }

        /// <summary>
        /// 選擇範圍顏色1
        /// </summary>
        private SolidColorBrush? _specifiedRangeBorderColor1 { set; get; }

        /// <summary>
        /// 選擇範圍顏色1
        /// </summary>
        public SolidColorBrush? SpecifiedRangeBorderColor1
        {
            get => this._specifiedRangeBorderColor1;
            set
            {
                if (this._specifiedRangeBorderColor1 != value)
                {
                    this._specifiedRangeBorderColor1 = value;
                    OnPropertyChanged(nameof(this.SpecifiedRangeBorderColor1));
                }
            }
        }

        /// <summary>
        /// 選擇範圍顏色2
        /// </summary>
        private SolidColorBrush? _specifiedRangeBorderColor2 { set; get; }

        /// <summary>
        /// 選擇範圍顏色2
        /// </summary>
        public SolidColorBrush? SpecifiedRangeBorderColor2
        {
            get => this._specifiedRangeBorderColor2;
            set
            {
                if (this._specifiedRangeBorderColor2 != value)
                {
                    this._specifiedRangeBorderColor2 = value;
                    OnPropertyChanged(nameof(this.SpecifiedRangeBorderColor2));
                }
            }
        }

        /// <summary>
        /// 選擇範圍物件
        /// </summary>
        private BaseRecordFullScreen? _baseRecordFullScreen { set; get; }

        /// <summary>
        /// 事件觸發
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SpecifiedRange(BaseRecordFullScreen brfs)
        {
            InitializeComponent();

            if (brfs == null)
            {
                throw new ExpectedInfo($@"Please check BaseSpecifiedRange, Object is Null", Code.ODI_005);
            }

            this._baseRecordFullScreen = brfs!;
            this.DataContext = this; // 設定 DataContext，讓 XAML 可以綁定變數

            //this.SpecifiedRangeBorderColor1 = System.Windows.Media.Brushes.Yellow;
            //this.SpecifiedRangeBorderColor2 = System.Windows.Media.Brushes.Black;
        }

        /// <summary>
        /// 繪製畫布範圍(移動等)
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        void Thumb_OnDragDelta(object Sender, DragDeltaEventArgs E)
        {
            try
            {
                if (Sender is FrameworkElement element)
                {
                    void DoTop()
                    {
                        var oldTop = Top;
                        var oldBottom = Top + Region.Height;
                        var top = Top + E.VerticalChange;

                        if (top > 0)
                            Top = top;
                        else
                        {
                            Top = 0;
                            Region.Width = oldBottom;
                            return;
                        }

                        var height = Region.Height - E.VerticalChange;

                        if (height > Region.MinHeight)
                            Region.Height = height;
                        else Top = oldTop;
                    }

                    void DoLeft()
                    {
                        var oldLeft = Left;
                        var oldRight = Left + Region.Width;
                        var left = Left + E.HorizontalChange;
                        Left = left;
                        //if (left > 0)
                        //    Left = left;
                        //else
                        //{
                        //    Left = 0;
                        //    Region.Width = oldRight;
                        //    return;
                        //}

                        //Left = left;
                        //if (oldRight > 0)
                        //{
                        //    Region.Width = oldRight;
                        //}

                        var width = Width - E.HorizontalChange;

                        //if (width > Region.MinWidth)
                        //    Region.Width = width;
                        //else Left = oldLeft;

                        if (width > 0)
                            Region.Width = width;
                    }

                    void DoBottom()
                    {
                        var height = Height + E.VerticalChange;

                        if (height > 0)
                            Region.Height = height;
                    }

                    void DoRight()
                    {
                        var width = Width + E.HorizontalChange;
                    
                        if (width > 0)
                            Region.Width = width;
                    }

                    void DoMove()
                    {
                        Left += E.HorizontalChange;
                        Top += E.VerticalChange;
                    }

                    switch (element.Tag)
                    {
                        case "Move":
                            DoMove();
                            break;
                        case "Top":
                            DoMove();
                            break;

                        case "Bottom":
                            DoBottom();
                            break;

                        case "Left":
                            DoLeft();
                            break;

                        case "Right":
                            DoRight();
                            break;

                        case "TopLeft":
                            DoTop();
                            DoLeft();
                            break;

                        case "TopRight":
                            DoTop();
                            DoRight();
                            break;

                        case "BottomLeft":
                            DoBottom();
                            DoLeft();
                            break;

                        case "BottomRight":
                            DoBottom();
                            DoRight();
                            break;
                    }

                    this._baseRecordFullScreen!.BaseSpecifiedRange!.Top = Top;
                    this._baseRecordFullScreen!.BaseSpecifiedRange!.Left = Left;
                    this._baseRecordFullScreen!.BaseSpecifiedRange!.Height = Height;
                    this._baseRecordFullScreen!.BaseSpecifiedRange!.Width = Width;
                    this._baseRecordFullScreen!.BaseSpecifiedRange!.BorderThickness = this._specifiedRangeBorderThickness.Top;
                }
            }
            catch (ExpectedInfo ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }
    }
}
