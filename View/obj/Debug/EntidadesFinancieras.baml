   M S B A M L   `   `   `  ?????  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5          View.EntidadesFinancieras   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $localclr-namespace:View     5      U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5           MaterialDesignPaper#???   5	        MaterialDesignBody#???  5
      $??Medium"?5      $??14(?5      $X??Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)?5      .??5      ?   ? RowDefinitions  5   
   ?? 5      $	??100 ?6   ?? 5      $
??631* ?6   .+?5   
   (  "MaterialDesignThemes.Wpf.ColorZone  +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth1=?5         ShadowEdges$ Bottom=?5   
   $??Stretch=?5   
   $??14q?6&   ???	#FF9E9E9E.??5      ??  ?? Orientation$ 
Horizontal=?6   .+?5      ?? $??Center=?6   $??Center=?69   
???   ?$??18,0,0,0q?6g   $??18(?6y   $??20??6?   
CLIENTES6?   '  !MaterialDesignThemes.Wpf.PackIcon  5      ?  ?ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$
 Bank=?6*   $??Right=?66   $??24??6R   $??24??6^   $??Center=?6i   $??1??6?   
???   ?$??10,0,0,0q?6?   5      5       ? 5"   
   $??1??6    ? ColumnDefinitions 5#      ?? 5$      $
??557* ?6#   ?? 5%      $
??273* ?6#   .+?5(      #  MaterialDesignThemes.Wpf.Card  $??Stretch=?6"   $??8,0,8,8q?6>   .??5)      ?   5*      ?? 5+      $??50 ?6(   ?? 5,      $??20 ?6(   ?? 5-      $
??287* ?6(   ?? 5.      $??60 ?6(   .+?50      ?? $??	Registros??6   $??10,0,0,0q?60   $
??Left=?6B   $??16(?6]   $??Bottom=?6k   ?? 51      $??1??6      MaterialDesignDarkSeparator#??[ 52      $
??Auto??6J   $	??8,0q?6X   &   System.Windows.Controls.DataGrid  53      -   5          Name$ tblEntidadesFinancieras??53      $??	10,8,10,4q?6@   $??2??6S      
IsReadOnly .    AutoGenerateColumns .  	   	RowHeight$	 32Q?54   T   
   Columns
 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$ ID??61   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6=   ?? *ef_id+  57      $ CLIENTE??61      Width5
  /System.Windows.Controls.DataGridLengthConverter$	 300
 6`      MinWidth$	 150Q?6l    6B   ?? *	ef_nombre+6l   4  .System.Windows.Controls.DataGridTemplateColumn  58         HeaderTemplate 59   "   ?? 5:   &   .R?5;   *   ?? $??COLOR??64   5<   '      CellTemplate 5>   "   ?? 5?   &   .R?5@   *   ?? $ 
Horizontal=?65   $??0,2,0,2q?5A   5   $	??150??6F   ??5@   N   ?? *
ef_color+5A   F   5B   '   5D        5E       5F   "   ?? 5G   &   .R?5H   *   ?? $??ACCION??64   5I   '    5K   "   ?? 5L   &   .R?5M   *   ?? $ 
Horizontal=?65   .+?5N   .   ??  ??Name$ 	btnEditar??65   -   5          MaterialDesignFlatButton#??[ 5N   H   $??	0,-2,10,0q?6?   $??24??6?   $??Center=?6?   .??5O   2     $ Pencil=?6J   
???   ?6Y   5P   /   5T   +   5U   '   5W      5Y      ? 5Z      $??3??6    5[      ?? 5\      $
??482* ?6/   ?? 5]      $
??217* ?6/   .+?5_      ?? $??0q?6%   $ 
Horizontal=?60   .+?5`      ?? $??Total clientes:??6$   $??	10,10,0,0q?6>   $??4??6Q   $??Center=?6^   $??Center=?6y   ?? 5a      -   5         ??Name$ txtTotalEntidades??5a   &   $??40??6A   $??5,10,0,0q?6L   {?.  
???????$??Center=?6?    ?? TextAlignment$ Center=?6?   5b      ?? 5c      -   5        $ btnNuevo??5c   !    + 'MaterialDesignFloatingActionLightButton#??[ 63    ? ToolTip$ Nuevo Registro??5d      $??1??5e      $??Right=?6-   $??0,0,10,0q?6I   $??50??6[   $??50??6f   ???	#FF9E9E9E???	#FF9E9E9E???	#DDFFFFFF.??5f        $??24??5g      $??24??5h      $
 plus=?5i      5j      5k      5l      5m        5o      $??Stretch=?6"   $??0,0,8,8q?6>   $??1??6O   .??5p      ?   5q      ?? 5r      $??50 ?6(   ?? 5s      $??20 ?6(   ?? 5t      $
??276* ?6(   .+?5v      ?? $??Detalle??6   $??10,0,0,0q?6.   $
??Left=?6@   $??16(?6[   $??Bottom=?6i   ?? 5w      $??1??6    #??[ 5x      $
??Auto??6J   $	??8,0q?6X   5y      5z      5|      5}      5~      