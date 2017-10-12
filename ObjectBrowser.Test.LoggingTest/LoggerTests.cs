using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Test.LoggingTest
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public async Task TestFileLogger()
        {
            var logger = new FileLogger.FileLogger();        
            logger.Log("Test",LogSeverity.Error);

            string msg = null;
            logger.GetFirstLogMessage(ref msg);
            Assert.AreEqual(msg,"Test");

            logger.Log("TestInfo", LogSeverity.Info);
            await logger.SaveLogs(LogSeverity.Error);

            var list = new List<string>();
            logger.GetLogs(list);
            Assert.IsTrue(list.First().ToLower().Contains("error"));
        }

        [TestMethod]
        public async Task TestDatabaseLogger()
        {
            var logger = new DatabaseLogger.DatabaseLogger();        
            logger.Log("Test",LogSeverity.Error);

            string msg = null;
            logger.GetFirstLogMessage(ref msg);
            Assert.AreEqual(msg,"Test");

            logger.Log("TestInfo", LogSeverity.Info);
            await logger.SaveLogs(LogSeverity.Error);

            var list = new List<DatabaseLogger.Log>();
            logger.GetLogs(list);
            Assert.IsTrue(list.First().Severity == "Error");
        }
    }
}
