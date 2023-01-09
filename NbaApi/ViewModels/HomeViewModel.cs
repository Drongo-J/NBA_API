using NbaApi.ApiEntities.Teams;
using NbaApi.Commands;
using NbaApi.Models;
using NbaApi.Services;
using NbaApi.Services.NBAApiService;
using NbaApi.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace NbaApi.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public RelayCommand SelectPageCommand { get; set; }
        public RelayCommand SelectionChangedCommand { get; set; }

        private ObservableCollection<Response> allTeams;

        public ObservableCollection<Response> AllTeams
        {
            get { return allTeams; }
            set { allTeams = value; OnPropertyChanged(); }
        }

        private ObservableCollection<PageNo> allPages;

        public ObservableCollection<PageNo> AllPages
        {
            get { return allPages; }
            set { allPages = value; OnPropertyChanged(); }
        }

        private PageNo selectedPageNo;

        public PageNo SelectedPageNo
        {
            get { return selectedPageNo; }
            set { selectedPageNo = value; OnPropertyChanged(); }
        }

        private List<Response> result = null;

        public HomeViewModel()
        {
            LoadData();

            SelectPageCommand = new RelayCommand((o) =>
            {
                var no = SelectedPageNo.No;
                AllTeams = new ObservableCollection<Response>(result.Skip((no - 1) * 10).Take(10));
            });

            SelectionChangedCommand = new RelayCommand((selectedteam) =>
            {
                //var team = selectedteam as Response;
                //var players = new NbaApiService().GetPlayersByTeamIdAsync(team.id);
                // randomly taking players
                var allplayers = JsonHelper<Player>.Deserialize($@"~/../../../Data\players.json");
                var teamPlayers = new List<Player>();
                var random = new Random();
                var indexes = new List<int>();  
                for (int x = 0; x < 12; x++)
                {
                    int index = 0;
                    do
                    {
                        index = random.Next(0, allplayers.Count);
                    } while (indexes.Contains(index));
                    indexes.Add(index);
                    teamPlayers.Add(allplayers[index]);
                }

                var countriesUC = new CountriesUC();
                var countriesVM = new CountriesUCViewModel(teamPlayers.ToList());
                countriesUC.DataContext = countriesVM;
                App.ChangePage(countriesUC);
            });
        }

        public async void LoadData()
        {
            SelectedPageNo = new PageNo { No = 1 };

            //if (File.Exists("players.json"))
            //{
            //    ////var result = JsonHelper<Player>.Deserialize("players.json");
            //    ////AllPlayers = new ObservableCollection<Response>(result);
            //}
            //else
            //{
            //    //playersResult = await service.GetPlayersByTeamIdAsync(1);
            //    //JsonHelper<Player>.Serialize(playersResult, "players.json");
            //    //var AllPlayers = new ObservableCollection<Player>(playersResult);//evde var silin
            //}

            string teamsFileName = $@"~/../../../Data\teams.json";
            if (File.Exists(teamsFileName))
            {
                result = JsonHelper<Response>.Deserialize(teamsFileName);
            }
            else
            {
                try
                {
                    var service = new NbaApiService();
                    result = await service.GetTeamsAsync();
                    JsonHelper<Response>.Serialize(result, "teams.json");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            AllPages = new ObservableCollection<PageNo>();
            var pageCount = decimal.Parse(result.Count.ToString()) / 10;
            int count = (int)Math.Ceiling(pageCount);

            for (int i = 0; i < count; i++)
            {
                allPages.Add(new PageNo
                {
                    No = i + 1
                });
            }

            foreach (var team in result)
            {
                if (team.logo == null)
                {
                    team.logo = @"\Assets\noImage.png";
                }
            }
            AllTeams = new ObservableCollection<Response>(result.Skip(0).Take(10));

        }
    }
}
