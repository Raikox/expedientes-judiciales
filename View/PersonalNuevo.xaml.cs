using Model;
using System;
using System.Linq;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for PersonalNuevo.xaml
    /// </summary>
    public partial class PersonalNuevo : Window
    {

        eoExpedientesEntities db = new eoExpedientesEntities();

        public PersonalNuevo()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int _IdInsertado;
                personal_prl personal_Prl = new personal_prl()
                {
                    prl_apellidos = txtApellidos.Text,
                    prl_nombres = txtNombres.Text,
                    prl_correo = txtCorreo.Text,
                    prl_celular = txtCelular.Text,
                    prl_dni = txtDni.Text
                };
                db.personal_prl.Add(personal_Prl);
                db.SaveChanges();
                _IdInsertado = personal_Prl.prl_id;

                sesion_ssn sesion_Ssn = new sesion_ssn()
                {
                    ssn_usuario = "",
                    ssn_password = "",
                    prl_id = _IdInsertado,
                    ssn_tipo = "Personal"
                };
                db.sesion_ssn.Add(sesion_Ssn);
                db.SaveChanges();
                Personal.table.ItemsSource = db.personal_prl.ToList().OrderBy(x => x.prl_apellidos);
                
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
