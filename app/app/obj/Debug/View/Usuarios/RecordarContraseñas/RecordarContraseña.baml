   M S B A M L   `   `   `  ����>  :app, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null_4clr-namespace:app.View.Usuarios.RecordarContraseñas&app.View.Usuarios.RecordarContraseñas  5   	   @    :app.View.Usuarios.RecordarContraseñas.RecordarContraseña   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Flocal4clr-namespace:app.View.Usuarios.RecordarContraseñas     5   	     � Title$  RecordarContraseña��5   	   $	��600��6$   $
��1000��61    � WindowStyle$
 none=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � 
ResizeMode$ NoResize=�5   	    � AllowsTransparency . 
������  � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ ../../../Dictionary1.xaml?�6%   %    5      .��5      � .+�5   
   ��  �� CornerRadius$ 20��6   
�������$��5q�66   ��5      �� 5      	 �� 
StartPoint$		 0,0&�6&   
 �� EndPoint$

 0 ,1&�67   .��5      �  � Color$ #CD6CE0��6#    � Offset$ 1Q�63   6@   � 5      $ #FFFFFF��6#   $
 0.75Q�63   6C   � 5      $ #8C52FF��6#   $	 0.0Q�63   6B   5      ._�5      �  � ColumnDefinitions 5       �� 5!      �� 5"      .+�5%      �� $��30,20q�6   .+�5&      ��  �� Orientation$ 
Horizontal=�6!   .+�5'      �� $ 
Horizontal=�6%   $��Center=�6>   .+�5(      �� $.��(/View/Usuarios/LogIn/logo_night_days.png��6$   $	��100��6V   �� 5)      $��Hotel Night Day`s��6(   $��
Montserrat)�6A   $
��Bold"�6Y   $��39(�6k   $��Center=�6y   5*      5+      �� 5,      $ 
Horizontal=�6!   $��	0,10,0,20q�6:   .+�5-      �� 
���   �$��80��65   $��2q�6@   $��
0,20,20,20q�6T   6j   �� 5.      $��Recordar Contraseña��6$   $��Center=�6?   $��25(�6Z      ColorPrimario#��[  6h     TextBlockGeneral#��[ 6�   5/      �� 50      .+�51      �� $��Email��6$   $
��0,10q�61   #��[ 6?   �� 52      -   5        $��txtEmail��52   "     TextBoxGeneral#��[ 62   6Z   �� 53      -   5         ��Name$ ErrorTextEmail��53   $   $��* Email no válido��6<     	ErrorText#��[ 6U   �� 54      $��Confirmar Email��6$   $
��0,10q�6;   #��[ 6I   �� 55      -   5        $��txtEmailConfirmar��55   "   #��[ 6;   6c   �� 56      -   5        $ ErrorTextEmailConfirmar��56   $   $��* Email no válido��6E   #��[ 6^   �� 57      -   5        $ ErrorTextEquals��57   $   $��* Email no coincide��6=   #��[ 6X   5:      �� 5;      $
��Left=�6!   .+�5<      �� -   5        $��	btnEnviar��5<   !   {�.    ButtonLogin#��[ 6D    � Cursor$
 Hand��6�   5?      5@      �� 5A      -   5        $��Regresar al LogIn...��5A        LabelGeneral#��[ 6;   6a   5B      �� 5D      $��1��6   
������ $��0q�6H   $ 	0,20,20,0��6\   ��5E      �� 5F       �� ImageSource$3 -/View/Usuarios/RecordarContraseñas/lobby.jpg��6*    j� Stretch$ UniformToFill=�6e   5H      5J      5K      5L      5M      