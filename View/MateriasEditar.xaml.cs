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
    /// Interaction logic for MateriasEditar.xaml
    /// </summary>
    public partial class MateriasEditar : Window
    {
        int _IdMateria;
        eoExpedientesEntities db = new eoExpedientesEntities();

        public MateriasEditar(int IdMateria)
        {
            this._IdMateria = IdMateria;
            InitializeComponent();

            CargarDatos(_IdMateria);
        }

        private void CargarDatos(int id)
        {
            try
            {
                List<materia_mra> list = db.materia_mra.ToList();
                materia_mra materia_Mra = list.Find(x => x.mra_id == id);
                txtNombre.Text = materia_Mra.mra_nombre;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                materia_mra materia_Mra =
                (from mra in db.materia_mra
                 where mra.mra_id == _IdMateria
                 select mra).Single();
                materia_Mra.mra_nombre = txtNombre.Text;
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
