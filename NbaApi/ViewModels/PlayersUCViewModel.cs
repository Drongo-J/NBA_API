using NbaApi.Commands;
using NbaApi.Models;
using NbaApi.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NbaApi.ViewModels
{
    public class PlayersUCViewModel : BaseViewModel
    {
        public RelayCommand BackCommand { get; set; }

        private ObservableCollection<Player> players;

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        public PlayersUCViewModel(List<Player> _players)
        {
            Players = new ObservableCollection<Player>(_players);

            BackCommand = new RelayCommand((b) =>
            {
                App.ExecuteBackCommand();
            });
        }

    }
}
