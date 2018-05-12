using MySql.Data.MySqlClient;
using Quickybakkers.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service
{
    public class SQLHelper
    {
        public MySqlConnection Connection { get; private set; }

        public MySqlDataReader ExecuteReader(string connectionstring, string statement, params MySqlParameter[] mysqlParameters)
        {
            Connection = new MySqlConnection(connectionstring);

            Connection.Open();
            System.Diagnostics.Trace.Write(String.Format("Start query '{0}'", statement));
            MySqlCommand cmd = new MySqlCommand(statement, Connection);
            if(mysqlParameters != null && mysqlParameters.Count() > 0)
                mysqlParameters.ToList().ForEach(p => { if (p != null) { cmd.Parameters.AddWithValue(p.ParameterName, p.Value); } });
            return cmd.ExecuteReader();
        }

        public virtual IEnumerable<TEnt> EnumerateAllBy<TEnt>(string connectionstring, string tablename, string propertyName, object propertyValue) where TEnt : class
        {
            var where = string.Format(" WHERE {0} = @{1}", propertyName, propertyName);
            return EnumerateAll<TEnt>(connectionstring, tablename, new MySqlParameter() { ParameterName = propertyName, Value = propertyValue });
        }

        public virtual IEnumerable<TEnt> EnumerateAll<TEnt>(string connectionstring, string tablename) where TEnt : class
        {
            return EnumerateAll<TEnt>(connectionstring, tablename, null);
        }

        public virtual IEnumerable<TEnt> EnumerateAll<TEnt>(string connectionstring, string tablename, MySqlParameter mysqlParameter) where TEnt : class
        {
            var where = "";
            if (mysqlParameter != null)
                where = string.Format( " WHERE {0} = @{0}", mysqlParameter.ParameterName);
                
            string statement = string.Format("SELECT * FROM {0}{1}", tablename, where);

            using (var reader = this.ExecuteReader(connectionstring, statement, mysqlParameter))
            {
                while (reader.Read())
                {
                    TEnt entity = (TEnt)Activator.CreateInstance(typeof(TEnt));
                    List<PropertyInfo> modelProperties = entity.GetType().GetProperties().OrderBy(p => p.MetadataToken).ToList();
                    for (int i = 0; i < modelProperties.Count; i++)
                        if (modelProperties[i].SetMethod != null)
                            modelProperties[i].SetValue(entity, Convert.ChangeType(reader.GetValue(i), modelProperties[i].PropertyType), null);

                    yield return entity;
                }
            }
        }

        public virtual int Create(string connectionstring, string tableName, IEnumerable<MySqlParameter> mySqlParameters)
        {
            return Create(connectionstring, tableName, mySqlParameters, false);
        }

        public virtual int Create(string connectionstring, string tableName, IEnumerable<MySqlParameter> mySqlParameters, bool returnLastInsertedRowId)
        {
            mySqlParameters = mySqlParameters.AddFixedColumns();

            int returnValue = 0;
            string statement;
            var valuesToUpdate = string.Join(",", mySqlParameters.Select(x => x.ParameterName.ToString()).ToArray());

            statement = string.Format("INSERT INTO {0} ({1}) values ({2});", tableName, valuesToUpdate.Replace("@", ""), valuesToUpdate);

            if (returnLastInsertedRowId)
                statement += "SELECT LAST_INSERT_ID();";

            Connection = new MySqlConnection(connectionstring);

            using (MySqlCommand cmd = new MySqlCommand(statement, Connection))
            {
                cmd.CommandType = CommandType.Text;
                mySqlParameters.ToList().ForEach(p => cmd.Parameters.AddWithValue(p.ParameterName, p.Value));

                try
                {
                    Connection.Open();
                    if (returnLastInsertedRowId)
                        returnValue = Convert.ToInt32(cmd.ExecuteScalar());
                    else
                        returnValue = cmd.ExecuteNonQuery();
                }
                catch (MySqlException sx)
                {
                    returnValue = -1;
                    Console.Write(sx); // your SQL error handling
                }
                catch (Exception ex)
                {
                    returnValue = -2;
                    Console.Write(ex); // other exception handling
                }
                finally
                {
                    // your cleanup routines
                    Connection.Close();
                    Console.Write("Rows Added = " + returnValue);
                }
                return returnValue;
            }
        }

        public virtual int Update(string connectionstring, string tableName, int id, IEnumerable<MySqlParameter> mySqlParameters)
        {
            mySqlParameters = mySqlParameters.AddFixedColumns();
            int rowsAffected = 0;
            string statement;
            var valuesToUpdate = string.Join(",", mySqlParameters.Select(x => string.Format("{0} = {1}", x.ParameterName.Replace("@", ""), x.ParameterName)).ToArray());

            statement = string.Format("UPDATE {0} SET {1} WHERE Id = {2};", tableName, valuesToUpdate, id);
            Connection = new MySqlConnection(connectionstring);

            using (MySqlCommand cmd = new MySqlCommand(statement, Connection))
            {
                cmd.CommandType = CommandType.Text;
                mySqlParameters.ToList().ForEach(p => cmd.Parameters.AddWithValue(p.ParameterName, p.Value));

                try
                {
                    Connection.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                    rowsAffected = id;
                }
                catch (MySqlException sx)
                {
                    rowsAffected = -1;
                    Console.Write(sx); // your SQL error handling
                }
                catch (Exception ex)
                {
                    rowsAffected = -2;
                    Console.Write(ex); // other exception handling
                }
                finally
                {
                    // your cleanup routines
                    Connection.Close();
                    Console.Write("Rows Added = " + rowsAffected);
                }
                return rowsAffected;
            }
        }

        public virtual int Delete(string connectionstring, string tableName, IEnumerable<int> ids)
        {
            int rowsAffected = 0;
            string statement = string.Format("DELETE FROM {0} WHERE Id = @id", tableName);
            Connection = new MySqlConnection(connectionstring);

            using (MySqlCommand cmd = new MySqlCommand(statement, Connection))
            {
                cmd.CommandType = CommandType.Text;
                foreach (var id in ids)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        Connection.Open();
                        rowsAffected += cmd.ExecuteNonQuery();
                    }
                    catch (MySqlException sx)
                    {
                        rowsAffected = -1;
                        Console.Write(sx); // your SQL error handling
                    }
                    catch (Exception ex)
                    {
                        rowsAffected = -2;
                        Console.Write(ex); // other exception handling
                    }
                    finally
                    {
                        // your cleanup routines
                        Connection.Close();
                        Console.Write("Rows Added = " + rowsAffected);
                    }
                }
                return rowsAffected;
            }
        }
    }

}

