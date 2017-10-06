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
    public class InterfaceNodeViewModel : NodeViewModelBase
    {
        private readonly TypeMetadata _metadata;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.InterfaceType;
        public override string Name { get; }

        public InterfaceNodeViewModel(TypeMetadata metadata)
        {
            _metadata = metadata;
            Name = metadata.TypeName;
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
            Children = _metadata.Methods.Select(metadata => new MethodNodeViewModel(metadata));
        });
    }
}
