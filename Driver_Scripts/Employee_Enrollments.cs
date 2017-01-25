using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Compatibility;
using NUnit.Framework;
//using Microsoft.vis
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using CXAPortal.PageObjects;
using CXAPortal.Utility_Scripts;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Windows.Forms;

namespace CXAPortal.Driver_Scripts
{
    class Employee_Enrollments :Shared_Functions
    {
       // public static IWebDriver driver { get; set; }
        public static String Excelpath;

        [SetUp]
        public void setup()
        {
            Shared_Functions.CXALogin("URL", "CompanyId", "Username", "Password", "Browser", "Client");
            Console.WriteLine("Completed Execution");
            string client = ConfigurationManager.AppSettings["Client"];
            Shared_Functions.AccessClient(client);
        }


        [Test]
        public void Employee_Enrollment()
        {
            Excelpath = "C:\\CXA_Automation\\CXAPortal\\TestData\\Employee_Enrollments.xlsx";
            string worksheet = "Employee_Enrollments";
           // int worksheetno = 1;
            ExcelLib.PopulateInCollection(Excelpath, worksheet);
            try
            {
                Console.WriteLine("Total rows are " + ExcelLib.table.Rows.Count);
                //DashboardMainPage.NavigatetoWellnessConfiguration();
                for (int i = 3; i <= ExcelLib.table.Rows.Count; i++)
                {
                      Console.WriteLine(ExcelLib.ReadData(i, "Employee"));
                      String Emp_Name = ExcelLib.ReadData(i, "Employee");              
                       NavigateToStatementOfAccount(Emp_Name,i);
                       NavigateToEmployeeEnrollment(Emp_Name, i);
                       NavigateToBenefitsStatement(Emp_Name, i);
                    //DashboardMainPage.captureData(path, worksheetno, i, ExcelLib.ReadData(i, "Type"), ExcelLib.ReadData(i, "Format"), ExcelLib.ReadData(i, "Value"));
                }

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
                Console.WriteLine(e.Message);
            }

        }

        public void NavigateToEmployeeEnrollment(string emp_Name, int row_value)
        {
            try
            {
                string value, Id_value;
               
                Java_ClickElement("xpath", "//a[@rel='FlexBenefitsMenu']", "xpath", "//*[@id='FlexBenefitsMenu']/ul/li[2]/a");
                Wait_Until("Xpath", "//input[@value='Continue']");
                for (int j = 8; j < 153; j++)
                {
                    String FieldName = ExcelLib.ReadValues(1, j);
                    Id_value = ExcelLib.ReadValues(2, j);
                    FieldName = FieldName.ToLower();
                    Console.WriteLine("Id_value name is " + Id_value);
                    //Id_value = Id_value.ToLower();
                    try
                    {
                        IWebElement Element = driver.FindElement(By.Id(Id_value));

                        if (FieldName.Contains("change"))
                        {
                            String Change_flag = ExcelLib.ReadValues(3, j);
                            if (Change_flag.ToLower() == "yes")
                            {
                                Console.WriteLine("Change value");
                                driver.FindElement(By.Id(Id_value)).Click();
                                Thread.Sleep(2000);

                            }
                        }
                        else if (FieldName.Contains("checkbox") || FieldName.Contains("radio"))
                        {
                            Console.WriteLine("Checkbox item");

                            if (Element.Displayed)
                            {
                                if (Element.Selected)
                                {

                                    WriteToExcel(Excelpath, 1, row_value, j + 1, "Item is Selected");
                                }
                                else
                                {
                                    WriteToExcel(Excelpath, 1, row_value, j + 1, "Item Not Selected");
                                }
                            }
                            else
                            {

                                WriteToExcel(Excelpath, 1, row_value, j + 1, "Unable to locate element:" + Id_value);
                            }


                        }
                     
                        else if (FieldName.Contains("indicator"))
                        {
                            Console.WriteLine("Indicator item");
                            string imagedisplay = Element.GetAttribute("outerHTML");

                            if (imagedisplay.Contains("BlueCircle.png"))
                            {
                                value = "Item Selected";
                                WriteToExcel(Excelpath, 1, row_value, j + 1, value);
                            }
                            else
                            {
                                value = "Item Not Selected";
                                WriteToExcel(Excelpath, 1, row_value, j + 1, value);
                            }

                        }
                        else if (FieldName.Contains("label"))
                        {
                            Console.WriteLine("label item");
                            string tickervalue = Element.GetAttribute("outerHTML");
                            
                          
                            Console.WriteLine("ticker value is "+ tickervalue );
                            if (tickervalue.Contains("NotSelected"))
                            {
                                value = "Not Selected";
                                WriteToExcel(Excelpath, 1, row_value, j + 1, value);
                            }
                            else
                            {
                                value = "Selected";
                                WriteToExcel(Excelpath, 1, row_value, j + 1, value);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Text item");

                            if (Element.Displayed)
                            {
                                value = Element.Text;
                                WriteToExcel(Excelpath, 1, row_value, j + 1, value);
                            }
                            else
                            {
                                WriteToExcel(Excelpath, 1, row_value, j + 1, "Element not present");
                            }

                        }

                    }catch(Exception e)
                    {
                        WriteToExcel(Excelpath, 1, row_value, j + 1, "Unable to locate element");
                    }

                    }
          }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        [TearDown]
        public void teardown()
        {
            try
            {
                // Shared_Functions.Logout();
                // Logout();

                MessageBox.Show("Execution completed");
            }
            catch( Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void NavigateToStatementOfAccount(string EmpName,int rowvalue)
        {

            
           
            Java_ClickElement("xpath", "//a[@rel='FlexBenefitsMenu']", "xpath", "//*[@id='FlexBenefitsMenu']/ul/li[1]/a");
            Thread.Sleep(3000);
            if (driver.FindElement(By.XPath("//*[@id='SelectEmployeePnl']/input")).Displayed)
            {
                driver.FindElement(By.XPath("//*[@id='SelectEmployeePnl']/input")).Click();
            }
            Wait_Until("id", "SearchValue");
            driver.FindElement(By.Id("SearchValue")).SendKeys(EmpName);
            driver.FindElement(By.Id("bthSearch")).Click();
            Wait_Until("XPath", "//*[@id='form1']/table[2]");
            Boolean link = Shared_Functions.IsElementPresent(By.LinkText(EmpName));
            if (link==true)
            {
                driver.FindElement(By.LinkText(EmpName)).Click();
                Wait_Until("id", "PlanYear");
                int count = GetTableCount("XPath", "//*[@id='form1']/div[3]/table[2]");
                Console.WriteLine("Table count is: "+count);
                for (int i=1;i<=count;i++)
                {
                    for (int j=1;j<8;j++)
                    {
                       
                            String FieldName = ExcelLib.ReadValues(1, j);
                            Console.WriteLine("Field name is " + FieldName);
                            String Id_value = ExcelLib.ReadValues(2, j);                            
                            Id_value = Id_value.Replace("row", "" + (i+2));
                            Console.WriteLine("Id_value name is " + Id_value);
                            string value = driver.FindElement(By.XPath(Id_value)).Text;
                            Console.WriteLine("Captured value is " +value);


                        if (FieldName.ToLower() == "click_link")
                        {
                            driver.FindElement(By.XPath(Id_value)).Click();
                            Thread.Sleep(3000);
                            Console.WriteLine("no of windows are " + driver.WindowHandles.Count);
                            driver.SwitchTo().Window(driver.WindowHandles.Last());
                        }
                       
                        else
                        {
                            WriteToExcel(Excelpath, 1, rowvalue, j+1, value);                            

                        }
                        
                    }
                    
                }
                driver.FindElement(By.XPath("//input[@value='Close']")).Click();
                Thread.Sleep(2000);
                driver.SwitchTo().Window(driver.WindowHandles.FirstOrDefault());
                Thread.Sleep(5000);
                //  Logout();
                //driver.FindElement(By.XPath("//input[contains(value(),'Administer')]")).Click();
                Console.WriteLine("Switched to default");
                driver.FindElement(By.XPath("//*[@id='SelectEmployeePnl']/input")).Click();
                Wait_Until("id", "bthSearch");
                

            }
            else
            {
                Console.WriteLine("Employee Name: "+ EmpName + " Not Found.Please verify");
            }

        }
        public static void NavigateToBenefitsStatement(string emp_Name, int row_value)
        {
            try
            {
                Java_ClickElement("xpath", "//a[@rel='FlexBenefitsMenu']", "xpath", "//*[@id='FlexBenefitsMenu']/ul/li[3]/a");

                Wait_Until("id", "PlanYear");
                for (int j = 154; j < ExcelLib.table.Columns.Count; j++)
                {

                    String FieldName = ExcelLib.ReadValues(1, j);
                    Console.WriteLine("Field name is " + FieldName);
                    String Id_value = ExcelLib.ReadValues(2, j);                    
                    Console.WriteLine("Id_value name is " + Id_value);
                    string value = driver.FindElement(By.XPath(Id_value)).Text;
                    Console.WriteLine("Captured value is " + value);                    
                    WriteToExcel(Excelpath, 1, row_value, j + 1, value);                  

                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



    }
}
