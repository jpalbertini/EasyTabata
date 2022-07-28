using EasyTabata.Models;

namespace EasyTabata.Services
{
    public class MockDataStore : IDataStore
    {
        readonly List<Tabata> items;

        public MockDataStore()
        {
            items = new List<Tabata>()
            {
                new Tabata { Title = "Default" },
                new Tabata { Title = "Default 2" }
            };

            foreach (var item in items)
                item.ID = Guid.NewGuid();
        }

        public async Task<bool> AddTabataAsync(Tabata item)
        {
            item.ID = Guid.NewGuid();
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateTabataAsync(Tabata item)
        {
            var oldTabata = items.Where((Tabata arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(oldTabata);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteTabataAsync(Guid id)
        {
            var oldTabata = items.Where((Tabata arg) => arg.ID == id).FirstOrDefault();
            items.Remove(oldTabata);

            return await Task.FromResult(true);
        }

        public async Task<Tabata> GetTabataAsync(Guid id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Tabata>> GetTabatasAsync()
        {
            return await Task.FromResult(items);
        }
    }
}
