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
    /// Interaction logic for AmbientesEditar.xaml
    /// </summary>
    public partial class AmbientesEditar : Window
    {

        int _IdAmbiente;
        eoExpedientesEntities db = new eoExpedientesEntities();

        public AmbientesEditar(int IdAmbiente)
        {
            this._IdAmbiente = IdAmbiente;
            InitializeComponent();

            CargarDatos(_IdAmbiente);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ambiente_abt ambiente_Abt = (from abt in db.ambiente_abt
                                             where abt.abt_id == _IdAmbiente
                                             select abt).Single();
                ambiente_Abt.abt_nombre = txtNombre.Text;
                ambiente_Abt.abt_descripcion = txtDescripcion.Text;
                db.SaveChanges();
                Ambientes.table.ItemsSource = db.ambiente_abt.ToList();
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
                List<ambiente_abt> list = db.ambiente_abt.ToList();
                ambiente_abt ambiente_Abt = list.Find(x => x.abt_id == id);
                txtNombre.Text = ambiente_Abt.abt_nombre;
                txtDescripcion.Text = ambiente_Abt.abt_descripcion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

    }
}
