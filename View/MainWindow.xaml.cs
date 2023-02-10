using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CargarSesionUno();
        }

        public void CargarSesionUno()
        {
            SesionUno sesionUno = new SesionUno();
            sesionUno.RecibeObjetoUno(this);
            gridLogin.Children.Clear();
            gridLogin.Children.Add(sesionUno);
        }
                
        public void CargarDashBoard(int idUsuario, string nombres,string sesion)
        {
            DashBoard dashBoard = new DashBoard(idUsuario, nombres, sesion)
            {
                MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight,
                MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth
            };
            dashBoard.Show();
            this.Close();
        }

        
    }
}
