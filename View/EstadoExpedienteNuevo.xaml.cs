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
    /// Interaction logic for EstadoExpedienteNuevo.xaml
    /// </summary>
    public partial class EstadoExpedienteNuevo : Window
    {
        eoExpedientesEntities db = new eoExpedientesEntities();
        public EstadoExpedienteNuevo()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                estado_edo estado_Edo = new estado_edo()
                {
                    edo_nombre = txtNombre.Text,
                    edo_descripcion = txtDescripcion.Text
                };
                db.estado_edo.Add(estado_Edo);
                db.SaveChanges();
                EstadoExpediente.table.ItemsSource = db.estado_edo.ToList().OrderBy(x => x.edo_nombre);
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
