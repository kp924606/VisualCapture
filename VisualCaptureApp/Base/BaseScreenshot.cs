using ILogger.AP;
using Judgment;
using HolyGift;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisualCaptureApp.Interface;
using Key = HolyGift.Key;
using VisualCaptureApp.AP;
using ILogger.Interface;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using VisualCaptureApp.Function;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows;
using Application = System.Windows.Application;
using VisualCaptureApp.View;
using ProjectLifeModuleManagement.Base;
using System.Windows.Media.Media3D;

namespace VisualCaptureApp.Base
{
    public class BaseScreenshot : IScreenshot, INotifyPropertyChanged
    {
        #region Static
        public const string DefaultSaveFolderPath = @"Data";
        public const string PullImageSourcePath = "image/Pull.png";
        public const string PushImageSourcePath = "image/Push.png";
        public const string HideImageSourcePath = "image/Hide1.png";
        public const string CloseImageSourcePath = "image/Close-1.png";
        public const string FolderSelectImageSourcePath = "image/Folder1.png";
        public const string FolderOpenImageSourcePath = "image/Folder2.png";

        /// <summary>
        /// 生命, 個體的溝通方式
        /// </summary>
        public static Action<ILogInfo>? Communication { get; set; }
        #endregion

        #region Properties
        /// <summary>
        /// 功能項目
        /// </summary>
        private int _functionIndex;
        public int FunctionIndex
        {
            get => this._functionIndex;
            set
            {
                if (value <= 0)
                {
                    this._functionIndex = 0;
                }
                else
                {
                    this._functionIndex = value;
                }
                // 通知屬性已變更
                OnPropertyChanged(nameof(FunctionIndex));
            }
        }

        /// <summary>
        /// 做單次就完成
        /// </summary>
        private bool _isDoOnce;
        public bool IsDoOnce
        {
            get => this._isDoOnce;

            set 
            {
                this._isDoOnce = value;
            }
        }

        /// <summary>
        /// 名稱
        /// </summary>
        public string Name
        {
            get
            {
                if (this.BaseCaptureFunctionL == null || this.BaseCaptureFunctionL.Count <= 0)
                {
                    return BaseCaptureFunction.ScreenshotFullScreen;
                }
                else
                {
                    return this.BaseCaptureFunctionL[this.FunctionIndex].Name!;                    
                }
            }
        }

        /// <summary>
        /// 圖片路徑
        /// </summary>
        private string? imagePath { get; set; }

        /// <summary>
        /// 圖片路徑
        /// </summary>
        public string ImagePath 
        {
            get
            {
                if (this.BaseCaptureFunctionL == null || this.BaseCaptureFunctionL.Count <= 0)
                {
                    return BaseCaptureFunction.ScreenshotFullScreenImagPath;
                }
                else
                {
                    return this.BaseCaptureFunctionL.FirstOrDefault()!.ImagePath!;
                }
            }
        }

        /// <summary>
        /// 拍照功能清單
        /// </summary>
        public List<BaseCaptureFunction>? BaseCaptureFunctionL { get; set; }

        /// <summary>
        /// 鍵盤快捷鍵清單
        /// </summary>
        public List<BaseKeyboardShortcut>? BaseKeyboardShortcutL { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        private string? saveFolder { get; set; }

        /// <summary>
        /// 名稱
        /// </summary>
        public string SaveFolder
        {
            get { return this.saveFolder!; }
            set
            {
                if (this.saveFolder != value)
                {
                    this.saveFolder = value;
                    // 通知屬性已變更
                    OnPropertyChanged(nameof(SaveFolder));
                }
            }
        }

        /// <summary>
        /// 隱藏視窗
        /// </summary>
        private bool hideWindow { get; set; }

        /// <summary>
        /// 隱藏視窗
        /// </summary>
        public bool HideWindow
        {
            get { return this.hideWindow; }
            set
            {
                if (this.hideWindow != value)
                {
                    this.hideWindow = value;
                    // 通知屬性已變更
                    OnPropertyChanged(nameof(HideWindow));
                }
            }
        }

        /// <summary>
        /// 視窗置頂
        /// </summary>
        private bool _isWindowTopMost { get; set; }

        /// <summary>
        /// 視窗置頂
        /// </summary>
        public bool IsWindowTopMost
        {
            get { return this._isWindowTopMost; }
            set
            {
                this._isWindowTopMost = value;
                // 通知屬性已變更
                OnPropertyChanged(nameof(IsWindowTopMost));
            }
        }

        /// <summary>
        /// 使用快捷鍵拍照
        /// </summary>
        private bool _isUseShortcutkeyScreenshot { get; set; }

        /// <summary>
        /// 使用快捷鍵拍照
        /// </summary>
        public bool IsUseShortcutkeyScreenshot
        {
            get { return this._isUseShortcutkeyScreenshot; }
            set
            {
                this._isUseShortcutkeyScreenshot = value;
                // 通知屬性已變更
                OnPropertyChanged(nameof(IsUseShortcutkeyScreenshot));
            }
        }

        /// <summary>
        /// 正在執行DoKeyboardWatchEvent,避免快捷鍵同時按下
        /// </summary>
        private bool _isDoKeyboardWatchEvent { get; set; }

        /// <summary>
        /// 監控鍵盤快捷鍵
        /// </summary>
        public BaseWatchKeyboard? BaseWatchKeyboard { set; get; }

        /// <summary>
        /// 執行快捷鍵指定鍵盤動作
        /// </summary>
        public Action<BaseKeyboardShortcut>? DoKeyboardWatchEvent { set; get; }

        /// <summary>
        /// 閃光動畫
        /// </summary>
        public FScreenshotFullScreen? FSFS { set; get; }

        /// <summary>
        /// 全螢幕錄影
        /// </summary>
        public BaseRecordFullScreen? BaseRecordFullScreen { set; get; }

        /// <summary>
        /// Window 指定範圍
        /// </summary>
        private SpecifiedRange? _specifiedRange { set; get; }

        /// <summary>
        /// 基本指定範圍
        /// </summary>
        private BaseSpecifiedRange? _baseSpecifiedRange { set; get; }

        #endregion

        /**/
        #region Constructor
        public BaseScreenshot(string defaultSaveFolderPath)
        {
            try
            {
                this.BaseCaptureFunctionL = new List<BaseCaptureFunction>();
                this.BaseCaptureFunctionL.Add(new BaseCaptureFunction(BaseCaptureFunction.ScreenshotFullScreen, BaseCaptureFunction.ScreenshotFullScreenImagPath, true));
                this.BaseCaptureFunctionL.Add(new BaseCaptureFunction(BaseCaptureFunction.ScreenshotSpecifyRange, BaseCaptureFunction.ScreenshotSpecifyRangeImagPath, true));
                this.BaseCaptureFunctionL.Add(new BaseCaptureFunction(BaseCaptureFunction.RecordFullScreen, BaseCaptureFunction.RecordFullScreenImagPath, true));
                this.BaseCaptureFunctionL.Add(new BaseCaptureFunction(BaseCaptureFunction.RecordSpecifiedRange, BaseCaptureFunction.RecordSpecifiedRangeImagPath, true));

                this.BaseKeyboardShortcutL = new List<BaseKeyboardShortcut>();
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 112, Description = @"F1", IsModifiersHasFlag =false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 113, Description = @"F2", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 114, Description = @"F3", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 115, Description = @"F4", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 116, Description = @"F5", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 117, Description = @"F6", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 118, Description = @"F7", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 119, Description = @"F8", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 120, Description = @"F9", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 121, Description = @"F10", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 122, Description = @"F11", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 123, Description = @"F12", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 13, Description = @"Enter", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 27, Description = @"ESC", IsModifiersHasFlag = false });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 112, Description = @"Control + F1", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 113, Description = @"Control + F2", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 114, Description = @"Control + F3", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 115, Description = @"Control + F4", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 116, Description = @"Control + F5", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 117, Description = @"Control + F6", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 118, Description = @"Control + F7", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 119, Description = @"Control + F8", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 120, Description = @"Control + F9", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 121, Description = @"Control + F10", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 122, Description = @"Control + F11", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 123, Description = @"Control + F12", IsModifiersHasFlag = true, CM = ConsoleModifiers.Control });

                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 112, Description = @"Shift + F1", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 113, Description = @"Shift + F2", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 114, Description = @"Shift + F3", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 115, Description = @"Shift + F4", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 116, Description = @"Shift + F5", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 117, Description = @"Shift + F6", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 118, Description = @"Shift + F7", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 119, Description = @"Shift + F8", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 120, Description = @"Shift + F9", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 121, Description = @"Shift + F10", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 122, Description = @"Shift + F11", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });
                this.BaseKeyboardShortcutL.Add(new BaseKeyboardShortcut() { Code = 123, Description = @"Shift + F12", IsModifiersHasFlag = true, CM = ConsoleModifiers.Shift });

                this._isWindowTopMost = true;

                this.BaseWatchKeyboard = new BaseWatchKeyboard();
                this.BaseWatchKeyboard.WatchEvent += KeyboardWatchEvent;
                this.DoKeyboardWatchEvent = null;

                BaseWatchKeyboard.Communication = Communication;

                //閃光動畫效果
                this.FSFS = new FScreenshotFullScreen();
                this.FSFS.IsAnimationEffects = true;

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(Key.SaveFolder, defaultSaveFolderPath);
                this.BaseRecordFullScreen = new BaseRecordFullScreen(BaseCaptureFunction.RecordFullScreen, dic);

                //指定範圍
                this._baseSpecifiedRange = new BaseSpecifiedRange();

                //設定模組內的溝通方式
                BMolecule.Communication = Communication;

            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }                
        #endregion

        /**/
        #region override from Interfaces(IScreenshot)
        public void Annihilation()
        {
            try
            {

            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <exception cref="ExpectedInfo"></exception>
        public void Start()
        {
            try
            {
                switch (this.Name)
                {
                    //全螢幕截圖
                    case BaseCaptureFunction.ScreenshotFullScreen:                        
                        this._isDoOnce = true;
                        this.DoScreenshotFullScreen();
                        if (this.FSFS!.IsAnimationEffects)
                        {
                            FScreenshotFullScreen.TriggerFlashEffectAsync();
                        }
                        
                        break;
                    //指定螢幕範圍截圖
                    case BaseCaptureFunction.ScreenshotSpecifyRange:
                        //123
                        this._isDoOnce = true;
                        this._specifiedRange!.Hide();
                        this.DoScreenshotFullScreenByRange();
                        if (this.FSFS!.IsAnimationEffects)
                        {
                            FScreenshotFullScreen.TriggerFlashEffectAsyncByRange(this._baseSpecifiedRange!.Left, this._baseSpecifiedRange!.Top, this._baseSpecifiedRange!.Width, this._baseSpecifiedRange!.Height);
                        }
                        this._specifiedRange!.Show();
                        break;
                    //全螢幕錄影
                    case BaseCaptureFunction.RecordFullScreen:
                        if (!this.BaseRecordFullScreen!.IsRunning)
                        {
                            this._isDoOnce = false;
                            //Dictionary<string, object> dic = new Dictionary<string, object>();
                            //dic.Add(Key.fps, 30);
                            //dic.Add(Key.SaveFolder, this.saveFolder!);
                            //this.BaseRecordFullScreen = new BaseRecordFullScreen(BaseCaptureFunction.RecordFullScreen, dic);

                            if (this.FSFS!.IsAnimationEffects)
                            {
                                FScreenshotFullScreen.TriggerFlashEffect(128, 0, 222, 0, 100);
                            }
                            this.BaseRecordFullScreen.SaveFolder = this.saveFolder!;
                            this.BaseRecordFullScreen.IsSpecifiedRange = false;
                            //SpinWait.SpinUntil(() => false, 100);
                            this.BaseRecordFullScreen.Start();
                        }
                        else
                        {
                            this._isDoOnce = true;
                            this.BaseRecordFullScreen.Interruption();
                            if (this.FSFS!.IsAnimationEffects)
                            {
                                FScreenshotFullScreen.TriggerFlashEffectAsync();
                            }
                        }
                        break;
                    //指定螢幕範圍錄影
                    case BaseCaptureFunction.RecordSpecifiedRange:
                        //準備開始錄製
                        if (!this.BaseRecordFullScreen!.IsRunning)
                        {
                            this._isDoOnce = false;
                            if (this.FSFS!.IsAnimationEffects)
                            {
                                //this.TriggerFlashEffect(128, 0, 222, 0, 100);
                                //FScreenshotFullScreen.TriggerFlashEffect(128, 0, 222, 0, 100);                                
                                FScreenshotFullScreen.TriggerFlashEffectAsyncByRange(this.BaseRecordFullScreen.BaseSpecifiedRange!.Left, this.BaseRecordFullScreen.BaseSpecifiedRange!.Top, this.BaseRecordFullScreen.BaseSpecifiedRange!.Width, this.BaseRecordFullScreen.BaseSpecifiedRange!.Height);

                            }
                            this.BaseRecordFullScreen.SaveFolder = this.saveFolder!;
                            this.BaseRecordFullScreen.IsSpecifiedRange = true;
                            //SpinWait.SpinUntil(() => false, 100);
                            this._specifiedRange!.SpecifiedRangeBorderColor1 = GenerallySize.SpecifiedRangeBorderDoColor1;
                            this._specifiedRange!.SpecifiedRangeBorderColor2 = GenerallySize.SpecifiedRangeBorderDoColor2;
                            this._specifiedRange!.IsShowCrossCanvas = false;
                            this.BaseRecordFullScreen.Start();
                        }
                        //錄製完成
                        else
                        {
                            this._isDoOnce = true;
                            this.BaseRecordFullScreen.Interruption();
                            this.BaseRecordFullScreen.IsSpecifiedRange = false;
                            if (this.FSFS!.IsAnimationEffects)
                            {
                                FScreenshotFullScreen.TriggerFlashEffectAsync();
                            }
                            this._specifiedRange!.SpecifiedRangeBorderColor1 = GenerallySize.SpecifiedRangeBorderDefaultColor1;
                            this._specifiedRange!.SpecifiedRangeBorderColor2 = GenerallySize.SpecifiedRangeBorderDefaultColor2;
                            this._specifiedRange!.IsShowCrossCanvas = true;
                        }
                        break;

                    default:
                        throw new ExpectedInfo($@"Please check Name, Unknow:[{this.Name}]", Code.FCT_004);
                        //break;
                }   
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 執行拍攝動畫閃光
        /// </summary>
        //private void TriggerFlashEffectAsync()
        //{
        //    try
        //    {
        //        FScreenshotFullScreen.TriggerFlashEffectAsync();
        //    }
        //    catch (ExpectedInfo ex)
        //    {
        //        throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
        //    }
        //    finally
        //    {
        //    }
        //}

        ///// <summary>
        ///// 執行拍攝動畫閃光
        ///// </summary>
        //private void TriggerFlashEffect(byte a, byte r, byte g, byte b, double time)
        //{
        //    try
        //    {
        //        FScreenshotFullScreen.TriggerFlashEffect(a, r, g, b, 100);
        //    }
        //    catch (ExpectedInfo ex)
        //    {
        //        throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
        //    }
        //    finally
        //    {
        //    }
        //}

        /// <summary>
        /// 執行全螢幕截圖
        /// </summary>
        /// <exception cref="ExpectedInfo"></exception>
        private void DoScreenshotFullScreen()
        {
            try
            {
                var date = DateTime.Now.ToString(@"yyyy-MM-dd_HH-mm-ss-fff");

                //依照螢幕截圖
                //var index = 1;
                //foreach (Screen screen in Screen.AllScreens)
                //{
                //    var screenshot = new Bitmap(screen.Bounds.Width, screen.Bounds.Height, PixelFormat.Format32bppArgb);

                //    var gfxScreenshot = Graphics.FromImage(screenshot);
                //    gfxScreenshot.CopyFromScreen(
                //        screen.Bounds.X,
                //        screen.Bounds.Y,
                //        0,
                //        0,
                //        screen.Bounds.Size,
                //        CopyPixelOperation.SourceCopy);
                //    // Save the screenshot
                //    var filePath = Path.Combine(this.saveFolder!, $@"{date}-{index}.jpeg");
                //    screenshot.Save(filePath, ImageFormat.Jpeg);
                //    Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"SaveScreenshot:[{filePath}]", Code.IFO_000));

                //    index++;
                //}

                // 所有螢幕合併截圖
                // 計算總寬度與最大高度
                int totalWidth = Screen.AllScreens.Sum(s => s.Bounds.Width);
                int maxHeight = Screen.AllScreens.Max(s => s.Bounds.Height);

                // 建立大圖
                using var combinedScreenshot = new Bitmap(totalWidth, maxHeight, PixelFormat.Format32bppArgb);
                using var gfx = Graphics.FromImage(combinedScreenshot);

                int currentX = 0;
                foreach (Screen screen in Screen.AllScreens)
                {
                    using var screenshot = new Bitmap(screen.Bounds.Width, screen.Bounds.Height, PixelFormat.Format32bppArgb);
                    using var gfxScreenshot = Graphics.FromImage(screenshot);

                    gfxScreenshot.CopyFromScreen(
                        screen.Bounds.X,
                        screen.Bounds.Y,
                        0,
                        0,
                        screen.Bounds.Size,
                        CopyPixelOperation.SourceCopy);

                    // 繪製到大圖的對應位置
                    gfx.DrawImage(screenshot, currentX, 0);

                    currentX += screen.Bounds.Width;
                }
                // 確保存檔資料夾存在
                var filePath = Path.Combine(this.saveFolder!, $@"{date}-AllScreens.jpeg");

                // 儲存高解析圖片
                combinedScreenshot.Save(filePath, ImageFormat.Jpeg);
                Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"SaveScreenshot:[{filePath}]", Code.IFO_000));
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 執行指定螢幕範圍截圖
        /// </summary>
        /// <exception cref="ExpectedInfo"></exception>
        private void DoScreenshotFullScreenByRange()
        {
            try
            {
                var date = DateTime.Now.ToString(@"yyyy-MM-dd_HH-mm-ss-fff");
                
                var captureRect = new Rectangle((int)this._baseSpecifiedRange!.Left, (int)this._baseSpecifiedRange!.Top, (int)this._baseSpecifiedRange!.Width - (int)this._baseSpecifiedRange!.BorderThickness, (int)this._baseSpecifiedRange!.Height);

                using var screenshot = new Bitmap(captureRect.Width, captureRect.Height, PixelFormat.Format32bppArgb);
                using var gfx = Graphics.FromImage(screenshot);

                gfx.CopyFromScreen(
                    captureRect.X,
                    captureRect.Y,
                    0,
                    0,
                    captureRect.Size,
                    CopyPixelOperation.SourceCopy);
                // 確保存檔資料夾存在
                var filePath = Path.Combine(this.saveFolder!, $@"{date}-ByRange.jpeg");

                // 儲存高解析圖片
                screenshot.Save(filePath, ImageFormat.Jpeg);
                Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"SaveScreenshot:[{filePath}]", Code.IFO_000));
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 偵測到符合指定的鍵盤按下事件
        /// </summary>
        /// <param name="vkCode"></param>
        public void KeyboardWatchEvent(BaseKeyboardShortcut bks)
        {
            try
            {
                if (this._isDoKeyboardWatchEvent)
                {
                    Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"The previous Touch Keyboard is still Running", Code.IFO_000));
                }
                else
                {
                    this._isDoKeyboardWatchEvent = true;
                    Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"Touch Keyboard, Code:[{bks.Code}], Description:[{bks.Description}], IsModifiersHasFlag:[{bks.IsModifiersHasFlag}], CM:[{bks.CM.ToString()}]", Code.IFO_000));                    
                    this.DoKeyboardWatchEvent!.Invoke(bks);
                    this._isDoKeyboardWatchEvent = false;
                }
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 功能切換
        /// </summary>
        /// <exception cref="ExpectedInfo"></exception>
        public void FunctionChange()
        {
            try
            {
                switch (this.Name)
                {
                    case BaseCaptureFunction.ScreenshotFullScreen:
                        if (this._specifiedRange != null)
                        {
                            this._specifiedRange.Close();
                        }
                        break;
                    case BaseCaptureFunction.ScreenshotSpecifyRange:
                        //123
                        if (this._specifiedRange != null)
                        {
                            this._specifiedRange.Close();
                        }
                        this._specifiedRange = new SpecifiedRange(this._baseSpecifiedRange!);
                        this._specifiedRange.SpecifiedRangeBorderColor1 = GenerallySize.SpecifiedRangeBorderDefaultColor1;
                        this._specifiedRange.SpecifiedRangeBorderColor2 = GenerallySize.SpecifiedRangeBorderDefaultColor2;
                        this._specifiedRange.IsShowCrossCanvas = true;
                        this._specifiedRange.Opacity = 0.6;
                        this._specifiedRange.Show();
                        break;
                    //全螢幕錄影
                    case BaseCaptureFunction.RecordFullScreen:
                        if (this._specifiedRange != null)
                        {
                            this._specifiedRange.Close();
                        }
                        break;
                    //指定螢幕範圍錄影
                    case BaseCaptureFunction.RecordSpecifiedRange:
                        //123
                        if (this._specifiedRange != null)
                        {
                            this._specifiedRange.Close();
                        }
                        this._specifiedRange = new SpecifiedRange(this.BaseRecordFullScreen!.BaseSpecifiedRange!);
                        this._specifiedRange.SpecifiedRangeBorderColor1 = GenerallySize.SpecifiedRangeBorderDefaultColor1;
                        this._specifiedRange.SpecifiedRangeBorderColor2 = GenerallySize.SpecifiedRangeBorderDefaultColor2;
                        this._specifiedRange.IsShowCrossCanvas = true;
                        this._specifiedRange.Opacity = 0.6;
                        this._specifiedRange.Show();
                        break;
                    default:
                        throw new ExpectedInfo($@"Please check Name, Unknow:[{this.Name}]", Code.FCT_004);
                        //break;
                }
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Code.FCT_002);
            }
            finally
            {
            }
        }
        #endregion

        /**/
        #region override from Interfaces(INotifyPropertyChanged)

        /// <summary>
        /// 屬性變更連動事件
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 執行屬性變更
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
