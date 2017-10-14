using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.Extensions;
using ObjectBrowser.Shared.Statics;

namespace ObjectBrowser.Shared.ViewModels.ItemViewModels
{
    public class StructNodeViewModel : NodeViewModelBase
    {
        private readonly TypeMetadata _metadata;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.StructType;
        public override string Name { get; }

        public StructNodeViewModel(TypeMetadata metadata)
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
            var items = new List<NodeViewModelBase>();

            using (var dependencyScope = ResourceLocator.ObtainScope())
            {
                items.AddRange(_metadata.Methods.Select(metadata =>
                    dependencyScope.ResolveWithParameter<MethodNodeViewModel, MethodMetadata>(metadata)));
            }

            items.AddRange(_metadata.Properties.Select(metadata => new PropertyNodeViewModel(metadata)));
            items.AddRange(_metadata.NestedTypes.Select(TypeMetadataToViewModel));

            Children = items;
        });
    }
}
