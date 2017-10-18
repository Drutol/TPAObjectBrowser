using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Shared.BL;

namespace ObjectBrowser.Test.SharedTest
{
    [TestClass]
    public class DetailsTest
    {
        [TestMethod]
        public void TestDetails()
        {
            var extractor = new MetadataDetailsProvider();

            var details = extractor.GetDetails(new TypeMetadata(12) {Modifiers = new TypeModifiers {IsAbstract = true}});

            Assert.IsTrue(details.Any(arg => arg.key == "IsAbstract:" && arg.value == "True"));
        }
    }
}
