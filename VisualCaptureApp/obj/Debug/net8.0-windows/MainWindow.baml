   M S B A M L   `   `   `  ����K  GVisualCaptureApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:VisualCaptureAppVisualCaptureApp  5   	   !    VisualCaptureApp.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:VisualCaptureApp     5   	   _ [Hardcodet.NotifyIcon.Wpf, Version=2.0.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)ni http://www.hardcodet.net/taskbar  66   -   5          � Title$  VisualCapture��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency$
 True��5   	   ���#f9e6c8���#FFAA00$��1q�5   	   ��5   	   �� *WindowHeight+ �� Mode$ TwoWay=���5   	   �� *WindowWidth+$ TwoWay=�.��5      � -   5         �Name$ MainGrid��5       � RowDefinitions 5   
   �� 5      -   5         ��Name$ FunctionRow��5   Q   ��6   �� *FunctionButtonHeight+$ OneWay=�6Q   �� 5      -   5        $ 
GeneralRow��5   N   ��6   �� *GeneralViewHeight+$ OneWay=�6N   �� 5      -   5        $ AboutMe��5   (   $��60 �6   6(   	 � ColumnDefinitions	 5   
   �� 5!      ��6   �� *FunctionButtonWidth+$ OneWay=��� 5$      ��6   �� *ScreenshotComboxWidth+$ OneWay=��� 5'      ��6   �� *HideACloseTotalWidth+$ OneWay=�.+�5+   
   �� $��0��6   $��0��6   
������ 
������ $��0q�6a   
 �� Padding$
 1q�6u   $��5,7,0,0q�6�   ._�5,      �� .+�5-      �� -   5         ��Name$ Button_Deatail��5-      $��Center=�5/      $��Center=�50      
��� ���
������ $��0q�52   +   ��5-   1   �� *FunctionButtonWidth+$ OneWay=���5.      �� *FunctionButtonHeight+$ OneWay=���54      �� 55      $��Uniform=�56      $��0,2,0,2q�6*   ��55       �� *PullButtonImageSourcePath+$ TwoWay=�z�57      �� 58   "    �� ScaleX$
 0.85Q�61    �� ScaleY$
 0.85Q�6?    J� RenderTransformOrigin 5:      8� 5;   "    8� X$	 0.5Q�6(    8� Y$	 0.5Q�60   $��Center=�5>   Z   $��Center=�5?   V   5@      5B      5C      5D      �� 5G   
   $��0��6   $��1��6   
������ 
������ $��0q�6a   $
 1q�6u   $��1,7,0,0q�6�   ._�5H      �� $��1��6   $��2��6&    �� Orientation$ 
Horizontal=�6:   $��Center=�6S   $��Center=�6p   .+�5J      �� -   5        $ Button_ScreenShotFunction��5J      $��Center=�5M      $��Center=�5N      
��� ���
������ $��0q�5P   +   ��5K      �� *FunctionButtonWidth+$ OneWay=���5L      �� *FunctionButtonHeight+$ OneWay=���5R      �� 5S      $��Uniform=�5T      $��1,1,1,1q�6*   ��5S       �� *DefaultFutrueImageSourcePath+$ TwoWay=�$��Center=�5U   Z   $��Center=�5V   V   5W      5Y      �� 5[      -   5         ��Name$ ComboxCaptureFunction��5[      $��0,0,0,0q�6�   $��Calibri)�6�   $��12(�6�   
��� �����6:   �� *ComboxCaptureFunctionWidth+$ OneWay=���5\      �� 5]      .R�5^      �� $
��Wrap=�6>   ��6(   �� *Name+6>   5_      5a      5c      5d      �� 5g   
   $��0��6   $��2��6   
������ 
������ $��0q�6a   $
 0q�6u   $��1,7,1,2q�6�   ._�5h      �� $��1��6   $��1��6&   $ 
Horizontal=�6:   $��Center=�6S   $��Center=�6p   .+�5j      �� -	   5        $ Button_HideWindow��5j      $��Center=�5m      $��Center=�5n      
��� ���
������ $��0q�5p   3   $��0,0,0,0q�5r      ��5k      �� *FunctionButtonWidth+$ OneWay=���5l      �� *FunctionButtonHeight+$ OneWay=���5t      �� 5u      $��Uniform=�6T   $��0,0,0,0q�6f   ��6    �� *HideImageSourcePath+$ OneWay=�z�5v      �� 5w   "   $
 0.90Q�61   $
 0.90Q�6?    5y      8� 5z   "   $	 0.5Q�6(   $	 0.5Q�60   $��Center=�5}   Z   $��Center=�5~   V   5      5�      �� 5�      -
   5        $ Button_CloseWindow��5�      $��Center=�5�      $��Center=�5�      
������ $��1q�5�      $��5,0,0,0q�5�      ��5�      �� *FunctionButtonWidth+$ OneWay=���5�      �� *FunctionButtonHeight+$ OneWay=���5�      �� 5�       �� 
TargetType" ��6    .�5�      ��  �� Property � �� �� Value 5�   "   �� 5�   &    �� 
TargetType" ��66   .R�5�   *   �� ���#ff3232._�5�   .   �� $��Center=�6?   $��Center=�6\   5�   +   5�   '   5�      5�      ��5�      �� 5�      $��Uniform=�6[   $��0,0,0,0q�6m   ��6    �� *CloseButtonImageSourcePath+$ OneWay=�z�5�      �� 5�   "   $
 0.80Q�61   $
 0.80Q�6?    5�      8� 5�   "   $	 0.5Q�6(   $	 0.5Q�60   $��Center=�5�   Z   $��Center=�5�   V   5�      5�      5�      5�      �� 5�   
   $��1��6   $��3��6"   $��7,7,2,0q�5�      $��Stretch=�5�      
������ ���#999999$��1q�5�      .@�5�      �� $��General��6   .��5�      �� -   5         ��Name$ GeneralPanel��5�      
������ ���#999999$��0q�6^   $
 0q�6r   $y�	Collapsed=�6~   $��0,0,0,0q�6�   $	��Top=�6�   ._�5�      �� $
��Auto=�6#   .��5�      �� .+�5�      �� $��Stretch=�6)   $	��Top=�6G   $��0,0,0,0q�6_   $ 
Horizontal=�6p   .+�5�   "   � .+�5�   &   �� $!��SaveFolder/儲存資料夾:��60   $��12(�6I   $
��Bold"�6W   $��5,0,0,0q�6i   $��Center=�6z   $��Center=�6�   �� 5�   &   -   5        
��� ���
������ $��0q�5�   1   $��5,0,0,0q�5�   1   ��5�   -   �� *FunctionSamllButtonWidth+$ OneWay=���5�   1   �� *FunctionSamllButtonHeight+$ OneWay=���5�   *   �� 5�   .   $��Uniform=�6p   $��0,0,0,0q�6�   ��64   �� *FolderSelectImageSourcePath+$ OneWay=�z�5�   2   �� 5�   6   $
 1.00Q�6E   $
 1.00Q�6S   $��Center=�5�   n   $��Center=�5�   j   5�   /   5�   '   �� 5�   &   -   5        $��Center=�5�   -   $��Center=�5�   -   
��� ���
������ $��0q�5�   G   $��15,0,0,0q�5�   -   ��5�   -   �� *FunctionSamllButtonWidth+$ TwoWay=���5�   -   �� *FunctionSamllButtonHeight+$ TwoWay=���5�   *   �� 5�   .   $��Uniform=�6n   $��0,0,0,0q�6�   ��64   �� *FolderOpenImageSourcePath+$ OneWay=�z�5�   2   �� 5�   6   $
 1.00Q�6E   $
 1.00Q�6S   $��Center=�5�   n   $��Center=�5�   j   5�   /   5�   '   5�   #   5�      �� 5�      $��Stretch=�6)   $	��Top=�6G   $��0,0,2,0q�6_   $ Vertical=�6p   .+�5�   "   � .+�5�   &   �� -   5         ��Name$ TextBoxGeneralSaveFolder��5�   .   $
��Auto��5�   1   $��5,0,0,0q�5�   1    �� TextWrapping$ NoWrap=�5�   1   $��Center=�5�   1   $��14(�5�   1   $��Stretch=�5�   1   ��5�   1   �� *TextButtonHeight+$ OneWay=���5�   1   �� *DefaultSaveFolderPath+$ TwoWay=�5�   1   5�   #   5�      �� 5�      $��1��6%   ���#BBBBBB$��1,3,1,0q�6E   �� 5�      $��Stretch=�6)   $	��Top=�6G   $��0,0,2,0q�6_   $ Vertical=�6p   .+�5   "   � .+�5  &   �� $$��WindowTopMost/視窗最前景:��60   $��12(�6L   $
��Bold"�6Z   $��5,5,0,0q�6l   $��Center=�6}   $��Center=�6�   5  #   � 5  "   .+�5  &   �� -   5        $�� ��5  /   $��5,5,0,0q�5  3    d� 	IsChecked 5  3   �� *IsWindowTopMost+$ TwoWay=���5  *   �� 5  .   " ��64   .�5  2   ��  � ��$ Center=�6X   �� 5  2    � ��$ Center=�6V   �� 5  2    �@d�IsThreeState$ False��6Q   �� 5  2    � |�$ False��6N    �� Triggers 5  2   �� 5  6    �� Binding 6B   �� *	IsChecked+ �� RelativeSource �� *Self+ �� Value True6�   .a�5  :   ��  � ��
 ����6W   5  7   5  /    � LayoutTransform 5  *   �� 5  .   $	 1.5Q�6=   $	 1.5Q�6J   5  '   5  #   5     �� 5"     $��1��6%   ���#BBBBBB$��1,3,1,0q�6E   �� 5%     $��Stretch=�6)   $	��Top=�6G   $��0,0,2,0q�6_   $ Vertical=�6p   .+�5&  "   � .+�5(  &   �� $'��!Keyboard Shortcut/鍵盤快速鍵��60   $��12(�6O   $
��Bold"�6]   $��5,5,0,0q�6o   $��Center=�6�   $��Center=�6�   �� 5)  &   $��Start&End/開始&結束:��60   $��12(�6P   $
��Bold"�6^   $��5,5,0,0q�6p   $��Center=�6�   $��Center=�6�   5+  #   5,     �� 5/     $��Stretch=�6)   $	��Top=�6G   $��5,5,2,0q�6_   $ 
Horizontal=�6p   .+�51  "   �� -   5        $$ ComboxKeyboardShortcutStartEnd��51  +     �� Text$   ��6S   $��0��6[   $��Stretch=�6k   ! �� SelectedIndex$! 0��6�   $��14(�6�   $
��Bold"�6�   ��6�   �� *DefaultComboxInputWidth+$ TwoWay=���52  &   �� 53  *   .R�54  .   �� $
��Wrap=�6U   ��68   �� *Description+6U   55  +   5:  #   �� 5<  "   -   5        " ��Name$'" !CheckBox_UseShortcutkeyScreenshot��5<  +   $�� ��6V   $��5,5,0,0q�5=  )   ��5A  &   �� 5B  *   " ��60   .�5C  .   ��  � ��$ Center=�6T   �� 5D  .    � ��$ Center=�6R   �� 5E  .    �@d�IsThreeState$ False��6M   �� 5F  .    � |�$ False��6J    5G  .   �� 5H  2    6>   �� *	IsChecked+ �� *Self+ True6�   .a�5I  6   ��  � ��
 ����6S   5J  3   5L  +    5O  &   �� 5P  *   $	 1.5Q�69   $	 1.5Q�6F   5R  #   5S     �� 5V     $��1��6%   ���#BBBBBB$��1,3,1,0q�6E   �� 5Y     $��Stretch=�6)   $	��Top=�6G   $��0,0,2,0q�6_   $ Vertical=�6p   .+�5Z  "   � 5\  #   5]     5_     5a     5b     5c     �� 5f     $��Setting��6   .��5h     �� -   5        $ SettingsPanel��5h     
������ 
��� ���$��0q�6^   $
 1q�6r   $y�	Collapsed=�6~   $��0,0,0,0q�6�   $��Stretch=�6�   ._�5i     �� $
��Auto=�6#   $
��Auto=�6F   .��5j      � -   5        #  �Name$# DetailFrame��5j      $  � NavigationUIVisibility$$ Hidden=�65   5k     5l     5n     5o     �� 5q  
   $��2��6   $��3��6"   $��Center=�66   $	��Top=�6S   $��0,0,0,0q�6k   $ Vertical=�6|   
������ .+�5r     �� $��	蔡承廷��6   $��12(�6#   $
��Bold"�61   $��Center=�6C   $��Center=�6`   $��0,3,0,0q�6{   6�   �� 5s     $$��Email:​​kp924606@gmail.com��6   $��12(�6:   $
��Bold"�6H   $��Center=�6Z   $��Center=�6w   $��0,2,0,0q�6�   6�   5t     3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  5w  
   -   5        %  Name$% MyNotifyIcon��5w     &   
IconSource$& image/TCT_Icon-1.ico��5x     '   ToolTipText$' VisualCapture��5y     $y�Hidden=�5z     5{     5~     5     