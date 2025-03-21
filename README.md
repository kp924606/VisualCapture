![](https://img.shields.io/badge/Creater-TCT-FFFF00) ![](https://img.shields.io/badge/development-csharp-006400) ![](https://img.shields.io/badge/SDK-DotNet8-blue) ![](https://img.shields.io/badge/Tool-VisualStudio2022-222222) ![](https://img.shields.io/badge/OS-Windows-FF8022) ![](https://img.shields.io/badge/UI-WPF-FF6666)

# VisualCapture
VisualCapture/視覺捕捉/全螢幕截圖(Full-screen screenshot)

只想使用執行檔案(不含程式碼)，請將以下資料夾下載"VisualCaptureApp/bin/Release/net8.0-windows".
   
If you only want to use the executable files (excluding the source code), please download the following folder:"VisualCaptureApp/bin/Release/net8.0-windows".

## 1. App Main Window
![image](https://github.com/user-attachments/assets/1d7a0041-9d96-4d8f-a5ac-86c6663a44f2)

------

## 2. App Splash Window
![image](https://github.com/user-attachments/assets/cf39130a-ebe8-4d70-a7d1-2b719c0c3a28)

------

## 3. App Leave Window
![image](https://github.com/user-attachments/assets/088f2dec-0679-4bb9-9a09-c3fd265a80c8)

------

## 4. App useing Guide

### 4-1. Show General settings and detailed settings
![image](https://github.com/user-attachments/assets/c2b20a7b-b9f1-4ebc-aab3-0957e5a6d8b2)

![image](https://github.com/user-attachments/assets/8a076235-81a5-44d7-bef9-3f86199f75b8)

![image](https://github.com/user-attachments/assets/f43a4f5b-464a-4c2e-a04b-a3f190c07e61)

------

### 4-2. Execute the specified shooting action
![image](https://github.com/user-attachments/assets/0884ed1e-1dfd-43c8-b0c4-0d153fa9e981)

action refer as Combox Selected.

![image](https://github.com/user-attachments/assets/7e0f481a-e474-466f-9a04-56dd303f1849)

```diff
! 未來會持續擴充更多功能。
! More features will be continuously added in the future.
```

------

### 4-3. General Settings Page
1.SaveFoler/儲存資料夾

  瀏覽您的電腦資料夾並選擇儲存資料夾。
  
  *Browser your computer Folder and Select SaveFolder.*
  
  ![image](https://github.com/user-attachments/assets/5e5b3680-406e-4004-8cc9-9aadb3998ceb)
  
  ![image](https://github.com/user-attachments/assets/03ac6254-5a51-4d8e-abeb-8761f3c6e5c2)

1-2.顯示您的設定儲存資料夾路徑。

  *Show your Setting SaveFolder Path.*
  
  ![image](https://github.com/user-attachments/assets/fbda326e-2585-49d4-907e-28634d8f4a27)

1-3.使用檔案總管，開啟儲存資料夾。

  *Use File Explorer to open the storage folder.*
  
  ![image](https://github.com/user-attachments/assets/2d2d80fa-13d0-4f13-b39b-c00f4b3eab9e)
  
  ![image](https://github.com/user-attachments/assets/1c5f587d-33d0-438d-a1b6-7578ef271019)

------

2.WindowTopMost/視窗最前景:

  啟用時，會讓此程式主視窗在全螢幕最前景。
  
  *When enabled, the main window of this program will stay in the foreground in full-screen mode.*
  
  ![image](https://github.com/user-attachments/assets/e942a782-a005-4d05-b3a5-28afa2c5dbbb)

------

3.Keyboard Shortcut/鍵盤快捷鍵:

  啟用時，當依照指定的快捷鍵觸發時，會執行指定拍照動作。
    
  *When enabled, the specified capture action will be executed when triggered by the designated shortcut key.*
  
  ![image](https://github.com/user-attachments/assets/c157c2b7-c206-435f-a44a-71ae68be4944)
  
  ![image](https://github.com/user-attachments/assets/bfb60fa4-8a9d-44b5-9ed8-bbf2a2589535)

------

### 4-4. Settings Page
這個畫面會依據您所選擇的功能切換專屬的設定.

*This screen will switch to the specific settings based on the feature you select.*


1.ScreenshotFullScreen:

  1-1:AnimationEffects/動畫效果:
  
  啟用時，會在執行拍攝時，讓全螢幕有短暫的白色閃爍動畫.
    
  *When enabled, a brief white flash animation will appear on the entire screen during the capture process.*
    
  ![image](https://github.com/user-attachments/assets/2a196678-7a85-4af5-8633-e3c2fe815ea2)

------

### 4-5. Hide Window
執行後會將此程式視窗隱藏至系統通知內。

*After execution, the program window will be hidden in the system tray.*

![image](https://github.com/user-attachments/assets/283141ea-dee1-45c4-90aa-5a0b8e9fb44b)

![image](https://github.com/user-attachments/assets/23da0afb-dc6a-4b4b-97c4-249763cbbda4)

![image](https://github.com/user-attachments/assets/91a86f9d-a1f1-49ca-b993-9f9dcca2f9b2)

------

### 4-6. Close App
執行後會顯示MessageBox，若按下 Yes 則將此程式關閉並呈現關閉動畫視窗。

*After execution, a MessageBox will be displayed. If 'Yes' is clicked, the program will close and show a closing animation window.*

![image](https://github.com/user-attachments/assets/08c3ed68-ed8a-446a-a096-94b3e269012b)

![image](https://github.com/user-attachments/assets/febb4a86-c218-4bb4-87f9-a180f0e60779)

![image](https://github.com/user-attachments/assets/f6545c50-587d-44bb-8c3c-dc55e8c69fb4)

------

# 1. Package Introduce

| **Item** | **Name** | **Version** | **Function** |
|----------|--------------|-------------|-------------|
| **1** | **Hardcodet.NotifyIcon.Wpf** | 2.01 | 系統通知區（通常位於桌面右下角）顯示自訂的通知圖示.<br>Display a custom notification icon in the system tray (usually located at the bottom right corner of the desktop). |
| **2** | **NLog** | 5.40 | 日誌框架.<br>logging framework. |


## 1-1. Hardcodet.NotifyIcon.Wpf
Hardcodet.NotifyIcon.Wpf 是一個為 WPF (Windows Presentation Foundation) 應用程式提供系統通知圖示功能的套件。它允許開發者在 Windows 的系統匣 (system tray) 上顯示應用程式的圖示，並提供與這些圖示互動的功能，例如顯示通知、提供右鍵選單等。

*Hardcodet.NotifyIcon.Wpf is a package that provides system tray icon functionality for WPF (Windows Presentation Foundation) applications. It allows developers to display an application's icon in the system tray on Windows and provides interaction features with these icons, such as displaying notifications and offering right-click context menus.*

### 主要功能：

- 1.顯示系統匣圖示：
  
  讓 WPF 應用程式可以在 Windows 的系統匣（通常在螢幕右下角）顯示一個圖示，這對於需要常駐於背景並定期通知使用者的應用程式特別有用。

  可以設定圖示、提示文字、氣泡通知等。

  *Allows WPF applications to display an icon in the system tray (usually located at the bottom right corner of the screen) on Windows, which is particularly useful for applications that need to stay in the background and periodically notify the user.You can configure the icon, tooltip text, balloon notifications, and more.*

- 2.顯示氣泡通知：

  可以利用 NotifyIcon 顯示類似 Windows 原生的氣泡通知，這對於提醒用戶特定狀況（例如新消息或進度更新）非常有用。

  通常包含標題、消息內容、顯示時間等屬性。

  *NotifyIcon can be used to display native Windows-style balloon notifications, which are useful for alerting users to specific situations (such as new messages or progress updates). These notifications typically include properties like title, message content, and display duration.*

- 3.提供右鍵選單功能：

  允許在圖示上點擊右鍵時顯示一個選單。這對於應用程式有額外操作選項（如設定、退出應用程式等）非常有用。

  *Allows displaying a context menu when right-clicking on the icon. This is particularly useful for applications that have additional operation options (such as settings, exiting the application, etc.).*

- 4.支援多種圖示：

  支援從應用程式資源或外部圖像檔案加載圖示，並能在系統匣顯示不同的圖示狀態。

  *Supports loading icons from application resources or external image files, and allows displaying different icon states in the system tray.*

- 5.動態更新通知圖示：

  允許動態改變圖示的顯示內容，這對於顯示即時狀態（例如處於不同狀態的圖示，或顯示未讀訊息數量）非常有效。
  *Allows dynamically changing the displayed icon content, which is particularly effective for showing real-time status (such as icons in different states or displaying the number of unread messages).*

------

## 1-2. NLog
NLog 是一個功能強大的 開源日誌框架，用於 .NET 應用程式的日誌記錄。它提供了一個靈活且高效的方式來記錄應用程式的運行狀況、錯誤訊息、警告、調試資訊等，並支援多種日誌輸出方式（如檔案、資料庫、控制台等）。

*NLog is a powerful open-source logging framework for .NET applications. It provides a flexible and efficient way to log application runtime status, error messages, warnings, debugging information, and more. NLog supports multiple logging output options, such as files, databases, and the console.*

### 主要功能：

- 1.多種輸出目標：
    - NLog 支援多種 日誌輸出目標，包括：      
    - 檔案（如 .txt, .log 等）
    - 資料庫（如 SQL Server）
    - 控制台（例如命令列視窗）
    - 事件日誌（Windows Event Log）
    - 電子郵件（發送日誌作為電子郵件）
    - 第三方平台（如 Slack、Twitter 等）
    - 每個日誌條目可以配置不同的目標，並根據日誌的等級（例如 Info, Error, Debug）選擇性地輸出。

- 2.靈活的日誌級別設置：

    - NLog 提供了多種 日誌級別，用來表示不同的日誌重要性：
    - Trace：最詳細的日誌，通常用於記錄調試信息。
    - Debug：用於記錄調試和開發過程中的詳細信息。
    - Info：常規的操作信息，例如應用程式啟動或關閉。
    - Warn：警告信息，表示系統運行中可能出現的問題。
    - Error：錯誤信息，通常是應用程式執行過程中發生的錯誤。
    - Fatal：致命錯誤，表示系統無法繼續運行。

- 3.靈活的配置：

    - NLog 支援通過 XML 或 JSON 配置檔案來設置日誌的格式、輸出目標和過濾規則，這樣你可以非常靈活地配置應用程式的日誌策略。  
    - 支援動態重新載入配置，可以在應用程式運行中修改配置而不需要重新啟動應用程式。

- 4.異常處理和堆疊追蹤：
  
    - NLog 支援將 異常堆疊追蹤（stack trace）和相關錯誤訊息記錄到日誌中，這對於排查問題非常有幫助。
  
- 5.條件過濾：

    - 可以根據日誌級別、標籤等條件來過濾日誌輸出，這樣可以更精確地控制哪些日誌信息被記錄。
  
- 6.自定義日誌格式：

    - 支援自定義日誌條目的格式，你可以控制日期、時間、日誌級別、訊息等顯示格式，讓日誌更加易讀且符合需求。
  
- 7.性能優化：

    - NLog 在記錄大量日誌時表現高效，它使用緩衝區和異步寫入方式來確保日誌記錄不會對應用程式性能造成過多影響。

------

# 2. Self-Developed Component Introduce

| **Item** | **Name** | **Function** |
|----------|--------------|-------------|
| **1** | **HolyGift.dll** | 神聖的名字.<br>Divine Name |
| **2** | **ILogger.dll** | 日誌物件.<br>Log Object. |
| **3** | **Judgment.dll** | 原因代碼.<br>Reason Code. |
| **4** | **OLogger.dll** | 紀錄檔案日誌.<br>Save Log File. |
| **5** | **TCTUtility.dll** | 通用功能.<br>General Functionality |

```diff
! 上述五的組件專案並未包含在此專案內部，若想在開發工具上使用請自行從 Release 資料夾內更換 dll.
! The component project mentioned in point five is not included within this project. If you wish to use it in the development tool, please manually replace the DLL from the Release folder.
```

## 2-1. Hardcodet.NotifyIcon.Wpf
定義通用真名，可用於各專案需求.

*Define a universal true name, which can be used for various project needs.*

### 主要功能：

- 1.可在需要固定名字的地方使用：

  *It can be used in places where a fixed name is required.*

```bash
HolyGift.Key.System
```
![image](https://github.com/user-attachments/assets/d56fe3b5-921e-49b6-a4bd-913ef4b92c25)

------

## 2-2. ILogger.dll
定義 Log 物件，可用於各專案需求.

*Define a Log object that can be used for various project requirements.*

### 主要功能：

- 1.可在需要紀錄 Log 的地方使用：

  *Can be used wherever log recording is needed.*

```bash
new LogInfo(HolyGift.Key.System, MethodBase.GetCurrentMethod()!.DeclaringType!.ToString(), MethodBase.GetCurrentMethod()!.Name, $@"Start...", Code.IFO_000);
```
![image](https://github.com/user-attachments/assets/1537ab64-1dcf-4418-bd90-eabc4c8af31e)

------

## 2-3. Judgment.dll
定義原因代碼，可用於錯誤原因及各種情況，便於分析及釐清問題.

*Define a reason code that can be used for error causes and various situations, making it easier to analyze and clarify issues.*

### 主要功能：

- 1.在專案內想要爆錯時始使用：

  *Used when you want to throw an error within the project.*

```bash
throw new ExpectedInfo($@"Please check BaseScreenshot BaseCaptureFunctionL, List is Null", Code.ODI_004);
```

![image](https://github.com/user-attachments/assets/bbe83a0a-d82c-4d11-80a9-38fd5d1a853d)

------

## 2-4. OLogger.dll
將 Log 物件，儲存成檔案.

*Save the Log object as a file.*

### 主要功能：

- 1.在專案內建立專屬的儲存物件及函式：

  *Create dedicated storage objects and functions within the project, and save them as files.*

```bash
OLogInfo? ologinfo = null;
ologinfo = new OLogInfo(Assembly.GetExecutingAssembly().GetName().Name, 30, 3, 777);
ologinfo!.Logger.Info($@"<{li.Name},{li.Class}>[{li.Method}][{li.ResultCode}] {li.Info}");
```
![image](https://github.com/user-attachments/assets/b1ecd3de-67ee-4aff-b463-a4b5a58beafa)

------

## 2-5. TCTUtility.dll
通用功能，提供常用到的函示。

*General functions, providing commonly used functions.*

### 主要功能：

- 1.在專案內可不需要自行或重複撰寫，可以直接使用：

  *It is not necessary to write or rewrite it yourself within the project; you can use it directly.*

```bash
namespace
TCTUtility.Function
```

------

## About Me
Thanks & Best Regards !

蔡承廷

​Senior Engineer of Semiconductor Product/Testing & ​Automation

Email: ​​kp924606@gmail.com

LinkedIn:https://www.linkedin.comin/tsai-cheng-ting/

