using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using GalaSoft.MvvmLight.CommandWpf;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.Extensions;
using ObjectBrowser.Shared.Statics;

namespace ObjectBrowser.Shared.ViewModels.ItemViewModels
{
    public class ClassNodeViewModel : NodeViewModelBase
    {
        private readonly TypeMetadata _metadata;
        private readonly ILogger _logger;
        private IEnumerable<NodeViewModelBase> _children;
        public override TypeKind Kind { get; } = TypeKind.ClassType;
        public override string Name { get; }

        public ClassNodeViewModel(TypeMetadata metadata, IMetadataDetailsProvider metadataDetailsProvider, ILogger logger)
        {
            _metadata = metadata;
            _logger = logger;

            if (_metadata.Methods == null)
                _metadata = _metadata.RootAssembly.RegisteredTypes.First(typeMetadata =>
                    typeMetadata.TypeHash == _metadata.TypeHash);

            Name = metadata.TypeName;
            Details = metadataDetailsProvider.GetDetails(_metadata).ToKeyValuePairList();

        }

        public override IEnumerable<NodeViewModelBase> Children
        {
            get => _children;
            set
            {
                _logger.Log($"Generated {value.Count()} children for {_metadata.TypeName} node.", LogSeverity.Info);
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
                items.AddRange(_metadata.Constructors.Select(metadata =>
                    dependencyScope.ResolveWithParameter<MethodNodeViewModel, MethodMetadata>(metadata)));
            }
            items.AddRange(_metadata.Properties.Select(metadata => new PropertyNodeViewModel(metadata)));
            items.AddRange(_metadata.NestedTypes.Select(TypeMetadataToViewModel));
            items.AddRange(_metadata.Fields.Select(metadata => new FieldNodeViewModel(metadata)));

            Children = items;          
        });
    }
}
