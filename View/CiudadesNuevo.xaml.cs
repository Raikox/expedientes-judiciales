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
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for CiudadesNuevo.xaml
    /// </summary>
    public partial class CiudadesNuevo : Window
    {
        eoExpedientesEntities db = new eoExpedientesEntities();

        public CiudadesNuevo()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ciudad_cdd ciudad_Cdd = new ciudad_cdd() { cdd_nombre = txtNombre.Text };
                db.ciudad_cdd.Add(ciudad_Cdd);
                db.SaveChanges();
                Ciudades.table.ItemsSource = db.ciudad_cdd.ToList();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
