using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for DashBoard.xaml
    /// </summary>
    public partial class DashBoard : Window
    {
        public static int _IdUsu;
        int _idUsuario;
        string _nombreUsuario;
        Boolean maximizado=false;
        public DashBoard(int idUsu, string nombreUsu, string tipoSesion)
        {
            this._idUsuario = idUsu;
            this._nombreUsuario = nombreUsu;
            InitializeComponent();
            Console.WriteLine("nombreUsu: " + nombreUsu);
            txtUsuarioPersonal.Text = _nombreUsuario;
            CargarProcesosJudiciales();            
            this.WindowState = WindowState.Maximized;
            maximizado = true;
            _IdUsu = _idUsuario;

            if(tipoSesion.Equals("Personal"))
            {
                OcultarItems();
            }
            else
            {
                btnSalirInicio.Visibility = Visibility.Hidden;
                btnSalirInicio.Visibility = Visibility.Collapsed;
            }
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }
        
        private void ColorZone_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }      
        
        private void ButtonProcesosJudiciales_Click(object sender, RoutedEventArgs e)
        {
            CargarProcesosJudiciales();
        }

        private void ButtonEntidadesFinancieras_Click(object sender, RoutedEventArgs e)
        {
            CargaUserControl(new EntidadesFinancieras());
        }

        private void ButtonCiudades_Click(object sender, RoutedEventArgs e)
        {
            CargaUserControl(new Ciudades());
        }

        private void ButtonJuzgados_Click(object sender, RoutedEventArgs e)
        {
            CargaUserControl(new Juzgados());
        }

        private void ButtonMaterias_Click(object sender, RoutedEventArgs e)
        {
            CargaUserControl(new Materias());
        }

        //Metodo de Carga Estandar        
        public void CargaUserControl(UIElement userControl)
        {
            //GridEscritorio.Children.Clear();
            //GridEscritorio.Children.Add(userControl);
            dockEscritorio.Children.Clear();
            dockEscritorio.Children.Add(userControl);
        }
        
        public void CargarProcesosJudiciales()
        {
            try
            {
                ProcesosJudiciales procesosJudiciales = new ProcesosJudiciales();
                procesosJudiciales.RecibeObjectoDashBoard(this);
                dockEscritorio.Children.Clear();
                dockEscritorio.Children.Add(procesosJudiciales);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public void CargarProcesosJudicialesNuevo()
        {
            ProcesosJudicialesNuevo procesosJudicialesNuevo = new ProcesosJudicialesNuevo();
            procesosJudicialesNuevo.RecibeObjectoDashBoard(this);
            dockEscritorio.Children.Clear();
            dockEscritorio.Children.Add(procesosJudicialesNuevo);
        }

        public void CargarProcesosJudicialesEditar(int IdExpediente)
        {
            ProcesosJudicialesEditar procesosJudicialesEditar = new ProcesosJudicialesEditar(IdExpediente);
            procesosJudicialesEditar.RecibeObjectoDashBoard(this);
            dockEscritorio.Children.Clear();
            dockEscritorio.Children.Add(procesosJudicialesEditar);
        }

        public void CargarInicioSesion()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnCajas_Click(object sender, RoutedEventArgs e)
        {
            CargaUserControl(new Cajas());
        }

        private void btnPersonal_Click(object sender, RoutedEventArgs e)
        {
            CargaUserControl(new Personal());
        }

        private void btnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if(maximizado)
            {
                this.WindowState = WindowState.Normal;
                maximizado = false;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                maximizado = true;
            }         

        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnAmbiente_Click(object sender, RoutedEventArgs e)
        {
            CargaUserControl(new Ambientes());
        }

        private void btnEstado_Click(object sender, RoutedEventArgs e)
        {
            CargaUserControl(new EstadoExpediente());
        }

        private void OcultarItems()
        {
            btnAmbiente.Visibility = Visibility.Hidden;
            btnPersonal.Visibility = Visibility.Hidden;
            btnEstado.Visibility = Visibility.Hidden;
            btnSalirFinal.Visibility = Visibility.Hidden;
            strFinal.Visibility = Visibility.Hidden;
        }

        private void btnSalirFinal_Click(object sender, RoutedEventArgs e)
        {
            CargarInicioSesion();
        }

        private void btnSalirInicio_Click(object sender, RoutedEventArgs e)
        {
            CargarInicioSesion();
        }
    }
}
