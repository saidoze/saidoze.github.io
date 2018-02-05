using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quicky.bakkers.models.Interfaces
{
    public interface IFirebaseModelBase<T>
    {
        string Key { get; set; }

        T WithKey(string key);
    }
}
