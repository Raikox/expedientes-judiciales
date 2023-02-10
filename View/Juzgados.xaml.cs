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
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for Juzgados.xaml
    /// </summary>
    public partial class Juzgados : UserControl
    {
        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;       

        public Juzgados()
        {
            InitializeComponent();
                        
            CargarTablaJuzgados();
        }

        private void CargarTablaJuzgados()
        {  
            try
            {
                List<juzgados> listJuzgados  = (from j in db.juzgado_jdo
                                                join c in db.ciudad_cdd
                                                on j.cdd_id equals c.cdd_id
                                                select new juzgados
                                                {
                                                    jdo_id = j.jdo_id,
                                                    jdo_nombre = j.jdo_nombre,
                                                    cdd_nombre = c.cdd_nombre,
                                                    cdd_id = c.cdd_id
                                                }).ToList();
                tblJuzgados.ItemsSource = listJuzgados.OrderBy(x => x.cdd_nombre).ThenBy(x => x.jdo_nombre);

                table = tblJuzgados;
                tblJuzgados.Columns[0].Visibility = Visibility.Collapsed;
                tblJuzgados.Columns[1].Visibility = Visibility.Collapsed;

                txtTotalJuzgados.Text = Convert.ToString(listJuzgados.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            JuzgadosNuevo juzgadosNuevo = new JuzgadosNuevo();
            juzgadosNuevo.ShowDialog();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idJuzgado = (tblJuzgados.SelectedItem as juzgados).jdo_id;
                int idCiudad = (tblJuzgados.SelectedItem as juzgados).cdd_id;
                JuzgadosEditar juzgadosEditar = new JuzgadosEditar(idJuzgado, idCiudad);
                juzgadosEditar.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

       
    }
}
