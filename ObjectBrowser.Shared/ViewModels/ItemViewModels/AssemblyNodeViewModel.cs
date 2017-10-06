using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Shared.ViewModels.ItemViewModels
{
    public class AssemblyNodeViewModel : NodeViewModelBase
    {
        private readonly AssemblyMetadata _metadata;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.Assembly;
        public override string Name => _metadata.Name;

        public AssemblyNodeViewModel(AssemblyMetadata metadata)
        {
            _metadata = metadata;
        }

        public override IEnumerable<NodeViewModelBase> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                RaisePropertyChanged();
            }
        }

        public override ICommand LoadChildrenCommand => new RelayCommand(() =>
        {
            Children = _metadata.Namespaces.Select(namespaceMetadata => new NamepsaceNodeViewModel(namespaceMetadata));
        });
    }
}
