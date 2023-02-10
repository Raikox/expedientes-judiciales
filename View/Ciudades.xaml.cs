using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for Ciudades.xaml
    /// </summary>
    public partial class Ciudades : UserControl
    {

        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;

        public Ciudades()
        {
            InitializeComponent();
            CargarTablaCiudades();
        }
        
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            CiudadesNuevo ciudadesNuevo = new CiudadesNuevo();            
            ciudadesNuevo.ShowDialog();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idCiudad = (tblCiudades.SelectedItem as ciudad_cdd).cdd_id;
                CiudadesEditar ciudadesEditar = new CiudadesEditar(idCiudad);
                ciudadesEditar.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        //private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //{

        //}

        public void CargarTablaCiudades()
        {
            try
            {
                List<ciudad_cdd> listCiudad = db.ciudad_cdd.ToList();
                tblCiudades.ItemsSource = listCiudad.OrderBy(x => x.cdd_nombre);
                table = tblCiudades;
                tblCiudades.Columns[0].Visibility = Visibility.Collapsed;

                txtTotalCiudades.Text = Convert.ToString(listCiudad.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        
    }
}
