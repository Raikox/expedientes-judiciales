   M S B A M L   `   `   `  ?????  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5          
View.Cajas   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:View     5      U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5         MaterialDesignPaper#???   5	        MaterialDesignBody#???  5
      $??Medium"?5      $??14(?5      $X??Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)?5      .??5      ?   ? RowDefinitions  5   
   ?? 5      $	??100 ?6   ?? 5      $
??631* ?6   .+?5   
   (  "MaterialDesignThemes.Wpf.ColorZone  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth1=?5         ShadowEdges$ Bottom=?5   
   $??Stretch=?5   
   $??14q?6&   ???	#FF9E9E9E.??5      ??  ?? Orientation$ 
Horizontal=?6   .+?5      ?? $??Center=?6   $??Center=?69   
???   ?$??18,0,0,0q?6g   $??18(?6y   $??20??6?   CAJAS EXPEDIENTES6?   '  !MaterialDesignThemes.Wpf.PackIcon  5      ?  ?ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ Archive=?6*   $??Right=?69   $??24??6U   $??24??6a   $??Center=?6l   $??1??6?   
???   ?$??10,0,0,0q?6?   5      5      ? 5   
   $??1??6    ? ColumnDefinitions 5      ?? 5      $
??557* ?6#   ?? 5       $
??273* ?6#   .+?5#      #  MaterialDesignThemes.Wpf.Card  $??Stretch=?6"   $??8,0,8,8q?6>   .??5$      ?   5%      ?? 5&      $??50 ?6(   ?? 5'      $??20 ?6(   ?? 5(      $
??287* ?6(   ?? 5)      $??60 ?6(   .+?5+      ?? $??	Registros??6   $??10,0,0,0q?60   $
??Left=?6B   $??16(?6]   $??Bottom=?6k   ?? 5,      $??1??6      MaterialDesignDarkSeparator#??[ 5-      $
??Auto??6J   $	??8,0q?6X   &   System.Windows.Controls.DataGrid  5.      -   5          Name$ tblCajas??5.      $??	10,8,10,4q?61   $??2??6D      
IsReadOnly .    AutoGenerateColumns .  	   	RowHeight$	 32Q?6   
   Columns
 5/      0  *System.Windows.Controls.DataGridTextColumn  50      ,  &System.Windows.Controls.DataGridColumn   Header$ ID??61   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6=   ?? *cja_id+  51      $ 
ID ENTIDAD??61    6E   ?? *ef_id+  52      $ N° CAJA??61      Width5
  /System.Windows.Controls.DataGridLengthConverter$ 80
 6a      MinWidth$ 30Q?6l    6B   ?? *
cja_numero+6l     53      $ CLIENTE??61   $	 300
 6`    6B   ?? *	ef_nombre+6`   4  .System.Windows.Controls.DataGridTemplateColumn  54         HeaderTemplate 55   "   ?? 56   &   .R?57   *   ?? $??ACCION??64   58   '      CellTemplate 5:   "   ?? 5;   &   .R?5<   *   ?? $ 
Horizontal=?65   .+?5=   .   ??  ??Name$ 	btnEditar??65   -   5          MaterialDesignFlatButton#??[ 5=   H   $??	0,-2,10,0q?6?   $??24??6?   $??Center=?6?   .??5>   2     $ Pencil=?6J   
???   ?6Y   5?   /   5C   +   5D   '   5F      5H      ? 5I      $??3??6    5J      ?? 5K      $
??482* ?6/   ?? 5L      $
??217* ?6/   .+?5N      ?? $??0q?6%   $ 
Horizontal=?60   .+?5O      ?? $??Total Cajas:??6$   $??	10,10,0,0q?6;   $??4??6N   $??Center=?6[   $??Center=?6v   ?? 5P      -   5         ??Name$ txtTotalCajas??5P   &   $??40??6>   $??5,10,0,0q?6I   {?.  
???????$??Center=?6?    ?? TextAlignment$ Center=?6?   5Q      ?? 5R      -   5        $ btnNuevo??5R   !    + 'MaterialDesignFloatingActionLightButton#??[ 63    ? ToolTip$ Nuevo Registro??5S      $??1??5T      $??Right=?6-   $??	0,0,10,10q?6I   $??50??6\   $??50??6g   ???	#FF9E9E9E???	#FF9E9E9E???	#DDFFFFFF$??1q?6?   .??5U        $??24??5V      $??24??5W      $
 plus=?5X      5Y      5Z      5[      5\        5^      $??Stretch=?6"   $??0,0,8,8q?6>   $??1??6O   .??5_      ?   5`      ?? 5a      $??50 ?6(   ?? 5b      $??20 ?6(   ?? 5c      $
??276* ?6(   .+?5e      ?? $??Detalle??6   $??10,0,0,0q?6.   $
??Left=?6@   $??16(?6[   $??Bottom=?6i   ?? 5f      $??1??6    #??[ 5g      $
??Auto??6J   $	??8,0q?6X   5h      5i      5k      5l      5m      