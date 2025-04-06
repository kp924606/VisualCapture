using HolyGift;
using ILogger.AP;
using ILogger.Enum;
using ILogger.Interface;
using Judgment;
using ProjectLifeModuleManagement.Base;
using ProjectLifeModuleManagement.Module;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using TCTUtility.Function;
using VisualCaptureApp.Interface;

namespace VisualCaptureApp.Base
{
    public class BaseRecordFullScreen : BMolecule, INotifyPropertyChanged
    {
        #region Static
       
        #endregion

        #region Property
        /// <summary>
        /// 模組, 執行截圖
        /// </summary>
        private MInfiniteLoop? _mCaptureScreenshot { set; get; }

        /// <summary>
        /// 模組, 截圖整合成影像
        /// </summary>
        private MInfiniteLoop? _mIntegrationScreenshot { set; get; }

        /// <summary>
        /// 每秒幀數
        /// </summary>
        private int fps {set; get;}

        /// <summary>
        /// 每秒幀數
        /// </summary>
        public int FPS
        {
            get { return this.fps; }
            set
            {
                if (this.fps != value)
                {
                    this.fps = value;
                }
            }
        }

        /// <summary>
        /// 影像品質
        /// </summary>
        private int _videoQuality { set; get; }

        /// <summary>
        /// 影像品質
        /// </summary>
        public int VideoQuality
        {
            get { return this._videoQuality; }
            set
            {
                if (this._videoQuality != value)
                {
                    this._videoQuality = value;
                }
            }
        }

        /// <summary>
        /// 擷取執行緒
        /// </summary>
        private Process? ffmpegProcess { get; set; }
        
        /// <summary>
        /// 檔案儲存位置
        /// </summary>
        private string? saveFolder { set; get; }

        /// <summary>
        /// 檔案儲存位置
        /// </summary>
        public string? SaveFolder
        {
            get { return this.saveFolder; }
            set
            {
                if (this.saveFolder != value)
                {
                    this.saveFolder = value;
                }
            }
        }

        /// <summary>
        /// 錄製時間
        /// </summary>
        private int recordTime { set; get; }

        /// <summary>
        /// 錄製時間
        /// </summary>
        public int RecordTime
        {
            get { return this.recordTime; }
            set
            {
                this.recordTime = value;
                OnPropertyChanged(nameof(this.RecordTime));
                OnPropertyChanged(nameof(this.RecordTimeDate));
            }
        }
        public string RecordTimeDate
        {
            get {
                TimeSpan time = TimeSpan.FromSeconds(this.recordTime);
                // 格式化輸出
                string formattedTime = $"{time.Days:D2}/{time.Hours:D2}/{time.Minutes:D2}/{time.Seconds:D2}";
                return formattedTime;
            }
            set
            {
                OnPropertyChanged(nameof(this.RecordTimeDate));
            }
        }

        /// <summary>
        /// 錄製音效
        /// </summary>
        private bool _isRecordAudio { get; set; }

        /// <summary>
        /// 錄製音效
        /// </summary>
        public bool IsRecordAudio
        {
            get { return this._isRecordAudio; }
            set
            {
                this._isRecordAudio = value;
                // 通知屬性已變更
                OnPropertyChanged(nameof(IsRecordAudio));
            }
        }

        /// <summary>
        /// 目前選擇的音效
        /// </summary>
        private int _currentAudioIndex { get; set; }

        /// <summary>
        /// 目前選擇的音效
        /// </summary>
        public int CurrentAudioIndex
        {
            get { return this._currentAudioIndex; }
            set
            {
                this._currentAudioIndex = value;
                // 通知屬性已變更
                OnPropertyChanged(nameof(CurrentAudioIndex));
            }
        }

        /// <summary>
        /// 音效清單
        /// </summary>
        private List<string>? _audioList { set; get; }

        /// <summary>
        /// 音效清單
        /// </summary>
        public List<string>? AudioList
        { 
            get => this._audioList;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Init
        public BaseRecordFullScreen(string name, Dictionary<string, object> dic)
            : base(name)
        {
            try
            {
                if (!this.CheckffmpegExists())
                {
                    throw new ExpectedInfo($@"Please check ffmpeg, ffmpeg is not installed or not available.", Code.CMD_001);
                }

                //this.fps = FUtility.GetIntAndCheckNOrEFromDic(dic, Key.fps);
                this.saveFolder = FUtility.GetStringAndCheckNOrEFromDic(dic, Key.SaveFolder);
                this._audioList = new List<string>();
                this.RecordTime = 0;
                this.GetAudioList();
                this.AddProject(new MRightNow(@"DoScreenshot_MRightNow", DoScreenshot, DoScreenshotFinish));
                this.AddProject(new MTimer(@"DoRecordTime_MTimer", DoRecordTime, DoRecordTimeFinish, 1, false, true));                
            }
            catch (ExpectedInfo ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {

            }
        }

        #endregion

        #region Method

        /// <summary>
        /// 確認 ffmpeg  是否存在
        /// </summary>
        /// <returns></returns>
        private bool CheckffmpegExists()
        {
            try
            {
                // 構建 ProcessStartInfo 來執行 ffmpeg -version 命令
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = "-version",  // 檢查 ffmpeg 版本
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // 啟動外部進程來執行 ffmpeg 命令
                using (Process process = Process.Start(startInfo)!)
                {
                    // 讀取並忽略標準輸出與錯誤輸出
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    // 如果成功執行，表示 ffmpeg 存在
                    if (process.ExitCode == 0)
                    {
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("FFmpeg Error: " + error);
                        return false;
                    }
                }
            }
            catch (ExpectedInfo ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
                return false;
            }
            catch (Exception ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
                return false;
            }
            finally
            {

            }
        }


        /// <summary>
        /// 取得音效清單
        /// </summary>
        /// <exception cref="ExpectedInfo"></exception>
        private void GetAudioList()
        {
            // 設定命令
            string command = "ffmpeg";
            string arguments = "-list_devices true -f dshow -i dummy";

            // 使用 Process 啟動外部命令並捕獲其輸出
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardErrorEncoding = Encoding.UTF8, // 設定錯誤輸出的編碼為 UTF-8
                StandardOutputEncoding = Encoding.UTF8  // 設定標準輸出的編碼為 UTF-8
            };

            List<string> audioDevices = new List<string>();

            try
            {
                using (Process process = Process.Start(startInfo)!)
                {
                    // 捕獲錯誤和標準輸出的內容
                    string output = process.StandardError.ReadToEnd();
                    process.WaitForExit();  // 等待命令執行結束

                    // 使用 findstr 來過濾出包含 "audio" 的行
                    string[] lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in lines)
                    {
                        // 用 "audio" 關鍵字來過濾音訊裝置
                        if (line.IndexOf("audio", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // 提取設備名稱並加入 List
                            var deviceName = line.Trim().Split('\"')[1];  // 取出雙引號中的部分
                            audioDevices.Add(deviceName);
                        }
                    }
                }
                this._audioList = audioDevices;
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
        /// 開始擷取影像
        /// </summary>
        /// <param name="obj"></param>
        private void DoScreenshot(object obj)
        {
            try
            {
                // 準備 FFmpeg 命令
                //string ffmpegArgs = $"-f gdigrab -framerate {this.fps} -i desktop -c:v libx264 -preset fast -tune zerolatency output_{DateTime.Now.ToString(@"yyyyMMddHHmmss")}.mp4";
                //ffmpegArgs = $"-y -f gdigrab -framerate 30 -i desktop -c:v mpeg4 -q:v 5 output.avi";
                //string ffmpegArgs = $@"-y -f gdigrab -framerate {this.fps} -i desktop -c:v mpeg4 -q:v 5 {this.saveFolder}\output_{DateTime.Now.ToString(@"yyyyMMddHHmmss")}.avi";

                //-y 自動覆蓋輸出檔案（如果已存在）
                //-f gdigrab 輸入格式為 GDI 繪圖介面截取 (Windows 專用)
                //-framerate 30 設定錄製的幀率（FPS）為 30
                //-i desktop 設定輸入來源為整個螢幕(desktop), 如果要錄製特定視窗，則可以用 -i title=視窗名稱
                //-c:v 代表選擇視訊編碼器 (Video Codec), mpeg4 為 avi, libx264 為 mp4
                //-q:v 視訊品質指標，數值範圍通常是 1 (最佳畫質)~ 31 (最低畫質)
                //-preset fast 壓縮效能與編碼速度, 控制編碼時間, 輸出影片的大小, ultrafast 速度最快 檔案最大 CPU 負擔最低
                //-crf 23 是 H.264 (libx264) 壓縮的品質控制參數，它決定了 影片的畫質與檔案大小之間的平衡, 數值範圍是 0（無損）到 51（最差）, 23 是 H.264 的預設值
                //-f dshow	使用 DirectShow 來擷取音訊 (適用 Windows)
                //-i audio="virtual-audio-capturer"	指定擷取系統音效的裝置
                //-c:a aac	音訊編碼格式，使用 aac (建議格式，適用於大多數播放器)
                //-b:a 192k	音訊位元率，設定為 192kbps (可調整)

                string ffmpegArgs = string.Empty;
                if (this.IsRecordAudio)
                {
                    ffmpegArgs = $@"-y -f gdigrab -framerate {this.fps} -i desktop -f dshow -i audio=""{this.AudioList![this.CurrentAudioIndex]}"" -c:v mpeg4 -q:v {this._videoQuality} {this.saveFolder}\output_{DateTime.Now.ToString(@"yyyyMMddHHmmss")}.avi";
                }
                else
                {
                    ffmpegArgs = $@"-y -f gdigrab -framerate {this.fps} -i desktop -c:v mpeg4 -q:v {this._videoQuality} {this.saveFolder}\output_{DateTime.Now.ToString(@"yyyyMMddHHmmss")}.avi";
                }

                //string ffmpegArgs = $@"-y -f gdigrab -framerate {this.fps} -i desktop -f dshow -i audio=""Line 1 (Virtual Audio Cable)"" -c:v mpeg4 -q:v {this._videoQuality} {this.saveFolder}\output_{DateTime.Now.ToString(@"yyyyMMddHHmmss")}.avi";
                
                //string ffmpegArgs = $@"-y -f gdigrab -framerate {this.fps} -i desktop -c:v mpeg4 -q:v {this._videoQuality} {this.saveFolder}\output_{DateTime.Now.ToString(@"yyyyMMddHHmmss")}.avi";

                //string ffmpegArgs = $@"-y -f gdigrab -framerate {this.fps} -i desktop -c:v mpeg4 -q:v 5 {this.saveFolder}\output_{DateTime.Now.ToString(@"yyyyMMddHHmmss")}.avi";
                //string ffmpegArgs = $@"-y -f gdigrab -framerate {this.fps} -i desktop -c:v libx264 -crf 2 {this.saveFolder}\output_{DateTime.Now.ToString(@"yyyyMMddHHmmss")}.mp4";

                // 建立 ProcessStartInfo
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = ffmpegArgs,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardInput = true, // 設置為 true 來重定向標準輸入
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                //確認執行擷取還在執行
                if (this.ffmpegProcess != null && !this.ffmpegProcess.HasExited)
                {
                    // 這會向 FFmpeg 傳送停止命令，並正常結束
                    this.ffmpegProcess.StandardInput.WriteLine("q");
                    this.ffmpegProcess.WaitForExit();
                    this.ffmpegProcess.Dispose();
                    BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, $@"FFmpeg Record Annihilation", Code.IFO_000, ILogType.Info, null, null));
                }

                // 啟動 FFmpeg 程序
                this.ffmpegProcess = Process.Start(startInfo);
                this.RecordTime = 0;

                // 顯示 FFmpeg 的錯誤訊息
                this.ffmpegProcess!.ErrorDataReceived += (sender, e) => {
                    try
                    {
                        if (e.Data != null)
                        {
                            BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, $@"FFmpeg Error:[{e.Data}]", Code.IFO_000, ILogType.Alarm, null, null));
                        }
                    }
                    catch (ExpectedInfo ex)
                    {
                        BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
                    }
                    catch (Exception ex)
                    {
                        BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
                    }
                    finally
                    {

                    }
                };
                this.ffmpegProcess.BeginErrorReadLine();

                // 顯示 FFmpeg 的訊息
                this.ffmpegProcess!.OutputDataReceived += (sender, e) => {
                    try
                    {
                        if (e.Data != null)
                        {
                            BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, $@"FFmpeg Output:[{e.Data}]", Code.IFO_000, ILogType.Info, null, null));
                        }
                    }
                    catch (ExpectedInfo ex)
                    {
                        BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
                    }
                    catch (Exception ex)
                    {
                        BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
                    }
                    finally
                    {

                    }
                };
                this.ffmpegProcess.BeginOutputReadLine();             
            }
            catch (ExpectedInfo ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {

            }
        }

        private void DoScreenshotFinish(object obj)
        {
            try
            {
            }
            catch (ExpectedInfo ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        private void DoRecordTime(object obj)
        {
            try
            {
                this.RecordTime += 1;
            }
            catch (ExpectedInfo ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {

            }
        }

        private void DoRecordTimeFinish(object obj)
        {
            try
            {
            }
            catch (ExpectedInfo ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }
        }

        #endregion

        #region override from Interfaces(BMolecule)
        public override void Interruption()
        {
            try
            {
                //確認執行擷取還在執行
                if (this.ffmpegProcess != null && !this.ffmpegProcess.HasExited)
                {
                    // 這會向 FFmpeg 傳送停止命令，並正常結束
                    this.ffmpegProcess.StandardInput.WriteLine("q");
                    this.ffmpegProcess.WaitForExit();
                    this.ffmpegProcess.Dispose();
                    this.ffmpegProcess = null;
                    this.RecordTime = 0;
                    BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, $@"FFmpeg Record Interruption", Code.IFO_000, ILogType.Info, null, null));
                }
                base.Interruption();
            }
            catch (ExpectedInfo ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.ExpectedInfo, ex.ReasonCode, ILogType.Error, ex, null));
            }
            catch (Exception ex)
            {
                BMolecule.Communication?.Invoke(new LogInfo(this.Name, this.GetType().Name, MethodBase.GetCurrentMethod()!.Name, Key.Catch, Code.FCT_002, ILogType.Catch, ex, null));
            }
            finally
            {
            }

        }
        #endregion

        #region Other Class
        protected class BaseSource 
        {
            public Bitmap? image { set; get; }
        }

        #endregion
    }
}
