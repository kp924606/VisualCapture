using ILogger.AP;
using Judgment;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Media;

namespace VisualCaptureApp.Function
{
    public class FScreenshotFullScreen
    {
        #region Properties

        /// <summary>
        /// 使用動畫效果
        /// </summary>
        private bool _isAnimationEffects;

        /// <summary>
        /// 使用動畫效果
        /// </summary>
        public bool IsAnimationEffects
        {
            get => this._isAnimationEffects;
            set
            {
                this._isAnimationEffects = value;
            }
        }
        #endregion

        #region Static

        /// <summary>
        /// 閃光動畫效果
        /// </summary>
        public static void TriggerFlashEffect()
        {
            try
            {
                // 計算所有螢幕範圍
                double minX = Screen.AllScreens.Min(s => s.Bounds.Left);
                double minY = Screen.AllScreens.Min(s => s.Bounds.Top);
                double maxX = Screen.AllScreens.Max(s => s.Bounds.Right);
                double maxY = Screen.AllScreens.Max(s => s.Bounds.Bottom);                
                System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(() =>
                {
                    // 創建全螢幕閃爍視窗
                    var flashWindow = new Window
                    {
                        WindowStyle = WindowStyle.None,
                        ResizeMode = ResizeMode.NoResize,
                        ShowInTaskbar = false,
                        Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(128, 255, 255, 255)),
                        //Background = System.Windows.Media.Brushes.Transparent,
                        AllowsTransparency = true, // 允許透明
                        Topmost = true,
                        Left = minX,
                        Top = minY,
                        Width = maxX - minX,
                        Height = maxY - minY,
                        Opacity = 0,
                    };

                    // 淡入淡出動畫
                    var fadeInOut = new DoubleAnimation
                    {
                        //1:完全可見
                        From = 0.3,
                        To = 0,
                        Duration = TimeSpan.FromMilliseconds(100), // 快速變亮
                        AutoReverse = false,
                        EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseIn } // 絲滑動畫
                    };

                    fadeInOut.Completed += (s, _) => flashWindow.Close(); // 動畫完成後關閉視窗

                    flashWindow.Loaded += (s, _) =>
                    {
                        var storyboard = new Storyboard();
                        storyboard.Children.Add(fadeInOut);
                        Storyboard.SetTarget(fadeInOut, flashWindow);
                        Storyboard.SetTargetProperty(fadeInOut, new PropertyPath(Window.OpacityProperty));
                        storyboard.Begin();
                    };

                    flashWindow.Show();
                }));

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
        #endregion
    }
}
