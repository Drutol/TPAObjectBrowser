using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using ObjectBrowser.Shared.BL;
using ObjectBrowser.Shared.ViewModels.ItemViewModels;

namespace ObjectBrowser.Shared.ViewModels
{
    public class BrowserViewModel : ViewModelBase
    {
        private readonly IAssemblyMetadataExtractor _assemblyMetadataExtractor;
        private List<NodeViewModelBase> _items;

        public BrowserViewModel(IAssemblyMetadataExtractor assemblyMetadataExtractor)
        {
            _assemblyMetadataExtractor = assemblyMetadataExtractor;
        }

        public void NavigatedTo()
        {
            Items = new List<NodeViewModelBase>
            {
                new AssemblyNodeViewModel(_assemblyMetadataExtractor.Extract(Assembly.GetEntryAssembly()))
            };
        }

        public List<NodeViewModelBase> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged();
            }
        }
    }
}
