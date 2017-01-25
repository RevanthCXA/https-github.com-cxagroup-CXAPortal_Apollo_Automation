using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CXAPortal.PageObjects;
using OpenQA.Selenium;
using System.IO;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Threading;
using System.Drawing.Imaging;
using CXAPortal.Utility_Scripts;
//using ExcelLibrary.SpreadSheet;
using ExcelLibrary.CompoundDocumentFormat;
using ClosedXML.Excel;
using OpenQA.Selenium.Remote;
using RelevantCodes.ExtentReports;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace CXAPortal.Utility_Scripts
{
    public class Shared_Functions
    {

        public static IWebDriver driver;
        public static LoginPage LoginPage;
        //public LoginPage;
        public static string datapath = "C:\\CXA_Automation\\CXAPortal\\";
        public static string ScreenshotPath,Resultspath;
        public static ExtentReports extent;
        public static ExtentTest extentTest;
        public string screenshotpic;
        public static void CXALogin(String URL, String CompanyId, String Uname, String Pwd, String browser, String Client)
        {
            try
            {
                Console.WriteLine("Login function");
                /// Retriving url,Browser,credential values from the Environment values

                String baseURL = ConfigurationManager.AppSettings[URL];
                String companyId = ConfigurationManager.AppSettings[CompanyId];
                String Username = ConfigurationManager.AppSettings[Uname];
                String Password = ConfigurationManager.AppSettings[Pwd];
                String Browser = ConfigurationManager.AppSettings[browser];
                String client = ConfigurationManager.AppSettings[Client];

                driver = getBrowser(Browser);
                Console.WriteLine("values  are " + companyId + Username + Password);
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(baseURL);
                var loginPage = new LoginPage(driver);
                Wait_Until("XPath", "//*[@id=\"LoginPanel\"]/tbody/tr/td[1]/a");
            
                driver_wait(By.Id("CompanyID"));
               SendKeys(By.Id("CompanyID"), companyId);
                SendKeys(By.Id("LoginID"), Username);
                SendKeys(By.Id("Password"), Password);                
                Click(By.Id("SignIn"));
                driver_wait(By.XPath("//a[@rel='ProfileMenu']"));
                //Wait_Until("css", "#menubar");
                Capture_Screenshot("Login", "Pass", "Login with "+ Username +"  Successful");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Capture_Screenshot("Error_Login", "fail", e.Message);
            }
        }

        public static void Logout()
        {
            try
            {

                if (driver.FindElement(By.XPath("//*[@id='menubar']/table/tbody/tr/td[2]/a[2]")).Displayed)
                {
                    //Capture_Screenshot("Before_Logout");
                    driver.FindElement(By.XPath("//*[@id='menubar']/table/tbody/tr/td[2]/a[2]")).Click();
                    Thread.Sleep(4000);
                    //  DashboardMainPage.TakeScreenshot("After_Logout");
                }
                //  driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Write_Results()
        {

        }

        // Method that's initialises FireFox driver


        public static void SendKeys(By by, String text)
        {
                driver.FindElement(by).SendKeys(text);
            
        }

        public static void Click(By by)
        {
            
                driver.FindElement(by).Click();
           
        }

        public static void Clear(By by)
        {
          
            driver.FindElement(by).Clear();
        }
            

        public static String GetText(By by)
        {
            string text=null;
           
            text=driver.FindElement(by).Text;
           
            return text;
        }
        public void SelectDropdown(By by,string value)
        {
            SelectElement se = new SelectElement(driver.FindElement(by));
            se.SelectByText(value);

        }
        public static bool IsElementPresent(By by)
        {
            
                driver.FindElement(by);
                return true;
           
        }

        public static int GetCount(By by)
        {
            return driver.FindElements(by).Count;

        }
        public static void Wait_Until(String proptype, String value)
        {
            Console.WriteLine("Waiting for Element :" + proptype + "/ value : " + value);

            


              int second = 0;
              for (second = 0; ; second++)
              {
                  Console.WriteLine("Second value is " + second);
                  if (second >= 60)
                  {
                      Capture_Screenshot("Error_" + proptype + "_value", "fail", "Unable to find Element Error");
                      Assert.Fail("timeout");
                  }
                  else  if (String.Equals(proptype, "Id", StringComparison.OrdinalIgnoreCase))

                      {
                          Console.WriteLine("Waiting for ID Element");
                          if (IsElementPresent(By.Id(value))) break;
                      }
                      else if (String.Equals(proptype, "Name", StringComparison.OrdinalIgnoreCase))

                      {
                          Console.WriteLine("Waiting for Name Element");
                          if (IsElementPresent(By.Name(value))) break;
                      }
                      else if (String.Equals(proptype, "Xpath", StringComparison.OrdinalIgnoreCase))

                      {
                          Console.WriteLine("Waiting for Xpath Element");
                          if (IsElementPresent(By.XPath(value))) break;
                      }
                      else if (String.Equals(proptype, "Css", StringComparison.OrdinalIgnoreCase))

                      {
                          Console.WriteLine("Waiting for CSS Element");
                          if (IsElementPresent(By.CssSelector(value))) break;
                      }
                      else if (String.Equals(proptype, "Link", StringComparison.OrdinalIgnoreCase))

                      {
                          Console.WriteLine("Waiting for LInktext Element");
                          if (IsElementPresent(By.LinkText(value))) break;
                      }
                      else if (String.Equals(proptype, "Partial", StringComparison.OrdinalIgnoreCase))

                      {
                          Console.WriteLine("Waiting for Partial link Element");
                          if (IsElementPresent(By.PartialLinkText(value))) break;
                      }


              }
        }
        public static void AccessClient(String Clientname)
        {
           
            driver_wait(By.XPath("//a[@rel='ProfileMenu']"));
           /* driver.FindElement(By.CssSelector("img[alt=\"Start\"]")).Click();
            //Driver.FindElement(By.Id("WinStartButton")).Click();
            Thread.Sleep(2000);
            IWebElement element = driver.FindElement(By.XPath("//a[contains(@href, '" + Clientname + "')]"));
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + element.Location.Y + ")");
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
            //Wait_Until("Id", "NotitifcationBar");*/
            Java_ClickElement("XPath", "//*[@id='WinStartButton']","XPath", "//a[contains(@href, '" + Clientname + "')]");
            driver_wait(By.XPath("//a[@rel='ProfileMenu']"));
        }

        public static void Capture_Screenshot(String filename,String Resultstatus,String Message)
        {
            //var location = screenshotpath +""+ filename+"_" + DateTime.Now + ".png";
            var location = ScreenshotPath + "\\" + filename +"_"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
            var ssdriver = driver as ITakesScreenshot;
            var screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(location, ImageFormat.Png);
            if (Resultstatus.ToLower()=="fail")
            {
                extentTest.Log(LogStatus.Fail, Message + " " + extentTest.AddScreenCapture(location));
            }
            else
            {
                extentTest.Log(LogStatus.Pass, Message + " " + extentTest.AddScreenCapture(location));
            }
            
        }
        public static IWebDriver getBrowser(String browserType)

        {
            Console.WriteLine("Browsertype is " + browserType);

            if (driver == null)

            {

                if (browserType.ToLower() == ("firefox"))

                {
                    Console.WriteLine("Browsertype is firefox");

                    /* FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\CXA_Automation\Drivers", "geckodriver.exe");
                     // ChromeDriver Driver= new ChromeDriver(@"C:\CXA_Automation\automation");
                     driver = new FirefoxDriver(service);*/
                    driver = new FirefoxDriver();
                   

                }

                else if (browserType.ToLower() == ("chrome"))

                {

                    Console.WriteLine("Browsertype is chrome");
                    driver = new ChromeDriver(@"C:\CXA_Automation\Drivers");

                }

                else if (browserType.ToLower() == ("ie"))

                {
                    Console.WriteLine("Browsertype is ie");
                    driver = new InternetExplorerDriver();

                }

            }
            return driver;

        }
        public static int GetTableCount(String proptype, String value)
        {
            int rowcount = 0;
            if (String.Equals(proptype, "Id", StringComparison.OrdinalIgnoreCase))

            {
                rowcount = driver.FindElements(By.Id(value)).Count;

            }
            else if (String.Equals(proptype, "Name", StringComparison.OrdinalIgnoreCase))

            {
                rowcount = driver.FindElements(By.Name(value)).Count;
            }
            else if (String.Equals(proptype, "XPath", StringComparison.OrdinalIgnoreCase))

            {
                Console.WriteLine("Table count Xpath");
                rowcount = driver.FindElements(By.XPath(value)).Count;
            }
            else if (String.Equals(proptype, "Css", StringComparison.OrdinalIgnoreCase))

            {
                rowcount = driver.FindElements(By.CssSelector(value)).Count;
            }
            return rowcount;
        }

        public static void Capturevalue(int startcol, int endcol, int rowno)
        {

        }
        public static void WriteToExcel(String path, int workSheetno, int rowno, int colno, String value)
        {

            //   String[] columns_header = { "D", "E", "F", "G", "H" };
            var workbook = new XLWorkbook(path); // load the existing excel file
            var worksheet = workbook.Worksheets.Worksheet(workSheetno);
            // var worksheet = workbook.Worksheets.Worksheet(sheetname);
            rowno = rowno + 1;
            Console.WriteLine("Current value at " + rowno + colno + " is :" + worksheet.Cell(rowno, colno).Value.ToString());

            for (int loop = 3; loop < (rowno + 1);)
            {
                if (worksheet.Cell(rowno, colno).IsEmpty())
                {
                    Console.WriteLine("Setting value: " + value + " at " + rowno + "/" + colno);

                    worksheet.Cell(rowno, colno).SetValue(value);
                    workbook.Save();
                    //  Console.WriteLine("Data written to " + path);
                    // workbook.Dispose();
                    break;
                }
                else
                {
                    //Range Line = (Range)worksheet.Rows[3];
                    worksheet.Row(rowno).InsertRowsBelow(1);
                    rowno = rowno + 1;

                    worksheet.Cell(rowno, colno).SetValue(value);
                    workbook.Save();
                    Console.WriteLine("Data written to " + rowno + colno);
                    // workbook.Dispose();
                    break;
                }
            }

            workbook.Dispose();

        }
        /*  public static String GetInnerHtml(this IWebElement element)
          {
              var remoteWebDriver = (RemoteWebElement)element;
              var javaScriptExecutor = (IJavaScriptExecutor)remoteWebDriver.WrappedDriver;
              var innerHtml = javaScriptExecutor.ExecuteScript("return arguments[0].innerHTML;", element).ToString();

              return innerHtml;
          }*/

        public static void Java_ClickElement(String Proptype1, String propvalue1, String Proptype2, String propvalue2)
        {
            if (Proptype1.ToLower() == "xpath")
            {
                IWebElement element = driver.FindElement(By.XPath(propvalue1));
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + element.Location.Y + ")");
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", element);
                if (!String.IsNullOrEmpty(Proptype2))
                {
                    IWebElement element2 = driver.FindElement(By.XPath(propvalue2));
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + element2.Location.Y + ")");
                    IJavaScriptExecutor executor2 = (IJavaScriptExecutor)driver;
                    executor2.ExecuteScript("arguments[0].click();", element2);
                }
                Thread.Sleep(2000);
            }
            else if (Proptype1.ToLower() == "linktext")
            {
                IWebElement element = driver.FindElement(By.LinkText(propvalue1));
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + element.Location.Y + ")");
                IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
                executor.ExecuteScript("arguments[0].click();", element);
                if (!String.IsNullOrEmpty(Proptype2))
                {
                    IWebElement element2 = driver.FindElement(By.LinkText(propvalue2));
                    ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + element2.Location.Y + ")");
                    IJavaScriptExecutor executor2 = (IJavaScriptExecutor)driver;
                    executor2.ExecuteScript("arguments[0].click();", element2);
                }
                Thread.Sleep(2000);
            }

        }

        
        public static void Execute_Excel(string Excelpath,string worksheetname)

        {

            ExcelLib.PopulateInCollection(Excelpath, worksheetname);
            Console.WriteLine("Total colimns are " + ExcelLib.table.Columns.Count);
            Console.WriteLine("Total rows are " + ExcelLib.table.Rows.Count);

            //rows 
            for (int row = 3; row <=ExcelLib.table.Rows.Count; row++)
            {
                for (int i = 0; i <= ExcelLib.table.Columns.Count-1; i++)
                {
                    string Fieldname = ExcelLib.ReadValues(1, i);
                    string prop = ExcelLib.ReadValues(2, i);
                    string propvalue = ExcelLib.ReadValues(row, i);
                    string EmpName = ExcelLib.ReadValues(row, 1);
                    Console.WriteLine("Values captured are " + Fieldname + "/" + prop + " / " + propvalue);
                    Fieldname = Fieldname.ToLower();

                    if (!String.IsNullOrEmpty(propvalue) && !String.IsNullOrEmpty(prop) && !String.IsNullOrEmpty(Fieldname))
                    {
                       
                        if (Fieldname.Contains("checkbox")  || Fieldname.Contains("button") ||

                                Fieldname.Contains("click_link"))
                        {
                            Wait_Until("id", prop);
                            Console.WriteLine("Click Element");
                            if (propvalue.ToLower() == "yes")
                            {
                                Thread.Sleep(3000);
                                //Click(By.Id(prop));
                                IWebElement radioElement = driver.FindElement(By.Id(prop));

                                // (4) Click that input element
                                
                                radioElement.Click();
                                Thread.Sleep(3000);
                                Capture_Screenshot(EmpName, "Pass", "Clicked " + Fieldname);
                            }

                        }
                        else if (Fieldname.Contains("radio"))
                        {

                            Wait_Until("id", prop);
                            if (propvalue.ToLower() == "yes")
                            {

                                Console.WriteLine("Clicking Radio button");
                                driver.FindElement(By.Id(prop)).Click();
                            }
                          
                        }
                        else if (Fieldname.Contains("dropdown"))
                        {
                            // Console.WriteLine("Screenshot paht is " + screenshotpath);
                            Console.WriteLine("Dropdown Element");
                            SelectElement se = new SelectElement(driver.FindElement(By.Id(prop)));
                            se.SelectByText(propvalue);
                            Capture_Screenshot(EmpName, "Pass", " Selected " + propvalue+" from "+Fieldname);
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            // Console.WriteLine("Screenshot paht is " + screenshotpath);
                            Console.WriteLine("Text Element");
                            //   Console.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                            
                            driver.FindElement(By.Id(prop)).SendKeys(propvalue);

                            Thread.Sleep(1000);
                            try
                            {
                                driver.FindElement(By.XPath("//*[@id='ui-datepicker-div']/div[2]/button[2]")).Click();
                                Thread.Sleep(1000);
                            }
                            catch (Exception e)
                            {

                            }

                            Capture_Screenshot(EmpName, "Pass", "Entered value : " + propvalue + " in " + Fieldname);
                        }
                        if (i.Equals(ExcelLib.table.Columns.Count - 1) && prop.ToLower()!= "new")
                        {
                            Console.WriteLine("Last Element");
                            
                            driver_wait(By.Id("EffectiveOn"));
                            Capture_Screenshot(EmpName + "_Creation", "Info", "Creation of Employee");
                            Thread.Sleep(2000);
                            Java_ClickElement("xpath", "//a[@rel='ProfileMenu']", "xpath", "//*[@id='ProfileMenu']/ul/li[10]/a");
                            
                            driver_wait(By.Id("frmID"));
                        }
                    }
                }
                }               
        }

        public static string CreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Console.WriteLine("That path exists already.");

            }
            else
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
                // di.Delete();
                // Console.WriteLine("The directory was deleted successfully.");
                Console.WriteLine(path);
            }
            ScreenshotPath = path;
            return path;
        }
      
        public void CreateHTMLReport(String TestName)
        {
            
            Resultspath = datapath + "TestResults\\"+ TestName+ "\\"+TestName+"_" + DateTime.Now.ToString("ddMMyyyyHHmmss");
            CreateDirectory(Resultspath);
            extent = new ExtentReports(Resultspath + "\\TestResults.html", DisplayOrder.OldestFirst);
            extent.LoadConfig("C:\\CXA_Automation\\CXAPortal\\extent-config.xml");
            

        }

        public void ScrollToBottom()
        {
            long scrollHeight = 0;

            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(400);
                }
            } while (true);
        }

        public static void HandleAlert()
        {
            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait1.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert1 = driver.SwitchTo().Alert();
            alert1.Accept();
            driver.SwitchTo().DefaultContent();
        }

        public static void driver_wait(By by)
        {
            Console.WriteLine("Waiting for " + by.ToString());
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            IWebElement myDynamicElement = wait.Until<IWebElement>((d) =>
            {
                Console.WriteLine("Element is visible");
                return d.FindElement(by);
            });
        }

    }

}

