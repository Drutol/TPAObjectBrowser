using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Shared.BL;
using ObjectBrowser.Shared.ViewModels.ItemViewModels;
using ObjectBrowser.TestAssembly.BusinesLogic;

namespace ObjectBrowser.Shared.ViewModels
{
    public class BrowserViewModel : ViewModelBase
    {
        private readonly IAssemblyMetadataExtractor _assemblyMetadataExtractor;
        private readonly IDataStorage _dataStorage;
        private List<NodeViewModelBase> _items;
        private bool _loading;
        private NodeViewModelBase _nodeViewModelBase;
        private List<KeyValuePair<string, string>> _selectedItemDetails;
        private AssemblyMetadata _metadata;

        public BrowserViewModel(IAssemblyMetadataExtractor assemblyMetadataExtractor, IDataStorage dataStorage)
        {
            _assemblyMetadataExtractor = assemblyMetadataExtractor;
            _dataStorage = dataStorage;
        }

        public async void NavigatedTo()
        {
            Loading = true;
            await Task.Run(async () =>
            {
                 //_metadata =
                 //   _assemblyMetadataExtractor.Extract(Assembly.GetAssembly(typeof(ServiceA)));
                _metadata = await _dataStorage.Retrieve();

                Items = new List<NodeViewModelBase>
                {
                    new AssemblyNodeViewModel(_metadata)
                };
            });
            //await _dataStorage.Save(_metadata);
            Loading = false;
        }

        public NodeViewModelBase NodeViewModelBase
        {
            get => _nodeViewModelBase;
            set
            {
                value?.LoadChildrenCommand.Execute(null);
                _nodeViewModelBase = value;
                RaisePropertyChanged();
            }
        }

        public List<NodeViewModelBase> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }

        public bool Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                RaisePropertyChanged();
            }
        }

        public List<KeyValuePair<string, string>> SelectedItemDetails
        {
            get { return _selectedItemDetails; }
            set
            {
                _selectedItemDetails = value;
                RaisePropertyChanged();
            }
        }

        public ICommand TreeViewItemSelectedCommand =>
            new RelayCommand<NodeViewModelBase>(vm =>
            {
                try
                {
                    NodeViewModelBase = vm;
                    SelectedItemDetails = vm.Details;
                }
                catch (Exception e)
                {

                }
            });

        public ICommand ListViewItemSelectedCommand =>
            new RelayCommand<NodeViewModelBase>(vm =>
            {
                try
                {
                    SelectedItemDetails = vm.Details;
                }
                catch (Exception e)
                {

                }

            });
    }
}
