using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ObjectBrowser.Shared.Statics;
using ObjectBrowser.Shared.ViewModels;

namespace ObjectBrowser.WPF.Statics
{
    public class ViewModelLocator
    {
        private static ILifetimeScope _scope = ResourceLocator.ObtainScope();

        public static BrowserViewModel BrowserViewModel => _scope.Resolve<BrowserViewModel>();
    }
}
