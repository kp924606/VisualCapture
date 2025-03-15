![](https://img.shields.io/badge/Creater-TCT-FFFF00) ![](https://img.shields.io/badge/development-csharp-006400) ![](https://img.shields.io/badge/Version-DotNet8-blue) ![](https://img.shields.io/badge/Tool-VisualStudio2022-222222) ![](https://img.shields.io/badge/OS-Windows-FF8022)

# VisualCapture
VisualCapture/視覺捕捉/全螢幕截圖("Full-screen screenshot")

# 1. App Main Window
![image](https://github.com/user-attachments/assets/1d7a0041-9d96-4d8f-a5ac-86c6663a44f2)

# 2. App Splash screen
![image](https://github.com/user-attachments/assets/cf39130a-ebe8-4d70-a7d1-2b719c0c3a28)


# 3. App Leave the screen
![image](https://github.com/user-attachments/assets/088f2dec-0679-4bb9-9a09-c3fd265a80c8)


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

# 2. Self-Developed Component Introduce

| **Item** | **Name** | **Function** |
|----------|--------------|-------------|
| **1** | **HolyGift.dll** | 神聖的名字.<br>Divine Name |
| **2** | **ILogger.dll** | 日誌物件.<br>Log Object. |
| **3** | **Judgment.dll** | 原因代碼.<br>Reason Code. |
| **4** | **OLogger.dll** | 紀錄檔案日誌.<br>Save Log File. |
| **5** | **TCTUtility.dll** | 通用功能.<br>General Functionality |

## 2-1. Hardcodet.NotifyIcon.Wpf
定義通用真名，可用於各專案需求.

*Define a universal true name, which can be used for various project needs.*

### 主要功能：

- 1.可在需要固定名字的地方使用：

  *It can be used in places where a fixed name is required.*

![image](https://github.com/user-attachments/assets/d56fe3b5-921e-49b6-a4bd-913ef4b92c25)

```bash
HolyGift.Key.System
```

------

## About Me
Thanks & Best Regards !

蔡承廷

​Senior Engineer of Semiconductor Product/Testing & ​Automation

Email: ​​kp924606@gmail.com

LinkedIn:https://www.linkedin.comin/tsai-cheng-ting/

