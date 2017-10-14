using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.Extensions;

namespace ObjectBrowser.Shared.ViewModels.ItemViewModels
{
    class MethodNodeViewModel : NodeViewModelBase
    {
        private readonly MethodMetadata _metadata;
        private readonly ILogger _logger;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.MethodType;
        public override string Name { get; }

        public MethodNodeViewModel(MethodMetadata metadata, IMetadataDetailsProvider metadataDetailsProvider, ILogger logger)
        {
            _metadata = metadata;
            _logger = logger;
            Name = metadata.Name;
            Details = metadataDetailsProvider.GetDetails(metadata).ToKeyValuePairList();
        }

        public override IEnumerable<NodeViewModelBase> Children
        {
            get => _children;
            set
            {
                _logger.Log($"Generated {value.Count()} children for {_metadata.Name} node.",LogSeverity.Info);
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
