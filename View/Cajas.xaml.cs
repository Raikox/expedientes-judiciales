using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for Cajas.xaml
    /// </summary>
    public partial class Cajas : UserControl
    {

        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;

        public Cajas()
        {
            InitializeComponent();

            CargarTablaCajas();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idCaja = (tblCajas.SelectedItem as cajas).cja_id;
                int idEntidad = (tblCajas.SelectedItem as cajas).ef_id;
                CajasEditar cajasEditar = new CajasEditar(idCaja, idEntidad);
                cajasEditar.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        //private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void CargarTablaCajas()
        {
            try
            {
                List<cajas> listCajas = (from c in db.caja_cja
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
                tblCajas.ItemsSource = listCajas.OrderBy(x => x.cja_numero);

                table = tblCajas;
                tblCajas.Columns[0].Visibility = Visibility.Collapsed;
                tblCajas.Columns[1].Visibility = Visibility.Collapsed;
                txtTotalCajas.Text = Convert.ToString(listCajas.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            CajasNuevo cajasNuevo = new CajasNuevo();
            cajasNuevo.ShowDialog();
        }
    }
}
