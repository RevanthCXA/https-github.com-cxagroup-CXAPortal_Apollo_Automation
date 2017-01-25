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
using RelevantCodes.ExtentReports;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;


namespace CXAPortal.Driver_Scripts
{
    class SmokeTest : Shared_Functions
    {
        public static String Excelpath,worksheet,Screenshotpath,TestResults,Resultspath;
        
        

        [SetUp]
        public void setup()
        {

            CreateHTMLReport("SmokeTest");
            
            extentTest = extent.StartTest("Smoke Test","SmokeTest");
            CXALogin("SmokeTest_URL", "SmokeTest_CompanyId", "SmokeTest_Username", "SmokeTest_Password", "SmokeTest_Browser", "SmokeTest_Client");
            Console.WriteLine(Screenshotpath);
            // Shared_Functions.AccessClient(client);
        }
        [Test]
        public void Smoketest()
        {
           
            // MessageBox.Show("Execution completed");
            string client = ConfigurationManager.AppSettings["NewClient"];
            try
            {
                //CreationOfClient(client);
           
                AccessClient(client);
                try
                {
                  //  ChangeUserLogonSettings();
                
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ChangeUserLoginSettings", "Fail", e.Message);
                }


                /*****  Changes the Policy period From Jan 1 2017 to Dec 31 2017 *****/
                try { 
                  //  ChangePolicyPeriod();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ChangePolicyPeriod", "Fail", e.Message);
                }

                /*****  Changes the Flex points EP= 4000 , DP=2000 *****/
                try { 
                     ChangeFlexPoints();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ChangeFlexPoints", "Fail", e.Message);
                }
                /*****  Deletes the Products and Enable the eligible options( 12X,24X etc)  *****/
              /*  try { 

                    ModifyProducts();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ModifyProducts", "Fail", e.Message);
                }

                /*****  Change the Claims prods to "Expired" except HS-HS,NF-TCM *****/
                try {

                 //   Claims_Configuration();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ClaimsConfiguration", "Fail", e.Message);
                }

                /***** Updates Link logic GTL-GCI to GTL-GPA and deletes other link logics *****/
                try {

                 //   ModifyLinkLogic();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ModifyLinkLogic", "Fail", e.Message);
                }

                /*****  Creates new Employee with Hire and Flex date is today *****/
               

                 //   CreationOfEmployee();
              

                /*****  Creates new Dependant to the existing Employee*****/
              //  CreationOfDependant();

                /*****  Process Work Life Event*****/
               // ProcessWLE();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                Capture_Screenshot("Error", "Fail", e.Message);
                Final();
            }
        }
        [Test]
        public  void UploadEmployee()
        {
            extentTest.Log(LogStatus.Info, "EMPLOYEE UPLOAD");
            AccessClient("Testing1");
            driver_wait(By.Id("WinStartButton"));
            Java_ClickElement("xpath", "//a[@rel='ProfileMenu']", "xpath", "//*[@id='ProfileMenu']/ul/li[12]/a");
            driver_wait(By.LinkText("Upload New Hires/Employees Data"));
            Click(By.LinkText("Upload New Hires/Employees Data"));
            driver_wait(By.Id("UploadBtn"));
            Click(By.Id("MovementFile"));
            
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "C:\\CXA_Automation\\CXAPortal\\EmployeeAutoit.exe";
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
            driver_wait(By.Id("UploadBtn"));
            Capture_Screenshot("EmployeeFIleUpload", "Pass", "Employee File Upload");
            Click(By.Id("UploadBtn"));
            HandleAlert();
            driver_wait(By.Id("SkipLE"));
            Thread.Sleep(2000);
            Click(By.Id("UploadBtn"));
            HandleAlert();
            driver_wait(By.XPath("//*[@id='form1']/table/tbody/tr/td[2]"));
            Capture_Screenshot("EmployeeFIleUploadSuccess", "Pass", "Employee File Uploaded Successfully");
            ProcessWLE();
        }
        [Test]
        public void UploadDependant()
        {
            extentTest.Log(LogStatus.Info, "EMPLOYEE UPLOAD");
            AccessClient("Testing1");
            driver_wait(By.Id("WinStartButton"));
            Java_ClickElement("xpath", "//a[@rel='ProfileMenu']", "xpath", "//*[@id='ProfileMenu']/ul/li[12]/a");
            driver_wait(By.LinkText("Upload New Hires/Employees Data"));
            Click(By.LinkText("Upload Dependants Data"));
            driver_wait(By.Id("UploadBtn"));
            Click(By.Id("MovementFile"));

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "C:\\CXA_Automation\\CXAPortal\\Dependant_Auto.exe";
            process.StartInfo = startInfo;
            process.Start();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver_wait(By.Id("UploadBtn"));
            Capture_Screenshot("DependantFIleUpload", "Pass", "Dependant File Upload");
            Click(By.Id("UploadBtn"));
            HandleAlert();
            driver_wait(By.Id("SkipLE"));
            Thread.Sleep(2000);
            Click(By.Id("UploadBtn"));
            HandleAlert();
            driver_wait(By.XPath("//*[@id='form1']/table/tbody/tr/td[2]"));
            Capture_Screenshot("DependantFIleUploadSuccess", "Pass", "Dependant File Uploaded Successfully");
            ProcessWLE();
        }

        public void CreationOfClient(string ClientName)
        {
           
                Console.WriteLine("Started Creation of Client");
                extentTest.Log(LogStatus.Info, "CREATION OF CLIENT: " + ClientName);
                driver.FindElement(By.Id("WinStartButton")).Click();
                Thread.Sleep(2000);
                Java_ClickElement("LinkText", "System", "LinkText", "Create a new Database");
               
                driver_wait(By.Id("NewDatabase"));
                driver.FindElement(By.Id("NewDatabase")).Click();
                driver_wait(By.Id("DatabaseName"));
                
                /*driver.FindElement(By.Id("DatabaseName")).SendKeys(ClientName);
                driver.FindElement(By.Id("CompanyID")).SendKeys(ClientName);
                driver.FindElement(By.Id("CompanyName")).SendKeys(ClientName);*/
                SendKeys(By.Id("DatabaseName"), ClientName);
                SendKeys(By.Id("CompanyID"), ClientName);
                SendKeys(By.Id("CompanyName"), ClientName);
                SelectDropdown(By.Id("ConfigTemplate"),"BMANAGER");                
                //Create Button

                Click(By.XPath("//input[@value='Create']"));    
                driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
                Thread.Sleep(10000);
                Capture_Screenshot("CreationOfClient", "Pass", "Created New Client Successfully: " + ClientName);
                Click(By.Id("WinStartButton"));               
                driver_wait(By.LinkText("Search Clients"));
                Java_ClickElement("LinkText", "Search Clients", "", "");               
                driver_wait(By.Id("SearchValue"));
                SendKeys(By.Id("SearchValue"), ClientName);
                Click(By.Id("bthSearch"));
                for (int i=0;i<=1000;i++)
                {
                    try
                    {
                        if (IsElementPresent(By.LinkText(ClientName)))
                        {
                            Capture_Screenshot("CreationOfClient", "Pass", "Created New Client Successfully: " + ClientName);
                            Click(By.LinkText(ClientName));
                            Thread.Sleep(10000);
                            break;
                        }
                        else
                   
                        Click(By.Id("bthSearch"));
                    }
                    catch (Exception e)
                    {

                    }

                }


            
        
        }
        public void ChangeUserLogonSettings()
        {
            extentTest.Log(LogStatus.Info, "CHANGE USER LOGIN SETTINGS");
            Console.WriteLine("User Logon Settings");
            Click(By.Id("WinStartButton"));
            Thread.Sleep(2000);
            Java_ClickElement("LinkText", "Configuration", "LinkText", "Client General Settings and Password Policy");

            driver_wait(By.LinkText("Configure Settings"));
            Click(By.LinkText("Configure Settings"));
            driver_wait(By.Id("ClientReferral"));
            Capture_Screenshot("ChangeUserLoginSettings", "Pass", "Updated User default login to ID");
            SelectDropdown(By.Id("LoginIDDMapTo"), "ID");
            Thread.Sleep(1000);
            ScrollToBottom();
            Click(By.XPath("//*[@id='tabs-1']/table/tfoot/tr/td/input"));

            driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
            Capture_Screenshot("ChangeUserLoginSettingsResult", "Pass", "Updated User Login Settings Successfully");
        }
        public void ChangePolicyPeriod()
        {
            
                extentTest.Log(LogStatus.Info, "CHANGING POLICY PERIOD");
                Console.WriteLine("Started Creation of Client");
                Click(By.Id("WinStartButton"));
                Thread.Sleep(2000);
                Java_ClickElement("LinkText", "Configuration", "LinkText", "New Implementation/Renewal");   
                               
                driver_wait(By.LinkText("Manage Flex Plan"));
                Click(By.LinkText("Manage Flex Plan"));
                
                driver_wait(By.Id("DeleteBtn0"));
                 Click(By.Id("EditBtn0"));


              /*  IWebElement element2 = driver.FindElement(By.Id("FlexPlanName"));
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0," + element2.Location.Y + ")");
                IJavaScriptExecutor executor2 = (IJavaScriptExecutor)driver;
                executor2.ExecuteScript("arguments[0].setAttribute('value', '2017')",element2);*/
                 // Wait_Until("Css", "#FlexPlanName");
                 driver_wait(By.ClassName("FormValue"));
            //driver.FindElement(By.CssSelector("#FlexPlanName")).Clear();

            try
            {
                Console.WriteLine("Check present : " + driver.FindElement(By.Id("form1")).Displayed);

            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                Console.WriteLine("FlexPlanID present : " + driver.FindElements(By.Id("FlexPlanID")).Count);

                var allCheckboxes = driver.FindElements(By.Id("FlexPlanID"));

                for (int i= 0;i< allCheckboxes.Count;i++)
                {
                    Console.WriteLine("value is "+driver.FindElement(By.Id("FlexPlanID")).Text);
                }

            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Console.WriteLine("PlanStart present : " + driver.FindElements(By.Id("PlanStart")).Count);

            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Console.WriteLine("PlanEnd present : " + driver.FindElements(By.Id("PlanEnd")).Count);

            }
            catch (ElementNotVisibleException e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                Console.WriteLine("Check present : " + driver.FindElements(By.CssSelector("#FlexPlanName")).Count);

            }
            catch(ElementNotVisibleException e)
            {
                Console.WriteLine(e.Message);       
            }

                Clear(By.CssSelector("#FlexPlanName"));
                Thread.Sleep(2000);
                SendKeys(By.CssSelector("#FlexPlanName"), "2017");
                Clear(By.CssSelector("#PlanStart"));
                driver_wait(By.CssSelector("#PlanEnd"));
                 SendKeys(By.CssSelector("#PlanStart"), "01/01/2017");
                 driver_wait(By.CssSelector("#PlanEnd"));
                Clear(By.CssSelector("#PlanEnd"));
                driver_wait(By.CssSelector("#PlanEnd"));
                SendKeys(By.CssSelector("#PlanEnd"), "12/31/2017");
                Click(By.Id("Save"));                   
                driver_wait(By.Id("Create"));
                Capture_Screenshot("ChangePolicyPeriod", "Pass", "Changed Policy Period ");

         
        }

        public void ChangeFlexPoints()
        {
           
                extentTest.Log(LogStatus.Info, "FLEX POINTS ALLOCATION");
                Click(By.Id("WinStartButton"));                
                driver_wait(By.LinkText("Configuration"));
                Java_ClickElement("LinkText", "Configuration", "LinkText", "Flex Benefits Configuration");
                
                driver_wait(By.LinkText("Setup/Initialize Flex Group"));
                Click(By.LinkText("Setup/Initialize Flex Points"));
                driver_wait(By.Id("FlexCurrencyRules"));
            Clear(By.Id("FlexCurrencyRules"));
            SendKeys(By.Id("FlexCurrencyRules"), "'SGD'");
                Click(By.Id("SaveFlexInfo"));
            driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
            Capture_Screenshot("ChangeFlexCurrency", "Pass", "Updated Flex Currency for Employee and Dependant Successfully");
            driver_wait(By.LinkText("Flex Points Allocation"));
                Click(By.LinkText("Flex Points Allocation"));
                driver_wait(By.Name("FlexPointsID1"));
                Clear(By.Id("FlexPointsAllocation1"));
                Thread.Sleep(2000);
                SendKeys(By.Id("FlexPointsAllocation1"), "4000");
                Clear(By.Id("FlexPointsAllocation2"));
                Thread.Sleep(2000);
                SendKeys(By.Id("FlexPointsAllocation2"), "2000");
                Thread.Sleep(2000);
                Capture_Screenshot("FlexpointsValues", "Pass", "Flex Points Allocation for Employee and Dependant");
                Thread.Sleep(2000);
                ScrollToBottom();
                //Click(By.XPath("//input[@value='Save Changes']"));
                Click(By.XPath("//*[@id='tabs-1']/table[2]/tfoot/tr/td/input"));
                driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
                Capture_Screenshot("ChangeFlexPoints", "Pass", "Updated Flex Points for Employee and Dependant Successfully");
            
          
        }

    public void ModifyProducts()
    {

        extentTest.Log(LogStatus.Info, "DELETE PRODUCTS");
        string[] products = new string[] { "GCI_ACC", "GCI_ADD", "GDI", "MAT" };

        Click(By.Id("WinStartButton"));
        Thread.Sleep(2000);
        Java_ClickElement("LinkText", "Configuration", "LinkText", "Flex Benefits Configuration");

        driver_wait(By.LinkText("Setup/Initialize Flex Group"));
        Click(By.LinkText("Setup Benefits/Products"));

        driver_wait(By.XPath("//input[@value='Save']"));
        Console.WriteLine("Total rows are " + products.Length);


        for (int i = 0; i < products.Length; i++)
        {

            Console.WriteLine("Product value is  " + products[i]);
            int value = GetTableCount("xpath", "//*[@id='form1']/table/tbody/tr");
            Console.WriteLine("Total rows are " + value.ToString());
            for (int j = 2; j <= value - 2; j++)
            {
                Console.WriteLine("J value is " + j + " Value is " + (value - 2));
                string text = GetText(By.XPath("//*[@id='form1']/table/tbody/tr[" + j + "]/td[5]"));
                Console.WriteLine("Text value is " + text);
                if (products[i].ToString().Equals(text))
                {
                    Console.WriteLine("Deleting value " + text);
                    Click(By.XPath("//*[@id='form1']/table/tbody/tr[" + j + "]/td[3]/a"));
                    Thread.Sleep(5000);

                    IAlert alert = driver.SwitchTo().Alert();
                    // Alert present; set the flag
                    // presentFlag = true;
                    // if present consume the alert


                    alert.Accept();

                    Thread.Sleep(5000);
                    driver.SwitchTo().DefaultContent();


                    extentTest.Log(LogStatus.Pass, "Product : " + products[i] + " deleted successfully");
                    Capture_Screenshot(products[i].ToString(), "Info", products[i] + " Snapshot:");
                    driver_wait(By.XPath("//input[@value='Save']"));

                    value = GetTableCount("xpath", "//*[@id='form1']/table/tbody/tr");
                    goto continueloop;

                }
                else
                {
                    if (j == (value - 2))
                    {
                        Console.WriteLine("Final value reached");

                        Capture_Screenshot(products[i].ToString(), "Fail", products[i] + " Not Found in List");

                    }
                }
            }
            continueloop:;
        }

        // Click(By.XPath("//*[@id='form1']/table/tbody/tr[13]/td[3]/a"));
    

            
            EnableProducts();
        }
        public void EnableProducts()
        {
            
                extentTest.Log(LogStatus.Info, "ENABLE PRODUCTS");
                ExtentTest EnableProdTest= extent.StartTest("EnableProducts","EnableProducts");
                int rowcount = GetTableCount("xpath", "//*[@id='form1']/table/tbody/tr");
                Console.WriteLine("Total rows in Enable products is " + rowcount);
                for (int i = 2; i <rowcount-1; i++)
                {
                    Console.WriteLine("I value is " + i);
                    string Productname = GetText(By.XPath("//*[@id='form1']/table/tbody/tr[" + i + "]/td[5]"));
                    Console.WriteLine("Product value " + Productname);
                    Click(By.XPath("//*[@id='form1']/table/tbody/tr[" + i + "]/td[2]/a"));
                   
                    driver_wait(By.LinkText("Eligibility"));
                    Click(By.LinkText("Eligibility"));
                   
                    driver_wait(By.XPath("//input[@value='Save Changes']"));
                    //ReadOnlyCollection<IWebElement> CheckboxElements = driver.FindElements(By.XPath("//input[contains(@id,'EligiblePlans1')]"));
                    /** Identify all checkboxes present **/

                    string checkboxes = "//input[contains(@id,'EligiblePlans1')]";
                    var allCheckboxes = driver.FindElements(By.XPath(checkboxes));

                        Console.WriteLine(" Total elements list are " + allCheckboxes.Count);

                   for ( int prodloop=2;prodloop<= allCheckboxes.Count;prodloop++)
                    {
                        Click(By.Id("EligiblePlans1" + prodloop));
                        Thread.Sleep(1000);

                            if (Productname.ToUpper() == "GPA" || Productname.ToUpper() == "GTL")
                            {

                                if (prodloop == 5)
                                {

                                    SelectElement se = new SelectElement(driver.FindElement(By.Name("NewHireDefaultOption1")));
                                    se.SelectByText("12x: 12 x Basic Monthly Salary");
                                    Thread.Sleep(2000);

                                ScrollToBottom();
                                Click(By.XPath("//*[@id='tabs-4']/table/tfoot/tr/td/input"));
                                goto loop;
                                }
                            }
                            else if (Productname.ToUpper() == "GHS" || (Productname.ToUpper() == "GMM"))
                            {
                                if (prodloop == allCheckboxes.Count)
                                {
                                    SelectElement se = new SelectElement(driver.FindElement(By.Name("NewHireDefaultPlan1")));

                                    se.SelectByIndex(2);
                                
                            }
                            }
                            else if (Productname.ToUpper() == "GP")
                            {
                                if (prodloop == 13)
                                {

                                    SelectElement se = new SelectElement(driver.FindElement(By.Name("NewHireDefaultPlan1")));
                                    //se.SelectByText("Plan02EE: Panel + Non-Panel $35/visit (Employee Only)");
                                    se.SelectByIndex(6);
                                    Thread.Sleep(2000);
                                    goto loop;
                                }
                            }

                            }
                    loop:;
                       Console.WriteLine("Clicking Save Button");
                        Thread.Sleep(3000);
                    ///Cliking the Save Changes button
                    if (Productname.ToUpper() != "GPA" && Productname.ToUpper() != "GTL")
                    {
                        ScrollToBottom();
                        Thread.Sleep(2000);
                        Click(By.CssSelector("table.DefaultTable:nth-child(6) > tfoot:nth-child(3) > tr:nth-child(1) > td:nth-child(1) > input:nth-child(1)"));
                        

                    }
                    driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
                  
                    Capture_Screenshot(Productname + "  Settings Modified Successfully", "Pass", Productname + " screenshot");
                    
                    Click(By.PartialLinkText("Back to Benefits Setup"));
                    
                    driver_wait(By.XPath("//input[@value='Add New Benefit']"));
                }
                ///Cliking the Save  button
              //  Java_ClickElement("XPath", "//input[@value='Save']", "", "");
                extent.EndTest(EnableProdTest);
                
            
            
        }
        public void Claims_Configuration()
        {
           
                extentTest.Log(LogStatus.Info, "CLAIMS CONFIGURATION");
                Click(By.Id("WinStartButton"));
                Thread.Sleep(2000);
                Java_ClickElement("LinkText", "Configuration", "LinkText", "Claims Configuration");
               
                driver_wait(By.LinkText("Setup Claim Items"));
                Click(By.LinkText("Setup Claim Items"));
                
                driver_wait(By.Id("AddNewItem"));
                int rowcount = GetTableCount("xpath", "//table[@class='DefaultTable']/tbody/tr");
                Console.WriteLine("Total rows in products is " + rowcount);

                /****  Changing the product values to Expired ****/
                for (int i = 2; i < rowcount; i++)
                {
                    Console.WriteLine("I value is " + i);
                    string prodname = GetText(By.XPath("//*[@id='tabs-1']/table/tbody/tr[" + i + "]/td[7]"));
                    Console.WriteLine("Product name is:  " + prodname);
                    // IWebElement DropdownID = driver.FindElement(By.Id(prodname+"Status"));
                    if (prodname != "HS-HS" & prodname != "NF-TCM")
                    {

                        SelectElement se = new SelectElement(driver.FindElement(By.Id(prodname + "Status")));
                        se.SelectByText("Expired");
                    }
                }
                Click(By.Id("SaveStatus"));
                Thread.Sleep(3000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(5000);
                driver.SwitchTo().DefaultContent();
                Capture_Screenshot("Claims_Configuration", "Pass", "Claims Configuration successfull done");
                /****  Eciting the Show products ****/
                for (int i = 2; i < rowcount; i++)
                {
                    Console.WriteLine("I value is " + i);
                    string prodname = GetText(By.XPath("//*[@id='tabs-1']/table/tbody/tr[" + i + "]/td[7]"));
                    Console.WriteLine("Product name is:  " + prodname);
                    // IWebElement DropdownID = driver.FindElement(By.Id(prodname+"Status"));
                    if (prodname == "HS-HS" || prodname == "NF-TCM")
                    {

                        Click(By.XPath("//*[@id='tabs-1']/table/tbody/tr[" + i + "]/td[2]/a"));

                        
                        driver_wait(By.Id("RulesUIToggle"));
                        Clear(By.Id("EmployeeEligibilityRules"));
                        if (prodname == "HS-HS")
                        {
                            String EPtext = "BenefitSelection.[MemberType] = 'Employee' AND BenefitSelection.[BenefitID] IN('GHS', 'GMM') AND BenefitSelection.[OptionPlanFinal] IN('Plan01EE', 'Plan01ES', 'Plan01EC', 'Plan01EF', 'Plan02EE', 'Plan02ES', 'Plan02EC', 'Plan02EF', 'Plan03EE', 'Plan03ES', 'Plan03EC', 'Plan03EF', 'Plan04EE', 'Plan04ES', 'Plan04EC', 'Plan04EF', 'Plan05EE', 'Plan05ES', 'Plan05EC', 'Plan05EF')";
                            SendKeys(By.Id("EmployeeEligibilityRules"), EPtext);
                        }
                        Thread.Sleep(4000);
                        Clear(By.Id("DependantEligibilityRules"));
                        if (prodname == "HS-HS")
                        {
                            String DPtext = "BenefitSelection.[MemberType] = ‘Dependant’ AND BenefitSelection.[BenefitID] IN('GHS', 'GMM') AND BenefitSelection.[OptionPlanFinal] IN ('Plan01EE', 'Plan01ES', 'Plan01EC', 'Plan01EF', 'Plan02EE', 'Plan02ES', 'Plan02EC', 'Plan02EF', 'Plan03EE', 'Plan03ES', 'Plan03EC', 'Plan03EF', 'Plan04EE', 'Plan04ES', 'Plan04EC', 'Plan04EF', 'Plan05EE', 'Plan05ES', 'Plan05EC', 'Plan05EF')";
                            SendKeys(By.Id("DependantEligibilityRules"), DPtext);
                        }
                        Thread.Sleep(4000);
                        ScrollToBottom();
                        Thread.Sleep(3000);
                        Click(By.XPath("//*[@id='ctl00']/table/tfoot/tr/td/input"));
                        
                        driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
                        Capture_Screenshot("Updated_Products", "Pass", "Updated Product values");

                        Click(By.PartialLinkText("Back to Claim Items Listing"));
                        
                        driver_wait(By.Id("SaveStatus"));

                    }

                    
                }
            
        }

        public void ModifyLinkLogic()
        {
            
                /********* Deleting Link Logic ***********/
                extentTest.Log(LogStatus.Info, "DELETE LINK LOGIC");
                Click(By.Id("WinStartButton"));
                Thread.Sleep(2000);
                Java_ClickElement("LinkText", "Configuration", "LinkText", "Flex Benefits Configuration");
                
                driver_wait(By.LinkText("Setup Benefits/Products Link Logic"));
                Click(By.LinkText("Setup Benefits/Products Link Logic"));
                
                driver_wait(By.Id("AddNew"));
                loop:;
                int rowcount = GetTableCount("xpath", "//*[@id='LinkLogicListingPnl']/table/tbody/tr");
                Console.WriteLine("Total rows in products is " + rowcount);
                
                /****  Changing the product values to Expired ****/
                for (int i = 2; i <= rowcount; i++)
                {
                    Console.WriteLine("I value is " + i);
                    string prodname = GetText(By.XPath("//*[@id='LinkLogicListingPnl']/table/tbody/tr[" +i+"]/td[5]"));
                    Console.WriteLine("Product name is:  " + prodname);
                    // IWebElement DropdownID = driver.FindElement(By.Id(prodname+"Status"));
                    if (prodname != "GTL_GCI")
                    {

                        Click((By.XPath("//*[@id='LinkLogicListingPnl']/table/tbody/tr[" + i + "]/td[3]/a")));

                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                        wait.Until(ExpectedConditions.AlertIsPresent());

                        IAlert alert = driver.SwitchTo().Alert();
                        alert.Accept();

                        // driver.SwitchTo().DefaultContent();
                        Capture_Screenshot("Claims_Configuration", "Pass", "Deleted Product : "+prodname);
                        rowcount = GetTableCount("xpath", "//*[@id='LinkLogicListingPnl']/table/tbody/tr");
                        goto loop;
                    }
                }

                driver_wait(By.Id("AddNew"));
                
                /*******  Modify Link Logic *********/
                extentTest.Log(LogStatus.Info, "UPDATING LINK LOGIC");
                Click((By.XPath("//*[@id='LinkLogicListingPnl']/table/tbody/tr[2]/td[2]/a")));

                
                driver_wait(By.Id("L2_RHSBenefit"));
                Clear(By.Id("LinkLogicID"));
                SendKeys(By.Id("LinkLogicID"), "GTL_GPA");
                Clear(By.Id("LinkLogicTitle"));
                SendKeys(By.Id("LinkLogicTitle"), "GTL_GPA");
                Click(By.Id("Between2SumAssuredBenefits"));
                SelectDropdown(By.Id("L2_LHSBenefit"), "GTL - Group Term Life");
                
                driver_wait(By.Id("L2_RHSBenefit"));
                SelectDropdown(By.Id("L2_RHSBenefit"), "GPA - Group Personal Accident");
                ScrollToBottom();
                Thread.Sleep(2000);
                Click(By.XPath("//*[@id='LifeInsurancePnl']/table/tfoot/tr/td/input[1]"));
                driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
                Capture_Screenshot("Mofify_Link_Logic", "Pass", "Modified GTL-GPA Link Logic");


            
        }
        public void CreationOfEmployee()
        {

            extentTest.Log(LogStatus.Info, "EMPLOYEE CREATION");
            driver_wait(By.Id("WinStartButton"));
            Java_ClickElement("xpath", "//a[@rel='ProfileMenu']", "xpath", "//*[@id='ProfileMenu']/ul/li[10]/a");
            
            driver_wait(By.Id("frmID"));
            // int worksheetno = 1;
            Excelpath = "C:\\CXA_Automation\\CXAPortal\\TestData\\SmokeTest.xlsx";
            Execute_Excel(Excelpath, "CreationOfEmployee");
            Capture_Screenshot("CreationOfEmployee", "Pass", "Created New Employee Successfully");

        }
        public void CreationOfDependant()
        {

            extentTest.Log(LogStatus.Info, "DEPENDANT CREATION");
            driver_wait(By.Id("WinStartButton"));
            Java_ClickElement("xpath", "//a[@rel='ProfileMenu']", "xpath", "//*[@id='ProfileMenu']/ul/li[11]/a");
          
            driver_wait(By.Id("SearchValue"));
            // int worksheetno = 1;
            Excelpath = "C:\\CXA_Automation\\CXAPortal\\TestData\\SmokeTest.xlsx";
            ExcelLib.PopulateInCollection(Excelpath, "CreationOfDependant");
            String EmpName = ExcelLib.ReadValues(3, 0);
            SendKeys(By.Id("SearchValue"), EmpName);
            Click(By.Id("bthSearch"));
            
            driver_wait(By.LinkText(EmpName));
            Click(By.LinkText(EmpName));
            driver_wait(By.Id("New"));
            Click(By.Id("New"));
            
            driver_wait(By.Id("frmID"));
            Execute_Excel(Excelpath, "CreationOfDependant");
            Capture_Screenshot("CreationOfDependant", "Pass", "Created New Dependant Successfully");

        }

        public void  ProcessWLE()
        {
            extentTest.Log(LogStatus.Info, "PROCESSING WORK LIFE EVENT");
            driver_wait(By.Id("WinStartButton"));
            Java_ClickElement("xpath", "//a[@rel='ProfileMenu']", "xpath", "//*[@id='ProfileMenu']/ul/li[12]/a");

            driver_wait(By.LinkText("Life/Work Event Processing (One by One)"));
            Click(By.LinkText("Life/Work Event Processing (One by One)"));
            driver_wait(By.Id("AddNewBtn"));
            continueloop:;
            int rowcount = GetTableCount("xpath", "//*[@id='Form1']/table/tbody/tr");
            Console.WriteLine("Total Employees needs Process are  " + rowcount);

            /****  Changing the product values to Expired ****/
            for (int i = 2; i <= rowcount; i++)
            {
                Console.WriteLine("I value is " + i);
                string Empname = GetText(By.XPath("//*[@id='Form1']/table/tbody/tr["+i+"]/td[2]"));
                string EmpText = GetText(By.XPath("//*[@id='Form1']/table/tbody/tr[" + i + "]/td[6]"));
                Console.WriteLine("Employee  name is:  " + Empname);
                Click(By.XPath("//*[@id='Form1']/table/tbody/tr["+i+"]/td[7]/input"));
                driver_wait(By.Id("RefreshProratioBtn"));
                Capture_Screenshot("WLEPage_" + Empname, "Pass", " WLE details for " + Empname);
                ScrollToBottom();
                driver_wait(By.Id("CompleteBtn"));
                if (EmpText.ToLower().Contains("new hire"))
                {
                    Console.WriteLine("Clicked New Hire Process button");
                    Click(By.Id("ProcessBtn"));
                }
                else
                {
                    Console.WriteLine("Clicked Mark step as completed Process button");
                    Click(By.Id("CompleteBtn"));
                }
               
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(ExpectedConditions.AlertIsPresent());

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
                // driver.SwitchTo().DefaultContent();
                Capture_Screenshot("ProcesWLE_"+Empname, "Pass", "Successfully Processed WLE for " + Empname);

               // ScrollToBottom();
                //Click(By.XPath("//*[@id='form1']/table/tfoot/tr[2]/td/input[4]"));
                driver_wait(By.Id("AddNewBtn"));
                goto continueloop;
                
            }

            }
        [TearDown]
        public void teardown()
        {

           // Logout();

            //driver.Navigate().GoToUrl(Resultspath + "\\TestResults.html");
        }
        [OneTimeTearDown]

        public void Final()
        {
            Logout();
            extent.EndTest(extentTest);

            // writing everything to document.
            extent.Flush();
        }

    }
}
