using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for ProcesosJudicialesEstado.xaml
    /// </summary>
    public partial class ProcesosJudicialesEstado : Window
    {
        eoExpedientesEntities db = new eoExpedientesEntities();
        int IdExpediente;
        String NumExpediente;

        public ProcesosJudicialesEstado(int IdExpediente, String NumExpediente)
        {
            this.IdExpediente = IdExpediente;
            this.NumExpediente = NumExpediente;

            InitializeComponent();

            CargarComboEstados();
            CargarPickerFecha();
            txtExpediente.Text = "Expediente N° "+NumExpediente;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idEstado = (int)cbxEstado.SelectedValue;
                int idPer = DashBoard._IdUsu;
                String motivo = txtMotivo.Text;
                var dateHoy = pkrFecha.SelectedDate.Value.Date;
                Console.WriteLine("_IdUsu al guardar: " + DashBoard._IdUsu);
                expediente_estado_ee ees = new expediente_estado_ee()
                {
                    ee_fecha = dateHoy,
                    ee_motivo = motivo,
                    exp_id = IdExpediente,
                    edo_id = idEstado,
                    prl_id = idPer
                };
                db.expediente_estado_ee.Add(ees);
                db.SaveChanges();

                ProcesosJudiciales.tableEstado.ItemsSource = (from ee in db.expediente_estado_ee
                                                              join edo in db.estado_edo on ee.edo_id equals edo.edo_id
                                                              join prl in db.personal_prl on ee.prl_id equals prl.prl_id
                                                              join exp in db.expediente_exp on ee.exp_id equals exp.exp_id
                                                              where exp.exp_id == IdExpediente
                                                              select new estado
                                                              {
                                                                  edo_id = ee.edo_id,
                                                                  ee_fecha = ee.ee_fecha,
                                                                  edo_nombre = edo.edo_nombre,
                                                                  ee_motivo = ee.ee_motivo,
                                                                  prl_nombres = prl.prl_nombres + " " + prl.prl_apellidos,
                                                                  exp_id = exp.exp_id
                                                              }).ToList();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CargarComboEstados()
        {
            try
            {
                List<estado_edo> listEstados = db.estado_edo.ToList();
                cbxEstado.ItemsSource = listEstados.OrderBy(x => x.edo_nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarPickerFecha()
        {
            var dateHoy = DateTime.Now;
            pkrFecha.SelectedDate = dateHoy;
        }
    }
}
