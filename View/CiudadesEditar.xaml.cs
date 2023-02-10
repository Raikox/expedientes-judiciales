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
    /// Interaction logic for CiudadesEditar.xaml
    /// </summary>
    public partial class CiudadesEditar : Window
    {
        int _IdCiudad;
        eoExpedientesEntities db = new eoExpedientesEntities();

        public CiudadesEditar(int IdCiudad)
        {
            this._IdCiudad = IdCiudad;
            InitializeComponent();
            CargarDatos(_IdCiudad);
        }

        
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ciudad_cdd ciudad_Cdd =
                (from cdd in db.ciudad_cdd
                 where cdd.cdd_id == _IdCiudad
                 select cdd).Single();
                ciudad_Cdd.cdd_nombre = txtNombre.Text;
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

        private void CargarDatos(int id)
        {
            try
            {
                List<ciudad_cdd> list = db.ciudad_cdd.ToList();
                ciudad_cdd ciudad_Cdd = list.Find(x => x.cdd_id == id);
                txtNombre.Text = ciudad_Cdd.cdd_nombre;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                       
        }
    }
}
