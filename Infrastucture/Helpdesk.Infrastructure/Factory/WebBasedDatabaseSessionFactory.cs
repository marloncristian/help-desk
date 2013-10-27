using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FluentNHibernate.Cfg;
using Helpdesk.Dominio.Core.Security;
using Helpdesk.Infrastructure.Mapping;
using NHibernate;
using NHibernate.Cfg;

namespace Helpdesk.Infrastructure.Factory
{
    public class WebBasedDatabaseSessionFactory : IDatabaseSessionFactory
    {

        #region Fields

        private const string CurrentSessionKey = "nhibernate.current_session";
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
                            _sessionFactory = Fluently
                                .Configure(config)
                                .Mappings(o => o.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
                                .BuildSessionFactory();
                        }
                    }
                }
                NHibernate.Glimpse.Plugin.RegisterSessionFactory(_sessionFactory);
                return _sessionFactory;
            }
        }

        #endregion

        #region Constructors

        static WebBasedDatabaseSessionFactory()
        {
        }

        #endregion


        #region IDatabaseSessionFactory Members


        public ISession Retrieve()
        {
            HttpContext context = HttpContext.Current;
            ISession currentSession = context.Items[CurrentSessionKey] as ISession;

            if (currentSession == null)
            {
                currentSession = SessionFactory.OpenSession();
                currentSession.FlushMode = FlushMode.Never;
                context.Items[CurrentSessionKey] = currentSession;
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
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                ISession currentSession = context.Items[CurrentSessionKey] as ISession;

                if (currentSession == null)
                {
                    return;
                }

                currentSession.Close();
                context.Items.Remove(CurrentSessionKey);
            }
        }

        #endregion

    }
}
