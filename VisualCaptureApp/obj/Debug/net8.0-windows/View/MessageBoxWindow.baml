   M S B A M L   `   `   `  ����K  GVisualCaptureApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:VisualCaptureApp.ViewVisualCaptureApp.View  5   	   ,    &VisualCaptureApp.View.MessageBoxWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:VisualCaptureApp.View     5   	   -   5          � Title$  MessageBoxWindow��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	   
�������$��Consolas)�5   	   $��6q�5   	   ��5	   	   �� *WindowHeight+ �� Mode$ TwoWay=���5
   	   �� *WindowWidth+$ TwoWay=���5   	   �� *GridTitleColor+$ TwoWay=� � 	Resources 5         ButtonHoverEffect �� 
TargetType �� Property �� Value	 �� ScaleX
 �� ScaleY �� RelativeSource �� 
TargetType ��Name �� CornerRadius �� Padding ��Name �� Triggers P� Property P� Value �� 
TargetName��%L  &	        �� 5   
   " ��6*   .�5      ��  �@J�RenderTransformOrigin$ 0.5,0.5&�66   �� 5       � z� 5      �� 5      $	 1Q�6%   $
 1Q�60   5      �� 5       � ��
 �   �6+   �� 5       � �� 6+   �� *Tag+ �� *Self+�� 5       � �� 5      �� 5       " ��6&   .R�5!      �� $ border��6!   #��z��61   $ 5��6[   $ 5q�6l   ._�5"      �� $ contentPresenter��6/   $��Center=�5#   +   $��Center=�5$   +   #��z��5%   +   5&       5'      P� 5)       �@J�IsMouseOver$
 True��6=   . �5*   "   �� $ border��6)    � ��
 �   �6S   �� 5+   "   $ contentPresenter��6)    � ��
 �����6i   �� 5,   "    � z� 5-   &   �� 5.   *   $
	 0.90Q�69   $

 0.90Q�6G   50   #   51      53      55      56      57      .��59      �  � ColumnDefinitions 5:   
   �� 5;      ��6   �� *WindowWidth+$ TwoWay=�5<      .+�5?   
   � $��0��6    � RowDefinitions 5@      �� 5A      $��60 �6    �� 5B      ��6    �� *GridContentHeight+$ TwoWay=��� 5C      $��80 �6    .+�5F      � -   5         �Name$ 
Grid_Title��5F      $��0��6'   ��64   �� *GridTitleColor+$ TwoWay=�.+�5G      �� $��Stretch=�6   $��Stretch=�6;   $��10,5,5,5q�6W    �� Orientation$ 
Horizontal=�6i   .+�5I      �� -   5         ��Name$ TextBlock_Title��5I       $��14(�6c   $
��Bold"�6q   $��Stretch=�6�   $��Center=�6�   $
��Wrap=�6�   ��69   �� *WindowTitle+$ TwoWay=�6�   5J      5K      � 5M      -   5        $ Grid_Content��5M      $��1��6)   ���#F6F6F6.+�5N      �� $��Center=�6   $��Center=�6:   $��10,5,5,5q�6U   $ 
Horizontal=�6g   .+�5O      � .+�5P      �� -   5         ��Name$ Button_ImageData��5P   !   $��45��6;   $��45��5Q   !   $��Center=�5R   !   $��Center=�5S   !   $��2q�5V   !   $��10,0,0,0q�5W   !   ��5T   !   �� *GridTitleColor+$ TwoWay=���5U   !   �� *GridTitleColor+$ TwoWay=���5Y      �� 5Z   "   $��Uniform=�5[   )   $��0,0,0,0q�6;   ��5Z   (   �� *TypeImageSourcePath+$ TwoWay=�$��Center=�5\   b   $��Center=�5]   ^   5^   #   ��5`      �� 5a   "   " ��6(   .�5b   &   ��  � �� 5c   *   �� 5d   .   " ��6>   .R�5e   2   �� #��z��69   #��z��6c   #��z��6�   $ 25��6�   ._�5f   6   �� $��Center=�6G   $��Center=�6d   5g   3   5h   /   5j   '   5k   #   5m      �� 5o      -   5         ��Name$ ScrollViewer_Content��5o   '   $
��Auto=�6E   $��10,0,0,0q�6h   .��5r      �� -   5         ��Name$ TextBox_Content��5r   &   $
��Auto��6m   $��16(�5s   (   $
��Bold"�66   $��Center=�6H   $��Center=�6e   $��5,5,5,5q�6�    �� TextWrapping$
 Wrap=�6�    �� TextAlignment$ Center=�6�    �� 
IsReadOnly$
 True��6�   $��0q�6�   ��5r   ?   �� *TextBlockWidth+$ TwoWay=���6{   �� *WindowContent+$ TwoWay=�5s   �   5u      5v      5w      5x      � 5z      -   5        $ Grid_ButtonFunction��5z      $��2��60   ���#e6e6e6.+�5{      � $��Center=�6   $��Center=�64   .+�5|      A� -	   5          A�Name$  UniformGridButton��5|   "   ! A� Rows$! 1��6=   $��Center=�6F   .+�5~      �� -
   5        $ 
Button_Yes��5~   !   $	��Yes��6I   $	��120��6W   $��50��6c   $��Center=�6o   $��Center=�6�   $��0��6�   $��	10,0,10,6q�6�   $��18(�6�   $
��Bold"�6�   ���#42dc42
" � Tag$" #42dc42��6�   
���   �#��[  6   �� 5�      -   5        $ 	Button_No��5�   !   $��No��6G   $	��120��6T   $��50��6`   $��Center=�6l   $��Center=�6�   $��1��6�   $��	10,0,10,6q�6�   $��18(�6�   $
��Bold"�6�   ���#dc4242$" #dc4242��6�   
�������#��[  6  �� 5�      -   5        $ Button_Close��5�   !   $��Close��6M   $	��120��6]   $��50��6i   $��Center=�6u   $��Center=�6�   $��2��6�   $��	10,0,10,6q�6�   $��18(�6�   $
��Bold"�6�   ���#888888$" #888888��6  
�������#��[  6&  5�      5�      5�      5�      5�      5�      