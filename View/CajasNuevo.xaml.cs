using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for CajasNuevo.xaml
    /// </summary>
    public partial class CajasNuevo : Window
    {

        eoExpedientesEntities db = new eoExpedientesEntities();

        public CajasNuevo()
        {
            InitializeComponent();
            CargarComboEntidadesFinancieras();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caja_cja caja_Cja = new caja_cja()
                {
                    cja_numero = Convert.ToInt32(txtCajaNumero.Text),
                    rto_id = (int)cbxEntidad.SelectedValue

                };
                db.caja_cja.Add(caja_Cja);
                db.SaveChanges();
                Cajas.table.ItemsSource = (from c in db.caja_cja
                                           join ef in db.registro_rto on c.rto_id equals ef.rto_id
                                           join tpo in db.tipo_tpo on ef.tpo_id equals tpo.tpo_id
                                           where tpo.tpo_numero == 1
                                           select new cajas
                                           {
                                               cja_id = c.cja_id,
                                               cja_numero = c.cja_numero,
                                               ef_id = ef.rto_id,
                                               ef_nombre = ef.rto_nombres
                                           }).ToList();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CargarComboEntidadesFinancieras()
        {
            try
            {
                //DataConext se lee a traves del Binding
                List<entidadfinanciera> list = (from ef in db.registro_rto
                                                join tpo in db.tipo_tpo on ef.tpo_id equals tpo.tpo_id
                                                where tpo.tpo_numero == 1
                                                select new entidadfinanciera
                                                {
                                                    ef_nombre = ef.rto_nombres,
                                                    ef_id = ef.rto_id
                                                }).ToList();
                DataContext = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
