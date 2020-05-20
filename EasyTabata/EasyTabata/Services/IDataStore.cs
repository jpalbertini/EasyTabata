using EasyTabata.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyTabata.Services
{
    interface IDataStore
    {
        Task<bool> AddTabataAsync(Tabata item);

        Task<bool> UpdateTabataAsync(Tabata item);

        Task<bool> DeleteTabataAsync(string title);

        Task<Tabata> GetTabataAsync(string title);

        Task<IEnumerable<Tabata>> GetTabatasAsync();
    }
}
