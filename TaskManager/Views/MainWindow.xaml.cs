﻿using System;
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
using TaskManager.ViewModels;

namespace TaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        MainViewModel MainViewModel { get; set; } 
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel = new MainViewModel();
            this.DataContext = MainViewModel;
            MainViewModel.Searchtxtbox = searchtxtbox;
            MainViewModel.Createtxtbox = createtxtbox;
            MainViewModel.AddBlackListtxtbox = addblacklist;
            MainViewModel.Proceslistview = proceslistview;
            MainViewModel.BlackList = BlackListlstbox;
        }
    }
}
