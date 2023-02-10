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
    /// Interaction logic for EstadoExpedienteEditar.xaml
    /// </summary>
    public partial class EstadoExpedienteEditar : Window
    {
        int _IdEstado;
        string _NombreEstado;
        string _DescripcionEstado;
        eoExpedientesEntities db = new eoExpedientesEntities();

        public EstadoExpedienteEditar(int IdEstado,string NombreEstado,string DescripcionEstado)
        {
            this._IdEstado = IdEstado;
            _NombreEstado = NombreEstado;
            _DescripcionEstado = DescripcionEstado;
            InitializeComponent();

            txtNombre.Text = _NombreEstado;
            txtDescripcion.Text = _DescripcionEstado;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                estado_edo estado_Edo = (from edo in db.estado_edo
                                         where edo.edo_id == _IdEstado
                                         select edo).Single();
                estado_Edo.edo_nombre = txtNombre.Text;
                estado_Edo.edo_descripcion = txtDescripcion.Text;
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

        private void CargarDatos(int id)
        {
            try
            {
                List<estado_edo> list = db.estado_edo.ToList();
                estado_edo estado_Edo = list.Find(x => x.edo_id == id);
                txtNombre.Text = estado_Edo.edo_nombre;
                txtDescripcion.Text = estado_Edo.edo_descripcion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
