using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ObjectBrowser.Console.Adpaters;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Shared.Statics;
using ObjectBrowser.Shared.ViewModels;
using ObjectBrowser.Shared.ViewModels.ItemViewModels;

namespace ObjectBrowser.Console
{
    class Program
    {
        private static BrowserViewModel _viewModel;
        private static Stack<NodeViewModelBase> _backStack = new Stack<NodeViewModelBase>();

        static async Task Main(string[] args)
        {
            ResourceLocator.RegisterDependencies(AdapterDelegate);
            _viewModel = ResourceLocator.BrowserViewModel;

            //System.Console.WriteLine("Input assembly path you'd like to load:");
            //var path = System.Console.ReadLine();

            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
            _viewModel.LoadTestAssemblyCommand.Execute(null);

            while (true)
            {
                var resp = System.Console.ReadLine();
                if (resp == "back")
                {
                    if(_backStack.Any())
                        _viewModel.TreeViewItemSelectedCommand.Execute(_backStack.Pop());
                    else
                        WriteNodes(_viewModel.Items);
                        
                }
                else if(resp == "quit")
                {
                    return;
                }
                else if(int.TryParse(resp,out int number))
                {
                    try
                    {
                        NodeViewModelBase node;
                        if (_backStack.Any())
                            node = _viewModel.NodeViewModelBase.Children.ElementAt(number);
                        else
                            node = _viewModel.Items[number];

                        if(_viewModel.NodeViewModelBase != null)
                            _backStack.Push(_viewModel.NodeViewModelBase);

                        _viewModel.TreeViewItemSelectedCommand.Execute(node);
                    }
                    catch
                    {
                        System.Console.WriteLine("There's no index at such index.");
                    }

                }
            }
        }

        private static void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(BrowserViewModel.NodeViewModelBase))
            {
                System.Console.WriteLine($"==={_viewModel.NodeViewModelBase.Kind.ToString()} {_viewModel.NodeViewModelBase.Name}===");
                WriteNodes(_viewModel.NodeViewModelBase.Children);
            }
            else if(propertyChangedEventArgs.PropertyName == nameof(BrowserViewModel.Items))
            {
                System.Console.WriteLine("Write number of item to exapnd it, `back` to go to its parent and `quit` to leave.");
                WriteNodes(_viewModel.Items);
            }
        }

        private static void WriteNodes(IEnumerable<NodeViewModelBase> nodes)
        {
            System.Console.Clear();
            int i = 0;
            foreach (var nodeViewModelBase in nodes)
            {
                System.Console.WriteLine($"[{i++}] {nodeViewModelBase.Kind.ToString()} - {nodeViewModelBase.Name}");
            }
        }

        private static void AdapterDelegate(ContainerBuilder builder)
        {
            builder.RegisterType<MessageBoxProvider>().As<IMessageBoxProvider>();
        }
    }
}
