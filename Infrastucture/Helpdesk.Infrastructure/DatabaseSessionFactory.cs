using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Helpdesk.Infrastructure.Factory;
using NHibernate;

namespace Helpdesk.Infrastructure
{
    public class DatabaseSessionFactory : IDatabaseSessionFactory
    {
        #region Fields

        private IDatabaseSessionFactory _sessionFactory;

        #endregion

        #region Private Methods

        private IDatabaseSessionFactory GetSessionFactory()
        {
            if (_sessionFactory != null)
            {
                return _sessionFactory;
            }

            _sessionFactory = HttpContext.Current == null
                                  ? (IDatabaseSessionFactory)new ThreadBasedDatabaseSessionFactory()
                                  : new WebBasedDatabaseSessionFactory();
            return _sessionFactory;
        }

        #endregion


        #region Public Methods

        public ISession Retrieve()
        {
            return GetSessionFactory().Retrieve();
        }

        public ISession OpenIndependentSession()
        {
            return GetSessionFactory().OpenIndependentSession();
        }

        public void Close()
        {
            GetSessionFactory().Close();
        }

        #endregion
    }
}
