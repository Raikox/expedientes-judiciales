   M S B A M L   `   `   `  ?????  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5      #    View.ProcesosJudicialesEstado   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:View     5      U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5         MaterialDesignPaper#???   5	        MaterialDesignBody#???  5
      $??Medium"?5      $??14(?5      $X??Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)?5      $??492.263??6n   $??484.804??6     ? WindowStartupLocation$  CenterScreen=?6?    ? WindowStyle$
 None=?6?   .??5      ?  ? RowDefinitions 5   
   ?? 5      $??80 ?6   ?? 5      $	??27* ?6   ?? 5      $	??61* ?6   .+?5   
   (  "MaterialDesignThemes.Wpf.ColorZone  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth1=?5         ShadowEdges$ Bottom=?5   
   $??Stretch=?5   
   $??14q?6&   ???	#FF9E9E9E.??5      ??  ?? Orientation$ 
Horizontal=?6   .+?5      ?? $??Center=?6   $??Center=?69   
???   ?$??18,0,0,0q?6g   $??17(?6y   $??20??6?   ASGINAR ESTADO6?   '  !MaterialDesignThemes.Wpf.PackIcon  5      ?  ?ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ 	Altimeter=?6*   $??Right=?6;   $??20??6W   $??20??6c   $??Center=?6n   $??1??6?   
???   ?$??10,0,0,3q?6?   5      5      ? 5   
   $??1??6   $??2??6    ? ColumnDefinitions 5       ?? 5!      $
??557* ?6#   .+?5#      #  MaterialDesignThemes.Wpf.Card  $??Stretch=?6"   $??8,0,8,8q?6>   $??1??6O   .??5$      ?  5%      ?? 5&      $??70 ?6(   ?? 5'      $??60 ?6(   ?? 5(      $??60 ?6(   ?? 5)      $	??120 ?6(   ?? 5*      $??20 ?6(   ?? 5+      $??50 ?6(   .+?5-      ?? -   5         ??Name$ txtExpediente??5-       $
??Left=?67   $??Center=?6R   $??15,0,0,5q?6m   $??16(?6   Expediente N°6?   (  "System.Windows.Controls.DatePicker  5.      -   5        	  Name$	 pkrFecha??5.   !   )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 Fecha??63    ( $MaterialDesignFloatingHintDatePicker#??[ 5/      $??1??6T   $	??Top=?6a   $
??18,0q?6y   $??42??6?   ?? 50      -   5         ??Name$ 	cbxEstado??50      $
 Estado??63    & "MaterialDesignFloatingHintComboBox#??[ 51      $??2??6R   $
??18,0q?6_   $??0q?6m   $	??Top=?6y   $??42??6?    ?? DisplayMemberPath$ 
edo_nombre??52   7    ?? SelectedValuePath$ edo_id??6V   ??6   ?? 53      ?? 54      -   5         ??Name$ 	txtMotivo??54      $
 Motivo.??62    ?? AcceptsReturn .  ?? 	IsEnabled .  ?? TextWrapping$
 Wrap=?56      " ?? VerticalScrollBarVisibility$
 Auto=?6*   $??3??57      $??
18,0,18,18q?6&   ?? 58      $??4??6      MaterialDesignDarkSeparator#??[ 59      $
??Auto??6J   $	??8,0q?6X   ?? 5:      $??5??6!   $ 
Horizontal=?6.   $??Right=?6G   .+?5;      ?? -   5         ??Name$ 
btnGuardar??5;   !   $??GUARDAR??65     MaterialDesignFlatButton#??[ 6G   $??Right=?6y   $??0,8,8,16q?6?   $??4??6?   $??Center=?6?   $	??112??6?   $??26??6?   ???	#FF9E9E9E??6?   Z? 6  ?? 5<      -   5        $ btnCancelar??5<   !   $??CANCELAR??66   #??[ 6I   $??0,8,8,16q?6{   $??4??6?   $??Center=?6?   $	??112??6?   $??26??6?   ???	#FF9E9E9E$??Right=?6  ??6?   Z? 6  5=      5?      5@      5B      5C      5D      