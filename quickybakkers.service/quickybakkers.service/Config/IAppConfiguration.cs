using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Config
{
    public interface IAppConfiguration
    {
        string ConnectionString { get; }
    }
}
