   M S B A M L   `   `   `  ���� K  GVisualCaptureApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:VisualCaptureApp.ViewVisualCaptureApp.View  ,    &VisualCaptureApp.View.MessageBoxWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:VisualCaptureApp.View     -     � Title$  MessageBoxWindow�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency$
 True��
�������$��Consolas)�$��6q����� *WindowHeight+ �� Mode$ TwoWay=����� *WindowWidth+$ TwoWay=����� *GridTitleColor+$ TwoWay=� � 	Resources    ButtonHoverEffect �� 
TargetType �� Property �� Value	 �� ScaleX
 �� ScaleY �� RelativeSource �� 
TargetType ��Name �� CornerRadius �� Padding ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        �� " ��.���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� �� $	 1Q�$
 1Q���  � ��
 �   ���  � �� �� *Tag+ �� *Self+��  � �� �� " ��.R��� $ border��#��z��$ 5��$ 5q�._��� $ contentPresenter��$��Center=�$��Center=�#��z�� P�  �@J�IsMouseOver$
 True��. ��� $ border�� � ��
 �   ��� $ contentPresenter�� � ��
 �������  � z� �� $
	 0.90Q�$

 0.90Q�.���  � ColumnDefinitions �� ���� *WindowWidth+$ TwoWay=�.+�� $��0�� � RowDefinitions �� $��60 ��� ���� *GridContentHeight+$ TwoWay=��� $��80 �.+�� -    �Name$ 
Grid_Title��$��0������ *GridTitleColor+$ TwoWay=�.+��� $��Stretch=�$��Stretch=�$��10,5,5,5q� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ TextBlock_Title��$��14(�$
��Bold"�$��Stretch=�$��Center=�$
��Wrap=����� *WindowTitle+$ TwoWay=�� -   $ Grid_Content��$��1�����#F6F6F6.+��� $��Center=�$��Center=�$��10,5,5,5q�$ 
Horizontal=�.+�� .+��� -    ��Name$ Button_ImageData��$��45��$��45��$��Center=�$��Center=�$��2q�$��10,0,0,0q����� *GridTitleColor+$ TwoWay=����� *GridTitleColor+$ TwoWay=����� $��Uniform=�$��0,0,0,0q����� *TypeImageSourcePath+$ TwoWay=�$��Center=�$��Center=����� " ��.���  � �� �� " ��.R��� #��z��#��z��#��z��$ 25��._��� $��Center=�$��Center=��� -    ��Name$ ScrollViewer_Content��$
��Auto=�$��10,0,0,0q�.���� -    ��Name$ TextBox_Content��$
��Auto��$��16(�$
��Bold"�$��Center=�$��Center=�$��5,5,5,5q� �� TextWrapping$
 Wrap=� �� TextAlignment$ Center=� �� 
IsReadOnly$
 True��$��0q�
������ ���� *TextBlockWidth+$ TwoWay=����� *WindowContent+$ TwoWay=�� -   $ Grid_ButtonFunction��$��2�����#e6e6e6.+�� $��Center=�$��Center=�.+�A� -	     A�Name$  UniformGridButton��! A� Rows$! 1��$��Center=�.+��� -
   $ 
Button_Yes��$	��Yes��$	��120��$��50��$��Center=�$��Center=�$��0��$��	10,0,10,6q�$��18(�$
��Bold"����#42dc42
" � Tag$" #42dc42��
���   �#��[  �� -   $ 	Button_No��$��No��$	��120��$��50��$��Center=�$��Center=�$��1��$��	10,0,10,6q�$��18(�$
��Bold"����#dc4242$" #dc4242��
�������#��[  �� -   $ Button_Close��$��Close��$	��120��$��50��$��Center=�$��Center=�$��2��$��	10,0,10,6q�$��18(�$
��Bold"����#888888$" #888888��
�������#��[  