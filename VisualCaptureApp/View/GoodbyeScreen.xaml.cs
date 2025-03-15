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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VisualCaptureApp.AP;

namespace VisualCaptureApp.View
{
    /// <summary>
    /// GoodbyeScreen.xaml 啟動畫面的互動邏輯
    /// </summary>
    public partial class GoodbyeScreen : Window, INotifyPropertyChanged
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
        #endregion

        public GoodbyeScreen()
        {
            try
            {
                InitializeComponent();

                // 創建一個 DoubleAnimation
                DoubleAnimation progressAnimation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.1,
                    Duration = new Duration(TimeSpan.FromSeconds(1.5)),
                    EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseIn } // 絲滑動畫
                };

                // 設定動畫完成時的事件處理函式
                progressAnimation.Completed += ProgressAnimation_Completed!;

                // 執行動畫
                this.BeginAnimation(Window.OpacityProperty, progressAnimation);
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
        /// 動畫完成後觸發的事件處理函式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressAnimation_Completed(object sender, EventArgs e)
        {
            try
            {
                // 關閉
                this.Close();
                Application.Current.Shutdown();
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
    }
}
