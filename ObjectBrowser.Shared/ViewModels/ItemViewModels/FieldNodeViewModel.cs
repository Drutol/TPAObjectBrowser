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
    public class FieldNodeViewModel : NodeViewModelBase
    {
        private readonly FieldMetadata _metadata;
        private IEnumerable<NodeViewModelBase> _children;

        public FieldNodeViewModel(FieldMetadata metadata)
        {
            _metadata = metadata;
            Name = metadata.Name;
            Kind = TypeKind.Field;
            LoadChildrenCommand = new RelayCommand(() =>
            {
                Children = new List<NodeViewModelBase>
                {
                    TypeMetadataToViewModel(_metadata.TypeMetadata)
                };
            });
        }

        public FieldNodeViewModel(EnumFieldMetadata metadata)
        {
            Name = $"{metadata.Name} Value: {metadata.Value}";
            Kind = TypeKind.EnumField;
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

        public override TypeKind Kind { get; } 
        public override string Name { get; }
        public override ICommand LoadChildrenCommand { get; }
    }
}
