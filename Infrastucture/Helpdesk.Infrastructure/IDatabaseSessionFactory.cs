using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Helpdesk.Infrastructure
{
    public interface IDatabaseSessionFactory
    {
        /// <summary>
        /// Abre uma sessão do NHibernate.
        /// </summary>
        /// <returns>Sessão do NHibernate.</returns>
        ISession Retrieve();

        /// <summary>
        /// Abre uma sessão independente. 
        /// ATENÇÃO: Essa sessão deve ser fechada MANUALMENTE!
        /// </summary>
        /// <returns>Session</returns>
        ISession OpenIndependentSession();

        /// <summary>
        /// Fecha a sessão do NHibernate.
        /// </summary>
        void Close();
    }
}
