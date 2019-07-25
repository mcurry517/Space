using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Space.Common;
using Space.DataAccess.Common;
using Space.Model;

namespace Space.DataAccess
{
    /// <summary>
    /// Purpose: Data Access Repository Class [CustomersRepository] for the table [dbo].[Customers].
    /// </summary>
    public class CustomersRepository : IRepository<CustomersEntity>, IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private DataHandler _dataHandler;
        private ConfigurationHandler _configurationHandler;
        private DbProviderFactory _dbProviderFactory;
        private string _connectionString;
        private string _connectionProvider;
        private int _errorCode, _rowsAffected;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public bool Insert(CustomersEntity entity)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("INSERT [dbo].[Customers] ");
                sb.Append("( ");
                sb.Append("[CustomerID], ");
                sb.Append("[FirstName], ");
                sb.Append("[LastName], ");
                sb.Append("[PhoneNumber], ");
                sb.Append("[EmailAddress], ");
                sb.Append(") ");
                sb.Append("VALUES ");
                sb.Append("( ");
                sb.Append(" @intCustomerID, ");
                sb.Append(" @chnFirstName, ");
                sb.Append(" @chnLastName, ");
                sb.Append(" @chnPhoneNumber, ");
                sb.Append(" @chnEmailAddress, ");
                sb.Append(") ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intCustomerID", CsType.Int, ParameterDirection.Input, entity.CustomerID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPhoneNumber", CsType.String, ParameterDirection.Input, entity.PhoneNumber);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmailAddress", CsType.String, ParameterDirection.Input, entity.EmailAddress);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Insert method for entity [Customers] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("CustomersRepository::Insert::Error occured.", ex);
            }
        }

        public bool Update(CustomersEntity entity)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("UPDATE [dbo].[Customers] ");
                sb.Append("SET ");
                sb.Append("[FirstName] = @chnFirstName, ");
                sb.Append("[LastName] = @chnLastName, ");
                sb.Append("[PhoneNumber] = @chnPhoneNumber, ");
                sb.Append("[EmailAddress] = @chnEmailAddress, ");
                sb.Append("WHERE ");
                sb.Append("[CustomerID] = @intCustomerID ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intCustomerID", CsType.Int, ParameterDirection.Input, entity.CustomerID);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnFirstName", CsType.String, ParameterDirection.Input, entity.FirstName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnLastName", CsType.String, ParameterDirection.Input, entity.LastName);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnPhoneNumber", CsType.String, ParameterDirection.Input, entity.PhoneNumber);
                        _dataHandler.AddParameterToCommand(dbCommand, "@chnEmailAddress", CsType.String, ParameterDirection.Input, entity.EmailAddress);


                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Update method for entity [Customers] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("CustomersRepository::Update::Error occured.", ex);
            }
        }

        public bool DeleteById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("DELETE FROM [dbo].[Customers] ");
                sb.Append("WHERE ");
                sb.Append("[CustomerID] = @intCustomerID ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intCustomerID", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Delete method for entity [Customers] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("CustomersRepository::Delete::Error occured.", ex);
            }
        }

        public CustomersEntity SelectById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            CustomersEntity returnedEntity = null;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("SELECT ");
                sb.Append("[CustomerID], ");
                sb.Append("[FirstName], ");
                sb.Append("[LastName], ");
                sb.Append("[PhoneNumber], ");
                sb.Append("[EmailAddress], ");
                sb.Append("FROM [dbo].[Customers] ");
                sb.Append("WHERE ");
                sb.Append("[CustomerID] = @intCustomerID ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db SelectByCustomerID command for entity [Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intCustomerID", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new CustomersEntity();
                                    entity.CustomerID = reader.GetInt32(0);
                                    entity.FirstName = reader.GetString(1);
                                    entity.LastName = reader.GetString(2);
                                    entity.PhoneNumber = reader.GetInt32(3);
                                    entity.EmailAddress = reader.GetString(4);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectByCustomerID method for entity [Customers] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("CustomersRepository::SelectByCustomerID::Error occured.", ex);
            }
        }

        public List<CustomersEntity> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<CustomersEntity>();

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("SELECT ");
                sb.Append("[CustomerID], ");
                sb.Append("[FistName], ");
                sb.Append("[LastName], ");
                sb.Append("[PhoneNumber], ");
                sb.Append("[EmailAddress], ");
                sb.Append("FROM [dbo].[Customers] ");
                sb.Append("ORDER BY [FirstName] ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [Customers] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new CustomersEntity();
                                    entity.CustomerID = reader.GetInt32(0);
                                    entity.FirstName = reader.GetString(1);
                                    entity.LastName = reader.GetString(2);
                                    entity.PhoneNumber = reader.GetInt32(3);
                                    entity.EmailAddress = reader.GetString(4);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectAll method for entity [Customers] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("CustomersRepository::SelectAll::Error occured.", ex);
            }
        }

        public CustomersRepository()
        {
            //Repository Initializations
            _configurationHandler = new ConfigurationHandler();
            _loggingHandler = new LoggingHandler();
            _dataHandler = new DataHandler();
            _connectionString = _configurationHandler.ConnectionString;
            _connectionProvider = _configurationHandler.ConnectionProvider;
            _dbProviderFactory = DbProviderFactories.GetFactory(_connectionProvider);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _configurationHandler = null;
                    _loggingHandler = null;
                    _dataHandler = null;
                    _dbProviderFactory = null;
                }
            }
            _bDisposed = true;
        }

        #endregion
    }
}


