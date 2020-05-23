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

        Task<bool> DeleteTabataAsync(Guid id);

        Task<Tabata> GetTabataAsync(Guid id);

        Task<IEnumerable<Tabata>> GetTabatasAsync();
    }
}
