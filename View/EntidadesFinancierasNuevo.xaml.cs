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
    /// Interaction logic for EntidadesFinancierasNuevo.xaml
    /// </summary>
    public partial class EntidadesFinancierasNuevo : Window
    {
        String _EntidadColor;
        eoExpedientesEntities db = new eoExpedientesEntities();
        private EntidadesFinancieras _objEntidadesFinancieras;

        public EntidadesFinancierasNuevo()
        {
            InitializeComponent();
        }

        public void RecibeObjectoEntidadesFinancierasNuevo(EntidadesFinancieras entidadesFinancieras)
        {
            this._objEntidadesFinancieras = entidadesFinancieras;
        }

        private void clrColorEntidad_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            _EntidadColor = Convert.ToString(clrColorEntidad.SelectedColor);

        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                registro_rto entidadfinanciera_Ef = new registro_rto()
                {
                    rto_apellidos = "",
                    rto_nombres = txtNombre.Text,
                    rto_color = _EntidadColor,
                    tpo_id = 1
                };
                db.registro_rto.Add(entidadfinanciera_Ef);
                db.SaveChanges();
                _objEntidadesFinancieras.CargarTablaEntidadesFinancieras();
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
