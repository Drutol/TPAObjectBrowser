using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Shared.ViewModels.ItemViewModels
{
    class MethodNodeViewModel : NodeViewModelBase
    {
        private readonly MethodMetadata _metadata;

        public MethodNodeViewModel(MethodMetadata metadata)
        {
            _metadata = metadata;
            Name = metadata.Name;
        }

        public override IEnumerable<NodeViewModelBase> Children { get; set; }
        public override TypeKind Kind { get; } = TypeKind.MethodType;
        public override string Name { get; }
        public override ICommand LoadChildrenCommand { get; }
    }
}
