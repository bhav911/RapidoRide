using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Models.SqlHelper
{
    public class SqlSPHelper
    {
        public static DataTable SqlStoredProcedure(string spnName, Dictionary<string, object> parameters)
        {
            string conStr = @"data source=192.168.1.117,1580;initial catalog=BhavyaModhiya_490;user id=sa;password=sit@123;";

            DataTable result = new DataTable();
            SqlConnection con = new SqlConnection(conStr);

            SqlCommand cmd = new SqlCommand(spnName, con);
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;

            foreach(KeyValuePair<string, object> kvp in parameters)
            {
                cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
            }

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(result);
            }

            return result;
        }
    }
}
