using ILogger.AP;
using Judgment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualCaptureApp.Base;

namespace VisualCaptureApp.View
{
    /// <summary>
    /// MessageBoxWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MessageBoxWindow : Window, INotifyPropertyChanged
    {
        #region Static Property
        public const string MessageInfoImagePath = @"/image/MessageInfoImage.png";
        public const string MessageErrorImagePath = @"/image/MessageErrorImage.png";
        public const string MessageAlarmImagePath = @"/image/MessageAlarmImage.png";
        public const string MessagePassImagePath = @"/image/MessagePassImage.png";
        public const double DefaultWindowHeight = 400;
        public const double DefaultWindowWidth = 600;
        public const double MaxWindowHeight = DefaultWindowHeight + 300;
        public const double MaxWindowWidth = DefaultWindowWidth + 200;
        public const double DefaultGridContentHeight = 160;
        public const double DiffWindowsHeight = 100;
        public const double AddTextHeight = 200;
        public const double MiniWindowHeight = 300;
        #endregion

        #region Property
        /// <summary>
        /// 視窗 Title
        /// </summary>
        private string? _windowsTitle { set; get; }

        public string WindowTitle
        {
            get
            {
                return this._windowsTitle!;
            }
            set
            {
                this._windowsTitle = value;
                OnPropertyChanged(nameof(this.WindowTitle));
            }
        }

        /// <summary>
        /// 圖片
        /// </summary>
        private string? _typeImageSourcePath;

        public string TypeImageSourcePath
        {
            get
            {
                return this._typeImageSourcePath!;
            }
            set
            {
                this._typeImageSourcePath = value;
                OnPropertyChanged(nameof(this.TypeImageSourcePath));
            }
        }

        /// <summary>
        /// 圖片
        /// </summary>
        private string? _windowContent { set; get; }

        public string WindowContent
        {
            get
            {
                return this._windowContent!;
            }
            set
            {
                this._windowContent = value;
                OnPropertyChanged(nameof(this.WindowContent));
            }
        }

        /// <summary>
        /// 視窗要呈現的樣式
        /// </summary>
        private MessageBoxWindowShowType _messageBoxWindowShowType { set; get; }

        /// <summary>
        /// 視窗按鈕清單
        /// </summary>
        private MessageBoxWindowButtonType _messageBoxWindowButtonType { set; get; }

        /// <summary>
        /// 結果
        /// </summary>
        private MessageBoxWindowResult _result { set; get; }

        /// <summary>
        /// 結果
        /// </summary>
        public MessageBoxWindowResult Result
        {
            get 
            {
                return this._result;
            }
        }

        /// <summary>
        /// GridTitleColor 顏色
        /// </summary>
        private SolidColorBrush? _gridTitleColor;
        
        public SolidColorBrush GridTitleColor
        {
            get
            {
                return this._gridTitleColor!;
            }
            set
            {
                this._gridTitleColor = value;
                OnPropertyChanged(nameof(this.GridTitleColor));
            }
        }

        /// <summary>
        /// 螢幕高度
        /// </summary>
        private double _windowHeight { set; get; }

        /// <summary>
        /// 螢幕高度
        /// </summary>
        public double WindowHeight
        {
            get
            {
                return this._windowHeight!;
            }
            set
            {
                this._windowHeight = value;
                OnPropertyChanged(nameof(this.WindowHeight));
            }
        }

        /// <summary>
        /// 螢幕寬度
        /// </summary>
        private double _windowWidth { set; get; }

        /// <summary>
        /// 螢幕寬度
        /// </summary>
        public double WindowWidth
        {
            get
            {
                return this._windowWidth!;
            }
            set
            {
                this._windowWidth = value;
                OnPropertyChanged(nameof(this.WindowWidth));
            }
        }

        /// <summary>
        /// TextBox 寬度
        /// </summary>
        private double _textBlockWidth { set; get; }

        /// <summary>
        /// TextBox 寬度
        /// </summary>
        public double TextBlockWidth
        {
            get
            {
                if (this._windowWidth <= 200)
                {
                    return this._windowWidth;
                }
                else
                {
                    return this._windowWidth - 100;
                }
            }
            set
            {
                OnPropertyChanged(nameof(this.TextBlockWidth));
            }
        }

        /// <summary>
        /// Grid Title 高度
        /// </summary>
        private double _gridTitleHeight { set; get; }

        /// <summary>
        /// Grid Title 高度
        /// </summary>
        public double GridTitleHeight
        {
            get
            {
                return this._gridTitleHeight;
            }
            set
            {
                this._gridTitleHeight = value;
                OnPropertyChanged(nameof(this.GridTitleHeight));
            }
        }

        /// <summary>
        /// Grid 內容高度
        /// </summary>
        private double _gridContentHeight { set; get; }

        /// <summary>
        /// Grid 內容高度
        /// </summary>
        public double GridContentHeight
        {
            get
            {
                return this._gridContentHeight;
            }
            set
            {
                this._gridContentHeight = value;
                OnPropertyChanged(nameof(this.GridContentHeight));
            }
        }

        /// <summary>
        /// Grid 按鈕高度
        /// </summary>
        private double _gridButtonHeight { set; get; }

        /// <summary>
        /// Grid 按鈕高度
        /// </summary>
        public double GridButtonHeight
        {
            get
            {
                return this._gridButtonHeight;
            }
            set
            {
                this._gridButtonHeight = value;
                OnPropertyChanged(nameof(this.GridButtonHeight));
            }
        }

        /// <summary>
        /// 目前螢幕
        /// </summary>
        private Screen _currentScreen => this.GetCurrentScreen();

        /// <summary>
        /// 事件觸發
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        //public MessageBoxWindow()
        //{
        //    InitializeComponent();
        //    // 設定 DataContext，讓 XAML 可以綁定變數
        //    this.DataContext = this;
        //}

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="showtype"></param>
        /// <param name="buttontype"></param>
        /// <exception cref="ExpectedInfo"></exception>
        public MessageBoxWindow(string title, string content, MessageBoxWindowShowType showtype, MessageBoxWindowButtonType buttontype)
        {
            try
            {
                InitializeComponent();

                // 設定 DataContext，讓 XAML 可以綁定變數 
                this.DataContext = this;

                this.WindowTitle = title;
                this.WindowContent = content;
                this._messageBoxWindowShowType = showtype;
                this._messageBoxWindowButtonType = buttontype;

                double tpMWW = 0;
                if (this._currentScreen.Bounds.Width < MaxWindowWidth)
                {
                    tpMWW = Math.Max(this._currentScreen.Bounds.Width - 100, DefaultWindowWidth);
                }
                else
                {
                    tpMWW = Math.Min(this._currentScreen.Bounds.Width - 100, MaxWindowWidth);
                }                
                //this.WindowWidth = DefaultWindowWidth;
                this.WindowWidth = tpMWW;

                double tpMWH = 0;
                if (this._currentScreen.Bounds.Height < MaxWindowHeight)
                {
                    tpMWH = Math.Min(this._currentScreen.Bounds.Height - 100, DefaultWindowHeight);
                }
                else
                {
                    tpMWH = Math.Min(this._currentScreen.Bounds.Height - 100, MiniWindowHeight);
                }
                //this.WindowHeight = DefaultWindowHeight;
                this.WindowHeight = tpMWH;

                this.Width = this.WindowWidth;
                this.Height = this.WindowHeight;
                this.GridContentHeight = DefaultGridContentHeight;               

                //this.GridTitleHeight = 40;
                //this.GridContentHeight = 200;
                //this.GridButtonHeight = 60;
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 按下按鈕 Yes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ExpectedInfo"></exception>
        private void DoButtonYes(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                this._result = MessageBoxWindowResult.Yes;
                this.DialogResult = true;
                this.Close();
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 按下按鈕 No
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ExpectedInfo"></exception>
        private void DoButtonNo(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                this._result = MessageBoxWindowResult.No;
                this.DialogResult = true;
                this.Close();
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 按下按鈕 Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ExpectedInfo"></exception>
        private void DoButtonClose(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                this._result = MessageBoxWindowResult.Close;
                this.DialogResult = true;
                this.Close();
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 視窗呈現
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ExpectedInfo"></exception>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (this._messageBoxWindowShowType)
                {
                    case MessageBoxWindowShowType.Info:
                        this.TypeImageSourcePath = MessageInfoImagePath;
                        this.GridTitleColor = new SolidColorBrush(Color.FromScRgb(1.0f, 50 / 255f, 220 / 255f, 255 / 255f));
                        break;
                    case MessageBoxWindowShowType.Error:
                        this.GridTitleColor = new SolidColorBrush(Color.FromScRgb(1.0f, 222 / 255f, 10 / 255f, 10 / 255f));
                        this.TypeImageSourcePath = MessageErrorImagePath;
                        break;
                    case MessageBoxWindowShowType.Alarm:
                        this.GridTitleColor = new SolidColorBrush(Color.FromScRgb(1.0f, 255 / 255f, 100 / 255f, 20 / 255f));
                        this.TypeImageSourcePath = MessageAlarmImagePath;
                        break;
                    case MessageBoxWindowShowType.Pass:
                        this.GridTitleColor = new SolidColorBrush(Color.FromScRgb(1.0f, 20 / 255f, 220 / 255f, 20 / 255f));
                        this.TypeImageSourcePath = MessagePassImagePath;
                        break;
                    default:
                        throw new ExpectedInfo($@"Please check MessageBoxWindowShowType, Unknow:[{this._messageBoxWindowShowType.ToString()}]", Code.FCT_004);
                        //break;
                }

                switch (this._messageBoxWindowButtonType)
                {
                    case MessageBoxWindowButtonType.Yes:
                        this.Button_No.Visibility = Visibility.Collapsed;
                        this.Button_Close.Visibility = Visibility.Collapsed;
                        break;
                    case MessageBoxWindowButtonType.YesNo:
                        this.Button_Close.Visibility = Visibility.Collapsed;
                        break;
                    case MessageBoxWindowButtonType.YesNoClose:
                        break;
                    case MessageBoxWindowButtonType.No:
                        this.Button_Yes.Visibility = Visibility.Collapsed;
                        this.Button_Close.Visibility = Visibility.Collapsed;
                        break;
                    case MessageBoxWindowButtonType.NoClose:
                        this.Button_Yes.Visibility = Visibility.Collapsed;
                        break;
                    case MessageBoxWindowButtonType.Close:
                        this.Button_Yes.Visibility = Visibility.Collapsed;
                        this.Button_No.Visibility = Visibility.Collapsed;
                        break;
                    default:
                        throw new ExpectedInfo($@"Please check MessageBoxWindowButtonType, Unknow:[{this._messageBoxWindowButtonType.ToString()}]", Code.FCT_004);
                        //break;
                }

                if (this.ScrollViewer_Content == null || this.TextBox_Content == null)
                {
                }
                else
                {
                    // 強制測量 TextBlock 的所需大小, PositiveInfinity(在高度方向自由展開), ActualWidth(目前顯示的寬度)
                    this.TextBox_Content.Measure(new Size(this.TextBox_Content.ActualWidth, double.PositiveInfinity));
                    //this.TextBlock_Content.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    double requiredTextHeight = this.TextBox_Content.DesiredSize.Height;
                    
                    // 取得 ScrollViewer 的顯示高度
                    double scrollViewHeight = this.ScrollViewer_Content.ViewportHeight;

                    // 如果內容高度超過可視區域，則增加視窗高度
                    if (requiredTextHeight > scrollViewHeight)
                    {

                        double tpMWH = 0;
                        if (this._currentScreen.Bounds.Height < MaxWindowHeight)
                        {
                            tpMWH = Math.Max(this._currentScreen.Bounds.Height - DiffWindowsHeight, this.WindowHeight);
                        }
                        else
                        {
                            tpMWH = Math.Max(this._currentScreen.Bounds.Height - DiffWindowsHeight, MaxWindowHeight);
                        }
                        
                        var tpNH = this.WindowHeight + (requiredTextHeight - scrollViewHeight);
                        // 限制最大增加高度
                        //double extraHeight = Math.Min(requiredTextHeight - scrollViewHeight + AddTextHeight, tpMWH);
                        double extraHeight = Math.Min(tpNH, tpMWH);

                        // 確定比原本的尺寸高
                        if (this.WindowHeight < extraHeight)
                        {
                            var tpDHeight = extraHeight - this.WindowHeight;
                            this.WindowHeight = extraHeight;
                            this.Height = this.WindowHeight;
                            this.GridContentHeight += tpDHeight;
                        }
                    }
                }
                
                this.SetScreenToCenter();
                this.Topmost = true;
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        ///// <summary>
        ///// 尺寸變化
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void TextBlockContent_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    try
        //    {
        //        double lineHeight = 22; // 根據 FontSize=16 大約估算
        //        int maxLines = 12;

        //        // 計算文字區域的行數
        //        int estimatedLines = (int)(TextBlock_Content.ActualHeight / lineHeight);

        //        if (estimatedLines > maxLines)
        //        {
        //            ///放大視窗
        //            ///
        //            // 取得目前 WPF 視窗的 Handle
        //            var window = System.Windows.Application.Current.MainWindow;
        //            var windowInteropHelper = new System.Windows.Interop.WindowInteropHelper(window);
        //            IntPtr handle = windowInteropHelper.Handle;

        //            // 找到目前視窗所在的 Screen
        //            Screen currentScreen = Screen.FromHandle(handle);

        //            // 取得螢幕解析度
        //            int screenWidth = currentScreen.Bounds.Width;
        //            int screenHeight = currentScreen.Bounds.Height;

        //            if (screenWidth < MaxWindowWidth)
        //            {
        //                this.WindowWidth = screenWidth;
        //            }
        //            else
        //            {
        //                this.WindowWidth = MaxWindowWidth;
        //            }

        //            if (screenHeight < MaxWindowHeight)
        //            {
        //                this.WindowHeight = screenHeight;
        //            }
        //            else
        //            {
        //                this.WindowHeight = MaxWindowHeight;
        //            }
        //            this.GridTitleHeight = 60;
        //            this.GridButtonHeight = 80;
        //            this.GridContentHeight = this.WindowHeight - this.GridTitleHeight - this.GridButtonHeight;                    
        //        }
        //        else
        //        {
        //            //預設大小
        //            this.WindowWidth = DefaultWindowWidth;
        //            this.WindowHeight = DefaultWindowHeight;

        //            this.GridTitleHeight = 60;
        //            this.GridContentHeight = 160;
        //            this.GridButtonHeight = 80;
        //        }
        //    }
        //    catch (ExpectedInfo ex)
        //    {
        //        throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
        //    }
        //    finally
        //    {
        //    }
        //}

        #region Function
        /// <summary>
        /// 取的目前 Screen
        /// </summary>
        /// <returns></returns>
        public Screen GetCurrentScreen()
        {
            try
            {
                WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);
                IntPtr handle = windowInteropHelper.Handle;

                // 找到目前視窗所在的 Screen
                Screen currentScreen = Screen.FromHandle(handle);
                return currentScreen;
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }            
        }

        /// <summary>
        /// 設定畫面在中央
        /// </summary>
        public void SetScreenToCenter()
        {
            try
            {
                var windowWidth = this.Width;
                var windowHeight = this.Height;

                // 設定視窗位置，使其顯示在螢幕中央
                this.Left = (this._currentScreen.Bounds.Width - windowWidth) / 2;
                this.Top = (this._currentScreen.Bounds.Height - windowHeight) / 2;
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{HolyGift.Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }           
        }
        #endregion
    }

    #region OtherClass
    /// <summary>
    /// 視窗要呈現的樣式
    /// </summary>
    public enum MessageBoxWindowShowType
    {
        /// <summary>
        /// 資訊.
        /// Information
        /// </summary>
        Info,

        /// <summary>
        /// 錯誤.
        /// Information
        /// </summary>
        Error,

        /// <summary>
        /// 警報.
        /// Information
        /// </summary>
        Alarm,

        /// <summary>
        /// 完成.
        /// Information
        /// </summary>
        Pass,
    }

    /// <summary>
    /// 視窗按鈕清單
    /// </summary>
    public enum MessageBoxWindowButtonType
    {
        /// <summary>
        /// Yes.
        /// Information
        /// </summary>
        Yes,

        /// <summary>
        /// YesNo.
        /// Information
        /// </summary>
        YesNo,

        /// <summary>
        /// YesNoClose.
        /// Information
        /// </summary>
        YesNoClose,

        /// <summary>
        /// No.
        /// Information
        /// </summary>
        No,

        /// <summary>
        /// NoClose.
        /// Information
        /// </summary>
        NoClose,

        /// <summary>
        /// Close.
        /// Information
        /// </summary>
        Close,
    }

    /// <summary>
    /// 結果
    /// </summary>
    public enum MessageBoxWindowResult
    {
        /// <summary>
        /// Yes.
        /// Information
        /// </summary>
        Yes,

        /// <summary>
        /// No.
        /// Information
        /// </summary>
        No,

        /// <summary>
        /// Close.
        /// Information
        /// </summary>
        Close,
    }

    #endregion    
}
