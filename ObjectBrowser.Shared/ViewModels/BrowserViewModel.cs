using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using ObjectBrowser.Shared.BL;

namespace ObjectBrowser.Shared.ViewModels
{
    public class BrowserViewModel : ViewModelBase
    {
        private readonly IAssemblyMetadataExtractor _assemblyMetadataExtractor;

        public BrowserViewModel(IAssemblyMetadataExtractor assemblyMetadataExtractor)
        {
            _assemblyMetadataExtractor = assemblyMetadataExtractor;
        }

        public void NavigatedTo()
        {
            var data = _assemblyMetadataExtractor.Extract(Assembly.GetEntryAssembly());
        }
    }
}
