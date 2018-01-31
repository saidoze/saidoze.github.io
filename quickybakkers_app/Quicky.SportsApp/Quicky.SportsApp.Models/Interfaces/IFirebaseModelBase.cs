using System;

namespace Quicky.SportsApp.Models.Interfaces
{
    public interface IFirebaseModelBase<T> 
    {
        string Key { get; set; }

        T WithKey(string key);
    }
}
