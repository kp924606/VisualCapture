using ILogger.AP;
using ILogger.Enum;
using ILogger.Interface;
using Judgment;
using OLogger.AP;
using System.Buffers.Text;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualCaptureApp.AP;
using VisualCaptureApp.Base;
using VisualCaptureApp.View;

namespace VisualCaptureApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Static

        public static BaseScreenshot? BaseSh { get; set; }

        #endregion

        #region Property
        //宣告輸出用的 Log 資訊物件
        OLogInfo? ologinfo = null;
        SemaphoreSlim semaphoreLogInfo = new SemaphoreSlim(1); // 限制最多執行緒同時執行

        /// <summary>
        /// 記錄當前細節畫面狀態
        /// </summary>
        private bool isExpandedPanel = false;

        /// <summary>
        /// 預設視窗高度
        /// </summary>
        public double WindowHeight
        {
            get => GenerallySize.WindowHeight;
            set
            {
                OnPropertyChanged(nameof(this.WindowHeight));
            }
        }

        /// <summary>
        /// 預設視窗寬度
        /// </summary>
        public double WindowWidth
        {
            get => GenerallySize.WindowWidth;
            set
            {
                OnPropertyChanged(nameof(this.WindowWidth));
            }
        }

        /// <summary>
        /// 功能按鈕的寬度
        /// </summary>
        public double FunctionButtonWidth
        {
            get => GenerallySize.FunctionButtonWidth;
            set
            {
                OnPropertyChanged(nameof(this.FunctionButtonWidth));
            }
        }

        public double ComboxCaptureFunctionWidth
        {
            get => GenerallySize.ComboxCaptureFunctionWidth;
            set
            {
                OnPropertyChanged(nameof(this.ComboxCaptureFunctionWidth));
            }
        }

        /// <summary>
        /// 功能按鈕的高度
        /// </summary>
        public double FunctionButtonHeight
        {
            get => GenerallySize.FunctionButtonHeight;
            set
            {
                OnPropertyChanged(nameof(this.FunctionButtonHeight));
            }
        }

        /// <summary>
        /// 通用視窗高度
        /// </summary>
        public double GeneralViewHeight
        {
            get => GenerallySize.GeneralViewHeight;
            set
            {
                OnPropertyChanged(nameof(this.GeneralViewHeight));
            }
        }

        /// <summary>
        /// 詳細視窗高度
        /// </summary>
        public double DetailViewHeight
        {
            get => GenerallySize.DetailViewHeight;
            set
            {
                OnPropertyChanged(nameof(this.DetailViewHeight));
            }
        }

        /// <summary>
        /// 拍照功能 Combox 寬度大小
        /// </summary>
        public double ScreenshotComboxWidth
        {
            get => GenerallySize.ScreenshotComboxWidth;
            set
            {
                OnPropertyChanged(nameof(this.ScreenshotComboxWidth));
            }
        }

        /// <summary>
        /// 隱藏和關閉按鈕總寬度大小
        /// </summary>       
        public double HideACloseTotalWidth
        {
            get => GenerallySize.HideACloseTotalWidth;
            set
            {
                OnPropertyChanged(nameof(this.HideACloseTotalWidth));
            }
        }

        /// <summary>
        /// 預設功能圖片
        /// </summary>
        private string? _defaultFutrueImageSourcePath { set; get; }

        public string DefaultFutrueImageSourcePath
        {
            get
            {
                return this._defaultFutrueImageSourcePath!;
            }
            set
            {
                this._defaultFutrueImageSourcePath = value;
                OnPropertyChanged(nameof(this.DefaultFutrueImageSourcePath));
            }
        }

        /// <summary>
        /// 下拉按鈕圖片
        /// </summary>
        private string? _pullButtonImageSourcePath;

        public string PullButtonImageSourcePath
        {
            get
            {
                return this._pullButtonImageSourcePath!;
            }
            set
            {
                this._pullButtonImageSourcePath = value;
                OnPropertyChanged(nameof(this.PullButtonImageSourcePath));
            }
        }

        /// <summary>
        /// General下拉按鈕圖片
        /// </summary>
        private string? _generalPullButtonImageSourcePath;

        public string GeneralPullButtonImageSourcePath
        {
            get
            {
                return this._generalPullButtonImageSourcePath!;
            }
            set
            {
                this._generalPullButtonImageSourcePath = value;
                OnPropertyChanged(nameof(this.GeneralPullButtonImageSourcePath));
            }
        }

        /// <summary>
        /// 隱藏視窗圖片
        /// </summary>        
        public string HideImageSourcePath
        {
            get => BaseScreenshot.HideImageSourcePath;
        }

        /// <summary>
        /// 關閉視窗圖片
        /// </summary>
        public string CloseButtonImageSourcePath
        {
            get => BaseScreenshot.CloseImageSourcePath;
        }

        /// <summary>
        /// 事件觸發
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 正在執行指定截圖功能
        /// </summary>
        private bool isDoScreenShotFunctioning = false;


        /// <summary>
        /// TextBox按鈕的高度
        /// </summary>
        public double TextButtonHeight
        {
            get => GenerallySize.TextButtonHeight;
            set
            {
                OnPropertyChanged(nameof(this.TextButtonHeight));
            }
        }

        /// <summary>
        /// 預設的儲存路徑
        /// </summary>
        public string? _defaultSaveFolderPath { set; get; }

        /// <summary>
        /// 預設的儲存路徑
        /// </summary>
        public string DefaultSaveFolderPath
        {
            get
            {
                return this._defaultSaveFolderPath!;
            }
            set
            {
                this._defaultSaveFolderPath = value;
                OnPropertyChanged(nameof(this.DefaultSaveFolderPath));
            }
        }

        /// <summary>
        /// 功能按鈕的寬度
        /// </summary>
        public double FunctionSamllButtonWidth
        {
            get => GenerallySize.FunctionSamllButtonWidth;
            set
            {
                OnPropertyChanged(nameof(this.FunctionSamllButtonWidth));
            }
        }

        /// <summary>
        /// 功能按鈕的高度
        /// </summary>
        public double FunctionSamllButtonHeight
        {
            get => GenerallySize.FunctionSamllButtonHeight;
            set
            {
                OnPropertyChanged(nameof(this.FunctionSamllButtonHeight));
            }
        }

        /// <summary>
        /// 資料夾選擇圖片
        /// </summary>
        public string FolderSelectImageSourcePath
        {
            get => BaseScreenshot.FolderSelectImageSourcePath;
        }

        /// <summary>
        /// 資料夾開啟圖片
        /// </summary>
        public string FolderOpenImageSourcePath
        {
            get => BaseScreenshot.FolderOpenImageSourcePath;
        }


        /// <summary>
        /// 視窗置頂
        /// </summary>
        public bool IsWindowTopMost
        {
            get
            {
                return BaseSh!.IsWindowTopMost;
            }
            set
            {
                BaseSh!.IsWindowTopMost = value;
                OnPropertyChanged(nameof(this.IsWindowTopMost));
            }
        }
        
        /// <summary>
        /// 使用快捷鍵拍照
        /// </summary>
        public bool IsUseShortcutkeyScreenshot
        {
            get
            {
                return BaseSh!.IsUseShortcutkeyScreenshot;
            }
            set
            {
                BaseSh!.IsUseShortcutkeyScreenshot = value;
                OnPropertyChanged(nameof(this.IsUseShortcutkeyScreenshot));
            }
        }

        public double DefaultComboxInputWidth
        {
            get => GenerallySize.DefaultComboxInputWidth;
            set
            {
                OnPropertyChanged(nameof(this.DefaultComboxInputWidth));
            }
        }

        #endregion

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                // 產出 Log 物件設置 
                ologinfo = new OLogInfo(Assembly.GetExecutingAssembly().GetName().Name, 30, 3, 777);

                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"
        /***********************************************
                          _ooOoo_
                         o8888888o
                         88"" . ""88
                         (| -_- |)
                         O\  =  /O
                      ____/`---'\____
                    .'  \\|     |//  `.
                   /  \\|||  :  |||//  \
                  /  _||||| -:- |||||-  \
                  |   | \\\  -  /// |   |
                  | \_|  ''\---/''  |   |
                  \  .-\__  `-`  ___/-. /
                ___`. .'  /--.--\  `. . __
             ."""" '<  `.___\_<|>_/___.'  >'"""".
            | | :  `- \`.;`\ _ /`;.`/ - ` : | |
            \  \ `-.   \_ __\ /__ _/   .-` /  /
       ======`-.____`-.___\_____/___.-`____.-'======
                          `=---='
       ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                佛祖保佑       永無BUG
       *********************************************/
", Code.IFO_000));

                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, @"
    /**************************************************************

      [       遵從自然規律，讓萬物自行發揮       ]  <-----------
                            |                                   |
                            |                                   |
      [        遵從自然規律，讓萬物自行相剋      ]              |
                            |                                   |
                            |                                   |
      [          精通萬物一切原理熟知於心        ]              |
                            |                                   |
                            |                                   |
      [          運用萬物一切原理強身健體        ]              |
                            |                                   |
                            |                                   |
      [  緣起緣滅，讓萬物一切自動運轉，自生自滅  ]              |
                            |                                   |
                            |                                   |
      [        時有時無，萬物一切可有可無        ]              |
                            |                                   |
                            |                                   |
      [             天人合一，無欲無求           ]              |
                            |                                   |
                            |                                   |
      [              萬物一切歸為無              ]              |
                            |                                   |
                            |                                   |
      [           心有所想，創建美妙世界         ] --------------

    ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
              如何成為長官的秘訣            劉彩萍指揮官
    ***************************************************************/
            ", Code.IFO_000));

                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"Start...", Code.IFO_000));

                BaseScreenshot.Communication = Communication;

                // 設定預設圖片
                this.PullButtonImageSourcePath = BaseScreenshot.PullImageSourcePath;

                // 設定儲存路徑圖片
                //var tpsp = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseScreenshot.DefaultSaveFolderPath);
                this._defaultSaveFolderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BaseScreenshot.DefaultSaveFolderPath);
                if (!Directory.Exists(this._defaultSaveFolderPath))
                {
                    Directory.CreateDirectory(this._defaultSaveFolderPath);
                }

                BaseSh = new BaseScreenshot();
                
                this.DataContext = this; // 設定 DataContext，讓 XAML 可以綁定變數                       
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
                SpinWait.SpinUntil(() => false, 1000);
            }
        }

        /// <summary>
        /// 畫面載入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PageOnLoad(object sender, RoutedEventArgs e)
        {
            try
            {                
                ///設定畫面在中央
                var screenWidth = SystemParameters.PrimaryScreenWidth;
                var screenHeight = SystemParameters.PrimaryScreenHeight;
                var windowWidth = this.WindowWidth;
                var windowHeight = this.WindowHeight;

                // 設定視窗位置，使其顯示在螢幕中央
                this.Left = (screenWidth - windowWidth) / 2;
                this.Top = (screenHeight - windowHeight) / 2;

                if (BaseSh!.BaseCaptureFunctionL != null || BaseSh.BaseCaptureFunctionL!.Count >= 0)
                {
                    //預設呈現第一個拍照功能
                    this.ComboxCaptureFunction.ItemsSource = BaseSh.BaseCaptureFunctionL;
                    this.ComboxCaptureFunction.SelectedIndex = 0;

                    //預設拍照功能是全螢幕截圖
                    this._defaultFutrueImageSourcePath = BaseSh!.BaseCaptureFunctionL!.FirstOrDefault()!.ImagePath;

                    // 詳細視窗, 預設載入第一個功能
                    var tpDViewName = BaseSh.BaseCaptureFunctionL!.FirstOrDefault()!.Name;
                    DetailFrame.Navigate(new Uri($@"View\{tpDViewName}.xaml", UriKind.Relative));                    
                }
                else
                {
                    this.Button_Deatail.IsEnabled = false;
                    this.Button_ScreenShotFunction.IsEnabled = false;
                    this.ComboxCaptureFunction.IsEnabled = false;                    

                    throw new ExpectedInfo($@"Please check BaseScreenshot BaseCaptureFunctionL, List is Null", Code.ODI_004);
                    //777 Add Page 呈現錯誤資訊
                }

                if (BaseSh!.BaseKeyboardShortcutL != null || BaseSh.BaseKeyboardShortcutL!.Count >= 0)
                {
                    //預設清單鍵盤按鍵是第一個
                    this.ComboxKeyboardShortcutStartEnd.ItemsSource = BaseSh.BaseKeyboardShortcutL;
                    this.ComboxKeyboardShortcutStartEnd.SelectedIndex = 0;
                    //預設快捷按鍵是第一個
                    //BaseSh.BaseWatchKeyboard!.SpecifyKeyShortcut = BaseSh.BaseKeyboardShortcutL.FirstOrDefault()!;
                    // 使用快捷鍵拍照
                    this.CheckBox_UseShortcutkeyScreenshot.IsChecked = true;                    
                }
                else
                {                   
                    throw new ExpectedInfo($@"Please check BaseScreenshot BaseKeyboardShortcutL, List is Null", Code.ODI_004);
                    //777 Add Page 呈現錯誤資訊
                }
                
                //執行指定快捷鍵動作
                BaseSh!.DoKeyboardWatchEvent = DoKeyboardWatchEvent;

            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        /// <summary>
        /// 執行快捷鍵動作
        /// </summary>
        /// <param name="bks"></param>
        public void DoKeyboardWatchEvent(BaseKeyboardShortcut bks)
        {
            try
            {
                this.DoScreenShotFunction(null!,null!);
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        /// <summary>
        /// 當滑鼠按下時開始拖曳視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // 這行會讓視窗跟隨滑鼠移動
                this.DragMove();
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        private void DoExpandedPanel(object sender, RoutedEventArgs e)
        {
            try
            {
                double targetHeight = this.isExpandedPanel ? GenerallySize.WindowHeight : GenerallySize.ExpandedWindowHeight;

                // 創建動畫
                DoubleAnimation heightAnimation = new DoubleAnimation
                {
                    To = targetHeight,
                    Duration = TimeSpan.FromSeconds(GenerallySize.AnimationDuration),
                    EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut } // 絲滑動畫
                };

                // 執行動畫
                this.BeginAnimation(Window.HeightProperty, heightAnimation);

                // 設定 UI 顯示/隱藏
                if (!this.isExpandedPanel)
                {
                    GeneralPanel.Visibility = Visibility.Visible;
                    SettingsPanel.Visibility = Visibility.Visible;
                    this.isExpandedPanel = true;
                    this.PullButtonImageSourcePath = BaseScreenshot.PushImageSourcePath;
                }
                else
                {
                    // 延遲隱藏，確保動畫結束後才消失
                    var hideTimer = new System.Timers.Timer(GenerallySize.AnimationDuration * 1000);
                    hideTimer.Elapsed += (s, args) =>
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            GeneralPanel.Visibility = Visibility.Collapsed;
                            SettingsPanel.Visibility = Visibility.Collapsed;
                        });
                        hideTimer.Dispose();
                    };
                    hideTimer.Start();
                    this.isExpandedPanel = false;
                    this.PullButtonImageSourcePath = BaseScreenshot.PullImageSourcePath;
                }
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }
        private void ComboxCaptureFunction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int selectedIndex = this.ComboxCaptureFunction.SelectedIndex;
                this.DefaultFutrueImageSourcePath = BaseSh!.BaseCaptureFunctionL![selectedIndex].ImagePath!;
                var tpDViewName = BaseSh.BaseCaptureFunctionL[selectedIndex].Name;
                DetailFrame.Navigate(new Uri($@"View\{tpDViewName}.xaml", UriKind.Relative));
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }            
        }

        private void HideWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                // 隱藏窗口並顯示在托盤
                this.Hide();
                MyNotifyIcon.Visibility = Visibility.Visible;
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        private void NotifyIcon_DoubleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                // 雙擊托盤圖示顯示窗口
                this.Show();
                this.WindowState = WindowState.Normal;
                this.Activate(); // 確保視窗在最前面
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }            
        }

        /// <summary>
        /// 關閉視窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                //var windowBW = new MessageBoxWindow("確認關閉", "確定要關閉程式嗎?", MessageBoxWindowShowType.Info, MessageBoxWindowButtonType.YesNo);

                var windowBW = new MessageBoxWindow("確認關閉", "確定要關閉程式嗎?", MessageBoxWindowShowType.Info, MessageBoxWindowButtonType.YesNo);
                //var windowBW = new MessageBoxWindow("確認關閉", "確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? 確定要關閉程式嗎? ", MessageBoxWindowShowType.Info, MessageBoxWindowButtonType.YesNo);

                bool? result = windowBW.ShowDialog(); // 以對話框模式開啟

                if (result == true && windowBW.Result == MessageBoxWindowResult.Yes) // 只有當 B 視窗 `DialogResult = true` 時才執行
                {
                    GoodbyeScreen goodbyeScreen = new GoodbyeScreen();
                    this.Close();
                    goodbyeScreen.Show();
                }
                else
                {

                }
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        /// <summary>
        /// 執行指定拍照功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoScreenShotFunction(object sender, RoutedEventArgs e)
        {
            try
            {
                // 拍攝還在做
                if (this.isDoScreenShotFunctioning)
                {
                    //777 錄影動作停止
                }
                //開始執行
                else
                {
                    this.isDoScreenShotFunctioning = true;
                    this.Button_ScreenShotFunction.Background = new SolidColorBrush(Colors.Green);

                    // 先關閉其他元件, 避免被觸發
                    DisOrEnableAllControls(false);

                    //執行指定拍照動作
                    BaseSh!.FunctionIndex = this.ComboxCaptureFunction.SelectedIndex;
                    BaseSh!.SaveFolder = this.DefaultSaveFolderPath;

                    //777 可依照使用者設置作操作
                    BaseSh!.HideWindow = BaseSh!.BaseCaptureFunctionL![this.ComboxCaptureFunction.SelectedIndex].HideWindow;

                    var tpisH = false;
                    if (BaseSh!.HideWindow && this.Visibility == Visibility.Visible)
                    {                            
                        this.Visibility = Visibility.Hidden;
                        this.Hide();
                        tpisH = true;
                    }
                    BaseSh!.Start();

                    if (tpisH)
                    {
                        this.Visibility = Visibility.Visible;
                        this.Show();
                    }

                    //做單次,做完了
                    if (BaseSh!.IsDoOnce)
                    {
                        this.isDoScreenShotFunctioning = false;
                        this.Button_ScreenShotFunction.Background = new SolidColorBrush(Colors.Yellow);
                    }
                    else
                    {                            
                    }                        
                }
                
                //BaseSh!.Name = @"666";
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
                // 開啟其他元件, 避免被觸發
                DisOrEnableAllControls(true);
            }
        }

        /// <summary>
        /// 關閉或開啟所有元件
        /// </summary>
        /// <param name="isEnabled"></param>
        private void DisOrEnableAllControls(bool isEnabled)
        {
            try
            {
                this.MainGrid.IsEnabled = isEnabled;
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }           
        }

        /// <summary>
        /// 選擇要儲存的資料夾路徑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderBrowserDialogWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "請選擇資料夾";
                    folderDialog.ShowNewFolderButton = true; // 是否允許創建新資料夾
                    folderDialog.InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.DefaultSaveFolderPath);
                    
                    if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.DefaultSaveFolderPath = folderDialog.SelectedPath;
                        //MessageBox.Show($"選擇的資料夾:{folderDialog.SelectedPath}");

                        Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"選擇儲存的資料夾:[{folderDialog.SelectedPath}]", Code.IFO_000));
                    }
                    else
                    { 
                    
                    }
                }
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        /// <summary>
        /// 開啟儲存的資料夾畫面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderOpenWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Directory.Exists(this.DefaultSaveFolderPath))
                {
                    System.Diagnostics.Process.Start($@"explorer.exe", this.DefaultSaveFolderPath);
                    Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"開啟儲存的資料夾:[{this.DefaultSaveFolderPath}]", Code.IFO_000));
                }
                else
                {
                    throw new ExpectedInfo($@"Please check SaveFolderPath, Don't Exist:[{this.DefaultSaveFolderPath}]", Code.ODI_008);
                }
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        private void WindowTopMost(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Topmost = this.IsWindowTopMost;
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"Setting:[{this.IsWindowTopMost}]", Code.IFO_000));
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        private void DoEnableWatchKeyboard(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (this.CheckBox_UseShortcutkeyScreenshot.IsChecked == true)
                {
                    BaseSh!.BaseWatchKeyboard!.RunWatch(BaseSh.BaseKeyboardShortcutL![this.ComboxKeyboardShortcutStartEnd.SelectedIndex]);
                    this.ComboxKeyboardShortcutStartEnd.IsEnabled = false;
                    Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"RunWatch, Code:[{BaseSh!.BaseWatchKeyboard!.SpecifyKeyShortcut!.Code}], Description:[{BaseSh!.BaseWatchKeyboard!.SpecifyKeyShortcut.Description}], IsModifiersHasFlag:[{BaseSh!.BaseWatchKeyboard!.SpecifyKeyShortcut.IsModifiersHasFlag}], CM:[{BaseSh!.BaseWatchKeyboard!.SpecifyKeyShortcut.CM.ToString()}]", Code.IFO_000));
                }
                else
                {
                    BaseSh!.BaseWatchKeyboard!.CloseWatch();
                    this.ComboxKeyboardShortcutStartEnd.IsEnabled = true;
                    Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"CloseWatch", Code.IFO_000));
                }
            }
            catch (ExpectedInfo ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                Communication(new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, HolyGift.Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        /// 溝通(可用來記錄 Log)
        public async void Communication(ILogInfo li)
        {
            //取得一個執行權限
            await semaphoreLogInfo.WaitAsync();

            try
            {
                await Task.Run(() =>
                {
                    try
                    {
                        switch (li.Type)
                        {
                            case ILogType.Info:
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.WriteLine($@"{DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss")}<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}] {li.Info}");
                                ologinfo!.Logger.Info($@"<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}] {li.Info}");
                                break;
                            case ILogType.Alarm:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.WriteLine($@"{DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss")}<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}] {li.Info}");
                                ologinfo!.Logger.Info($@"<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}] {li.Info}");
                                break;
                            case ILogType.Error:
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.WriteLine($@"{DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss")}<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}][{li.Info}] {li.Error}");
                                ologinfo!.Logger.Info($@"<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}][{li.Info}] {li.Error}");
                                break;
                            case ILogType.Catch:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.WriteLine($@"{DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss")}<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}][{li.Info}] {li.Error}");
                                ologinfo!.Logger.Info($@"<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}][{li.Info}] {li.Error}");
                                break;
                            case ILogType.Fail:
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.WriteLine($@"{DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss")}<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}][{li.Info}] {li.Error}");
                                ologinfo!.Logger.Info($@"<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}][{li.Info}] {li.Error}");
                                break;
                            case ILogType.Pass:
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.WriteLine($@"{DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss")}<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}] {li.Info}");
                                ologinfo!.Logger.Info($@"<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}] {li.Info}");
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine($@"{DateTime.Now.ToString(@"yyyy/MM/dd HH:mm:ss")}<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}][{li.Info}] {li.Error}");
                                ologinfo!.Logger.Info($@"<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}][{li.Info}] {li.Error}");
                                break;
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                    }
                });
            }
            catch
            {
            }
            finally
            {
                //釋放執行權限資源
                semaphoreLogInfo.Release();
            }
        }

    }
}