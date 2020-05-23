using EasyTabata.Models;
using EasyTabata.Services;
using EasyTabata.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

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

        public void DeleteTabata(Guid id)
        {
            if(DependencyService.Get<IDataStore>().DeleteTabataAsync(id).Result)
                updateItems();
        }

        public void updateItems()
        {
            Items.Clear();
            var storedList = DependencyService.Get<IDataStore>().GetTabatasAsync().Result;
            foreach (var item in storedList)
                Items.Add(item);
        }
    }
}
