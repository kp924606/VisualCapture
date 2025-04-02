using ILogger.AP;
using ILogger.Enum;
using Judgment;
using ProjectLifeModuleManagement.Base;
using System;
using System.Collections.Generic;
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
    public partial class RecordFullScreen : Page
    {
        public RecordFullScreen()
        {
            InitializeComponent();
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
    }
}
