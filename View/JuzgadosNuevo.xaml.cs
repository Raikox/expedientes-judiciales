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
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for JuzgadosNuevo.xaml
    /// </summary>
    public partial class JuzgadosNuevo : Window
    {

        eoExpedientesEntities db = new eoExpedientesEntities();

        public JuzgadosNuevo()
        {
            InitializeComponent();
            CargarComboCiudades();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                juzgado_jdo juzgado_Jdo = new juzgado_jdo()
                {
                    jdo_nombre = txtNombre.Text,
                    cdd_id = (int)cbxCiudad.SelectedValue

                };
                db.juzgado_jdo.Add(juzgado_Jdo);
                db.SaveChanges();
                Juzgados.table.ItemsSource = (from j in db.juzgado_jdo
                                              join c in db.ciudad_cdd
                                              on j.cdd_id equals c.cdd_id
                                              select new juzgados
                                              {
                                                  jdo_id = j.jdo_id,
                                                  jdo_nombre = j.jdo_nombre,
                                                  cdd_nombre = c.cdd_nombre,
                                                  cdd_id = c.cdd_id
                                              }).ToList().OrderBy(x => x.cdd_nombre).ThenBy(x => x.jdo_nombre);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarComboCiudades()
        {
            try
            {
                //DataConext se lee a traves del Binding
                List<ciudad_cdd> list = db.ciudad_cdd.ToList();
                DataContext = list;
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
