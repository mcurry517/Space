using System;
using System.Collections.Generic;
using Space.Common;
using Space.DataAccess;
using Space.Model;

namespace Space.BusinessLogic
{
    /// <summary>
    /// Purpose: Business Logic Class [CustomersBusiness] for handling the business constrains on table [dbo].[Customers].
    /// </summary>
    public class CustomersBusiness : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public bool InsertEmployee(CustomersEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new CustomersRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:CustomersBusiness::InsertCustomer::Error occured.", ex);
            }
        }

        public bool UpdateEmployee(CustomersEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new CustomersRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:CustomersBusiness::UpdateCustomer::Error occured.", ex);
            }
        }

        public bool DeleteCustomerById(int CustomerID)
        {
            try
            {
                using (var repository = new CustomersRepository())
                {
                    return repository.DeleteById(CustomerID);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:EmployeesBusiness::DeleteCustomerById::Error occured.", ex);
            }
        }

        public CustomersEntity SelectCustomerById(int CustomerID)
        {
            try
            {
                CustomersEntity returnedEntity;
                using (var repository = new CustomersRepository())
                {
                    returnedEntity = repository.SelectById(CustomerID);
                    if (returnedEntity != null);
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:CustomersBusiness::SelectCustomerById::Error occured.", ex);
            }
        }

        public List<CustomersEntity> SelectAllCustomers()
        {
            var returnedEntities = new List<CustomersEntity>();

            try
            {
                using (var repository = new CustomersRepository())
                {
                    foreach (var entity in repository.SelectAll())
                    {
                       returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:CustomersBusiness::SelectAllCustomers::Error occured.", ex);
            }
        }

       
        public CustomersBusiness()
        {
            _loggingHandler = new LoggingHandler();
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
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion
    }
}
