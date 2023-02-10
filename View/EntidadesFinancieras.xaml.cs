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
    /// Interaction logic for EntidadesFinancieras.xaml
    /// </summary>
    public partial class EntidadesFinancieras : UserControl
    {
        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;

        public EntidadesFinancieras()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarTablaEntidadesFinancieras();
        }

        public void CargarTablaEntidadesFinancieras()
        {
            try
            {
                List<entidadfinanciera> listCliente = (from rto in db.registro_rto
                                                      join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                                      where tpo.tpo_numero == 1
                                                      select new entidadfinanciera
                                                      {
                                                          ef_id = rto.rto_id,
                                                          ef_nombre = rto.rto_nombres,
                                                          ef_color = rto.rto_color
                                                      }).ToList();
                // 1: entidad financiera  -  2: persona  -  3: empresa
                tblEntidadesFinancieras.ItemsSource = listCliente.OrderBy(x => x.ef_nombre);
                table = tblEntidadesFinancieras;
                tblEntidadesFinancieras.Columns[0].Visibility = Visibility.Collapsed;
                txtTotalEntidades.Text = Convert.ToString(listCliente.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idEntidad = (tblEntidadesFinancieras.SelectedItem as entidadfinanciera).ef_id;
                EntidadesFinancierasEditar entidadesFinancierasEditar = new EntidadesFinancierasEditar(idEntidad);
                entidadesFinancierasEditar.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            EntidadesFinancierasNuevo entidadesFinancierasNuevo = new EntidadesFinancierasNuevo();
            entidadesFinancierasNuevo.RecibeObjectoEntidadesFinancierasNuevo(this);
            entidadesFinancierasNuevo.ShowDialog();
        }
    }
}
