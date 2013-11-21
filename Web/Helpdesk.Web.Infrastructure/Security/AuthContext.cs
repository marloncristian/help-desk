using Helpdesk.Dominio.Core.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Helpdesk.Infrastructure.Security
{
    public class AuthContext
    {
        /// <summary>
        /// chave de usuario para o cache
        /// </summary>
        private const string UserCacheKey = "user.cache";

        /// <summary>
        /// Uses the iis cache to mantain the user authentication context
        /// </summary>
        /// <param name="userCompany"></param>
        public static bool SignIn(int codigo, string nome, UsuarioTipo tipo)
        {
            try
            {
                var context = new AuthSession
                {
                    Codigo = codigo,
                    Nome = nome, 
                    Tipo = tipo
                };

                HttpContext.Current.Cache
                    .Add(UserCacheKey, context, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(30),
                         CacheItemPriority.Default, null);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes user from iis cache 
        /// </summary>
        public static void SignOut()
        {
            HttpContext.Current.Cache.Remove(UserCacheKey);
        }

        /// <summary>
        /// Login de usuario
        /// </summary>
        public static AuthSession User
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;

                if (HttpContext.Current.Cache[UserCacheKey] == null)
                    return null;

                return (HttpContext.Current.Cache[UserCacheKey] as AuthSession);
            }
        }

        /// <summary>
        /// Retorna se existem usuarios logados
        /// </summary>
        public static bool IsConnected
        {
            get { return User != null; }
        }
    }
}
