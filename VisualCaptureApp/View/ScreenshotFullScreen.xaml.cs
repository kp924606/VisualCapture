using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisualCaptureApp.AP;
using VisualCaptureApp.Base;

namespace VisualCaptureApp.View
{
    /// <summary>
    /// ScreenshotFullScreen.xaml 的互動邏輯
    /// </summary>
    public partial class ScreenshotFullScreen : Page
    {
        #region Property
        /// <summary>
        /// 預設Page高度
        /// </summary>
        public double PageHeight
        {
            get => GenerallySize.GeneralViewHeight;
            set
            {
                OnPropertyChanged(nameof(this.PageHeight));
            }
        }

        /// <summary>
        /// 預設Page高度
        /// </summary>
        public double PageWidth
        {
            get => GenerallySize.PageWidth;
            set
            {
                OnPropertyChanged(nameof(this.PageWidth));
            }
        }

        /// <summary>
        /// 視窗置頂
        /// </summary>
        public bool IsAnimationEffects
        {
            get
            {
                if (MainWindow.BaseSh == null || MainWindow.BaseSh!.FSFS == null)
                {
                    return false;
                }
                else
                {
                    return MainWindow.BaseSh!.FSFS!.IsAnimationEffects;
                }
            }
            set
            {
                if (MainWindow.BaseSh == null || MainWindow.BaseSh!.FSFS == null)
                {
                    MainWindow.BaseSh = new BaseScreenshot();
                    MainWindow.BaseSh!.FSFS!.IsAnimationEffects = false;
                }
                else
                {
                    MainWindow.BaseSh!.FSFS!.IsAnimationEffects = value;
                }
                OnPropertyChanged(nameof(this.IsAnimationEffects));
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


        public ScreenshotFullScreen()
        {
            InitializeComponent();
            //this.DataContext = MainWindow.BaseSh;
            this.DataContext = this;
        }

        public void PageOnLoad(object sender, RoutedEventArgs e)
        {
        }
    }
}
