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
    /// Interaction logic for Personal.xaml
    /// </summary>
    public partial class Personal : UserControl
    {

        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;
        int _IdSesion;

        public Personal()
        {
            InitializeComponent();

            CargarTablaPersonal();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            int idPersonal = (tblPersonal.SelectedItem as personal_prl).prl_id;
            PersonalEditar personalEditar = new PersonalEditar(idPersonal);
            personalEditar.ShowDialog();
        }
        
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            PersonalNuevo personalNuevo = new PersonalNuevo();
            personalNuevo.ShowDialog();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sesion_ssn sesion_Ssn = (from ssn in db.sesion_ssn
                                         where ssn.ssn_id == _IdSesion
                                         select ssn).Single();
                sesion_Ssn.ssn_usuario = txtUsuario.Text;
                sesion_Ssn.ssn_password = txtPassword.Password;
                sesion_Ssn.ssn_tipo = Convert.ToString(cbxTipo.SelectedValue);
                db.SaveChanges();

                //use the message queue to send a message.
                var messageQueue = SnackbarThree.MessageQueue;
                var message = "SESION GUARDADA CORRECTAMENTE.";
                //the message queue can be called from any thread
                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }

        private void CargarTablaPersonal()
        {
            try
            {
                List<personal_prl> listPersonal = db.personal_prl.ToList();
                tblPersonal.ItemsSource = listPersonal.OrderBy(x => x.prl_apellidos);
                table = tblPersonal;
                tblPersonal.Columns[0].Visibility = Visibility.Collapsed;

                txtTotalPersonal.Text = Convert.ToString(listPersonal.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarComboTipo()
        {
            try
            {
                //DataConext se lee a traves del Binding
                List<sesion> list = new List<sesion>();
                list.Add(new sesion { ssn_tipo = "Personal" });
                list.Add(new sesion { ssn_tipo = "Administrador" });
                DataContext = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarDetalle()
        {           
            try
            {
                int idPersonal = (tblPersonal.SelectedItem as personal_prl).prl_id;
                List<personal_prl> list = db.personal_prl.ToList();
                personal_prl personal_Prl = list.Find(x => x.prl_id == idPersonal);
                txtCorreo.Text = personal_Prl.prl_correo;
                txtCelular.Text = personal_Prl.prl_celular;
                txtDni.Text = personal_Prl.prl_dni;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarSesion()
        {
            try
            {
                int idPersonal = (tblPersonal.SelectedItem as personal_prl).prl_id;
                List<sesion_ssn> list = db.sesion_ssn.ToList();
                sesion_ssn sesion_Ssn = list.Find(x => x.prl_id == idPersonal);
                _IdSesion = sesion_Ssn.ssn_id;
                txtUsuario.Text = sesion_Ssn.ssn_usuario;
                txtPassword.Password = sesion_Ssn.ssn_password;
                cbxTipo.SelectedValue = sesion_Ssn.ssn_tipo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void tblPersonal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tblPersonal.SelectedIndex != -1)
            {
                CargarDetalle();
                CargarComboTipo();
                CargarSesion();
                btnGuardar.IsEnabled = true;
            }            
        }
    }
}
