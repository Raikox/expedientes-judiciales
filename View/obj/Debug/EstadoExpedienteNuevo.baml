   M S B A M L   `   `   `  ?????  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5           View.EstadoExpedienteNuevo   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:View     5      U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5         MaterialDesignPaper#???   5	        MaterialDesignBody#???  5
      $??Medium"?5      $??14(?5      $X??Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)?5      $??342.382??6n   $??429.804??6     ? WindowStartupLocation$  CenterScreen=?6?    ? WindowStyle$
 None=?6?   .??5      ?  ? RowDefinitions 5   
   ?? 5      $??80 ?6   ?? 5      $
??631* ?6   .+?5   
   (  "MaterialDesignThemes.Wpf.ColorZone  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth1=?5         ShadowEdges$ Bottom=?5   
   $??Stretch=?5   
   $??14q?6&   ???	#FF9E9E9E.??5      ??  ?? Orientation$ 
Horizontal=?6   .+?5      ?? $??Center=?6   $??Center=?69   
???   ?$??18,0,0,0q?6g   $??17(?6y   $??20??6?   NUEVO ESTADO EXPEDIENTE6?   '  !MaterialDesignThemes.Wpf.PackIcon  5      ?  ?ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ 	Altimeter=?6*   $??Right=?6;   $??20??6W   $??20??6c   $??Center=?6n   $??1??6?   
???   ?$??10,0,0,3q?6?   5      5      ? 5   
   $??1??6    ? ColumnDefinitions 5      ?? 5       $
??557* ?6#   .+?5"      #  MaterialDesignThemes.Wpf.Card  $??Stretch=?6"   $??8,0,8,8q?6>   $??1??6O   .??5#      ?  5$      ?? 5%      $??80 ?6(   ?? 5&      $??90 ?6(   ?? 5'      $??20 ?6(   ?? 5(      $??50 ?6(   .+?5*      ?? -   5         ??Name$ 	txtNombre??5*      $??18q?5+       % !MaterialDesignFloatingHintTextBox#??[ 5,      $?? ??5-      $??14(?6   $??42??6,   .??68   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint	 5.      ?? 5/      $ 
Horizontal=?6)   .+?50   "   ?? 	Nombre.65   51      .??52   :   53      ?? 54      -   5        $ txtDescripcion??54      $	 Descripcion.??67   
 ?? AcceptsReturn
 .  ?? 	IsEnabled .  ?? TextWrapping$
 Wrap=?56      " ?? VerticalScrollBarVisibility$
 Auto=?6*   $??1??57      $??
18,0,18,18q?6&   ?? 58      $??2??6      MaterialDesignDarkSeparator#??[ 6-   $
??Auto??6b   $	??8,0q?6p   ?? 59      $??3??6!   $ 
Horizontal=?6.   $??Right=?6G   .+?5:      ?? -   5         ??Name$ 
btnGuardar??5:   !   $??GUARDAR??65     MaterialDesignFlatButton#??[ 6G   $??Right=?6y   $??0,8,8,16q?6?   $??4??6?   $??Center=?6?   $	??112??6?   $??26??6?   ???	#FF9E9E9E??6?   Z? 6  ?? 5;      -   5        $ btnCancelar??5;   !   $??CANCELAR??66   #??[ 6I   $??0,8,8,16q?6{   $??4??6?   $??Center=?6?   $	??112??6?   $??26??6?   ???	#FF9E9E9E$??Right=?6  ??6?   Z? 6  5<      5>      5?      5A      5B      5C      