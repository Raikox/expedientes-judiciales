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
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for PersonalEditar.xaml
    /// </summary>
    public partial class PersonalEditar : Window
    {

        int _IdPersonal;
        eoExpedientesEntities db = new eoExpedientesEntities();

        public PersonalEditar(int IdPersonal)
        {
            this._IdPersonal = IdPersonal;
            InitializeComponent();

            CargarDatos(_IdPersonal);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                personal_prl personal_Prl = (from prl in db.personal_prl
                                             where prl.prl_id == _IdPersonal
                                             select prl).Single();
                personal_Prl.prl_apellidos = txtApellidos.Text;
                personal_Prl.prl_nombres = txtNombres.Text;
                personal_Prl.prl_correo = txtCorreo.Text;
                personal_Prl.prl_celular = txtCelular.Text;
                personal_Prl.prl_dni = txtDni.Text;
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

        private void CargarDatos(int id)
        {
            try
            {
                List<personal_prl> list = db.personal_prl.ToList();
                personal_prl personal_Prl = list.Find(x => x.prl_id == id);
                txtApellidos.Text = personal_Prl.prl_apellidos;
                txtNombres.Text = personal_Prl.prl_nombres;
                txtCorreo.Text = personal_Prl.prl_correo;
                txtCelular.Text = personal_Prl.prl_celular;
                txtDni.Text = personal_Prl.prl_dni;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
