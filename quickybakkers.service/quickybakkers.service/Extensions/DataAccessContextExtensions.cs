using Actemium.DataAccess;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Extensions
{
    public static class DataAccessContextExtensions
    {
        //public MySqlDataReader ExecuteReader(this DataAccessContext context, string statement, params SqlParameter[] parameters)
        //{
            
        //    this.Open();
        //    System.Diagnostics.Trace.Write(String.Format("Start query '{0}'", statement));
        //    MySqlCommand cmd = new MySqlCommand(statement, this.Connection);
        //    return cmd.ExecuteReader();
        //}

        //protected void Open()
        //{
        //    if (this.Connection == null)
        //        this.Connection = new SqlCeConnection(this.ConnectionString);

        //    if (this.Connection.State == ConnectionState.Closed)
        //        this.Connection.Open();
        //}
    }
}
