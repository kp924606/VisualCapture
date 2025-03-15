using HolyGift;
using ILogger.AP;
using ILogger.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace VisualCaptureApp.Base
{
    /// <summary>
    /// 監控鍵盤, KeyboardHook（低層鍵盤掛鉤，全域監聽）

    /// </summary>
    public class BaseWatchKeyboard : Window
    {
        #region Property
        public int Code { set; get; }
        public BaseKeyboardShortcut? SpecifyKeyShortcut { set; get; }
        private LowLevelKeyboardProc? _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        // Ctrl 和 Shift 鍵的虛擬鍵碼
        private const int VK_LCONTROL = 0xA2; // 左 Ctrl
        private const int VK_RCONTROL = 0xA3; // 右 Ctrl
        private const int VK_LSHIFT = 0xA0;   // 左 Shift
        private const int VK_RSHIFT = 0xA1;   // 右 Shift
        //private const int VK_LMENU = 0xA4;    // 左 Alt
        //private const int VK_RMENU = 0xA5;    // 右 Alt

        //Press and hold, 1000000000000000
        private const int VK_PressHhold = 0x8000;    // 右 Alt

        //0x8000
        #endregion

        #region Static
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        /// <summary>
        /// 溝通
        /// </summary>
        public static Action<ILogInfo>? Communication { get; set; }
        #endregion

        // 定義一個委託
        public delegate void WatchEventDelegate(BaseKeyboardShortcut bks);
        public event WatchEventDelegate? WatchEvent;

        private bool isRunning {set; get;}

        public bool IsRunning {
            get 
            {
                return isRunning;
            }
        }

        /// <summary>
        /// 對應 ModifiersHasFlag 卡Ctrl, Shift 的清單
        /// </summary>
        private List<int>? _modifiersHasFlagKeyList { set; get; }
        
        public BaseWatchKeyboard()
        { 
        
        }

        public BaseWatchKeyboard(BaseKeyboardShortcut bks)
        {
            try
            {
                if (bks == null)
                {
                    throw new ExpectedInfo($@"Please check BaseKeyboardShortcut, Data is Null", Judgment.Code.ODI_005);
                }
                this.SpecifyKeyShortcut = bks;

                this._modifiersHasFlagKeyList = new List<int>();
                
                if (this.SpecifyKeyShortcut!.CM == ConsoleModifiers.Control)
                {
                    this._modifiersHasFlagKeyList.Add(VK_LCONTROL);
                    this._modifiersHasFlagKeyList.Add(VK_RCONTROL);
                }
                else if (this.SpecifyKeyShortcut!.CM == ConsoleModifiers.Shift)
                {
                    this._modifiersHasFlagKeyList.Add(VK_LSHIFT);
                    this._modifiersHasFlagKeyList.Add(VK_RSHIFT);
                }
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Judgment.Code.FCT_002);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 開始監控
        /// </summary>
        /// <param name="bks"></param>
        /// <exception cref="ExpectedInfo"></exception>
        public void RunWatch(BaseKeyboardShortcut bks)
        {
            try
            {
                if (bks == null)
                {
                    throw new ExpectedInfo($@"Please check BaseKeyboardShortcut, Data is Null", Judgment.Code.ODI_005);
                }

                this.SpecifyKeyShortcut = bks;
                this._modifiersHasFlagKeyList = new List<int>();

                if (this.SpecifyKeyShortcut!.CM == ConsoleModifiers.Control)
                {
                    this._modifiersHasFlagKeyList.Add(VK_LCONTROL);
                    this._modifiersHasFlagKeyList.Add(VK_RCONTROL);
                }
                else if (this.SpecifyKeyShortcut!.CM == ConsoleModifiers.Shift)
                {
                    this._modifiersHasFlagKeyList.Add(VK_LSHIFT);
                    this._modifiersHasFlagKeyList.Add(VK_RSHIFT);
                }

                if (this.IsRunning)
                {
                    this.CloseWatch();
                }

                this._proc = this.HookCallback;
                this._hookID = this.SetHook(this._proc);
                this.isRunning = true;
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Judgment.Code.FCT_002);
            }
            finally
            {
            }            
        }

        /// <summary>
        /// 結束監控
        /// </summary>
        /// <exception cref="ExpectedInfo"></exception>
        public void CloseWatch()
        {
            try
            {
                UnhookWindowsHookEx(this._hookID);
                this.isRunning = false;
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Judgment.Code.FCT_002);
            }
            finally
            {
            }           
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            try
            {
                using (Process curProcess = Process.GetCurrentProcess())
                using (ProcessModule curModule = curProcess.MainModule!)
                {
                    return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Judgment.Code.FCT_002);
            }
            finally
            {
            }           
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            try
            {
                //Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"[{nCode}]", Judgment.Code.IFO_000));

                //按下動作(放開不算)
                if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    //// 判斷是否有按下 Ctrl 或 Shift
                    //bool isCtrlPressed = (GetAsyncKeyState(VK_LCONTROL) & 0x8000) != 0 ||
                    //                     (GetAsyncKeyState(VK_RCONTROL) & 0x8000) != 0;
                    //bool isShiftPressed = (GetAsyncKeyState(VK_LSHIFT) & 0x8000) != 0 ||
                    //                      (GetAsyncKeyState(VK_RSHIFT) & 0x8000) != 0;
                    //bool isAltPressed = (GetAsyncKeyState(VK_LMENU) & 0x8000) != 0 ||
                    //            (GetAsyncKeyState(VK_RMENU) & 0x8000) != 0;

                    //if (isCtrlPressed)
                    //{
                    //    Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"按下Ctrl", Judgment.Code.IFO_000));
                    //}
                    //if (isShiftPressed)
                    //{
                    //    Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"按下Shift", Judgment.Code.IFO_000));
                    //}

                    //if (isAltPressed)
                    //{
                    //    Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"按下Alt", Judgment.Code.IFO_000));
                    //}

                    int vkCode = Marshal.ReadInt32(lParam);
                    //符合指定的快捷鍵碼
                    if (vkCode == this.SpecifyKeyShortcut!.Code)
                    {
                        //ModifiersHasFlag 有卡 Ctrl, Shif
                        if (this.SpecifyKeyShortcut!.IsModifiersHasFlag)
                        {
                            ///確認指定的 ModifiersHasFlag 按鈕有被按下(不卡是左邊的還右邊的按鈕)
                            //var tpCheckR = false;
                            //int tpVKR = 0;
                            //foreach (var tpVKKey in this._modifiersHasFlagKeyList!)
                            //{ 
                            //    if((GetAsyncKeyState(tpVKKey) & VK_PressHhold) != 0)
                            //    {
                            //        tpCheckR = true;
                            //        tpVKR = tpVKKey;
                            //    }
                            //}

                            var tpVKR = this._modifiersHasFlagKeyList!.FirstOrDefault(tpVKKey => (GetAsyncKeyState(tpVKKey) & VK_PressHhold) != 0, 0);
                            var tpCheckR = tpVKR != 0;

                            if (tpCheckR)
                            {
                                //Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"按下ModifiersHasFlag:[{tpVKR}]+指定的按鈕:[{this.SpecifyKeyShortcut!.Code}]", Judgment.Code.IFO_000));
                                this.WatchEvent?.Invoke(this.SpecifyKeyShortcut);
                            }
                            
                        }
                        else
                        {
                            //Communication?.Invoke(new LogInfo(Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"指定的按鈕:[{this.SpecifyKeyShortcut!.Code}]", Judgment.Code.IFO_000));
                            this.WatchEvent?.Invoke(this.SpecifyKeyShortcut);
                        }                       
                    }
                }
                return CallNextHookEx(this._hookID, nCode, wParam, lParam);
            }
            catch (ExpectedInfo ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.ExpectedInfo}[{ex}]", ex.ReasonCode);
            }
            catch (Exception ex)
            {
                throw new ExpectedInfo($@"[{this.GetType().Name},{MethodBase.GetCurrentMethod()!.Name}]:{Key.Catch}[{ex}]", Judgment.Code.FCT_002);
            }
            finally
            {
            }
        }
    }
}
