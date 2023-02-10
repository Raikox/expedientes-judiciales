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
    /// Interaction logic for AmbientesNuevo.xaml
    /// </summary>
    public partial class AmbientesNuevo : Window
    {

        eoExpedientesEntities db = new eoExpedientesEntities();

        public AmbientesNuevo()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ambiente_abt ambiente_Abt = new ambiente_abt()
                {
                    abt_nombre = txtNombre.Text,
                    abt_descripcion = txtDescripcion.Text
                };
                db.ambiente_abt.Add(ambiente_Abt);
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
    }
}
