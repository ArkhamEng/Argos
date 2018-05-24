using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Util
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load(@"C:\Users\Administrador\Downloads\CPdescarga.xml");

            //SaveStates(doc);

            //SaveTowns(doc);

            SaveSettlements(doc);
        }

        static void SaveStates(XDocument doc)
        {
            //obtengo estados
            var States = (from i in doc.Descendants("{NewDataSet}table")
                          select new
                          {
                              StateId = i.Element("{NewDataSet}c_estado").Value,
                              Name = i.Element("{NewDataSet}d_estado").Value,
                          }).ToList();

            //agrupo los estados
            var GrpStates = (from s in States
                             group s by new
                             {
                                 s.Name,
                                 s.StateId
                             } into grp
                             select new State
                             {
                                 Name = grp.Key.Name,
                                 StateId = grp.Key.StateId
                             }
                       ).OrderBy(s => s.StateId).ToList();

            try
            {
                using (var db = new ArgosDBEntities())
                {
                    db.State.AddRange(GrpStates);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }


        static void SaveTowns(XDocument doc)
        {
            var Municipality = (from i in doc.Descendants("{NewDataSet}table")
                                select new
                                {
                                    IdMunicipality = i.Element("{NewDataSet}c_mnpio").Value,
                                    StateId = i.Element("{NewDataSet}c_estado").Value,
                                    Name = i.Element("{NewDataSet}D_mnpio").Value,
                                }).ToList();

            //agrupo los estados
            var grpTowns = (from m in Municipality
                            group m by new
                            {
                                m.Name,
                                m.IdMunicipality,
                                m.StateId
                            } into grp
                            select new Town
                            {
                                TownId = grp.Key.StateId + grp.Key.IdMunicipality,
                                Name = grp.Key.Name,
                                StateId = grp.Key.StateId
                            }
                       ).OrderBy(m => m.TownId).ToList();

            try
            {
                BulkTowns(grpTowns);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        static void SaveSettlements(XDocument doc)
        {
            //obtengo asentamientos
            var Assenment = (from i in doc.Descendants("{NewDataSet}table")
                             select new
                             {
                                 IdAsenta = i.Element("{NewDataSet}id_asenta_cpcons").Value,
                                 IdCode = i.Element("{NewDataSet}d_codigo").Value,
                                 IdTown = i.Element("{NewDataSet}c_estado").Value + i.Element("{NewDataSet}c_mnpio").Value,
                                 Name = i.Element("{NewDataSet}d_asenta").Value,
                                 Tipo = i.Element("{NewDataSet}d_tipo_asenta").Value,
                                 Zona = i.Element("{NewDataSet}d_zona").Value
                             }).ToList();


            //agrupo los estados
            var grpSettlements = (from a in Assenment
                                  group a by new
                                  {
                                      a.IdAsenta,
                                      a.IdCode,
                                      a.Name,
                                      a.Tipo,
                                      a.Zona,
                                      a.IdTown
                                  } into grp
                                  select new Settlement
                                  {
                                      TownId = grp.Key.IdTown,
                                      Code = grp.Key.IdCode,
                                      Name = grp.Key.Name,
                                      Type = grp.Key.Tipo,
                                      Zone = grp.Key.Zona,
                                      InsDate =DateTime.Now,
                                      InsUser="System"
                                  }
                       ).ToList();


            try
            {
                BulkSettlments(grpSettlements);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }


        static void CompleateTable(XDocument doc)
        {
            var CompleateTable = (from i in doc.Descendants("{NewDataSet}table")
                                  where i.Element("{NewDataSet}d_codigo").Value.Contains("91826")
                                  select new
                                  {
                                      Codigo = i.Element("{NewDataSet}d_codigo").Value,
                                      IdAsenta = i.Element("{NewDataSet}id_asenta_cpcons").Value,
                                      Asenta = i.Element("{NewDataSet}d_asenta").Value,
                                      TipoAsenta = i.Element("{NewDataSet}d_tipo_asenta").Value,
                                      TipoZona = i.Element("{NewDataSet}d_zona").Value,
                                      IdMunicipio = i.Element("{NewDataSet}c_mnpio").Value,
                                      Municipio = i.Element("{NewDataSet}D_mnpio").Value,
                                      IdCiudad = i.Element("{NewDataSet}c_cve_ciudad").Value,
                                      Ciudad = i.Element("{NewDataSet}d_ciudad").Value,
                                      CP = i.Element("{NewDataSet}d_CP").Value,
                                      IdEstado = i.Element("{NewDataSet}c_estado").Value,
                                      Estado = i.Element("{NewDataSet}d_estado").Value,

                                  }
                                  ).Take(10).ToList();
        }

        public static void BulkTowns(List<Town> list)
        {
            var dt = ToDataTable(list);

            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["SystemDB"].ToString();

            using (var connection = new SqlConnection(cs))
            {
                connection.Open();

                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers, null))
                {
                    bulkCopy.BatchSize = 5000;
                    bulkCopy.ColumnMappings.Add("[TownId]", "[TownId]");
                    bulkCopy.ColumnMappings.Add("[StateId]", "[StateId]");
                    bulkCopy.ColumnMappings.Add("[Name]", "[Name]");

                    bulkCopy.DestinationTableName = "[Config].[Town]";
                    bulkCopy.WriteToServer(dt);
                }
            }
        }


        public static void BulkSettlments(List<Settlement> list)
        {
            var dt = ToDataTable(list);

            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["SystemDB"].ToString();

            using (var connection = new SqlConnection(cs))
            {
                connection.Open();

                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers, null))
                {
                    bulkCopy.BatchSize = 5000;
                    bulkCopy.ColumnMappings.Add("[TownId]", "[TownId]");
                    bulkCopy.ColumnMappings.Add("[Code]", "[Code]");
                    bulkCopy.ColumnMappings.Add("[Name]", "[Name]");
                    bulkCopy.ColumnMappings.Add("[Type]", "[Type]");
                    bulkCopy.ColumnMappings.Add("[Zone]", "[Zone]");
                    bulkCopy.ColumnMappings.Add("[InsDate]", "[InsDate]");
                    bulkCopy.ColumnMappings.Add("[InsUser]", "[InsUser]");
                    bulkCopy.DestinationTableName = "[Config].[Settlement]";
                    bulkCopy.WriteToServer(dt);
                }
            }
        }


        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
