   M S B A M L   `   `   `  ����>  :app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:app.View.Reservasapp.View.Reservas  5   	   *    $app.View.Reservas.InformacionReserva   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:app.View.Reservas     5   	     � Title$  InformacionReserva��5      $	��600��6,   $
��1000��69    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	   
������  � AllowsTransparency .  � WindowStartupLocation$ CenterScreen=�5   	    � 	Resources 5         ColorPrimario  ColorSecundario  
ColorFondo  
ColorTexto  BrushColorPrimario �� Color  BrushColorSecundario  BrushColorFondo  BrushColorTexto��%?  &	        &	      &	 >     &	 ]     &	 |     7   &	 �     7  &	 �     7  &	 �     ��5   
   #5B4B8A��6/   ��5   
   #8A73C1��61   ��5   
   #2D2B3C��6,   ��5   
   #FFFFFF��6,   �� 5   
   8   65   �� 5   
   8   67   �� 5   
   8   62   �� 5   
   $ Black��62   5      .��5      � .+�5   
   ��  �� CornerRadius$ 20��6   ���	#CC000000$��5q�6:   ��5      �� 5       �� 
StartPoint$	 0,0&�6&   	 �� EndPoint$
	 0 ,1&�67   .��5       � 
 � Color#
 [ 6#    � Offset$ 1Q�6L   � 5!      $
 #FFFFFF��6#   $
 0.75Q�63   � 5"      #
 [  6#   $	 0.0Q�6J   5#      ._�5&      �� $��1��6   $	��265��6"   $	��500��6.   $ 20��6;   
���� ��$��8 5 8 5q�6b   ��5'      �� 5(      $ 	#FFD9D9D9��6&   ._�5*      �� $��Center=�6   $��Center=�6:   .+�5+      �� $��Reserva��6    $��32(�6/   $	��110��6=   $��0 0 0 30q�6I   ���#8A73C16r   �� 5,      -   5         ��Name$ 	txtNombre��5,       $��A nombre de:��63   $��Center=�6G   $��14(�6d   $��0,10,0,0q�6r   
����+��6�   �� 5-      -   5        $ 
txtUsuario��5-       $��Nombre de Usuario��64   $��Center=�6M   $��14(�6j   $��0,10,0,0q�6x   #��[ 6�   �� 5/      $��Habitación Reservada:��6    $��Center=�6=   $��14(�6Z   $��0,10,0,0q�6h   
����+��6z   �� 50      -   5        $ txtHabitacion��50       $��Nombre de la Habitación��67   $��Center=�6V   $��14(�6s   $��0,10,0,0q�6�   #��[ 6�   �� 52      $��Fecha de Entrada:��6    $��Center=�69   $��14(�6V   $��0,10,0,0q�6d   
����+��6v   �� 53      -   5        $ txtFechaEntrada��53       $��
10-02-2025��69   $��Center=�6K   $��14(�6h   $��0,10,0,0q�6v   #��[ 6�   �� 55      $��Fecha de Salida:��6    $��Center=�68   $��14(�6U   $��0,10,0,0q�6c   
����+��6u   �� 56      -   5        $ txtFechaSalida��56       $��
21-02-2025��68   $��Center=�6J   $��14(�6g   $��0,10,0,0q�6u   #��[ 6�   �� 58      $��Estado de la Reserva:��6    $��Center=�6=   $��14(�6Z   $��0,10,0,0q�6h   
����+��6z   �� 59      -   5        $ txtEstadoReserva��59       $��
Confirmada��6:   $��Center=�6L   $��14(�6i   $��0,10,0,0q�6w   #��[ 6�   �� 5;      -   5         ��Name$ 	btnCerrar��5;      $��0 20 0 0q�60   $��45��6B   $	��160��6N   $��Center=�5<      $��Cerrar��68   #��[ 5=      
���� ��$��3 2 3 4q�6c   $��18(�6}    5>       �� 
TargetType �� Property �� Value��%_   '��       �� 5?      " ��6$   .�5@   "   ��  �@��CornerRadius$ 20��6A   5A      5B      5C      5D      5E      5G      5H      5I      