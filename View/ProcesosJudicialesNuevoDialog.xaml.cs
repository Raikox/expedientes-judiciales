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
    /// Interaction logic for ProcesosJudicialesNuevoDialog.xaml
    /// </summary>
    public partial class ProcesosJudicialesNuevoDialog : Window
    {
        private eoExpedientesEntities db = new eoExpedientesEntities();
        private Boolean Demandante, Demandado;
        private ProcesosJudicialesNuevo _objProcesosJudicialesNuevo;

        public ProcesosJudicialesNuevoDialog(Boolean _demandante, Boolean _demandado)
        {
            Demandante = _demandante;
            Demandado = _demandado;
            InitializeComponent();

            string text = _demandante ? "AGREGAR DEMANDANTE" : (_demandado ? "AGREGAR DEMANDADO" : "AGREGAR AVAL");
            txtCabecera.Text = text;

            CargarListEntidadesFinancieras();
            CargarListPersonas();
            CargarListEmpresas();

            if (_demandante)
            {
                //tabPrincipal.SelectedItem = tabItemCliente;
                tabItemCliente.IsSelected = true;
            }
            else if (_demandado)
            {
                tabItemPersona.IsSelected = true;
            }
            else
            {
                tabItemPersona.IsSelected = true;
            }
        }

        private void btnGuardarPersona_Click(object sender, RoutedEventArgs e)
        {
            if(rbnPersonaNuevo.IsChecked == true) //nueva persona
            {
                if(!txtApellido.Text.Equals("") && !txtNombre.Equals(""))
                {
                    try
                    {
                        int idPersona;
                        string nombrePersona;
                        registro_rto registro_Rto = new registro_rto()
                        {
                            rto_nombres = txtNombre.Text,
                            rto_apellidos = txtApellido.Text,
                            rto_color = "#FFFFFF",
                            tpo_id = 2
                        };
                        db.registro_rto.Add(registro_Rto);
                        db.SaveChanges();

                        //se obtiene el id insertado de la persona
                        idPersona = registro_Rto.rto_id;
                        nombrePersona = registro_Rto.rto_nombres + " " + registro_Rto.rto_apellidos;

                        //se procede a guardar los datos
                        if (Demandante)
                        {
                            _objProcesosJudicialesNuevo.txtDemandante.Text = nombrePersona;
                            _objProcesosJudicialesNuevo._idDemandante = idPersona;
                            Close();
                        }
                        else if (Demandado)
                        {
                            AddDemandado(
                            new pjdemandados
                            {
                                rto_id = idPersona,
                                demandado_nombre = nombrePersona
                            });

                            ProcesosJudicialesNuevo.tableDemandados.ItemsSource = null;
                            ProcesosJudicialesNuevo.tableDemandados.ItemsSource = _objProcesosJudicialesNuevo.listDemandados;
                            Close();
                        }
                        else
                        {
                            AddAval(
                            new pjavales
                            {
                                rto_id = idPersona,
                                aval_nombre = nombrePersona
                            });

                            ProcesosJudicialesNuevo.tableAvales.ItemsSource = null;
                            ProcesosJudicialesNuevo.tableAvales.ItemsSource = _objProcesosJudicialesNuevo.listAvales;
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese nombres y apellidos", "No se pudo agregar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (rbnPersonaListado.IsChecked == true) //registro existente 
            {
                if(lstPersonas.SelectedValue != null)
                {
                    try
                    {
                        int idPersona = (int)lstPersonas.SelectedValue;
                        string nombrePersona = ((persona)lstPersonas.SelectedItem).rto_persona;

                        if (Demandante)
                        {
                            _objProcesosJudicialesNuevo.txtDemandante.Text = nombrePersona;
                            _objProcesosJudicialesNuevo._idDemandante = idPersona;
                            Close();
                        }
                        else if (Demandado)
                        {
                            if (!_objProcesosJudicialesNuevo.listDemandados.Exists(x => x.rto_id == idPersona))
                            {
                                AddDemandado(
                                new pjdemandados
                                {
                                    rto_id = idPersona,
                                    demandado_nombre = nombrePersona
                                });

                                ProcesosJudicialesNuevo.tableDemandados.ItemsSource = null;
                                ProcesosJudicialesNuevo.tableDemandados.ItemsSource = _objProcesosJudicialesNuevo.listDemandados;
                                Close();
                            }
                        }
                        else
                        {
                            if (!_objProcesosJudicialesNuevo.listAvales.Exists(x => x.rto_id == idPersona))
                            {
                                AddAval(
                                new pjavales
                                {
                                    rto_id = idPersona,
                                    aval_nombre = nombrePersona
                                });

                                ProcesosJudicialesNuevo.tableAvales.ItemsSource = null;
                                ProcesosJudicialesNuevo.tableAvales.ItemsSource = _objProcesosJudicialesNuevo.listAvales;
                                Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                } 
                else
                {
                    MessageBox.Show("Primero debe seleccionar un item de la lista", "No se pudo agregar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }

        private void btnCancelarPersona_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnGuardarEntidad_Click(object sender, RoutedEventArgs e)
        {
            if(lstEntidades.SelectedValue != null)
            {
                int idEntidad = (int)lstEntidades.SelectedValue;
                string nombreEntidad = ((entidadfinanciera)lstEntidades.SelectedItem).ef_nombre;

                //En entidad f. no se puede agregar nuevo registro, se hace a traves
                //de su propia ventana.
                //Se filtra a que tabla ira el registro.
                if (Demandante)
                {
                    _objProcesosJudicialesNuevo.txtDemandante.Text = nombreEntidad;
                    _objProcesosJudicialesNuevo._idDemandante = idEntidad;
                    Close();
                }
                else if (Demandado)
                {
                    try
                    {
                        if (!_objProcesosJudicialesNuevo.listDemandados.Exists(x => x.rto_id == idEntidad))
                        {
                            AddDemandado(
                                new pjdemandados
                                {
                                    rto_id = idEntidad,
                                    demandado_nombre = nombreEntidad
                                });

                            ProcesosJudicialesNuevo.tableDemandados.ItemsSource = null;
                            ProcesosJudicialesNuevo.tableDemandados.ItemsSource = _objProcesosJudicialesNuevo.listDemandados;
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                }
                else
                {
                    try
                    {
                        if (!_objProcesosJudicialesNuevo.listAvales.Exists(x => x.rto_id == idEntidad))
                        {
                            AddAval(
                                new pjavales
                                {
                                    rto_id = idEntidad,
                                    aval_nombre = nombreEntidad
                                });

                            ProcesosJudicialesNuevo.tableAvales.ItemsSource = null;
                            ProcesosJudicialesNuevo.tableAvales.ItemsSource = _objProcesosJudicialesNuevo.listAvales;
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Primero debe seleccionar un item de la lista", "No se pudo agregar", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
                
        private void btnGuardarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            if (rbnEmpresaNuevo.IsChecked == true) //nueva empresa
            {
                if(!txtNombreEmpresa.Text.Equals(""))
                {
                    try
                    {
                        int idEmpresa;
                        string nombreEmpresa;
                        registro_rto registro_Rto = new registro_rto()
                        {
                            rto_nombres = txtNombreEmpresa.Text,
                            rto_apellidos = "",
                            rto_color = "#FFFFFF",
                            tpo_id = 3
                        };
                        db.registro_rto.Add(registro_Rto);
                        db.SaveChanges();

                        //se obtiene el id insertado de la persona
                        idEmpresa = registro_Rto.rto_id;
                        nombreEmpresa = registro_Rto.rto_nombres;

                        //se procede a guardar los datos
                        if (Demandante)
                        {
                            _objProcesosJudicialesNuevo.txtDemandante.Text = nombreEmpresa;
                            _objProcesosJudicialesNuevo._idDemandante = idEmpresa;
                            Close();
                        }
                        else if (Demandado)
                        {
                            AddDemandado(
                            new pjdemandados
                            {
                                rto_id = idEmpresa,
                                demandado_nombre = nombreEmpresa
                            });

                            ProcesosJudicialesNuevo.tableDemandados.ItemsSource = null;
                            ProcesosJudicialesNuevo.tableDemandados.ItemsSource = _objProcesosJudicialesNuevo.listDemandados;
                            Close();
                        }
                        else
                        {
                            AddAval(
                            new pjavales
                            {
                                rto_id = idEmpresa,
                                aval_nombre = nombreEmpresa
                            });

                            ProcesosJudicialesNuevo.tableAvales.ItemsSource = null;
                            ProcesosJudicialesNuevo.tableAvales.ItemsSource = _objProcesosJudicialesNuevo.listAvales;
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese nombre de la empresa", "No se pudo agregar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (rbnEmpresaListado.IsChecked == true) //registro existente 
            {
                if(lstEmpresas.SelectedValue != null)
                {
                    try
                    {
                        int idEmpresa = (int)lstEmpresas.SelectedValue;
                        List<registro_rto> listR = db.registro_rto.ToList();
                        registro_rto r = listR.Find(x => x.rto_id == idEmpresa);
                        string nombreEmpresa = r.rto_nombres;

                        if (Demandante)
                        {
                            _objProcesosJudicialesNuevo.txtDemandante.Text = nombreEmpresa;
                            _objProcesosJudicialesNuevo._idDemandante = idEmpresa;
                            Close();
                        }
                        else if (Demandado)
                        {
                            if (!_objProcesosJudicialesNuevo.listDemandados.Exists(x => x.rto_id == idEmpresa))
                            {
                                AddDemandado(
                                    new pjdemandados
                                    {
                                        rto_id = idEmpresa,
                                        demandado_nombre = nombreEmpresa
                                    });

                                ProcesosJudicialesNuevo.tableDemandados.ItemsSource = null;
                                ProcesosJudicialesNuevo.tableDemandados.ItemsSource = _objProcesosJudicialesNuevo.listDemandados;
                                Close();
                            }
                        }
                        else
                        {
                            if (!_objProcesosJudicialesNuevo.listAvales.Exists(x => x.rto_id == idEmpresa))
                            {
                                AddAval(
                                new pjavales
                                {
                                    rto_id = idEmpresa,
                                    aval_nombre = nombreEmpresa
                                });

                                ProcesosJudicialesNuevo.tableAvales.ItemsSource = null;
                                ProcesosJudicialesNuevo.tableAvales.ItemsSource = _objProcesosJudicialesNuevo.listAvales;
                                Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    
                }
                else
                {
                    MessageBox.Show("Primero debe seleccionar un item de la lista", "No se pudo agregar", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void AddDemandado(pjdemandados _pjdemandados)
        {
            _objProcesosJudicialesNuevo.listDemandados.Add(_pjdemandados);
        }

        private void AddAval(pjavales _pjavales)
        {
            _objProcesosJudicialesNuevo.listAvales.Add(_pjavales);
        }

        private void btnCancelarEntidad_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCancelarEmpresa_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CargarListEntidadesFinancieras()
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
                lstEntidades.ItemsSource = list.OrderBy(x => x.ef_nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarListPersonas()
        {
            try
            {
                List<persona> listP = (from per in db.registro_rto
                                       join tpo in db.tipo_tpo on per.tpo_id equals tpo.tpo_id
                                       where tpo.tpo_numero == 2
                                       select new persona
                                       {
                                           rto_id = per.rto_id,
                                           rto_apellidos = per.rto_apellidos,
                                           rto_nombres = per.rto_nombres,
                                           rto_persona = per.rto_nombres + " " + per.rto_apellidos
                                       }).ToList();
                lstPersonas.ItemsSource = listP.OrderBy(x => x.rto_nombres);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }

        private void CargarListEmpresas()
        {
            try
            {
                var listEmpresas = db.registro_rto.Where(e => e.tpo_id == 3).Select(e => new { e.rto_nombres, e.rto_id }).ToList();
                lstEmpresas.ItemsSource = listEmpresas.OrderBy(x => x.rto_nombres);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void txtEmpresaBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            String busqueda = txtEmpresaBusqueda.Text;
            IEnumerable<empresa> resultado = null;

            if ( busqueda.Equals("") )
            {
                CargarListEmpresas();
            }
            else
            {
                List<empresa> listE = (from emp in db.registro_rto
                                       join tpo in db.tipo_tpo on emp.tpo_id equals tpo.tpo_id
                                       where tpo.tpo_numero == 3
                                       select new empresa
                                       {
                                           rto_id = emp.rto_id,
                                           rto_nombres = emp.rto_nombres 
                                       }).ToList();
                
                resultado = listE.Where(y => y.rto_nombres.Contains(busqueda));
                

            }

            if(resultado != null)
            {
                lstEmpresas.ItemsSource = resultado.OrderBy(x => x.rto_nombres);
            }
        }

        private void txtPersonaBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        {
            String busqueda = txtPersonaBusqueda.Text;
            IEnumerable<persona> resultado = null;

            if (busqueda.Equals(""))
            {
                CargarListPersonas();
            }
            else
            {
                List<persona> listP = (from per in db.registro_rto
                                       join tpo in db.tipo_tpo on per.tpo_id equals tpo.tpo_id
                                       where tpo.tpo_numero == 2
                                       select new persona
                                       {
                                           rto_id = per.rto_id,
                                           rto_apellidos = per.rto_apellidos,
                                           rto_nombres = per.rto_nombres,
                                           rto_persona = per.rto_nombres + " " + per.rto_apellidos
                                       }).ToList();

                resultado = listP.Where(x => x.rto_persona.Contains(busqueda));

            }

            if (resultado != null)
            {
                lstPersonas.ItemsSource = resultado.OrderBy(x => x.rto_persona);
            }
        }

        public void RecibeObjetoProcesosJudicialesNuevo(ProcesosJudicialesNuevo procesosJudicialesNuevo)
        {
            _objProcesosJudicialesNuevo = procesosJudicialesNuevo;
        }

    }
}
