using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;
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
        private bool _limitToRootNamespace = true;

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
                var asm = Assembly.LoadFrom(asmPath);
                await LoadAssembly(asm);
            }
            catch (Exception e)
            {
                _logger.Log($"Error during model creation:\n{e}",LogSeverity.Error);
                await _messageBoxProvider.ShowMessageBoxOk("Error during assembly model creation", "Error");
            }

            Loading = false;
        }

        private async Task LoadAssembly(Assembly asm)
        {
            await Task.Run(() =>
            {
                _logger.Log($"Starting loading assembly from Assembly object: {asm.FullName}", LogSeverity.Info);
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                _metadata = _assemblyMetadataExtractor.Extract(asm, LimitToRootNamespace);
                stopwatch.Stop();
                _logger.Log($"Finished loading from Assembly, took {stopwatch.ElapsedMilliseconds}ms", LogSeverity.Info);
                LoadAssembly(_metadata);
            });
        }

        private void LoadAssembly(AssemblyMetadata asm)
        {
            Items = new List<NodeViewModelBase>
            {
                new AssemblyNodeViewModel(asm)
            };
        }

        #region Properties

        public bool LimitToRootNamespace
        {
            get { return _limitToRootNamespace; }
            set
            {
                _limitToRootNamespace = value; 
                RaisePropertyChanged();
            }
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
                    _logger.Log($"Error while navigating to treeview {vm.Name} node:\n{e}", LogSeverity.Error);
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
                    _logger.Log($"Error while navigating to listview {vm.Name} node:\n{e}", LogSeverity.Error);
                }

            });

        public ICommand SaveAssemblyCommand => new RelayCommand(async () =>
        {
            if (_metadata == null)
            {
                _logger.Log("Attempted to save assembly before loading.", LogSeverity.Warning);
                await _messageBoxProvider.ShowMessageBoxOk("You have to load assembly first in order to save it.",
                    "No loaded asembly.");
                return;
            }

            Loading = true;
            await _dataStorage.Save(_metadata);
            Loading = false;

            await _messageBoxProvider.ShowMessageBoxOk("Model has been saved successfully.",
                "Success");
        });

        public ICommand LoadAssemblyCommand => new RelayCommand(async () =>
        {
            Loading = true;
            try
            {
                var stopwatch = new Stopwatch();
                _logger.Log("Starting loading assembly from storage plugin.", LogSeverity.Info);
                stopwatch.Start();
                _metadata = await _dataStorage.Retrieve();
                stopwatch.Stop();
                _logger.Log($"Finished loading from storage, took {stopwatch.ElapsedMilliseconds}ms", LogSeverity.Info);
                Items = new List<NodeViewModelBase>
                {
                    new AssemblyNodeViewModel(_metadata)
                };
            }
            catch (Exception e)
            {
                await _messageBoxProvider.ShowMessageBoxOk("Couldn't find any saved assembly metadata.",
                    "Assembly not found.");
            }
            finally
            {
                Loading = false;
            }
        });

        public ICommand LoadTestAssemblyCommand => new RelayCommand(async () =>
        {
            Loading = true;
            try
            {
                var asm = Assembly.GetAssembly(typeof(ServiceA));
                await LoadAssembly(asm);
            }
            catch (Exception e)
            {
                await _messageBoxProvider.ShowMessageBoxOk("Couldn't find any saved assembly metadata.",
                    "Assembly not found.");
            }
            finally
            {
                Loading = false;
            }
        });


        #endregion



    }
}
