﻿using NbaApi.Services.NBAApiService;
using NbaApi.ViewModels;
using NbaApi.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NbaApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            App.MyGrid = MyGrid;
            var homeUC = new HomeUC();
            var homeVM = new HomeViewModel();
            homeUC.DataContext = homeVM;
            App.ChangePage(homeUC);
        }
    }
}
