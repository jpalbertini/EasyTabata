using EasyTabata.Models;
using EasyTabata.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EasyTabata.ViewModels
{
    class TabataListViewModel
    {
        public ObservableCollection<Tabata> Items { get; set; }
        public ICommand DeleteTabataCommand { get; private set; }
        public TabataListViewModel()
        {
            Items = new ObservableCollection<Tabata>();
            DeleteTabataCommand = new Command<Guid>(DeleteTabata);
            updateItems();
        }

        public Tabata AddTabata()
        {
            return new Tabata();
        }

        public void DeleteTabata(Guid id)
        {
            if (DependencyService.Get<IDataStore>().DeleteTabataAsync(id).Result)
                updateItems();
        }

        public void updateItems()
        {
            Items.Clear();
            var storedList = DependencyService.Get<IDataStore>().GetTabatasAsync().Result;
            storedList = from tbt in storedList
                         orderby tbt.Title ascending
                         select tbt;

            foreach (var item in storedList)
                Items.Add(item);
        }
    }
}
