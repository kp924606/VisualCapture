   M S B A M L   `   `   `  ����K  GVisualCaptureApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:VisualCaptureApp.ViewVisualCaptureApp.View  5   	   (    "VisualCaptureApp.View.SplashScreen   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:VisualCaptureApp.View     5   	     � Title$  SplashScreen��5   	   $	��220��6   $	��800��6+    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � AllowsTransparency$
 True��5   	   ���#f5e6c8$��Consolas)�5   	   ���#FFAA00$��1q�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��400 �6   5      �� 5      $	��400 �6   5      .+�5   
   �� $��1��6   ���#c8ffff$��Stretch=�63   $��Stretch=�6Q   �� 5   
   $��0��6   $
��Left=�6%   $��Center=�6@   $��10,20,10,20q�6[   .+�5      � .+�5      �� $��/image/TCT_Icon-1.png��6   $	��128��67   $	��128��6C   $
��Left=�6P   �� 5      $��VisualCaptureApp��6   $��26(�64   $
��Bold"�6B   $
��Left=�6T   $��Center=�6o   $��10,5,5,5q�6�   6�   5       �� 5!      $��
Loading...��6   $��16(�6*   $��Light"�68   $
��Left=�6K   $��Center=�6f   $��0,15,0,2q�6�   6�   5#      �� 5%   
   -   5         ��Name$ progressBarBackground��5%      $��10��6^   
�������$
��Left=�6{   $��Center=�6�   $��10,185,10,5q�6�   ��63   �� *WindowWidth+ �� Mode$ TwoWay=�6�   �� 5(   
   -   5        $ progressBarRectangle��5(      $��0��62   $��10��6<   
������$
��Left=�6Z   $��Center=�6u   $��10,185,10,5q�6�   �� 5*   
   $��1��6   $
��Left=�6%   $��Center=�6@   $��
0,10,10,10q�6[   .+�5+      �� $��	蔡承廷��6   $��20(�6#   $
��Bold"�61   $
��Left=�6C   $��Center=�6^   $��10,0,0,5q�6y   6�   �� 5,      $H��BSenior Engineer of Semiconductor Product/Testing And ​Automation��6   $��14(�6`   $
��Bold"�6n   $
��Left=�6�   $��Center=�6�   $��10,0,0,5q�6�   $
��Wrap=�6�   6�   �� 5-      $$��Email:​​kp924606@gmail.com��6   $��14(�6:   $
��Bold"�6H   $
��Left=�6Z   $��Center=�6u   $��10,0,0,5q�6�   6�   �� 5.      $:��4LinkedIn:https://www.linkedin.comin/tsai-cheng-ting/��6   $��14(�6T   $
��Bold"�6b   $
��Left=�6t   $��Center=�6�   $��10,0,0,5q�6�   $
��Wrap=�6�   6�   �� 5/      $�� ��6   $��14(�6    $
��Bold"�6.   $
��Left=�6@   $��Center=�6[   $��10,0,0,2q�6v   $
��Wrap=�6�   6�   �� 51      $'��!Copyright © TCT Rights Reserved.��6   $��14(�6@   $
��Bold"�6N   $
��Left=�6`   $��Center=�6{   $��10,0,0,2q�6�   $
��Wrap=�6�   6�   53      55      56      