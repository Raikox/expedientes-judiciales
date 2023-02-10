using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace View
{
    /// <summary>
    /// Interaction logic for Materias.xaml
    /// </summary>
    public partial class Materias : UserControl
    {
        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;

        public Materias()
        {
            InitializeComponent();
            CargarTablaMaterias();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            MateriasNuevo materiasNuevo = new MateriasNuevo();
            materiasNuevo.ShowDialog();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idMateria = (tblMaterias.SelectedItem as materia_mra).mra_id;
                MateriasEditar materiasEditar = new MateriasEditar(idMateria);
                materiasEditar.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        //private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //{
        //    int idMateria = (tblMaterias.SelectedItem as materia_mra).mra_id;
        //    var borrarRegistro = db.materia_mra.Where(mra => mra.mra_id == idMateria).Single();
        //    db.materia_mra.Remove(borrarRegistro);
        //    db.SaveChanges();
        //    CargarTablaMaterias();
        //}

        private void CargarTablaMaterias()
        {
            try
            {
                List<materia_mra> listMateria = db.materia_mra.ToList();
                tblMaterias.ItemsSource = listMateria.OrderBy(x => x.mra_nombre);
                table = tblMaterias;
                tblMaterias.Columns[0].Visibility = Visibility.Collapsed;
                txtTotalMaterias.Text = Convert.ToString(listMateria.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
