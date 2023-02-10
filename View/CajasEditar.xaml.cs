using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CajasEditar.xaml
    /// </summary>
    public partial class CajasEditar : Window
    {
        int _IdCaja;
        int _IdEntidad;
        eoExpedientesEntities db = new eoExpedientesEntities();

        public CajasEditar(int IdCaja, int IdEntidad)
        {
            this._IdCaja = IdCaja;
            this._IdEntidad = IdEntidad;
            InitializeComponent();

            CargarComboEntidadesFinancieras();
            CargarDatos(_IdCaja, _IdEntidad);
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

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                caja_cja caja_Cja = (from cja in db.caja_cja
                                     where cja.cja_id == _IdCaja
                                     select cja).Single();
                caja_Cja.cja_numero = Convert.ToInt32(txtCajaNumero.Text);
                caja_Cja.rto_id = (int)cbxEntidad.SelectedValue;
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

        private void CargarDatos(int idC, int idE)
        {
            try
            {
                List<caja_cja> listj = db.caja_cja.ToList();
                caja_cja caja_Cja = listj.Find(x => x.cja_id == idC);
                txtCajaNumero.Text = Convert.ToString(caja_Cja.cja_numero);
                cbxEntidad.SelectedValue = Convert.ToString(idE);
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
