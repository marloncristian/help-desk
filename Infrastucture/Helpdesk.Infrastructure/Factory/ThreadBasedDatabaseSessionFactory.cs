using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NHibernate;
using NHibernate.Cfg;

namespace Helpdesk.Infrastructure.Factory
{
    public class ThreadBasedDatabaseSessionFactory : IDatabaseSessionFactory
    {
        #region Fields

        private static readonly Dictionary<int, ISession> Sessions = new Dictionary<int, ISession>();
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static object _sync = new object();

        private static ISessionFactory _sessionFactory;
        private static object _sessionFactorySync = new object();

        #endregion

        #region Properties

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    lock (_sessionFactorySync)
                    {
                        if (_sessionFactory == null)
                        {
                            var config = new Configuration().Configure();
                            _sessionFactory = config.BuildSessionFactory();
                        }
                    }
                }
                return _sessionFactory;
            }
        }

        #endregion

        #region Constructors

        static ThreadBasedDatabaseSessionFactory()
        {
        }

        #endregion

        #region Methods

        public ISession Retrieve()
        {
            ISession currentSession;

            lock (_sync)
            {
                if (!Sessions.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                {
                    currentSession = SessionFactory.OpenSession();
                    currentSession.FlushMode = FlushMode.Never;
                    Sessions.Add(Thread.CurrentThread.ManagedThreadId, currentSession);
                }
                else
                {
                    currentSession = Sessions[Thread.CurrentThread.ManagedThreadId];
                }
            }

            return currentSession;
        }

        public ISession OpenIndependentSession()
        {
            var session = SessionFactory.OpenSession();
            session.FlushMode = FlushMode.Never;
            return session;
        }

        public void Close()
        {
            lock (_sync)
            {
                if (Sessions.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                {
                    ISession currentSession = Sessions[Thread.CurrentThread.ManagedThreadId];
                    currentSession.Close();
                    Sessions.Remove(Thread.CurrentThread.ManagedThreadId);
                }
            }
        }

        #endregion
    }
}
