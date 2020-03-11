﻿using System.Configuration;
using System.Windows;
using Client.Application.EF;

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EntityInstance.UserID = int.Parse(ConfigurationManager.AppSettings["userId"]);
            Show();
            DataContext = new ViewModel.VM_MainWindow(this);
        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel.VM_MainWindow).EditCard.Execute(new object());
        }
    }
}
