using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWinServicesForce.LogicLayer
{
    public interface IBusinessProcess : IDisposable
    {
        /// <summary>
        /// Runs the business logic here.
        /// </summary>
        Task Run();
    }
}
