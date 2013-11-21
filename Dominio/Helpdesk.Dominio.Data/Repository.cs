using Helpdesk.Infrastructure;
using NHibernate;

namespace Republica.Middleware.Repository
{
    public abstract class Repository<TEntity> where TEntity : class
    {
        #region Properties

        internal ISession Session { get; private set; }

        #endregion

        #region Constructors

        private Repository(ISession session)
        {
            Session = session;
        }

        protected Repository()
            : this(new DatabaseSessionFactory().Retrieve())
        { }

        #endregion

        #region Methods

        public virtual void Add(TEntity obj)
        {
            Session.SaveOrUpdate(obj);
            Session.Flush();
        }

        public void Remove(TEntity obj)
        {
            Session.Delete(obj);
            Session.Flush();
        }

        public virtual TEntity FindById(int id)
        {
            var obj = Session.Load<TEntity>(id);
            return Unproxy(Session, obj);
        }

        private static T Unproxy<T>(ISession session, T maybeProxy) where T : class
        {
            if (maybeProxy != null)
            {
                maybeProxy.ToString();
                maybeProxy.GetType();
                maybeProxy.Equals(null);
            }
            return (T)session.GetSessionImplementation().PersistenceContext.Unproxy(maybeProxy);
        }

        #endregion
    }
}
