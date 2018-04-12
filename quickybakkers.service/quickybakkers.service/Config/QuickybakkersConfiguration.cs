using RA.Core.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quickybakkers.Service.Config
{
    [Export(typeof(IAppConfiguration))]
    public class QuickybakkersConfiguration : IAppConfiguration
    {
        public string ConnectionString => @"Server=localhost; database=quickybakkers_dev; UID=sa; password=123456";
    }
}
