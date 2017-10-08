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
    public class EnumNodeViewModel : NodeViewModelBase
    {
        private readonly TypeMetadata _metadata;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.EnumType;
        public override string Name { get; }

        public EnumNodeViewModel(TypeMetadata metadata)
        {
            _metadata = metadata;
            Name = _metadata.TypeName;
        }

        public override IEnumerable<NodeViewModelBase> Children
        {
            get => _children;
            set
            {
                _children = value;
                RaisePropertyChanged();
            }
        }

        public override ICommand LoadChildrenCommand => new RelayCommand(() =>
        {
            Children = _metadata.EnumFields?.Select(metadata => new FieldNodeViewModel(metadata));
        });
    }
}
