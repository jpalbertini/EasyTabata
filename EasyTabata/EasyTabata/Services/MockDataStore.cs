using EasyTabata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EasyTabata.Services
{
    public class MockDataStore: IDataStore
    {
        readonly List<Tabata> items;

        public MockDataStore()
        {
            items = new List<Tabata>()
            {
                new Tabata { Title = "Default" },
                new Tabata { Title = "Default 2" }
            };
        }

        public async Task<bool> AddTabataAsync(Tabata item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateTabataAsync(Tabata item)
        {
            var oldTabata = items.Where((Tabata arg) => arg.Title == item.Title).FirstOrDefault();
            items.Remove(oldTabata);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteTabataAsync(string title)
        {
            var oldTabata = items.Where((Tabata arg) => arg.Title == title).FirstOrDefault();
            items.Remove(oldTabata);

            return await Task.FromResult(true);
        }

        public async Task<Tabata> GetTabataAsync(string title)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Title == title));
        }

        public async Task<IEnumerable<Tabata>> GetTabatasAsync()
        {
            return await Task.FromResult(items);
        }
    }
}
