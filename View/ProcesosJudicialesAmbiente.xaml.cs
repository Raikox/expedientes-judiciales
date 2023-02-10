using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for ProcesosJudicialesAmbiente.xaml
    /// </summary>
    public partial class ProcesosJudicialesAmbiente : Window
    {

        eoExpedientesEntities db = new eoExpedientesEntities();
        int IdExpediente;
        String NumExpediente;

        public ProcesosJudicialesAmbiente(int IdExpediente, String NumExpediente)
        {
            this.IdExpediente = IdExpediente;
            this.NumExpediente = NumExpediente;
            InitializeComponent();

            CargarComboAmbientes();
            CargarPickerFecha();
            txtExpediente.Text = "Expediente N° " + NumExpediente;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idAmbiente = (int)cbxAmbiente.SelectedValue;
                var dateHoy = pkrFecha.SelectedDate.Value.Date;
                int idPer = DashBoard._IdUsu;
                String motivo = txtMotivo.Text;
                expediente_ambiente_ea eam = new expediente_ambiente_ea()
                {
                    ea_fecha = dateHoy,
                    ea_motivo = motivo,
                    exp_id = IdExpediente,
                    abt_id = idAmbiente,
                    prl_id = idPer
                };
                db.expediente_ambiente_ea.Add(eam);
                db.SaveChanges();

                ProcesosJudiciales.tableAmbiente.ItemsSource = (from ea in db.expediente_ambiente_ea
                                                                join abt in db.ambiente_abt on ea.abt_id equals abt.abt_id
                                                                join exp in db.expediente_exp on ea.exp_id equals exp.exp_id
                                                                join prl in db.personal_prl on ea.prl_id equals prl.prl_id
                                                                where exp.exp_id == IdExpediente
                                                                select new ambiente
                                                                {
                                                                    exp_id = exp.exp_id,
                                                                    abt_id = ea.abt_id,
                                                                    ea_id = ea.ea_id,
                                                                    abt_nombre = abt.abt_nombre,
                                                                    ea_fecha = ea.ea_fecha,
                                                                    ea_motivo = ea.ea_motivo,
                                                                    prl_nombre = prl.prl_nombres + " " + prl.prl_apellidos
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

        private void CargarComboAmbientes()
        {
            try
            {
                List<ambiente_abt> listAmbientes = db.ambiente_abt.ToList();
                cbxAmbiente.ItemsSource = listAmbientes.OrderBy(x => x.abt_nombre);
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
