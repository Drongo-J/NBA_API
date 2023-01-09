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
    public class CountriesUCViewModel : BaseViewModel
    {
		public RelayCommand BackCommand { get; set; }
		public RelayCommand SelectionChangedCommand { get; set; }

        private ObservableCollection<Country> countries;

		public ObservableCollection<Country> Countries
        {
			get { return countries; }
			set { countries = value; OnPropertyChanged(); }
		}

        public CountriesUCViewModel(List<Player> players)
		{
            var list = new List<Country>();
            var countryNames = players.Where(x => x.birth.country != null).Select(x => x.birth.country).Distinct().ToList();
            foreach (var c in countryNames)
            {
                list.Add(new Country
                {
                    CountryName = c,
                    ImagePath = $"https://countryflagsapi.com/png/{c}"
                });
            }

            Countries = new ObservableCollection<Country>(list);

			BackCommand = new RelayCommand((b) =>
            {
                App.ExecuteBackCommand();
            });

            SelectionChangedCommand = new RelayCommand((s) =>
            {
                var selectedcountry = s as Country;
                var playersUC = new PlayersUC();
                var playersUCVM = new PlayersUCViewModel(players.Where(p=>p.birth.country == selectedcountry.CountryName).ToList());
                playersUC.DataContext = playersUCVM;
                App.ChangePage(playersUC);
            });
        }
	}
}
