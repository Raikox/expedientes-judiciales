   M S B A M L   `   `   `  ?????  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5          View.SesionUno   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:View     5      U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         MaterialDesignBody#???   5	        MaterialDesignPaper#???  5
      $??Medium"?5      $??14(?5      $X??Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)?5      $??389.057??5      $??295.847??6   .??5      7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  !    AutoApplyTransitionOrigins  .  ?? SelectedIndex$ 0??5   	   ??.  |?.  .@?5   
   <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 5      ;  5MaterialDesignThemes.Wpf.Transitions.TransitionEffect  5         Kind$ ExpandIn=?62   .??5      ?  J? IsHitTestVisible . |?.   ? FocusVisualStyle 6=   Z?  ? RowDefinitions 5      ?? 5      $
??127* ?6$   ?? 5      $
??120* ?6$   ?? 5      $
??134* ?6$   .+?5      ?? -   5         ??Name$ 
txtUsuario??5      $??25,33,25,45q?6.    % !MaterialDesignFloatingHintTextBox#??[ 6C   $?? ??5      $??16(?6   $??42??6(   $??Center=?64   $??1??6O   .??6\   )  #MaterialDesignThemes.Wpf.HintAssist   Hint 5       ?? 5!      	 ?? Orientation$	 
Horizontal=?6%   .+?5"      '  !MaterialDesignThemes.Wpf.PackIcon  ?  ?ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]
   Kind$
 Account=?66   ?? 5#      	Usuario61   5$      .??5%   6   5&      ?? 5'      -   5         ??Name$ btnSiguiente??5'      $??	SIGUIENTE??6/   $
??Left=?6C   $??	25,36,0,0q?6^   $??2??6q   $	??Top=?6~   $	??243??6?      MoveNextCommand#??Z 6?   6?   ?? 5(      $??Ingrese sus datos para??6   $??
42,37,42,0q?69   $	??Top=?6M   $??20(?6e   $??Center=?6s   ?? 5)      $??acceder al sistema??6   $??
25,68,25,0q?65   $	??Top=?6I   $??20(?6a   $??Center=?6o   ?? 5*      -   5        $ btnClose??5*        MaterialDesignFlatButton#??[ 6+   $
??Left=?6]   $??23??6x   $??	250,0,0,0q?6?   $	??Top=?6?   $??46??6?   
???????.??5+        $
 Close=?6.   
???   ?6<   5,      5-      5.        5/   
    50        51      $ SlideInFromTop=?62   .??54      ? |?.   6%   Z?  55      ?? 56      $
??127* ?6$   ?? 57      $
??120* ?6$   ?? 58      $
??134* ?6$   .+?5:      G? -   5         G?Name$ txtPassword??5:      $??25,33,25,45q?63    ) %MaterialDesignFloatingHintPasswordBox#??[ 6H   $??16(?5;      $??42??6    $??Center=?6,   $??1??6G    5<      ?? 5=      $	 
Horizontal=?6%   .+?5>        $

 Lock=?66   ?? 5?      ?? 6)   $?Contraseña??6-   6B   5@      5B      ?? 5C      -   5        $ btnIngresar??5C      $??INGRESAR AL SISTEMA??6.   $
??Left=?6L   $??	25,36,0,0q?6g   $??2??6z   $	??Top=?6?   $	??243??6?   6?   ?? 5D      -   5        $ btnCloseDos??5D      #??[ 6.   $
??Left=?6`   $??23??6{   $??	250,0,0,0q?6?   $	??Top=?6?   $??46??6?   
???????.??5E        $
 Close=?6.   
???   ?6<   5F      ?? 5G      -   5        $ btnBack??5G       * &MaterialDesignFloatingActionMiniButton#??[ 6*      MovePreviousCommand#??Z 5H       ? ToolTip$ Retornar??5I      ???	#FF9E9E9E???	#FF9E9E9E.??5J        $??24??6.   $??24??69   $
 KeyboardBackspace=?6E   5K      5L      5M      5N      5O      