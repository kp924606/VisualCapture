   M S B A M L   `   `   `  ���� K  GVisualCaptureApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:VisualCaptureAppVisualCaptureApp  !    VisualCaptureApp.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:VisualCaptureApp     _ [Hardcodet.NotifyIcon.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)ni http://www.hardcodet.net/taskbar  -     � Title$  VisualCapture�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � AllowsTransparency$
 True�����#f9e6c8���#FFAA00$��1q����� *WindowHeight+ �� Mode$ TwoWay=����� *WindowWidth+$ TwoWay=�.��� -    �Name$ MainGrid�� � RowDefinitions �� -    ��Name$ FunctionRow������ *FunctionButtonHeight+$ OneWay=��� -   $ 
GeneralRow������ *GeneralViewHeight+$ OneWay=��� -   $ AboutMe��$��60 �	 � ColumnDefinitions	 �� ���� *FunctionButtonWidth+$ OneWay=��� ���� *ScreenshotComboxWidth+$ OneWay=��� ���� *HideACloseTotalWidth+$ OneWay=�.+��� $��0��$��0��
������ 
������ $��0q�
 �� Padding$
 1q�$��5,7,0,0q�._��� .+��� -    ��Name$ Button_Deatail��$��Center=�$��Center=�
��� ���
������ $��0q����� *FunctionButtonWidth+$ OneWay=����� *FunctionButtonHeight+$ OneWay=����� $��Uniform=�$��0,2,0,2q����� *PullButtonImageSourcePath+$ TwoWay=�z���  �� ScaleX$
 0.85Q� �� ScaleY$
 0.85Q� J� RenderTransformOrigin 8�  8� X$	 0.5Q� 8� Y$	 0.5Q�$��Center=�$��Center=��� $��0��$��1��
������ 
������ $��0q�$
 1q�$��1,7,0,0q�._��� $��1��$��2�� �� Orientation$ 
Horizontal=�$��Center=�$��Center=�.+��� -   $ Button_ScreenShotFunction��$��Center=�$��Center=�
��� ���
������ $��0q����� *FunctionButtonWidth+$ OneWay=����� *FunctionButtonHeight+$ OneWay=����� $��Uniform=�$��1,1,1,1q����� *DefaultFutrueImageSourcePath+$ TwoWay=�$��Center=�$��Center=��� -    ��Name$ ComboxCaptureFunction��$��0,0,0,0q�$��Calibri)�$��12(�
��� ������� *ComboxCaptureFunctionWidth+$ OneWay=����� .R��� $
��Wrap=����� *Name+�� $��0��$��2��
������ 
������ $��0q�$
 0q�$��1,7,1,2q�._��� $��1��$��1��$ 
Horizontal=�$��Center=�$��Center=�.+��� -	   $ Button_HideWindow��$��Center=�$��Center=�
��� ���
������ $��0q�$��0,0,0,0q����� *FunctionButtonWidth+$ OneWay=����� *FunctionButtonHeight+$ OneWay=����� $��Uniform=�$��0,0,0,0q����� *HideImageSourcePath+$ OneWay=�z��� $
 0.90Q�$
 0.90Q� 8� $	 0.5Q�$	 0.5Q�$��Center=�$��Center=��� -
   $ Button_CloseWindow��$��Center=�$��Center=�
������ $��1q�$��5,0,0,0q����� *FunctionButtonWidth+$ OneWay=����� *FunctionButtonHeight+$ OneWay=�����  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R��� ���#ff3232._��� $��Center=�$��Center=����� $��Uniform=�$��0,0,0,0q����� *CloseButtonImageSourcePath+$ OneWay=�z��� $
 0.80Q�$
 0.80Q� 8� $	 0.5Q�$	 0.5Q�$��Center=�$��Center=��� $��1��$��3��$��7,7,2,0q�$��Stretch=�
������ ���#999999$��1q�.@��� $��General��.���� -    ��Name$ GeneralPanel��
������ ���#999999$��0q�$
 0q�$y�	Collapsed=�$��0,0,0,0q�$	��Top=�._��� $
��Auto=�.���� .+��� $��Stretch=�$	��Top=�$��0,0,0,0q�$ 
Horizontal=�.+�� .+��� $!��SaveFolder/儲存資料夾:��$��12(�$
��Bold"�$��5,0,0,0q�$��Center=�$��Center=��� -   
��� ���
������ $��0q�$��5,0,0,0q����� *FunctionSamllButtonWidth+$ OneWay=����� *FunctionSamllButtonHeight+$ OneWay=����� $��Uniform=�$��0,0,0,0q����� *FolderSelectImageSourcePath+$ OneWay=�z��� $
 1.00Q�$
 1.00Q�$��Center=�$��Center=��� -   $��Center=�$��Center=�
��� ���
������ $��0q�$��15,0,0,0q����� *FunctionSamllButtonWidth+$ TwoWay=����� *FunctionSamllButtonHeight+$ TwoWay=����� $��Uniform=�$��0,0,0,0q����� *FolderOpenImageSourcePath+$ OneWay=�z��� $
 1.00Q�$
 1.00Q�$��Center=�$��Center=��� $��Stretch=�$	��Top=�$��0,0,2,0q�$ Vertical=�.+�� .+��� -    ��Name$ TextBoxGeneralSaveFolder��$
��Auto��$��5,0,0,0q� �� TextWrapping$ NoWrap=�$��Center=�$��14(�$��Stretch=����� *TextButtonHeight+$ OneWay=����� *DefaultSaveFolderPath+$ TwoWay=��� $��1�����#BBBBBB$��1,3,1,0q��� $��Stretch=�$	��Top=�$��0,0,2,0q�$ Vertical=�.+�� .+��� $$��WindowTopMost/視窗最前景:��$��12(�$
��Bold"�$��5,5,0,0q�$��Center=�$��Center=�� .+��� -   $�� ��$��5,5,0,0q� d� 	IsChecked �� *IsWindowTopMost+$ TwoWay=����� " ��.���  � ��$ Center=���  � ��$ Center=���  �@d�IsThreeState$ False����  � |�$ False�� �� Triggers ��  �� Binding �� *	IsChecked+ �� RelativeSource �� *Self+ �� Value True.a���  � ��
 ���� � LayoutTransform �� $	 1.5Q�$	 1.5Q��� $��1�����#BBBBBB$��1,3,1,0q��� $��Stretch=�$	��Top=�$��0,0,2,0q�$ Vertical=�.+�� .+��� $'��!Keyboard Shortcut/鍵盤快速鍵��$��12(�$
��Bold"�$��5,5,0,0q�$��Center=�$��Center=��� $��Start&End/開始&結束:��$��12(�$
��Bold"�$��5,5,0,0q�$��Center=�$��Center=��� $��Stretch=�$	��Top=�$��5,5,2,0q�$ 
Horizontal=�.+��� -   $$ ComboxKeyboardShortcutStartEnd��  �� Text$   ��$��0��$��Stretch=�! �� SelectedIndex$! 0��$��14(�$
��Bold"����� *DefaultComboxInputWidth+$ TwoWay=����� .R��� $
��Wrap=����� *Description+�� -   " ��Name$'" !CheckBox_UseShortcutkeyScreenshot��$�� ��$��5,5,0,0q����� " ��.���  � ��$ Center=���  � ��$ Center=���  �@d�IsThreeState$ False����  � |�$ False�� ��  �� *	IsChecked+ �� *Self+ True.a���  � ��
 ���� �� $	 1.5Q�$	 1.5Q��� $��1�����#BBBBBB$��1,3,1,0q��� $��Stretch=�$	��Top=�$��0,0,2,0q�$ Vertical=�.+�� �� $��Setting��.���� -   $ SettingsPanel��
������ 
��� ���$��0q�$
 1q�$y�	Collapsed=�$��0,0,0,0q�$��Stretch=�._��� $
��Auto=�$
��Auto=�.�� � -   #  �Name$# DetailFrame��$  � NavigationUIVisibility$$ Hidden=��� $��2��$��3��$��Center=�$	��Top=�$��0,0,0,0q�$ Vertical=�
������ .+��� $��	蔡承廷��$��12(�$
��Bold"�$��Center=�$��Center=�$��0,3,0,0q��� $$��Email:​​kp924606@gmail.com��$��12(�$
��Bold"�$��Center=�$��Center=�$��0,2,0,0q�3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   %  Name$% MyNotifyIcon��&   
IconSource$& image/TCT_Icon-1.ico��'   ToolTipText$' VisualCapture��$y�Hidden=�