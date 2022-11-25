using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices.Interfaces
{
    /// <summary>
    /// A factory to create instances of <see cref="IWebServiceExecutor"/>.
    /// </summary>
    public interface IWebServiceExecutorFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="IWebServiceExecutor"/>. Actual implementation returned is based on
        /// the <paramref name="name"/> passed. <paramref name="param"/> is needed for construction of some implementations.
        /// </summary>
        /// <param name="name">Name of actual implementation to instantiate.</param>
        /// <param name="param">Additional parameters to pass to. (Required for certain implementations)</param>
        /// <returns>An <see cref="IWebServiceExecutor"/> instance.</returns>
        IWebServiceExecutor CreateInstance(string name, params string[] param);
    }
}
