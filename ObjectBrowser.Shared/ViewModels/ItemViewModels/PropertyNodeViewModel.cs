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
    public class PropertyNodeViewModel : NodeViewModelBase
    {
        private readonly PropertyMetadata _metadata;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.Property;
        public override string Name { get; }

        public PropertyNodeViewModel(PropertyMetadata metadata)
        {
            _metadata = metadata;
            Name = metadata.Name;
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
            Children = new List<NodeViewModelBase>
            {
                TypeMetadataToViewModel(_metadata.TypeMetadata)
            };
        });
    }
}
