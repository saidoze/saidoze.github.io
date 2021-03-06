﻿using quicky.bakkers.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace quicky.bakkers.services.Providers
{
    public interface IPlayerProvider
    {
        //Task<bool> AddItemAsync(T item);
        //Task<bool> UpdateItemAsync(T item);
        //Task<bool> DeleteItemAsync(T item);
        Task<List<Player>> GetPlayersAsync();
        //Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
