using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for ProcesosJudicialesEditar.xaml
    /// </summary>
    public partial class ProcesosJudicialesEditar : UserControl
    {
        public DashBoard _objDashBoard;

        eoExpedientesEntities db = new eoExpedientesEntities();

        public List<pjdemandados> listDemandados = new List<pjdemandados>();
        public List<pjavales> listAvales = new List<pjavales>();
        public static DataGrid tableDemandados;
        public static DataGrid tableAvales;
        public int _idDemandante;
        private int _IdExpediente;

        public ProcesosJudicialesEditar(int IdExpediente)
        {
            _IdExpediente = IdExpediente;
            InitializeComponent();

            CargarComboMateria();
            CargarComboJuzgado();
            CargarComboEntidadFinanciera();            

            CargarTablaDemandados();
            CargarTablaAvales();

            CargarDatos();
        }

        private void CargarDatos()
        {       
            try
            {
                List<expedientes> listExp = (from exp in db.expediente_exp
                                             join rto in db.registro_rto on exp.rto_id equals rto.rto_id
                                             join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                             join cja in db.caja_cja on exp.cja_id equals cja.cja_id
                                             where exp.exp_id == _IdExpediente
                                             select new expedientes
                                             {
                                                 exp_numero = exp.exp_numero,
                                                 exp_orden = exp.exp_orden,
                                                 mra_id = exp.mra_id,
                                                 jdo_id = exp.jdo_id,
                                                 rto_id = rto.rto_id,
                                                 demandante = (tpo.tpo_numero == 1) ? rto.rto_nombres : (tpo.tpo_numero == 2) ? (rto.rto_nombres + " " + rto.rto_apellidos) : rto.rto_nombres,
                                                 cja_id = cja.cja_id,
                                                 cli_id = cja.rto_id
                                             }
                                       ).ToList();

                //Datos generales
                txtNumero.Text = listExp.Single().exp_numero;
                cbxMateria.SelectedValue = Convert.ToString(listExp.Single().mra_id);
                cbxJuzgado.SelectedValue = Convert.ToString(listExp.Single().jdo_id);

                //Demandante
                _idDemandante = listExp.Single().rto_id;
                txtDemandante.Text = listExp.Single().demandante;

                //Ubicacion
                txtOrden.Text = Convert.ToString(listExp.Single().exp_orden);
                cbxEntidad.SelectedValue = Convert.ToString(listExp.Single().cli_id);

                CargarComboCajas(Convert.ToInt32(cbxEntidad.SelectedValue));

                cbxCaja.SelectedValue = Convert.ToString(listExp.Single().cja_id);

                //Demandados
                listDemandados = (from ed in db.expediente_demandado_ed
                                  join rto in db.registro_rto on ed.rto_id equals rto.rto_id
                                  join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                  where ed.exp_id == _IdExpediente
                                  select new pjdemandados
                                  {
                                      rto_id = rto.rto_id,
                                      demandado_nombre = (tpo.tpo_numero == 1) ? rto.rto_nombres : (tpo.tpo_numero == 2) ? (rto.rto_nombres + " " + rto.rto_apellidos) : rto.rto_nombres
                                  }
                                  ).ToList();
                tblDemandados.ItemsSource = null;
                tblDemandados.ItemsSource = listDemandados;

                //Avales
                listAvales = (from ea in db.expediente_aval_ea
                              join rto in db.registro_rto on ea.rto_id equals rto.rto_id
                              join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                              where ea.exp_id == _IdExpediente
                              select new pjavales
                              {
                                  rto_id = rto.rto_id,
                                  aval_nombre = (tpo.tpo_numero == 1) ? rto.rto_nombres : (tpo.tpo_numero == 2) ? (rto.rto_nombres + " " + rto.rto_apellidos) : rto.rto_nombres
                              }
                              ).ToList();
                tblAvales.ItemsSource = null;
                tblAvales.ItemsSource = listAvales;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                      
        }
        
        private void CargarTablaDemandados()
        {
            tblDemandados.ItemsSource = null;
            tblDemandados.ItemsSource = listDemandados.OrderBy(x => x.demandado_nombre);
            tblDemandados.Columns[0].Visibility = Visibility.Collapsed;
            tblDemandados.Columns[1].Visibility = Visibility.Collapsed;
            tableDemandados = tblDemandados;
        }

        private void CargarTablaAvales()
        {
            tblAvales.ItemsSource = null;
            tblAvales.ItemsSource = listAvales.OrderBy(x => x.aval_nombre);
            tblAvales.Columns[0].Visibility = Visibility.Collapsed;
            tblAvales.Columns[1].Visibility = Visibility.Collapsed;
            tableAvales = tblAvales;
        }

        private void CargarComboMateria()
        {
            try
            {
                List<materia_mra> listM = db.materia_mra.ToList();
                cbxMateria.ItemsSource = listM.OrderBy(x => x.mra_nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarComboJuzgado()
        {
            try
            {
                List<juzgados> listJ = (from jdo in db.juzgado_jdo
                                        join cdd in db.ciudad_cdd on jdo.cdd_id equals cdd.cdd_id
                                        select new juzgados
                                        {
                                            jdo_id = jdo.jdo_id,
                                            jdo_full = jdo.jdo_nombre + " - " + cdd.cdd_nombre
                                        }).ToList();
                cbxJuzgado.ItemsSource = listJ.OrderBy(x => x.cdd_nombre).ThenBy(x => x.jdo_nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarComboEntidadFinanciera()
        {
            try
            {
                //DataConext se lee a traves del Binding
                List<entidadfinanciera> list = (from ef in db.registro_rto
                                                join tpo in db.tipo_tpo on ef.tpo_id equals tpo.tpo_id
                                                where tpo.tpo_numero == 1
                                                select new entidadfinanciera
                                                {
                                                    ef_nombre = ef.rto_nombres,
                                                    ef_id = ef.rto_id
                                                }).ToList();
                cbxEntidad.ItemsSource = list.OrderBy(x => x.ef_nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
               
        public void RecibeObjectoDashBoard(DashBoard dashBoard)
        {
            this._objDashBoard = dashBoard;
        }

        private void btnCancelarExpediente_Click(object sender, RoutedEventArgs e)
        {
            _objDashBoard.CargarProcesosJudiciales();
        }

        private void btnNuevoDemandante_Click(object sender, RoutedEventArgs e)
        {
            ProcesosJudicialesEditarDialog procesosJudicialesEditarDialog = new ProcesosJudicialesEditarDialog(true, false);
            procesosJudicialesEditarDialog.RecibeObjetoProcesosJudicialesEditar(this);
            procesosJudicialesEditarDialog.ShowDialog();
        }

        private void btnNuevoDemandado_Click(object sender, RoutedEventArgs e)
        {
            ProcesosJudicialesEditarDialog procesosJudicialesEditarDialog = new ProcesosJudicialesEditarDialog(false, true);
            procesosJudicialesEditarDialog.RecibeObjetoProcesosJudicialesEditar(this);
            procesosJudicialesEditarDialog.ShowDialog();
        }

        private void btnNuevoAvales_Click(object sender, RoutedEventArgs e)
        {
            ProcesosJudicialesEditarDialog procesosJudicialesEditarDialog = new ProcesosJudicialesEditarDialog(false, false);
            procesosJudicialesEditarDialog.RecibeObjetoProcesosJudicialesEditar(this);
            procesosJudicialesEditarDialog.ShowDialog();
        }

        private void btnGuardarExpediente_Click(object sender, RoutedEventArgs e)
        {
            if (!txtNumero.Text.Equals("") && !txtOrden.Text.Equals("") && !txtDemandante.Text.Equals("")
                && cbxMateria.SelectedValue != null && cbxJuzgado.SelectedValue != null && cbxEntidad.SelectedValue != null)
            {
                try
                {
                    string expNumero = txtNumero.Text;
                    int expOrden = Convert.ToInt32(txtOrden.Text);
                    int idDemandante = _idDemandante;
                    int idMateria = Convert.ToInt32(cbxMateria.SelectedValue);
                    int idJuzgado = Convert.ToInt32(cbxJuzgado.SelectedValue);
                    int idCaja = Convert.ToInt32(cbxCaja.SelectedValue);

                    //Guardar cambios - ya no hay estado ni ambiente
                    expediente_exp expediente_Exp = (from exp in db.expediente_exp
                                                     where exp.exp_id == _IdExpediente
                                                     select exp).Single();
                    expediente_Exp.exp_numero = txtNumero.Text;
                    expediente_Exp.exp_orden = Convert.ToInt32(txtOrden.Text);
                    expediente_Exp.rto_id = idDemandante;
                    expediente_Exp.mra_id = idMateria;
                    expediente_Exp.jdo_id = idJuzgado;
                    expediente_Exp.cja_id = idCaja;
                    db.SaveChanges();

                    //Borrar todos los demandados
                    var borrarDemandados = db.expediente_demandado_ed.Where(ed => ed.exp_id == _IdExpediente);
                    db.expediente_demandado_ed.RemoveRange(borrarDemandados);
                    db.SaveChanges();

                    //Agregar demandados
                    if (listDemandados.Any())
                    {
                        foreach (pjdemandados d in listDemandados)
                        {
                            expediente_demandado_ed ed = new expediente_demandado_ed()
                            {
                                exp_id = _IdExpediente,
                                rto_id = d.rto_id
                            };
                            db.expediente_demandado_ed.Add(ed);
                            db.SaveChanges();
                        }
                    }

                    //Borrar todos los avales
                    var borrarAvales = db.expediente_aval_ea.Where(ea => ea.exp_id == _IdExpediente);
                    db.expediente_aval_ea.RemoveRange(borrarAvales);
                    db.SaveChanges();

                    //Agregar todos los avales
                    /*Agregar avales (exp_id - rto_id)*/
                    if (listAvales.Any())
                    {
                        foreach (pjavales a in listAvales)
                        {
                            expediente_aval_ea ea = new expediente_aval_ea()
                            {
                                exp_id = _IdExpediente,
                                rto_id = a.rto_id
                            };
                            db.expediente_aval_ea.Add(ea);
                            db.SaveChanges();
                        }
                    }
                    Console.WriteLine("hola");
                    _objDashBoard.CargarProcesosJudiciales();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }                
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos", "No se pudo agregar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEliminarDemandado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idDemandado = (tblDemandados.SelectedItem as pjdemandados).rto_id;
                var itemToRemove = listDemandados.Single(x => x.rto_id == idDemandado);
                listDemandados.Remove(itemToRemove);
                CargarTablaDemandados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnEliminarAval_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int idAval = (tblAvales.SelectedItem as pjavales).rto_id;
                var itemToRemove = listAvales.Single(x => x.rto_id == idAval);
                listAvales.Remove(itemToRemove);
                CargarTablaAvales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void cbxEntidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbxEntidad.SelectedValue != null)
            {
                CargarComboCajas((int)cbxEntidad.SelectedValue);
            }            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CargarComboCajas(int idEntidad)
        {
            try
            {
                var listC = db.caja_cja.Where(cja => cja.rto_id == idEntidad).Select(cja => new { cja.cja_numero, cja.cja_id }).ToList();
                cbxCaja.ItemsSource = listC.OrderBy(x => x.cja_numero);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
