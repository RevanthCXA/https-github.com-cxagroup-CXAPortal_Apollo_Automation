using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using NUnit.Framework;


namespace CXAPortal.Utility_Scripts
{
   public class ReportingManager
    {
        /// <summary>
        /// Create new instance of Extent report
        /// </summary>
        //private static readonly ExtentReports _instance = new ExtentReports(TestContext.CurrentContext.TestDirectory + "\\TestResults.html");
        private static readonly ExtentReports _instance = new ExtentReports("C:\\CXA_Automation\\CXAPortal\\TestResults.html");

        static ReportingManager() { }
        private ReportingManager() { }

        /// <summary>
        /// Property to return the instance of the report.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ExtentReports Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}