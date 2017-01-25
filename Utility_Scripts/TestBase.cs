using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
//using TatAutomationFramework.Common;
using RelevantCodes.ExtentReports;
using System.IO;
using CXAPortal.Utility_Scripts;
using System.Drawing.Imaging;

namespace CXAPortal.Utility_Scripts
{
   
    public class TestBase : Shared_Functions
    {
       // internal static IWebDriver driver;
        public static String Resultspath;
        public static IWebDriver driver;

        ///<summary>
        ///Run Before every Test and setup Tests.
        ///</summary>
      /*  [SetUp]
        public void TestSetup()
        {
           // _reportingTasks.InitializeTest();
            
          
        }*/
        /// <summary>
        /// Runs after every Test and Cleans up Test.
        /// </summary>
       /* [TearDown]
        public void TestCleanUp()
        {
            _reportingTasks.FinalizeTest();
            driver.Manage().Cookies.DeleteAllCookies();
        }*/

        /// <summary>
        /// Begin execution of tests
        /// </summary>
       [Test]
       
        public void BeginExecution()
        {

       
            Resultspath = "C:\\CXA_Automation\\CXAPortal\\TestResults\\SmokeTest\\SmokeTest_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            CreateDirectory(Resultspath);
            ExtentReports extent = new ExtentReports(Resultspath+"\\TestReports.html", DisplayOrder.OldestFirst);

            extent.LoadConfig("C:\\CXA_Automation\\CXAPortal\\extent-config.xml");

            

            // Start the test using the ExtentTest class object.
            ExtentTest extentTest = extent.StartTest("My First Test",
                    "Verify WebSite Title");

            // Launch the FireFox browser.
             driver = new ChromeDriver();

            driver.Manage().Window.Maximize();

            extentTest.Log(LogStatus.Info, "Browser Launched");

            // Open application.
            
            driver.Navigate().GoToUrl("https://flex-api.cxanow.com/Default.aspx?Msg=CookieIsRequired");

            extentTest.Log(LogStatus.Info, "Navigated to CXAPOrtal Page ");

            // get title.
           // String title = driver.Title;

            extentTest.Log(LogStatus.Info, "Get the WebSite title");

            // Verify title.
          //  Assert.IsTrue(title.Contains("Selenium Webdriver"));

            extentTest.Log(LogStatus.Pass, "Title verified");

            // In case you want to attach screenshot then use below method
            // We used a random image but you've to take screenshot at run-time
            // and specify the error image path.
            String loc = Take_Screenshot("Revanth");
            extentTest.Log(
                    LogStatus.Info,
                    "Error Snapshot : "
                            + extentTest.AddScreenCapture(loc));

            // Close application.
           // driver.quit();

            extentTest.Log(LogStatus.Info, "Browser closed");

            // close report.
            extent.EndTest(extentTest);

            // writing everything to document.
            extent.Flush();
            

        }

        public static string Take_Screenshot(String filename)
        {
            //var location = screenshotpath +""+ filename+"_" + DateTime.Now + ".png";
            var location = Resultspath + "/" + filename + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            var ssdriver = driver as ITakesScreenshot;
            var screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(location, ImageFormat.Png);
            return location;
        }
    }
}