using quicky.bakkers.models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quicky.bakkers.models
{
    public class Setting : IFirebaseModelBase<Setting>
    {
        public Setting()
        {
        }

        public string Key { get; set; }
        public string AssemblyVersionIos { get; set; }
        public string AssemblyVersionAndroid { get; set; }

        public Setting WithKey(string key)
        {
            this.Key = key;
            return this;
        }
    }
}
