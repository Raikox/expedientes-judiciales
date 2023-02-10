using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for EstadoExpediente.xaml
    /// </summary>
    public partial class EstadoExpediente : UserControl
    {
        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;

        public EstadoExpediente()
        {
            InitializeComponent();
            CargarTablaEstados();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idEstado = (tblEstados.SelectedItem as estado_edo).edo_id;
                string nombreEstado = (tblEstados.SelectedItem as estado_edo).edo_nombre;
                string descripcionEtado = (tblEstados.SelectedItem as estado_edo).edo_descripcion;
                EstadoExpedienteEditar estadoExpedienteEditar = new EstadoExpedienteEditar
                    (idEstado, nombreEstado, descripcionEtado);
                estadoExpedienteEditar.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        //private void btnEliminar_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            EstadoExpedienteNuevo estadoExpedienteNuevo = new EstadoExpedienteNuevo();
            estadoExpedienteNuevo.ShowDialog();
        }

        private void CargarTablaEstados()
        {
            try
            {
                List<estado_edo> listEstado = db.estado_edo.ToList();
                tblEstados.ItemsSource = listEstado.OrderBy(x => x.edo_nombre);
                table = tblEstados;
                tblEstados.Columns[0].Visibility = Visibility.Collapsed;
                txtTotalEstados.Text = Convert.ToString(listEstado.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
