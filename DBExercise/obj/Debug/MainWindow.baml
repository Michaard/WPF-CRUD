   M S B A M L   `   `   `  ����E  ADBExercise, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:DBExercise
DBExercise  5   	       DBExercise.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:DBExercise     5   	     � Title$  Test SQL w .Net��5   	   $	��350��6!   $	��800��6.   #��� ��6:   $��0q�6�   $��Disabled=�6�   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $ 	#FF095987��6   $ 1Q�6-   5      .��5      � $
��Left=�6   $	��320��6&   $��0,0,-195,-1q�63   $	��Top=�6H   $	��800��6`    J� RenderTransformOrigin$ 0.5,0.5&�6l   .+�5   
   �� -   5         ��Name$ 	ButtonAdd��5      $��Dodaj��6$   $
��Left=�64   $��
60,204,0,0q�6O   $	��Top=�6c   $��75��6{   $��16(�6�   6�   �� 5   
   -   5        $ ButtonDelete��5      $��Usuń��6'   $
��Left=�66   $��245,204,0,0q�6Q   $	��Top=�6f   $��75��6~   $��16(�6�   6�   �� 5   
   -   5        $ ButtonModify��5      $��Zmień��6'   $
��Left=�67   $��153,204,0,0q�6R   $	��Top=�6g   $��76��6   $��16(�6�   6�   �� 5   
   $
��Imie��6   $
��Left=�6   $��	60,81,0,0q�6:   $	��Top=�6M   #��� ��6{   $��16(�6�   ��6e   Z� 6�   �� 5   
   $��Nazwisko��6   $
��Left=�6#   $��
60,112,0,0q�6>   $	��Top=�6R   #��� ��6�   $��16(�6�   ��6j   Z� 6�   �� 5   
   $��Data Urodzenia��6   $
��Left=�6)   $��
60,144,0,0q�6D   $	��Top=�6X   #��� ��6�   $��16(�6�   ��6p   Z� 6�   �� 5   
   -   5         ��Name$ TextBox_Imie��5      $
��Left=�6(   $��23��6C   $��
199,84,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��120��6�   $��16(�6�   6�   �� 5   
   -   5        $ TextBox_Nazwisko��5      $
��Left=�6,   $��23��6G   $��199,115,0,0q�6S   $
 Wrap=�6h   $	��Top=�6|   $	��120��6�   $��16(�6�   6�   �� 5   
   -   5        $ TextBox_DataUrodzenia��5      $
��Left=�61   $��23��6L   $��199,147,0,0q�6X   $
 Wrap=�6m   $	��Top=�6�   $	��120��6�   $��16(�6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 DataGrid_Main��5      
   AutoGenerateColumns
 .    CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly . $
��Left=�6�   $	��272��6�   $��
359,18,0,0q�6�   $	��Top=�6�   $	��411��6�   $��16(�6�   6�   �� 5   
   $��
YYYY-MM-DD��6   $
��Left=�6%   $��225,164,0,0q�6@   $	��Top=�6U   #��� ��6�   $��10(�6�   ��6m   Z� 6�   5      5      