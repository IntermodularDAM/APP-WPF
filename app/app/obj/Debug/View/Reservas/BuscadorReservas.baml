   M S B A M L   `   `   `  ����>  :app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:app.View.Reservasapp.View.Reservas  5   	   (    "app.View.Reservas.BuscadorReservas   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:app.View.Reservas     5   	   -   5          � Title$  BuscadorReservas��5   	   $	��600��6"   $
��1000��6/    � WindowStyle$
 none=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . 
������ .��5      � .+�5   
   ��  �� CornerRadius$ 20��6   
�������$��5q�66   ��5      �� 5       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 0,1&�67   .��5      �  � Color$ #5B4B8A��6#   	 � Offset$	 0Q�63   � 5      $ #B1A7D6��6#   $		 0.5Q�63   � 5      $ #D6D6E4��6#   $	 1Q�63   5      5      � 5   
   
 � RowDefinitions
 5      �� 5      �� 5      $��4* �6    �� 5      $��5* �6    .+�5"      ��  �� Orientation$ 
Horizontal=�6   $��0 15 0 0q�62   .+�5#      �� $��50��6   $��	20 0 20 0q�6#   $(��"/View/Reservas/Logo-Night-Days.png��66   �� 5$      $��
Night Days��6   $��32(�6.   $��50��6<   $
��Bold"�6H   ���#5B4B8A6Z   �� 5&      $��	20 0 90 0q�6   �� 5(      ���#5B4B8A$��0 0 0 3q�6/   $	��250��6I   ._�5)      �� $��Realice su Reserva��6    $��26(�6:   $	��210��6H   $��35��6T   ���#5B4B8A6`   5*      �� 5,      -   5        $��Volver��5,      $��40��6*   $	��160��66   $��	200 0 0 0q�6B   ���#8A73C1
����������#5B4B8A$��2q�6�   5-      �� 50      $��1��6   $	��950��6&   .+�51      �� $��	Buscador:��6   $��36(�6-   $
��Bold"�6;   ���#5B4B8A6M   �� 52       �� Padding$ 10q�6   $	��920��6&   ���#8A73C1$��2q�6H   $ 20��6\   ._�53      � .+�54      � $��0 0 0 0q�6$   .+�55      � $��Center=�6(   $	��350��6C   .+�56   "   �� $��Número de Huéspedes:��6(   $��18(�6G   $
��Bold"�6U   ���#5B4B8A6g   �� 57   "   $��50��6)   $��10 0 0 0q�64   
���   �$��2q�6Z   ._�58   &   �� -   5         ��Name$ txtHuespedes��58   0   $��1��6F   $��18(�6O   $��Center=�6]   $��Center=�6x   59   #   �� 5:   "   .+�5;   &   �� -   5        $��+��5;   -   69   �� 5<   &   -   5        $��-��5<   -   69   5=   #   5>      �� 5@      $	��280��6)   $��0 0 20 0q�65   .+�5A   "   �� $��Precio por Noche:��6(   $��18(�6D   $
��Bold"�6R   ���#5B4B8A6d   �� 5B   "   -   5         ��Name$ sliderPrecio��5B   )    � Minimum$ 0Q�6?    � Maximum$	 800Q�6K    �� TickFrequency$ 20Q�6Y    �� IsSnapToTickEnabled . 6�   �� 5C   "   -   5        $ 	txtPrecio��5C   ,   $��Right=�6?   $��Máximo: 0€��6[   $��16(�6m   $
��Bold"�6{   ���#5B4B8A$��0 10 0 0q�6�   5D      �� 5E      $	��200��6)   .+�5F   "   �� $��Extras:��6(   $��18(�6:   $
��Bold"�6H   ���#5B4B8A6Z   � 5G   "   .+�5H   &   �� -   5         ��Name$ chkCuna��5H   /   $
��Cuna��6@   $��14(�6O   $
��Bold"�6]   $��0 0 10 0q�6o   ���#5B4B8A6�   �� 5I   &   -	   5        $ chkCamaExtra��5I   /   $��
Cama Extra��6E   $��14(�6Z   $
��Bold"�6h   ���#5B4B8A6z   5J   #   5K      5L      � 5M      $	��550��6$   $��	10 0 50 0q�60   .+�5N      �� $��0 0 50 0q�6)   .+�5O   "   �� $��Fecha de Entrada:��6(   $��18(�6D   $
��Bold"�6R   ���#5B4B8A6d   (  "System.Windows.Controls.DatePicker  5P   "   -
   5          Name$ dpFechaEntrada��5P   -   $	��250��6E   $��50��6Q   ���#8A73C16]   5Q      �� 5S      .+�5T   "   �� $��Fecha de Salida:��6(   $��18(�6C   $
��Bold"�6Q   ���#5B4B8A6c     5U   "   -   5        $ dpFechaSalida��5U   -   $	��250��6D   $��50��6P   ���#8A73C16\   5V      5W      �� 5X      $ 
Horizontal=�6%   .+�5Y      �� -   5        $	��120��5Y   %   $��45��61   $��Ofertas��6=   $��16(�6O   $��0 0 20 0q�6]   ���#8A73C1
����������#5B4B8A$��2q�6�   6�   �� 5Z      -   5        $	��120��5Z   %   $��45��61   $��Buscar��6=   $��16(�6N   ���#8A73C1
����������#5B4B8A$��2q�6�   6�   5[      5\      5]      5^      �� 5`      $��2��6   $	��950��6&   .+�5a      �� $��Resultados:��6   $��36(�6/   $
��Bold"�6=   ���#5B4B8A6O   &   System.Windows.Controls.DataGrid  5c      -   5          Name$ listResultados��5c      $	��225��63      AutoGenerateColumns .     CanUserAddRows .  ��6@   �� *AllHabitaciones+   RowStyle 5d      �� 5e       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6    .�5h      ��  �� Property � �� �� Value �	#FFF1F1F16;   �� 5i       � ��$
 10,8q�67   �� 5j       � ��$ 70��67   �� 5k       � ��$ Medium"�6;    �� Triggers 5p      �� 5q   "    �� Binding 6.   �� *_id+ �� Value  6F   .a�5r   &   ��  � y�$ 	Collapsed=�6C   5s   #   �� 5t   "    6.   �� *_id+ 6F   Z� .a�5u   &   ��  � y�$ 	Collapsed=�6C   5v   #   P� 5w   "    P� Property �@J�IsMouseOver  P� Value  . . �5x   &   ��  � �� �	#FFFDD7FF6C   5z   #   P� 5{   "    �@ 
IsSelected  . . �5|   &   ��  � �� �	#FFFDD7FF6C   �� 5}   &    � �� �#CD6CE06D   �� 5~   &    � ��$ 1q�6H   5   #   P� 5�   "    �@ AlternationIndex$  1��6F   . �5�   &   ��  � ��
 �����6C   5�   #   5�      !   Columns! 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn"   Width5  /System.Windows.Controls.DataGridLengthConverter$	" 125 6-   #   Header$# ID��69   1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 6E   �� *_id+%   	CellStyle% 5�      �� 5�   "   *  $System.Windows.Controls.DataGridCell"  6(   .�5�   &   ��  � ��$ Center=�6J   �� 5�   &    � ��$ Center=�6L   �� 5�   &    � ��$ 20(�6A    5�   &   P� 5�   *    �@J�IsMouseOver  . . �5�   .   ��  � �� �	#FFFDD7FF6K   5�   +   P� 5�   *    �@ 
IsSelected  . . �5�   .   ��  � �� �	#FFFDD7FF6K   �� 5�   .    � ��
 ���� 6L   �� 5�   .    � ��$ 1q�6P   �� 5�   .    � ��
 �   �6K   5�   +   5�   #   5�        5�      $	" 125 6-   $# Nombre��69   $ 6I   �� *nombre+% 5�      �� 5�   "   "  6(   .�5�   &   ��  � ��$ Center=�6J   �� 5�   &    � ��$ Center=�6L   �� 5�   &    � ��$ 20(�6A    5�   &   P� 5�   *    �@J�IsMouseOver  . . �5�   .   ��  � �� �	#FFFDD7FF6K   5�   +   P� 5�   *    �@ 
IsSelected  . . �5�   .   ��  � �� �	#FFFDD7FF6K   �� 5�   .    � ��
 ���� 6L   �� 5�   .    � ��$ 1q�6P   �� 5�   .    � ��
 �   �6K   5�   +   5�   #   5�        5�      $	" 125 6-   $
# Tipo��69   $ 6G   �� *tipo+% 5�      �� 5�   "   "  6(   .�5�   &   ��  � ��$ Center=�6J   �� 5�   &    � ��$ Center=�6L   �� 5�   &    � ��$ 20(�6A    5�   &   P� 5�   *    �@J�IsMouseOver  . . �5�   .   ��  � �� �	#FFFDD7FF6K   5�   +   P� 5�   *    �@ 
IsSelected  . . �5�   .   ��  � �� �	#FFFDD7FF6K   �� 5�   .    � ��
 ���� 6L   �� 5�   .    � ��$ 1q�6P   �� 5�   .    � ��
 �   �6K   5�   +   5�   #   5�        5�      $	" 125 6-   $# Precio por Noche��69   $ 6S   �� *precio_noche+& �� StringFormat$& C��% 5�      �� 5�   "   "  6(   .�5�   &   ��  � ��$ Center=�6J   �� 5�   &    � ��$ Center=�6L   �� 5�   &    � ��$ 20(�6A    5�   &   P� 5�   *    �@J�IsMouseOver  . . �5�   .   ��  � �� �	#FFFDD7FF6K   5�   +   P� 5�   *    �@ 
IsSelected  . . �5�   .   ��  � �� �	#FFFDD7FF6K   �� 5�   .    � ��
 ���� 6L   �� 5�   .    � ��$ 1q�6P   �� 5�   .    � ��
 �   �6K   5�   +   5�   #   5�        5�      $	" 125 6-   '   
Visibility$' 	Collapsed=�69   $# Precio Original��6P   $ 6i   �� *precio_noche_original+$& C��% 5�      �� 5�   "   "  6(   .�5�   &   ��  � ��$ Center=�6J   �� 5�   &    � ��$ Center=�6L   �� 5�   &    � ��$ 20(�6A    5�   &   P� 5�   *    �@J�IsMouseOver  . . �5�   .   ��  � �� �	#FFFDD7FF6K   5�   +   P� 5�   *    �@ 
IsSelected  . . �5�   .   ��  � �� �	#FFFDD7FF6K   �� 5�   .    � ��
 ���� 6L   �� 5�   .    � ��$ 1q�6P   �� 5�   .    � ��
 �   �6K   5�   +   5�   #   5�      4	  .System.Windows.Controls.DataGridTemplateColumn	  5�      $	" 250 61   ( 	  CellTemplate( 5�      �� 5�   "   .R�5�   &   �� $��Center=�61   .+�5�   *   �� -   5        $��Reservar��5�   K   $	��120��6^   $��40��6j   ���#8A73C1
����������#5B4B8A$��2q�6�   ��6�   �� *DataContext.ReservarComando+) �� RelativeSource) �� * �� AncestorType$* WindowL���6-  �� 5�   +   5�   '   5�   #   5      5     5     5     5     5     