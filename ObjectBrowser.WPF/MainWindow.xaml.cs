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
using Microsoft.Win32;
using ObjectBrowser.Shared.Statics;
using ObjectBrowser.Shared.ViewModels.ItemViewModels;
using ObjectBrowser.WPF.Statics;

namespace ObjectBrowser.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {

        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ViewModelLocator.BrowserViewModel.TreeViewItemSelectedCommand.Execute(
                TreeView.SelectedItem as NodeViewModelBase);
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            if(ListView.SelectedItem == null)
                return;
            
            ViewModelLocator.BrowserViewModel.ListViewItemSelectedCommand.Execute(
                ListView.SelectedItem as NodeViewModelBase);
        }

        private async void BrowseButtonOnClick(object sender, RoutedEventArgs e)
        {
            var picker = new OpenFileDialog();



            // Set filter for file extension and default file extension 
            picker.DefaultExt = ".dll";


            // Display OpenFileDialog by calling ShowDialog method 
            if (picker.ShowDialog() == true)
            {
                await ViewModelLocator.BrowserViewModel.LoadAssembly(picker.FileName);
            }
        }
    }
}
