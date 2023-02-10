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
    /// Interaction logic for EntidadesFinancierasEditar.xaml
    /// </summary>
    public partial class EntidadesFinancierasEditar : Window
    {
        String _EntidadColor;
        int _IdEntidad;
        eoExpedientesEntities db = new eoExpedientesEntities();        

        public EntidadesFinancierasEditar(int IdEntidad)
        {
            this._IdEntidad = IdEntidad;
            InitializeComponent();
            CargarDatos(_IdEntidad);
        }        

        private void clrColorEntidad_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            _EntidadColor = Convert.ToString(clrColorEntidad.SelectedColor);

        }

        private void CargarDatos(int id)
        {
            try
            {
                List<entidadfinanciera> list = (from rto in db.registro_rto
                                                join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                                where tpo.tpo_numero == 1
                                                select new entidadfinanciera
                                                {
                                                    ef_id = rto.rto_id,
                                                    ef_nombre = rto.rto_nombres,
                                                    ef_color = rto.rto_color
                                                }).ToList();

                entidadfinanciera entidadfinanciera_Ef = list.Find(x => x.ef_id == id);
                txtNombre.Text = entidadfinanciera_Ef.ef_nombre;
                clrColorEntidad.SelectedColor = (Color)ColorConverter.ConvertFromString(entidadfinanciera_Ef.ef_color);
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
                registro_rto entidadfinanciera_Ef =
                                (from ef in db.registro_rto
                                 where ef.rto_id == _IdEntidad
                                 select ef).Single();
                entidadfinanciera_Ef.rto_nombres = txtNombre.Text;
                entidadfinanciera_Ef.rto_color = _EntidadColor;
                db.SaveChanges();
                EntidadesFinancieras.table.ItemsSource = (from rto in db.registro_rto
                                                          join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                                          where tpo.tpo_numero == 1
                                                          select new entidadfinanciera
                                                          {
                                                              ef_id = rto.rto_id,
                                                              ef_nombre = rto.rto_nombres,
                                                              ef_color = rto.rto_color
                                                          }).ToList().OrderBy(x => x.ef_nombre);
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
