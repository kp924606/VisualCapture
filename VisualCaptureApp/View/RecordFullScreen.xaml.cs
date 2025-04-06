using ILogger.AP;
using ILogger.Enum;
using Judgment;
using ProjectLifeModuleManagement.Base;
using System;
using System.Buffers.Text;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisualCaptureApp.View
{
    /// <summary>
    /// RecordFullScreen.xaml 的互動邏輯
    /// </summary>
    public partial class RecordFullScreen : Page, INotifyPropertyChanged
    {
        #region Property

        ///// <summary>
        ///// 錄製時間
        ///// </summary>
        //private int _recordTime { set; get; }

        ///// <summary>
        ///// 錄製時間
        ///// </summary>
        //public int RecordTime
        //{
        //    get
        //    {
        //        return this._recordTime;
        //    }
        //    set
        //    {
        //        if (this._recordTime != value)
        //        {
        //            this._recordTime = value;
        //            OnPropertyChanged(nameof(this.RecordTime));
        //        }
        //    }
        //}

        /// <summary>
        /// 事件觸發
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public RecordFullScreen()
        {
            InitializeComponent();

            // 設定 DataContext，讓 XAML 可以綁定變數 
            //this.DataContext = this;
            this.DataContext = MainWindow.BaseSh!.BaseRecordFullScreen;
        }

        public void PageOnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                this.TextBlockFPS.Text = this.SliderFPS.Value.ToString();
                this.TextBlockVideoQuality.Text = this.SliderVideoQuality.Value.ToString();

                if (MainWindow.BaseSh!.BaseRecordFullScreen != null)
                {
                    MainWindow.BaseSh!.BaseRecordFullScreen!.FPS = (int)this.SliderFPS.Value;
                    MainWindow.BaseSh!.BaseRecordFullScreen!.VideoQuality = (int)this.SliderVideoQuality.Value;

                    if (MainWindow.BaseSh!.BaseRecordFullScreen!.AudioList != null || MainWindow.BaseSh!.BaseRecordFullScreen!.AudioList!.Count > 0)
                    {
                        this.ComboxAudio.ItemsSource = MainWindow.BaseSh!.BaseRecordFullScreen!.AudioList;
                        this.ComboxAudio.SelectedIndex = 0;
                    }                    
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

        /// <summary>
        /// FPS 變化時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderFPS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                this.TextBlockFPS.Text = this.SliderFPS.Value.ToString();
                if (MainWindow.BaseSh!.BaseRecordFullScreen != null)
                {
                    MainWindow.BaseSh!.BaseRecordFullScreen!.FPS = (int)this.SliderFPS.Value;
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

        /// <summary>
        /// 影像品質變化時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderVideoQuality_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                this.TextBlockVideoQuality.Text = this.SliderVideoQuality.Value.ToString();
                if (MainWindow.BaseSh!.BaseRecordFullScreen != null)
                {
                    MainWindow.BaseSh!.BaseRecordFullScreen!.VideoQuality = (int)this.SliderVideoQuality.Value;
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

        /// <summary>
        /// 選擇功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboxAudio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {                
                if (MainWindow.BaseSh!.BaseRecordFullScreen != null)
                {
                    int selectedIndex = this.ComboxAudio.SelectedIndex;
                    MainWindow.BaseSh!.BaseRecordFullScreen!.CurrentAudioIndex = selectedIndex;
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
