namespace Web.DBConnection
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Xml;
    using Web.DBData;

    public sealed class SqlHelper
    {
        private SqlHelper()
        {
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues)
        {
            if ((commandParameters != null) && (parameterValues != null))
            {
                if (commandParameters.Length != parameterValues.Length)
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }
                int index = 0;
                int length = commandParameters.Length;
                while (index < length)
                {
                    if (parameterValues[index] is IDbDataParameter)
                    {
                        IDbDataParameter parameter = parameterValues[index];
                        if (parameter.Value == null)
                        {
                            commandParameters[index].Value = DBNull.Value;
                        }
                        else
                        {
                            commandParameters[index].Value = parameter.Value;
                        }
                    }
                    else if (parameterValues[index] == null)
                    {
                        commandParameters[index].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[index].Value = parameterValues[index];
                    }
                    index++;
                }
            }
        }

        private static void AssignParameterValues(SqlParameter[] commandParameters, DataRow dataRow)
        {
            if ((commandParameters != null) && (dataRow != null))
            {
                int num = 0;
                foreach (SqlParameter parameter in commandParameters)
                {
                    if ((parameter.ParameterName == null) || (parameter.ParameterName.Length <= 1))
                    {
                        throw new Exception(string.Format("Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.", num, parameter.ParameterName));
                    }
                    if (dataRow.Table.Columns.IndexOf(parameter.ParameterName.Substring(1)) != -1)
                    {
                        parameter.Value = dataRow[parameter.ParameterName.Substring(1)];
                    }
                    num++;
                }
            }
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (commandParameters != null)
            {
                foreach (SqlParameter parameter in commandParameters)
                {
                    if (parameter != null)
                    {
                        if (((parameter.Direction == ParameterDirection.InputOutput) || (parameter.Direction == ParameterDirection.Input)) && (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
            }
        }

        public static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            SqlCommand command = new SqlCommand(spName, connection) {
                CommandType = CommandType.StoredProcedure
            };
            if ((sourceColumns != null) && (sourceColumns.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                for (int i = 0; i < sourceColumns.Length; i++)
                {
                    spParameterSet[i].SourceColumn = sourceColumns[i];
                }
                AttachParameters(command, spParameterSet);
            }
            return command;
        }

        public static SqlParameter CreateOutParam(string ParamName, SqlDbType sqlType, int size)
        {
            return new SqlParameter { ParameterName = ParamName, SqlDbType = sqlType, Size = size, Direction = ParameterDirection.Output };
        }

        public static bool ExecuteBoolDataField(string connectionString, CommandType commandType, string commandText)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText);
            if (row == null)
            {
                return false;
            }
            return (bool) row[0];
        }

        public static bool ExecuteBoolDataField(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText, commandParameters);
            if (row == null)
            {
                return false;
            }
            return (bool) row[0];
        }

        public static bool ExecuteBoolReader(string connectionString, CommandType commandType, string commandText)
        {
            bool flag;
            SqlDataReader reader = ExecuteReader(connectionString, commandType, commandText);
            if (reader.Read())
            {
                flag = (bool) reader[0];
            }
            else
            {
                flag = false;
            }
            reader.Close();
            return flag;
        }

        public static string ExecuteDataField(string connectionString, CommandType commandType, string commandText)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText);
            if (row == null)
            {
                return null;
            }
            return row[0].ToString();
        }

        public static string ExecuteDataField(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText, commandParameters);
            if (row == null)
            {
                return null;
            }
            return row[0].ToString();
        }

        public static DataRow ExecuteDataRow(string connectionString, CommandType commandType, string commandText)
        {
            DataTable table = ExecuteDataTable(connectionString, commandType, commandText);
            if (table == null)
            {
                return null;
            }
            return table.Rows[0];
        }

        public static DataRow ExecuteDataRow(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataTable table = ExecuteDataTable(connectionString, commandType, commandText, commandParameters);
            if (table == null)
            {
                return null;
            }
            return table.Rows[0];
        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connection, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataset(connectionString, commandType, commandText, null);
        }

        public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                command.Parameters.Clear();
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                return dataSet;
            }
        }

        public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            bool mustCloseConnection = false;
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                command.Parameters.Clear();
                return dataSet;
            }
        }

        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataSet set2;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    set2 = ExecuteDataset(connection, commandType, commandText, commandParameters);
                }
                finally
                {
                    connection.Close();
                }
            }
            return set2;
        }

        public static DataSet ExecuteDatasetTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteDataset(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDatasetTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
        }

        public static DataSet ExecuteDatasetTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
        }

        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText)
        {
            DataSet set = ExecuteDataset(connectionString, commandType, commandText);
            if (((set != null) && (set.Tables.Count >= 1)) && (set.Tables[0].Rows.Count >= 1))
            {
                return set.Tables[0];
            }
            return null;
        }

        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataSet set = ExecuteDataset(connectionString, commandType, commandText, commandParameters);
            if (((set != null) && (set.Tables.Count >= 1)) && (set.Tables[0].Rows.Count >= 1))
            {
                return set.Tables[0];
            }
            return null;
        }

        public static DateTime ExecuteDateTimeDataField(string connectionString, CommandType commandType, string commandText)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText);
            if (row == null)
            {
                return DateTime.Parse("2005-1-1");
            }
            return (DateTime) row[0];
        }

        public static DateTime ExecuteDateTimeDataField(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText, commandParameters);
            if (row == null)
            {
                return DateTime.Parse("2005-1-1");
            }
            return (DateTime) row[0];
        }

        public static int ExecuteIntDataField(string connectionString, CommandType commandType, string commandText)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText);
            if (row == null)
            {
                return 0;
            }
            return (int) row[0];
        }

        public static int ExecuteIntDataField(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText, commandParameters);
            if (row == null)
            {
                return 0;
            }
            return (int) row[0];
        }

        public static int ExecuteIntReader(string connectionString, CommandType commandType, string commandText)
        {
            int num;
            SqlDataReader reader = ExecuteReader(connectionString, commandType, commandText);
            if (reader.Read())
            {
                num = (int) reader[0];
            }
            else
            {
                num = 0;
            }
            reader.Close();
            return num;
        }

        public static long ExecuteLongDataField(string connectionString, CommandType commandType, string commandText)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText);
            if (row == null)
            {
                return 0L;
            }
            return Convert.ToInt64(row[0]);
        }

        public static long ExecuteLongDataField(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText, commandParameters);
            if (row == null)
            {
                return 0L;
            }
            return Convert.ToInt64(row[0]);
        }

        public static long ExecuteLongReader(string connectionString, CommandType commandType, string commandText)
        {
            long num;
            SqlDataReader reader = ExecuteReader(connectionString, commandType, commandText);
            if (reader.Read())
            {
                num = (long) reader[0];
            }
            else
            {
                num = 0L;
            }
            reader.Close();
            return num;
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connection, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(transaction, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return num;
        }

        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            bool mustCloseConnection = false;
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            int num = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return num;
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            int num;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    num = ExecuteNonQuery(connection, commandType, commandText, commandParameters);
                }
                finally
                {
                    connection.Close();
                }
            }
            return num;
        }

        public static int ExecuteNonQueryTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQueryTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        }

        public static int ExecuteNonQueryTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteReader(connection, commandType, commandText, null);
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteReader(transaction, commandType, commandText, null);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(connectionString, commandType, commandText, null);
        }

        public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, SqlConnectionOwnership.External);
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlDataReader reader;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                reader = ExecuteReader(connection, null, commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
            }
            catch
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw;
            }
            return reader;
        }

        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, SqlConnectionOwnership connectionOwnership)
        {
            SqlDataReader reader;
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            try
            {
                SqlDataReader reader2;
                PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
                if (connectionOwnership == SqlConnectionOwnership.External)
                {
                    reader2 = command.ExecuteReader();
                }
                else
                {
                    reader2 = command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                bool flag2 = true;
                foreach (SqlParameter parameter in command.Parameters)
                {
                    if (parameter.Direction != ParameterDirection.Input)
                    {
                        flag2 = false;
                    }
                }
                if (flag2)
                {
                    command.Parameters.Clear();
                }
                reader = reader2;
            }
            catch
            {
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                throw;
            }
            return reader;
        }

        public static SqlDataReader ExecuteReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteReader(connection, CommandType.StoredProcedure, spName);
        }

        public static SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
        }

        public static SqlDataReader ExecuteReaderTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connection, commandType, commandText, null);
        }

        public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteScalar(transaction, commandType, commandText, null);
        }

        public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connectionString, commandType, commandText, null);
        }

        public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
            object obj2 = command.ExecuteScalar();
            command.Parameters.Clear();
            if (mustCloseConnection)
            {
                connection.Close();
            }
            return obj2;
        }

        public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            bool mustCloseConnection = false;
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            object obj2 = command.ExecuteScalar();
            command.Parameters.Clear();
            return obj2;
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            object obj2;
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    obj2 = ExecuteScalar(connection, commandType, commandText, commandParameters);
                }
                finally
                {
                    connection.Close();
                }
            }
            return obj2;
        }

        public static object ExecuteScalarTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteScalar(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalarTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
        }

        public static object ExecuteScalarTypedParams(string connectionString, string spName, DataRow dataRow)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        public static string ExecuteStringReader(string connectionString, CommandType commandType, string commandText)
        {
            string str;
            SqlDataReader reader = ExecuteReader(connectionString, commandType, commandText);
            if (reader.Read())
            {
                str = reader[0].ToString().Trim();
            }
            else
            {
                str = null;
            }
            reader.Close();
            return str;
        }

        public static DateTime ExecuteTimeReader(string connectionString, CommandType commandType, string commandText)
        {
            DateTime time;
            SqlDataReader reader = ExecuteReader(connectionString, commandType, commandText);
            if (reader.Read())
            {
                time = (DateTime) reader[0];
            }
            else
            {
                time = DateTime.Parse("2005-1-1");
            }
            reader.Close();
            return time;
        }

        public static byte ExecuteTinyIntDataField(string connectionString, CommandType commandType, string commandText)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText);
            if (row == null)
            {
                return 0;
            }
            return (byte) row[0];
        }

        public static byte ExecuteTinyIntDataField(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            DataRow row = ExecuteDataRow(connectionString, commandType, commandText, commandParameters);
            if (row == null)
            {
                return 0;
            }
            return (byte) row[0];
        }

        public static byte ExecuteTinyintReader(string connectionString, CommandType commandType, string commandText)
        {
            byte num;
            SqlDataReader reader = ExecuteReader(connectionString, commandType, commandText);
            if (reader.Read())
            {
                num = (byte) reader[0];
            }
            else
            {
                num = 0;
            }
            reader.Close();
            return num;
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
        {
            return ExecuteXmlReader(connection, commandType, commandText, null);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
        {
            return ExecuteXmlReader(transaction, commandType, commandText, null);
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
        }

        public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            XmlReader reader;
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            bool mustCloseConnection = false;
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            try
            {
                PrepareCommand(command, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
                XmlReader reader2 = command.ExecuteXmlReader();
                command.Parameters.Clear();
                reader = reader2;
            }
            catch
            {
                if (mustCloseConnection)
                {
                    connection.Close();
                }
                throw;
            }
            return reader;
        }

        public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            bool mustCloseConnection = false;
            PrepareCommand(command, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            XmlReader reader = command.ExecuteXmlReader();
            command.Parameters.Clear();
            return reader;
        }

        public static XmlReader ExecuteXmlReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
        }

        public static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((dataRow != null) && (dataRow.ItemArray.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, dataRow);
                return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
        }

        public static void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
        }

        public static void FillDataset(SqlConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, spParameterSet);
            }
            else
            {
                FillDataset(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        public static void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            FillDataset(transaction, commandType, commandText, dataSet, tableNames, null);
        }

        public static void FillDataset(SqlTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            if ((transaction != null) && (transaction.Connection == null))
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if ((spName == null) || (spName.Length == 0))
            {
                throw new ArgumentNullException("spName");
            }
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, spName);
                AssignParameterValues(spParameterSet, parameterValues);
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, spParameterSet);
            }
            else
            {
                FillDataset(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
            }
        }

        public static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    FillDataset(connection, commandType, commandText, dataSet, tableNames);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static void FillDataset(string connectionString, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    FillDataset(connection, spName, dataSet, tableNames, parameterValues);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public static void FillDataset(SqlConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public static void FillDataset(SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public static void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            if ((connectionString == null) || (connectionString.Length == 0))
            {
                throw new ArgumentNullException("connectionString");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private static void FillDataset(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            SqlCommand command = new SqlCommand {
                CommandTimeout = BTPGameManager.GetConnTime()
            };
            bool mustCloseConnection = false;
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                if ((tableNames != null) && (tableNames.Length > 0))
                {
                    string sourceTable = "Table";
                    for (int i = 0; i < tableNames.Length; i++)
                    {
                        if ((tableNames[i] == null) || (tableNames[i].Length == 0))
                        {
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        }
                        adapter.TableMappings.Add(sourceTable, tableNames[i]);
                        sourceTable = sourceTable + ((i + 1)).ToString();
                    }
                }
                adapter.Fill(dataSet);
                command.Parameters.Clear();
            }
            if (mustCloseConnection)
            {
                connection.Close();
            }
        }

        public static int GetIntValue(SqlParameter parameter, int defaultValue)
        {
            if (parameter.Value != DBNull.Value)
            {
                return Convert.ToInt32(parameter.Value);
            }
            return defaultValue;
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if ((commandText == null) || (commandText.Length == 0))
            {
                throw new ArgumentNullException("commandText");
            }
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                if (transaction.Connection == null)
                {
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        public static void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
        {
            if (insertCommand == null)
            {
                throw new ArgumentNullException("insertCommand");
            }
            if (deleteCommand == null)
            {
                throw new ArgumentNullException("deleteCommand");
            }
            if (updateCommand == null)
            {
                throw new ArgumentNullException("updateCommand");
            }
            if ((tableName == null) || (tableName.Length == 0))
            {
                throw new ArgumentNullException("tableName");
            }
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.UpdateCommand = updateCommand;
                adapter.InsertCommand = insertCommand;
                adapter.DeleteCommand = deleteCommand;
                adapter.Update(dataSet, tableName);
                dataSet.AcceptChanges();
            }
        }

        private enum SqlConnectionOwnership
        {
            Internal,
            External
        }
    }
}

