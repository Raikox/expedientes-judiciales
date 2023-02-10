using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for ProcesosJudiciales.xaml
    /// </summary>
    public partial class ProcesosJudiciales : UserControl
    {

        eoExpedientesEntities db = new eoExpedientesEntities();
        public static DataGrid table;
        public DashBoard _objDashBoard;
        public static DataGrid tableEstado;
        public static DataGrid tableAmbiente;
        private int idExpediente;
        private String numExpediente;

        public ProcesosJudiciales()
        {
            InitializeComponent();
            
            CargarTablaProcesos();
            CargarComboEntidadFinanciera();
            Console.WriteLine("_IdUsu: " + DashBoard._IdUsu);
        }

        public void RecibeObjectoDashBoard(DashBoard dashBoard)
        {
            this._objDashBoard = dashBoard;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            _objDashBoard.CargarProcesosJudicialesNuevo();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int IdExpediente = (tblExpedientes.SelectedItem as expedientes).exp_id;
                _objDashBoard.CargarProcesosJudicialesEditar(IdExpediente);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarTablaProcesos()
        {
            try
            {
                List<expedientes> listExpe = (from exp in db.expediente_exp
                                              join rto in db.registro_rto on exp.rto_id equals rto.rto_id
                                              join mra in db.materia_mra on exp.mra_id equals mra.mra_id
                                              join jdo in db.juzgado_jdo on exp.jdo_id equals jdo.jdo_id
                                              join cja in db.caja_cja on exp.cja_id equals cja.cja_id
                                              join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                              join cdd in db.ciudad_cdd on jdo.cdd_id equals cdd.cdd_id
                                              select new expedientes
                                              {
                                                  exp_id = exp.exp_id,
                                                  rto_id = rto.rto_id, //demandante
                                                  mra_id = mra.mra_id,
                                                  jdo_id = jdo.jdo_id,
                                                  cja_id = cja.cja_id,
                                                  exp_orden = exp.exp_orden,
                                                  exp_numero = exp.exp_numero,
                                                  // 1: entidad financiera  -  2: persona  -  3: empresa
                                                  demandante = (tpo.tpo_numero == 1) ? rto.rto_nombres : (tpo.tpo_numero == 2) ? (rto.rto_nombres + " " + rto.rto_apellidos) : rto.rto_nombres,
                                                  mra_nombre = mra.mra_nombre,
                                                  jdo_nombre = jdo.jdo_nombre + " - " + cdd.cdd_nombre
                                              }).ToList();

                tblExpedientes.ItemsSource = listExpe.OrderBy(x => x.demandante);

                table = tblExpedientes;
                tblExpedientes.Columns[0].Visibility = Visibility.Collapsed;
                tblExpedientes.Columns[1].Visibility = Visibility.Collapsed;
                tblExpedientes.Columns[2].Visibility = Visibility.Collapsed;
                tblExpedientes.Columns[3].Visibility = Visibility.Collapsed;
                tblExpedientes.Columns[4].Visibility = Visibility.Collapsed;
                tblExpedientes.Columns[5].Visibility = Visibility.Collapsed;

                txtTotalExpedientes.Text = Convert.ToString(listExpe.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CargarUbicacion()
        {
            try
            {
                int idExpediente = (tblExpedientes.SelectedItem as expedientes).exp_id;

                //caja
                int idEntidad;
                int idCaja = (tblExpedientes.SelectedItem as expedientes).cja_id;
                List<caja_cja> list = db.caja_cja.ToList();
                caja_cja caja_Cja = list.Find(x => x.cja_id == idCaja);
                idEntidad = caja_Cja.rto_id;

                //orden
                int orden = (tblExpedientes.SelectedItem as expedientes).exp_orden;

                //entidadf
                List<registro_rto> listEntidad = db.registro_rto.ToList();
                registro_rto entidadfinanciera_Ef = listEntidad.Find(x => x.rto_id == idEntidad);

                Color color = (Color)ColorConverter.ConvertFromString(entidadfinanciera_Ef.rto_color);

                
                txtCaja.Text = Convert.ToString(caja_Cja.cja_numero);
                txtOrden.Text = Convert.ToString(orden);
                
                txtEntidad.Text = entidadfinanciera_Ef.rto_nombres;
                txtColor.Background = new SolidColorBrush(color);

                //ambiente                   
                string nombreAmbiente = "";
                List<ambiente> listUbicacion = (from ea in db.expediente_ambiente_ea
                                                join abt in db.ambiente_abt on ea.abt_id equals abt.abt_id
                                                join exp in db.expediente_exp on ea.exp_id equals exp.exp_id
                                                join prl in db.personal_prl on ea.prl_id equals prl.prl_id
                                                where exp.exp_id == idExpediente
                                                select new ambiente
                                                {
                                                    exp_id = exp.exp_id,
                                                    abt_id = ea.abt_id,
                                                    ea_id = ea.ea_id,
                                                    abt_nombre = abt.abt_nombre,
                                                    ea_fecha = ea.ea_fecha,
                                                    ea_motivo = ea.ea_motivo,
                                                    prl_nombre = prl.prl_nombres + " " + prl.prl_apellidos
                                                }
                                                ).ToList();

                if(listUbicacion.Any())
                {
                    int idMax = listUbicacion.Max(x => x.ea_id);
                    foreach (ambiente a in listUbicacion)
                    {
                        if (a.ea_id == idMax)
                        {
                            nombreAmbiente = a.abt_nombre;
                        }
                    }

                    //asignar
                    tblUbicacion.ItemsSource = listUbicacion;
                    tableAmbiente = tblUbicacion;
                    txtAmbiente.Text = nombreAmbiente;
                }
                else
                {
                    tblUbicacion.ItemsSource = listUbicacion;
                    tableAmbiente = tblUbicacion;
                    txtAmbiente.Text = "NO SE AGREGO UN AMBIENTE";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void CargarDetalle()
        {
            try
            {
                //numero exp - demandante - demandado - avales - materia - juzgado
                int idExpediente = (tblExpedientes.SelectedItem as expedientes).exp_id;
                string numeroExpediente = (tblExpedientes.SelectedItem as expedientes).exp_numero;
                string demandante = (tblExpedientes.SelectedItem as expedientes).demandante;
                string materia = (tblExpedientes.SelectedItem as expedientes).mra_nombre;
                string juzgado = (tblExpedientes.SelectedItem as expedientes).jdo_nombre;

                // 1: entidad financiera  -  2: persona  -  3: empresa
                List<string> listDemandados = new List<string>();
                listDemandados = (from ed in db.expediente_demandado_ed
                                  join rto in db.registro_rto on ed.rto_id equals rto.rto_id
                                  join exp in db.expediente_exp on ed.exp_id equals exp.exp_id
                                  join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                  where ed.exp_id == idExpediente
                                  select (tpo.tpo_numero == 1) ? rto.rto_nombres : (tpo.tpo_numero == 2) ? (rto.rto_nombres + " " + rto.rto_apellidos) : rto.rto_nombres
                                  ).ToList().OrderBy(x => x).ToList();

                // 1: entidad financiera  -  2: persona  -  3: empresa
                List<string> listAvales = new List<string>();
                listAvales = (from ea in db.expediente_aval_ea
                              join rto in db.registro_rto on ea.rto_id equals rto.rto_id
                              join exp in db.expediente_exp on ea.exp_id equals exp.exp_id
                              join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                              where ea.exp_id == idExpediente && tpo.tpo_numero == 2
                              select rto.rto_nombres + " " + rto.rto_apellidos
                              ).ToList().OrderBy(x => x).ToList();

                //Asigna datos
                txtExpediente.Text = numeroExpediente;
                txtDemandante.Text = demandante;
                lstDemandado.Items.Clear();
                foreach (string d in listDemandados)
                {
                    lstDemandado.Items.Add(d);
                }
                lstAval.Items.Clear();
                foreach (string a in listAvales)
                {
                    lstAval.Items.Add(a);
                }
                txtMateria.Text = materia;
                txtJuzgado.Text = juzgado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
                        
        }

        private void CargarTablaEstado()
        {
            try
            {
                idExpediente = (tblExpedientes.SelectedItem as expedientes).exp_id;
                numExpediente = (tblExpedientes.SelectedItem as expedientes).exp_numero;

                List<estado> listEstado = (from ee in db.expediente_estado_ee
                                           join edo in db.estado_edo on ee.edo_id equals edo.edo_id
                                           join prl in db.personal_prl on ee.prl_id equals prl.prl_id
                                           join exp in db.expediente_exp on ee.exp_id equals exp.exp_id
                                           where exp.exp_id == idExpediente
                                           select new estado
                                           {
                                               edo_id = ee.edo_id,
                                               ee_fecha = ee.ee_fecha,
                                               edo_nombre = edo.edo_nombre,
                                               ee_motivo = ee.ee_motivo,
                                               prl_nombres = prl.prl_nombres + " " + prl.prl_apellidos,
                                               exp_id = exp.exp_id
                                           }).ToList();
                tblEstado.ItemsSource = listEstado;
                tableEstado = tblEstado;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void tblExpedientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tblExpedientes.SelectedIndex != -1)
            {
                CargarDetalle();
                CargarTablaEstado();
                CargarUbicacion();
                
            }
        }
        
        private void btnNuevoEstado_Click(object sender, RoutedEventArgs e)
        {
            ProcesosJudicialesEstado procesosJudicialesEstado = new ProcesosJudicialesEstado(idExpediente,numExpediente);
            procesosJudicialesEstado.ShowDialog();
        }

        private void btnNuevoAmbiente_Click(object sender, RoutedEventArgs e)
        {
            ProcesosJudicialesAmbiente procesosJudicialesAmbiente = new ProcesosJudicialesAmbiente(idExpediente, numExpediente);
            procesosJudicialesAmbiente.ShowDialog();
        }
        
        private void txtBusqueda_TextChanged(object sender, TextChangedEventArgs e)
        { 
            try
            {
                List<expedientes> listExp = (from exp in db.expediente_exp
                                             join rto in db.registro_rto on exp.rto_id equals rto.rto_id
                                             join mra in db.materia_mra on exp.mra_id equals mra.mra_id
                                             join jdo in db.juzgado_jdo on exp.jdo_id equals jdo.jdo_id
                                             join cja in db.caja_cja on exp.cja_id equals cja.cja_id
                                             join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                             join cdd in db.ciudad_cdd on jdo.cdd_id equals cdd.cdd_id
                                             select new expedientes
                                             {
                                                 exp_id = exp.exp_id,
                                                 rto_id = rto.rto_id, //demandante
                                                 mra_id = mra.mra_id,
                                                 jdo_id = jdo.jdo_id,
                                                 cja_id = cja.cja_id,
                                                 exp_orden = exp.exp_orden,
                                                 exp_numero = exp.exp_numero,
                                                 // 1: entidad financiera  -  2: persona  -  3: empresa
                                                 demandante = (tpo.tpo_numero == 1) ? rto.rto_nombres : (tpo.tpo_numero == 2) ? (rto.rto_nombres + " " + rto.rto_apellidos) : rto.rto_nombres,
                                                 mra_nombre = mra.mra_nombre,
                                                 jdo_nombre = jdo.jdo_nombre + " - " + cdd.cdd_nombre
                                             }).ToList();

                //List avales
                List<pjavales> listAvales = (from ea in db.expediente_aval_ea
                                             join rto in db.registro_rto on ea.rto_id equals rto.rto_id
                                             join exp in db.expediente_exp on ea.exp_id equals exp.exp_id
                                             join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                             where tpo.tpo_numero == 2
                                             select new pjavales
                                             {
                                                 exp_id = exp.exp_id,
                                                 rto_id = rto.rto_id,
                                                 aval_nombre = rto.rto_nombres + " " + rto.rto_apellidos
                                             }
                                            ).ToList();

                //List demandados
                List<pjdemandados> listDemandados = (from ed in db.expediente_demandado_ed
                                                     join rto in db.registro_rto on ed.rto_id equals rto.rto_id
                                                     join exp in db.expediente_exp on ed.exp_id equals exp.exp_id
                                                     join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                                     select new pjdemandados
                                                     {
                                                         exp_id = exp.exp_id,
                                                         rto_id = rto.rto_id,
                                                         demandado_nombre = (tpo.tpo_numero == 1) ? rto.rto_nombres : (tpo.tpo_numero == 2) ? (rto.rto_nombres + " " + rto.rto_apellidos) : rto.rto_nombres
                                                     }
                                                      ).ToList();

                String busqueda = txtBusqueda.Text;
                IEnumerable<expedientes> resultado = null;
                IEnumerable<expedientes> resultadoAux = null;
                IEnumerable<pjdemandados> listDemandadosR;
                IEnumerable<pjavales> listAvalesR;

                if (busqueda.Equals("")) //Default
                {
                    tblExpedientes.ItemsSource = listExp;
                    table = tblExpedientes;
                    tblExpedientes.Columns[0].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[1].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[2].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[3].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[4].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[5].Visibility = Visibility.Collapsed;
                }
                else if (rbnNumero.IsChecked == true) //num expediente
                {

                    resultado = listExp.Where(x => x.exp_numero.Contains(busqueda));
                }
                else if (rbnDemandante.IsChecked == true) //demandante
                {

                    resultado = listExp.Where(x => x.demandante.Contains(busqueda));
                }
                else if (rbnDemandado.IsChecked == true) //demandados
                {
                    int aux = 0;
                    resultado = null;
                    resultadoAux = null;
                    //Busca coincidencias con demandados
                    listDemandadosR = listDemandados.Where(x => x.demandado_nombre.Contains(busqueda));

                    if (listDemandadosR.Any())
                    {
                        //Si existen demandados en la busqueda, se recorren para
                        //ver a que expedientes pertenecen
                        foreach (pjdemandados x in listDemandadosR)
                        {
                            if (x.exp_id != aux) //valida que no se repitan los expedientes
                            {
                                resultadoAux = listExp.Where(y => y.exp_id == x.exp_id);

                                //validacion para concatenar el resultado
                                if (resultado == null)
                                {
                                    resultado = resultadoAux;
                                }
                                else
                                {
                                    if (resultadoAux.Any())
                                    {
                                        resultado = resultado.Concat(resultadoAux);
                                    }
                                }
                                aux = x.exp_id;
                            }
                        }
                    }
                    else
                    {
                        List<expedientes> L = new List<expedientes>();
                        resultado = L;
                    }
                }
                else if (rbnAval.IsChecked == true) //avales
                {
                    int aux = 0;
                    resultado = null;
                    resultadoAux = null;
                    listAvalesR = listAvales.Where(x => x.aval_nombre.Contains(busqueda));

                    if (listAvalesR.Any())
                    {
                        //Si existen avales en la busqueda, se recorren para
                        //ver a que expedientes pertenecen
                        foreach (pjavales x in listAvalesR)
                        {
                            if (x.exp_id != aux) //valida que no se repitan los expedientes
                            {
                                resultadoAux = listExp.Where(y => y.exp_id == x.exp_id);

                                //validacion para concatenar el resultado
                                if (resultado == null)
                                {
                                    resultado = resultadoAux;
                                }
                                else
                                {
                                    if (resultadoAux.Any())
                                    {
                                        resultado = resultado.Concat(resultadoAux);
                                    }
                                }
                                aux = x.exp_id;
                            }
                        }
                    }
                    else
                    {
                        List<expedientes> L = new List<expedientes>();
                        resultado = L;
                    }
                }

                //Asigna el resultado a la tabla
                if (resultado != null)
                {
                    tblExpedientes.ItemsSource = resultado;
                    table = tblExpedientes;
                    tblExpedientes.Columns[0].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[1].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[2].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[3].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[4].Visibility = Visibility.Collapsed;
                    tblExpedientes.Columns[5].Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void cbxEntidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int idEntidad = Convert.ToInt32(cbxEntidadesFinancieras.SelectedValue);
                List<expedientes> listExpe = (from exp in db.expediente_exp
                                              join rto in db.registro_rto on exp.rto_id equals rto.rto_id
                                              join mra in db.materia_mra on exp.mra_id equals mra.mra_id
                                              join jdo in db.juzgado_jdo on exp.jdo_id equals jdo.jdo_id
                                              join cja in db.caja_cja on exp.cja_id equals cja.cja_id
                                              join tpo in db.tipo_tpo on rto.tpo_id equals tpo.tpo_id
                                              join cdd in db.ciudad_cdd on jdo.cdd_id equals cdd.cdd_id
                                              where cja.rto_id == idEntidad
                                              select new expedientes
                                              {
                                                  exp_id = exp.exp_id
                                              }).ToList();

                txtTotalEntidad.Text = Convert.ToString(listExpe.Count);
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
                cbxEntidadesFinancieras.ItemsSource = list.OrderBy(x => x.ef_nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
