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
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using VisualCaptureApp.AP;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;

namespace VisualCaptureApp.View
{
    /// <summary>
    /// SplashScreen.xaml 啟動畫面的互動邏輯
    /// </summary>
    public partial class SplashScreen : Window, INotifyPropertyChanged
    {
        #region Property
        /// <summary>
        /// 預設視窗寬度
        /// </summary>
        public double WindowWidth
        {
            get => GenerallySize.SplashScreenWindowWidth;
            set
            {
                OnPropertyChanged(nameof(this.WindowWidth));
            }
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
        /// 下一個要呈現的主要視窗
        /// </summary>
        private MainWindow? mainWindow { set; get; }

        /// <summary>
        /// 確認下一個要呈現的主要視窗初始化完成
        /// </summary>
        private bool _isMainWindowInitFinish { set; get; }

        private DispatcherTimer? timer { set; get; }
        #endregion

        public SplashScreen()
        {
            try
            {
                InitializeComponent();

                this._isMainWindowInitFinish = false;

                // 創建一個 DoubleAnimation
                DoubleAnimation progressAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = GenerallySize.SplashScreenWindowWidth,
                    Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                    EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseInOut } // 絲滑動畫
                };

                // 設定動畫完成時的事件處理函式
                progressAnimation.Completed += ProgressAnimation_Completed!;
                
                // 將動畫應用到進度條
                this.progressBarRectangle.BeginAnimation(Rectangle.WidthProperty, progressAnimation);

                // 這裡將 MainWindow 的創建操作調度回主執行緒
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    this.mainWindow = new MainWindow();
                    this._isMainWindowInitFinish = true;
                });
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
        /// 設定動畫完成時的事件處理函式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressAnimation_Completed(object sender, EventArgs e)
        {
            try
            {
                //設定檢查條件，使用 DispatcherTimer 每隔一段時間檢查
                this.timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(200) // 每 100 毫秒檢查一次
                };
                this.timer.Tick += (sender, e) => CheckAndStopAnimation();
                this.timer.Start();

                ///---v
                //確認還在初始化
                //while (!this._isMainWindowInitFinish)
                //{
                //    SpinWait.SpinUntil(() => false, 100);
                //}
                //// 顯示 MainWindow
                //this.mainWindow!.Show();
                //this.Close(); // 關閉目前視窗
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
        /// 確認完成並呈現下一個要呈現的主要視窗即關閉目前視窗
        /// </summary>
        private void CheckAndStopAnimation()
        {
            if (this._isMainWindowInitFinish && this.mainWindow != null)
            {
                // 停止檢查
                this.timer!.Stop();
                // 停止動畫
                //this.BeginAnimation(Window.OpacityProperty, null); // 停止動畫

                this.mainWindow!.Show();
                // 創建動畫
                DoubleAnimation fadeInAnimation = new DoubleAnimation
                {
                    From = 0.6,    // 起始透明度
                    To = 1.0,      // 目標透明度
                    Duration = new Duration(TimeSpan.FromSeconds(0.5)), // 動畫時間
                    EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut } // 動畫效果
                };
                this.mainWindow!.BeginAnimation(Window.OpacityProperty, fadeInAnimation);

                DoubleAnimation closeAnimation = new DoubleAnimation
                {
                    From = 1.0,    // 起始透明度
                    To = 0.0,      // 目標透明度
                    Duration = new Duration(TimeSpan.FromSeconds(0.5)), // 動畫時間
                    EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseIn } // 動畫效果
                };
                this.BeginAnimation(Window.OpacityProperty, closeAnimation);
                this.Hide();
                closeAnimation.Completed += (s, _) => this.Close(); // 關閉目前視窗

                //this.Close(); // 關閉目前視窗
            }
        }

    }
}
