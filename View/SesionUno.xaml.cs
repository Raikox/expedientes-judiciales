using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace View
{
    /// <summary>
    /// Interaction logic for SesionUno.xaml
    /// </summary>
    public partial class SesionUno : UserControl
    {

        eoExpedientesEntities db = new eoExpedientesEntities();

        public SesionUno()
        {
            InitializeComponent();
        }

        public static MainWindow _objMainWindowUno;

        public void RecibeObjetoUno(MainWindow objMainWindow)
        {
            SesionUno._objMainWindowUno = objMainWindow;
        }
            

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        
        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            //verificar usuario y pass
            string _usuario = txtUsuario.Text;
            string _password = txtPassword.Password;
            string _passwordDB;
            try
            {
                List<sesion_ssn> listaSesion = db.sesion_ssn.ToList();
                int _idUsuario;
                string _nombreUsuario;
                string _tipoUsuario;
                //Se encontró el usuario
                if (listaSesion.Any(x => x.ssn_usuario == _usuario))
                {
                    sesion_ssn sesion_Ssn = listaSesion.Find(x => x.ssn_usuario == _usuario);
                    _passwordDB = sesion_Ssn.ssn_password;
                    _idUsuario = sesion_Ssn.prl_id;
                    _tipoUsuario = sesion_Ssn.ssn_tipo;

                    //Se encontró el password
                    if (_passwordDB.Equals(_password))
                    {
                        //Obtiene nombres usuario
                        List<personal_prl> listaPersonal = db.personal_prl.ToList();
                        personal_prl personal_Prl = listaPersonal.Find(x => x.prl_id == _idUsuario);
                        _nombreUsuario = personal_Prl.prl_nombres + " " + personal_Prl.prl_apellidos;
                        _objMainWindowUno.CargarDashBoard(_idUsuario, _nombreUsuario, _tipoUsuario);

                    }
                    else
                    {
                        MessageBox.Show("La contraseña ingresada es incorrecta", "Error al validar", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("El usuario ingresado es incorrecto", "Error al validar", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(Exception ex)
            {                
                MessageBox.Show(ex.ToString());
            }
            
            
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Keyboard.ClearFocus();
            Keyboard.Focus(txtPassword);
        }
    }
}
