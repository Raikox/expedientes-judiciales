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
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for JuzgadosEditar.xaml
    /// </summary>
    /// 
    public partial class JuzgadosEditar : Window
    {

        int _IdJuzgado;
        int _IdCiudad;
        eoExpedientesEntities db = new eoExpedientesEntities();

        public JuzgadosEditar(int IdJuzgado, int IdCiudad)
        {
            this._IdJuzgado = IdJuzgado;
            this._IdCiudad = IdCiudad;
            InitializeComponent();

            CargarComboCiudades();
            CargarDatos(_IdJuzgado, _IdCiudad);
        }

        private void CargarDatos(int idJ, int idC)
        {
            try
            {
                List<juzgado_jdo> listj = db.juzgado_jdo.ToList();
                juzgado_jdo juzgado_Jdo = listj.Find(x => x.jdo_id == idJ);
                txtNombre.Text = juzgado_Jdo.jdo_nombre;
                cbxCiudad.SelectedValue = Convert.ToString(idC);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarComboCiudades()
        {
            try
            {
                //DataConext se lee a traves del Binding
                List<ciudad_cdd> list = list = db.ciudad_cdd.ToList();
                DataContext = list;
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
                juzgado_jdo juzgado_Jdo =
                (from jdo in db.juzgado_jdo
                 where jdo.jdo_id == _IdJuzgado
                 select jdo).Single();
                juzgado_Jdo.jdo_nombre = txtNombre.Text;
                juzgado_Jdo.cdd_id = (int)cbxCiudad.SelectedValue;
                db.SaveChanges();
                Juzgados.table.ItemsSource = (from j in db.juzgado_jdo
                                              join c in db.ciudad_cdd
                                              on j.cdd_id equals c.cdd_id
                                              select new juzgados
                                              {
                                                  jdo_id = j.jdo_id,
                                                  jdo_nombre = j.jdo_nombre,
                                                  cdd_nombre = c.cdd_nombre,
                                                  cdd_id = c.cdd_id
                                              }).ToList().OrderBy(x => x.cdd_nombre).ThenBy(x => x.jdo_nombre);
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
