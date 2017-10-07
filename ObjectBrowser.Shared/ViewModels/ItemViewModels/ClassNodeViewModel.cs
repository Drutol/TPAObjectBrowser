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
    public class ClassNodeViewModel : NodeViewModelBase
    {
        private readonly TypeMetadata _metadata;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.ClassType;
        public override string Name { get; }

        public ClassNodeViewModel(TypeMetadata metadata)
        {
            _metadata = metadata;
            Name = metadata.TypeName;
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
            var items = new List<NodeViewModelBase>();

            items.AddRange(_metadata.Methods.Select(metadata => new MethodNodeViewModel(metadata)));
            items.AddRange(_metadata.Properties.Select(metadata => new PropertyNodeViewModel(metadata)));
            items.AddRange(_metadata.NestedTypes.Select(TypeMetadataToViewModel));
            items.AddRange(_metadata.Fields.Select(metadata => new FieldNodeViewModel(metadata)));

            Children = items;          
        });
    }
}
