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
        private readonly ILogger _logger;
        private readonly IMessageBoxProvider _messageBoxProvider;
        private List<NodeViewModelBase> _items;
        private bool _loading;
        private NodeViewModelBase _nodeViewModelBase;
        private List<KeyValuePair<string, string>> _selectedItemDetails;
        private AssemblyMetadata _metadata;

        public BrowserViewModel(IAssemblyMetadataExtractor assemblyMetadataExtractor, IDataStorage dataStorage, ILogger logger, IMessageBoxProvider messageBoxProvider)
        {
            _assemblyMetadataExtractor = assemblyMetadataExtractor;
            _dataStorage = dataStorage;
            _logger = logger;
            _messageBoxProvider = messageBoxProvider;
        }

        public async Task LoadAssembly(string asmPath)
        {
            Loading = true;
            try
            {
                //var asm = Assembly.LoadFrom(asmPath);
                var asm = Assembly.GetAssembly(typeof(ServiceA));
                await Task.Run(async () =>
                {
                    _metadata = _assemblyMetadataExtractor.Extract(asm);
                    Items = new List<NodeViewModelBase>
                    {
                        new AssemblyNodeViewModel(_metadata)
                    };
                });

            }
            catch (Exception e)
            {

            }

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
