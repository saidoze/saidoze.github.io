using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using quicky.bakkers.models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quicky.bakkers.services.Services
{
    public class ServiceBase<T> where T : IFirebaseModelBase<T>
    {
        //https://github.com/rlamasb/Firebase.Xamarin
        private string _firebaseAuthorizationToken = Constants.FirebaseAuthorizationToken;
        private FirebaseClient _firebaseClient;
        private string _firebaseItemDbName;

        public ServiceBase(string itemDbName)
        {
            _firebaseClient = new FirebaseClient(Constants.FirebaseBaseUrl);
            _firebaseItemDbName = itemDbName;
        }

        public virtual List<T> CreateObjects(IReadOnlyCollection<FirebaseObject<T>> items)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllItemsAsync()
        {
            try
            {
                var items = await _firebaseClient
                  .Child(_firebaseItemDbName)
                  //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                  //.OrderByKey()
                  //.LimitToFirst(2)
                  .OnceAsync<T>();

                if (items != null)
                    return CreateObjects(items);

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<T> GetAllItems()
        {
            try
            {
                var items = _firebaseClient
                  .Child(_firebaseItemDbName)
                  //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
                  //.OrderByKey()
                  //.LimitToFirst(2)
                  .OnceAsync<T>().Result;

                if (items != null)
                    return CreateObjects(items);

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> SaveItem(T item)
        {
            try
            {
                if (string.IsNullOrEmpty(item.Key))
                {
                    var result = await _firebaseClient
                      .Child(_firebaseItemDbName)
                      .PostAsync(item, false);

                    if (result != null)
                        return item.WithKey(result.Key);
                }
                else
                {
                    await _firebaseClient
                      .Child(_firebaseItemDbName)
                      .Child(item.Key)
                      .PutAsync(item);
                }

                return item;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteItemByKey(string key)
        {
            try
            {
                await _firebaseClient
                      .Child(_firebaseItemDbName)
                      .Child(key)
                      .DeleteAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
