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

namespace ObjectBrowser.Shared.ViewModels.ItemViewModels
{
    class MethodNodeViewModel : NodeViewModelBase
    {
        private readonly MethodMetadata _metadata;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.MethodType;
        public override string Name { get; }

        public MethodNodeViewModel(MethodMetadata metadata)
        {
            _metadata = metadata;
            Name = metadata.Name;
            Details = metadata.GetDetails();
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
            if(_metadata.ReturnType == null)
                return;

            Children = new List<NodeViewModelBase>
            {
                TypeMetadataToViewModel(_metadata.ReturnType)
            };
        });
    }
}
