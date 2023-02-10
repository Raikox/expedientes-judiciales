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
    /// Interaction logic for MateriasNuevo.xaml
    /// </summary>
    public partial class MateriasNuevo : Window
    {
        eoExpedientesEntities db = new eoExpedientesEntities();

        public MateriasNuevo()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                materia_mra materia_Mra = new materia_mra() { mra_nombre = txtNombre.Text };
                db.materia_mra.Add(materia_Mra);
                db.SaveChanges();
                Materias.table.ItemsSource = db.materia_mra.ToList().OrderBy(x => x.mra_nombre);
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
