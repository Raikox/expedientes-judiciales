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
    /// Interaction logic for ProcesosJudicialesNuevo.xaml
    /// </summary>
    public partial class ProcesosJudicialesNuevo : UserControl
    {
        public DashBoard _objDashBoard;

        eoExpedientesEntities db = new eoExpedientesEntities();

        public List<pjdemandados> listDemandados = new List<pjdemandados>();
        public List<pjavales> listAvales = new List<pjavales>();
        public static DataGrid tableDemandados;
        public static DataGrid tableAvales;
        public int _idDemandante;        

        public ProcesosJudicialesNuevo()
        {
            InitializeComponent();

            CargarComboMateria();
            CargarComboJuzgado();
            CargarComboEntidadFinanciera();
            CargarComboAmbientes();
            CargarComboEstados();

            CargarTablaDemandados();
            CargarTablaAvales();

        }

        
        private void btnNuevoDemandado_Click(object sender, RoutedEventArgs e)
        {           
            ProcesosJudicialesNuevoDialog procesosJudicialesNuevoDialog = new ProcesosJudicialesNuevoDialog(false, true);
            procesosJudicialesNuevoDialog.RecibeObjetoProcesosJudicialesNuevo(this);
            procesosJudicialesNuevoDialog.ShowDialog();
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

        private void btnNuevoAvales_Click(object sender, RoutedEventArgs e)
        {
            ProcesosJudicialesNuevoDialog procesosJudicialesNuevoDialog = new ProcesosJudicialesNuevoDialog(false, false);
            procesosJudicialesNuevoDialog.RecibeObjetoProcesosJudicialesNuevo(this);
            procesosJudicialesNuevoDialog.ShowDialog();
        }

        private void btnNuevoDemandante_Click(object sender, RoutedEventArgs e)
        {
            ProcesosJudicialesNuevoDialog procesosJudicialesNuevoDialog = new ProcesosJudicialesNuevoDialog(true,false);
            procesosJudicialesNuevoDialog.RecibeObjetoProcesosJudicialesNuevo(this);
            procesosJudicialesNuevoDialog.ShowDialog();
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

        private void CargarComboEstados()
        {
            try
            {
                List<estado_edo> listEstados = db.estado_edo.ToList();
                cbxEstado.ItemsSource = listEstados.OrderBy(x => x.edo_nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarComboCajas(int idEntidad)
        {
            try
            {
                var listC = db.caja_cja.Where(cja => cja.rto_id == idEntidad).Select(cja => new { cja.cja_numero, cja.cja_id }).ToList();
                cbxCaja.ItemsSource = listC;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarComboAmbientes()
        {
            try
            {
                List<ambiente_abt> listAmbientes = db.ambiente_abt.ToList();
                cbxAmbiente.ItemsSource = listAmbientes.OrderBy(x => x.abt_nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void cbxEntidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            CargarComboCajas(Convert.ToInt32(cbxEntidad.SelectedValue));
            
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

        private void btnGuardarExpediente_Click(object sender, RoutedEventArgs e)
        {
            
            int idExpedienteAgregado;
            if(!txtNumero.Text.Equals("") && !txtOrden.Text.Equals("") && !txtDemandante.Text.Equals("")
                && cbxMateria.SelectedValue != null && cbxJuzgado.SelectedValue != null && cbxEntidad.SelectedValue != null
                && cbxEstado.SelectedValue != null && cbxAmbiente.SelectedValue != null && cbxCaja.SelectedValue != null)
            {
                try
                {
                    /*Agrega datos a la tabla expedientes (incluye demandante)*/
                    string expNumero = txtNumero.Text;
                    int expOrden = Convert.ToInt32(txtOrden.Text);
                    int idDemandante = _idDemandante;
                    int idMateria = Convert.ToInt32(cbxMateria.SelectedValue);
                    int idJuzgado = Convert.ToInt32(cbxJuzgado.SelectedValue);
                    int idCaja = Convert.ToInt32(cbxCaja.SelectedValue);

                    expediente_exp expediente_Exp = new expediente_exp()
                    {
                        exp_numero = expNumero,
                        exp_orden = expOrden,
                        rto_id = idDemandante,
                        mra_id = idMateria,
                        jdo_id = idJuzgado,
                        cja_id = idCaja
                    };
                    db.expediente_exp.Add(expediente_Exp);
                    db.SaveChanges();

                    //expediente insertado
                    idExpedienteAgregado = expediente_Exp.exp_id;

                    /*Agregar demandados (exp_id - rto_id)*/
                    if (listDemandados.Any())
                    {
                        foreach (pjdemandados d in listDemandados)
                        {
                            expediente_demandado_ed ed = new expediente_demandado_ed()
                            {
                                exp_id = idExpedienteAgregado,
                                rto_id = d.rto_id
                            };
                            db.expediente_demandado_ed.Add(ed);
                            db.SaveChanges();
                        }
                    }

                    /*Agregar avales (exp_id - rto_id)*/
                    if (listAvales.Any())
                    {
                        foreach (pjavales a in listAvales)
                        {
                            expediente_aval_ea ea = new expediente_aval_ea()
                            {
                                exp_id = idExpedienteAgregado,
                                rto_id = a.rto_id
                            };
                            db.expediente_aval_ea.Add(ea);
                            db.SaveChanges();
                        }
                    }

                    /*Historial estados*/
                    var dateHoy = DateTime.Now;
                    int idEstado = (int)cbxEstado.SelectedValue;
                    int idPer = DashBoard._IdUsu;
                    Console.WriteLine("_IdUsu al guardar: " + DashBoard._IdUsu);
                    expediente_estado_ee ee = new expediente_estado_ee()
                    {
                        ee_fecha = dateHoy,
                        ee_motivo = "Registro del expediente en sistema",
                        exp_id = idExpedienteAgregado,
                        edo_id = idEstado,
                        prl_id = idPer
                    };
                    db.expediente_estado_ee.Add(ee);
                    db.SaveChanges();

                    /*Historial ambiente*/
                    int idAmbiente = (int)cbxAmbiente.SelectedValue;
                    expediente_ambiente_ea eam = new expediente_ambiente_ea()
                    {
                        ea_fecha = dateHoy,
                        ea_motivo = "Registro del expediente en sistema",
                        exp_id = idExpedienteAgregado,
                        abt_id = idAmbiente,
                        prl_id = idPer
                    };
                    db.expediente_ambiente_ea.Add(eam);
                    db.SaveChanges();

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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnCancelarExpediente_Click(object sender, RoutedEventArgs e)
        {
            _objDashBoard.CargarProcesosJudiciales();
        }

        public void RecibeObjectoDashBoard(DashBoard dashBoard)
        {
            this._objDashBoard = dashBoard;
        }
    }
}
