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
using System.Windows.Forms;
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
        /// 邊框邊界
        /// </summary>
        const int SnapThreshold = 3; // 吸附距離（像素）

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
        /// 顯示十字架
        /// </summary>
        private bool _isShowCrossCanvas;

        /// <summary>
        /// 顯示十字架
        /// </summary>
        public bool IsShowCrossCanvas
        {
            get => this._isShowCrossCanvas;
            set
            {
                this._isShowCrossCanvas = value;
                OnPropertyChanged(nameof(IsShowCrossCanvas));
            }
        }

        /// <summary>
        /// 選擇範圍物件
        /// </summary>
        //private BaseRecordFullScreen? _baseRecordFullScreen { set; get; }

        ///基本指定範圍
        private BaseSpecifiedRange _baseSpecifiedRange { set; get; }

        /// <summary>
        /// 事件觸發
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="brfs"></param>
        /// <exception cref="ExpectedInfo"></exception>
        public SpecifiedRange(BaseSpecifiedRange bsr)
        {
            //public SpecifiedRange(BaseRecordFullScreen brfs)
            InitializeComponent();

            if (bsr == null)
            {
                throw new ExpectedInfo($@"Please check BaseSpecifiedRange, Object is Null", Code.ODI_005);
            }

            this._baseSpecifiedRange = bsr!;
            this.DataContext = this; // 設定 DataContext，讓 XAML 可以綁定變數
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
                        //var oldLeft = Left;
                        //var oldRight = Left + Region.Width;
                        //var left = Left + E.HorizontalChange;
                        //Left = left;                        

                        //var width = Width - E.HorizontalChange;
                        //if (width > 0)
                        //    Region.Width = width;
                        //---

                        double minLeft = Screen.AllScreens.Min(s => s.Bounds.Left);                       

                        double oldLeft = Left;
                        double newLeft = oldLeft + E.HorizontalChange;
                        double oldRight = oldLeft + Width;

                        // 限制不要超出螢幕左邊界
                        if (newLeft < minLeft)
                            newLeft = minLeft;
                        double newWidth = oldRight - newLeft;

                        if (newWidth > Region.MinWidth)
                        {
                            Left = newLeft;
                            Region.Width = newWidth;
                        }
                    }

                    void DoBottom()
                    {
                        //var height = Height + E.VerticalChange;

                        //if (height > 0)
                        //    Region.Height = height;

                        //---

                        double maxBottom = Screen.AllScreens.Max(s => s.Bounds.Bottom);

                        double newHeight = Height + E.VerticalChange;
                        double maxHeight = maxBottom - Top;

                        // 限制不能超出螢幕下邊界
                        if (newHeight > 0 && (Top + newHeight <= maxBottom))
                            Region.Height = newHeight;
                        else
                            Region.Height = maxHeight;
                    }

                    void DoRight()
                    {
                        //var width = Width + E.HorizontalChange;

                        //if (width > 0)
                        //    Region.Width = width;

                        //---
                        double maxRight = Screen.AllScreens.Max(s => s.Bounds.Right);

                        double newWidth = Width + E.HorizontalChange;
                        double maxWidth = maxRight - Left;

                        // 限制不能超出螢幕右邊界
                        if (newWidth > 0 && (Left + newWidth <= maxRight))
                            Region.Width = newWidth;
                        else
                            Region.Width = maxWidth;
                    }

                    void DoMove()
                    {
                        //Left += E.HorizontalChange;
                        //Top += E.VerticalChange;
                        //---
                        //// 取得目前所在螢幕（含工作列）
                        //var screen = System.Windows.Forms.Screen.FromHandle(
                        //    new System.Windows.Interop.WindowInteropHelper(this).Handle
                        //);
                        //var screenBounds = screen.Bounds;

                        //// 原本位置 + 拖曳變化
                        //double newLeft = Left + E.HorizontalChange;
                        //double newTop = Top + E.VerticalChange;

                        //// 計算最大 Left / Top（不超出螢幕）
                        //double maxLeft = screenBounds.Right - Width;
                        //double maxTop = screenBounds.Bottom - Height;

                        //// ======== X 軸吸附邏輯 ========
                        //if (Math.Abs(newLeft - screenBounds.Left) <= SnapThreshold)
                        //    Left = screenBounds.Left;
                        //else if (Math.Abs(newLeft - maxLeft) <= SnapThreshold)
                        //    Left = maxLeft;
                        //else
                        //    Left = Math.Clamp(newLeft, screenBounds.Left, maxLeft);

                        //// ======== Y 軸吸附邏輯 ========
                        //if (Math.Abs(newTop - screenBounds.Top) <= SnapThreshold)
                        //    Top = screenBounds.Top;
                        //else if (Math.Abs(newTop - maxTop) <= SnapThreshold)
                        //    Top = maxTop;
                        //else
                        //    Top = Math.Clamp(newTop, screenBounds.Top, maxTop);

                        //---
                        double minLeft = Screen.AllScreens.Min(s => s.Bounds.Left);
                        double minTop = Screen.AllScreens.Min(s => s.Bounds.Top);
                        double maxRight = Screen.AllScreens.Max(s => s.Bounds.Right);
                        double maxBottom = Screen.AllScreens.Max(s => s.Bounds.Bottom);

                        // 原本位置 + 拖曳變化
                        double newLeft = Left + E.HorizontalChange;
                        double newTop = Top + E.VerticalChange;

                        // 計算最大 Left / Top（不超出螢幕）
                        double maxLeft = maxRight - Width;
                        double maxTop = maxBottom - Height;

                        // ======== X 軸吸附邏輯 ========
                        if (Math.Abs(newLeft - minLeft) <= SnapThreshold)
                            Left = minLeft;
                        else if (Math.Abs(newLeft - maxLeft) <= SnapThreshold)
                            Left = maxLeft;
                        else
                            Left = Math.Clamp(newLeft, minLeft, maxLeft);

                        // ======== Y 軸吸附邏輯 ========
                        if (Math.Abs(newTop - minTop) <= SnapThreshold)
                            Top = minTop;
                        else if (Math.Abs(newTop - maxTop) <= SnapThreshold)
                            Top = maxTop;
                        else
                            Top = Math.Clamp(newTop, minTop, maxTop);
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

                    this._baseSpecifiedRange!.Top = Top;
                    this._baseSpecifiedRange!.Left = Left;
                    this._baseSpecifiedRange!.Height = Height;
                    this._baseSpecifiedRange!.Width = Width;
                    this._baseSpecifiedRange!.BorderThickness = this._specifiedRangeBorderThickness.Top;
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
