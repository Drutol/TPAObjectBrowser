﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Shared.ViewModels.ItemViewModels
{
    public abstract class NodeViewModelBase : ViewModelBase
    {
        private bool _isExpanded;
        public virtual IEnumerable<NodeViewModelBase> Children { get; set; } = new List<NodeViewModelBase>();
        public abstract TypeKind Kind { get; }
        public abstract string Name { get; }
        public abstract ICommand LoadChildrenCommand { get; }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                if (value)
                    LoadChildrenCommand?.Execute(null);
            }
        }

        protected NodeViewModelBase TypeMetadataToViewModel(TypeMetadata metadata)
        {
            switch (metadata.TypeKind)
            {
                case TypeKind.EnumType:
                    return new EnumNodeViewModel(metadata);
                case TypeKind.StructType:
                    return new StructNodeViewModel(metadata);
                case TypeKind.InterfaceType:
                    return new InterfaceNodeViewModel(metadata);
                case TypeKind.ClassType:
                    return new ClassNodeViewModel(metadata);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
