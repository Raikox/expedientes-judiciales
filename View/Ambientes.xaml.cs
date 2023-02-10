using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for Ambientes.xaml
    /// </summary>
    public partial class Ambientes : UserControl
    {
        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;

        public Ambientes()
        {
            InitializeComponent();

            CargarTablaAmbientes();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idAmbiente = (tblAmbientes.SelectedItem as ambiente_abt).abt_id;
                AmbientesEditar ambientesEditar = new AmbientesEditar(idAmbiente);
                ambientesEditar.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        //private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void CargarTablaAmbientes()
        {
            try
            {
                var listAmbiente  = db.ambiente_abt.ToList();
                tblAmbientes.ItemsSource = listAmbiente.OrderBy(x => x.abt_nombre);
                table = tblAmbientes;
                tblAmbientes.Columns[0].Visibility = Visibility.Collapsed;

                txtTotalAmbientes.Text = Convert.ToString(listAmbiente.Count);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AmbientesNuevo ambientesNuevo = new AmbientesNuevo();
            ambientesNuevo.ShowDialog();
        }
    }
}
