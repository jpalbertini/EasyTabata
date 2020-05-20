using EasyTabata.Models;
using EasyTabata.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace EasyTabata.ViewModels
{
    class TabataListViewModel
    {
        public ObservableCollection<Tabata> Items { get; set; }

        public TabataListViewModel()
        {
            Items = new ObservableCollection<Tabata>();
            var storedList = DependencyService.Get<IDataStore>().GetTabatasAsync().Result;
            foreach(var item in storedList)
                Items.Add(item);
        }
    }
}
