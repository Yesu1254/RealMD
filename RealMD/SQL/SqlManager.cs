using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RealMD.Controllers
{
    public class SqlManager
    {
        //please change this line toa according to your settings 
        public string sqlConnectionString = "user Id = sa; password=3rep @uf-; database=realmd; data source = localhost\\SQLEXPRESS; Integrated Security=True";

        public bool ExecuteNonQuery(string query)
        {
            using (SqlConnection sqlCon = new SqlConnection(sqlConnectionString))
            {
                sqlCon.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand(query, sqlCon);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new Exception($"Sql Query Error ({ex.Message}): " + query);
                }

            }

            return true;

        }

        public System.Data.DataTable GetDataTable(string query)
        {
            var dt = new System.Data.DataTable();
            using (SqlConnection sqlCon = new SqlConnection(sqlConnectionString))
            {
                sqlCon.Open();

                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.Fill(dt);

            }

            return dt;
        }
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        var val = dr[column.ColumnName];
                        if (dr[column.ColumnName] is System.DBNull) val = null;

                        pro.SetValue(obj, val, null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}