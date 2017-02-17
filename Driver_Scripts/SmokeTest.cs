using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Compatibility;
using NUnit.Framework;
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
        public static String Excelpath,worksheet,Screenshotpath,TestResults,Resultspath,ClaimNo,Client, Emp_Username;
        string[] Accountvalues = new string[6];
        [SetUp]
        public void setup()
        {

            CreateHTMLReport("SmokeTest");            
            extentTest = extent.StartTest("Smoke Test","SmokeTest");
            //CXALogin("SmokeTest_URL", "SmokeTest_CompanyId", "SmokeTest_Username", "SmokeTest_Password", "SmokeTest_Browser", "SmokeTest_Client");
            //Employee portal
            Client = ConfigurationManager.AppSettings["SmokeTest_Client"];
            //CXALogin("URL", "CompanyId", "Username", "Password", "Browser", "Client");
            Console.WriteLine(Screenshotpath);
            // Shared_Functions.AccessClient(client);
        }
        [Test]
        public void Smoketest()
        {

            // MessageBox.Show("Execution completed");
            // string client = ConfigurationManager.AppSettings["NewClient"];
            Client = ConfigurationManager.AppSettings["SmokeTest_Client"];
            try
            {
               // CreationOfClient(client);
           //NOte: Comment Access client if creation of client is enabled
                AccessClient(Client);
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
                  //   ChangeFlexPoints();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ChangeFlexPoints", "Fail", e.Message);
                }
                /*****  Deletes the Products and Enable the eligible options( 12X,24X etc)  *****/
                try { 

                  //  ModifyProducts();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ModifyProducts", "Fail", e.Message);
                }

                /*****  Change the Claims prods to "Expired" except HS-HS,NF-TCM *****/
                try {

                   //Claims_Configuration();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ClaimsConfiguration", "Fail", e.Message);
                }

                /***** Updates Link logic GTL-GCI to GTL-GPA and deletes other link logics *****/
                try {

                    //ModifyLinkLogic();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error_ModifyLinkLogic", "Fail", e.Message);
                }

                /*****  Creates new Employee with Hire and Flex date is today *****/

                try
                {

                      //CreationOfEmployee();
                      //CreationOfDependant();
                    //  ProcessWLE();
                     // UploadEmployee();
                     // UploadDependant();
                      
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                    Capture_Screenshot("Error", "Fail", e.Message);
                }
                               
            }
            catch (Exception e)
            {
                Console.WriteLine("Error is " + e.Message + "" + e.StackTrace);

                Capture_Screenshot("Error", "Fail", e.Message);
                Final();
            }
        }
       // [Test]
        public  void UploadEmployee()
        {
            extentTest.Log(LogStatus.Info, "EMPLOYEE UPLOAD");
           // AccessClient("sample5");
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
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver_wait(By.Id("UploadBtn"));
            Capture_Screenshot("EmployeeFIleUpload", "Pass", "Employee File Upload");
            Click(By.Id("UploadBtn"));
            HandleAlert();
            driver_wait(By.Id("SkipLE"));
            Thread.Sleep(2000);
            Capture_Screenshot("EmployeeFIleUploadDetails", "Pass", "Employee File Upload status");
            Click(By.Id("UploadBtn"));
            HandleAlert();
            driver_wait(By.XPath("//*[@id='form1']/table/tbody/tr/td[2]"));
            Capture_Screenshot("EmployeeFIleUploadSuccess", "Pass", "Employee File Uploaded Successfully");
            ProcessWLE();
            Thread.Sleep(5000);
            //UploadDependant(); 
        }
        //[Test]
        public void UploadDependant()
        {
            extentTest.Log(LogStatus.Info, "EMPLOYEE UPLOAD");
          //  AccessClient("sample5");
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
            Capture_Screenshot("DependantFIleUploadDetails", "Pass", "Dependant File Upload status");
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
                Thread.Sleep(2000);
                Capture_Screenshot("ClientDetails", "Pass", "ClientDetails Page");
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

                    }
                    catch(Exception e)
                    {
                        Click(By.Id("bthSearch"));
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
                 driver_wait(By.ClassName("FormValue"));       

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
                     HandleAlert();
                    Thread.Sleep(5000);
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
               // countloop:;
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
                    //    Click(By.XPath("//*[@id='tabs-1']/table/tbody/tr[" + i + "]/td[3]/a"));
                    //    HandleAlert();
                     //   driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
                        //goto countloop;
                    }
                }
                ScrollToBottom();
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
                            String DPtext = "BenefitSelection.[MemberType] = 'Dependant' AND BenefitSelection.[BenefitID] IN('GHS', 'GMM') AND BenefitSelection.[OptionPlanFinal] IN ('Plan01EE', 'Plan01ES', 'Plan01EC', 'Plan01EF', 'Plan02EE', 'Plan02ES', 'Plan02EC', 'Plan02EF', 'Plan03EE', 'Plan03ES', 'Plan03EC', 'Plan03EF', 'Plan04EE', 'Plan04ES', 'Plan04EC', 'Plan04EF', 'Plan05EE', 'Plan05ES', 'Plan05EC', 'Plan05EF')";
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

                        HandleAlert();                       
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
            //AccessClient("QA_Automation");
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
            //AccessClient("QA_Automation");
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
            String text = GetText(By.XPath("//*[@id='Form1']/table/tbody/tr[2]"));
            if (text.Contains("No Pending Life/Work Events"))
            {
                Console.WriteLine("No Pending Events");
            }
            else {
                /****  Adminstering the Work Life Events ****/
                for (int i = 2; i <= rowcount; i++)
                {
                    Console.WriteLine("I value is " + i);
                    string Empname = GetText(By.XPath("//*[@id='Form1']/table/tbody/tr[" + i + "]/td[2]"));
                    string EmpText = GetText(By.XPath("//*[@id='Form1']/table/tbody/tr[" + i + "]/td[6]"));
                    Console.WriteLine("Employee  name is:  " + Empname);
                    Click(By.XPath("//*[@id='Form1']/table/tbody/tr[" + i + "]/td[7]/input"));
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
                    HandleAlert();
                    driver_wait(By.XPath("//span[@class='ui-icon ui-icon-info']"));
                    // driver.SwitchTo().DefaultContent();
                    Capture_Screenshot("ProcesWLE_" + Empname, "Pass", "Successfully Processed WLE for " + Empname);

                    try
                    {

                        ScrollToBottom();

                        Click(By.XPath("//*[@id='form1']/table/tfoot/tr[2]/td/input[4]"));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("NO Cancel button");
                    }
                    driver_wait(By.Id("AddNewBtn"));
                    goto continueloop;
                }
            }

            }
        [Test]
        public void EmployeePortal()
        {

            CXAEmpLogin("Employee_Portal", "SmokeTest_EmpCompanyId", "SmokeTest_EmpUsername", "SmokeTest_EmpPassword", "SmokeTest_Browser");
            
                VerifyEmployeeHomePage();
          
            // VerifyShop();
           try
            { 
                VerifyMyClaims();
             }catch(Exception e)
             {
                Capture_Screenshot("ClaimsError", "Fail", "Claims Error");
             }

            Employee_logout();

}
        public void VerifyEmployeeHomePage()
        {
            //***Verifying the Enrolment countdown card on the home page ***
            extentTest.Log(LogStatus.Info, "VERIFY DASHBOARD ITEMS");
            string[] dashboard_items = { "Message board", "My Lifestyle", "My Claims", "Introducing ACME Benefits Program"};

            try
            {
                string enrollment_text = GetText(By.XPath("//div[@class='countdown']"));

                if (!enrollment_text.ToLower().Contains("Nan"))
                {
                    Console.WriteLine("Pas value;");
                    Capture_Screenshot("EmployeeCounterPresent", "Pass", "Employee Enrolment Timer dislayed successfully");
                }
                else
                {
                    Console.WriteLine("Fail value");
                    Capture_Screenshot("EmployeeCounterAbsent", "Fail", "Employee Enrolment Timer Not Displayed");
                }
            }catch(Exception e)

            {
                Capture_Screenshot("EmployeeCounterAbsent", "Fail", "Employee Enrolment Timer Not Displayed");
            }

            //***Verifying the Message Board on the home page ***

            int count = GetTableCount("Xpath", "//div[@class='card__section']");
            Console.WriteLine("Count value is " + count);

            IList<IWebElement> elementList = driver.FindElements(By.XPath("//div[@class='card__section']"));

            Boolean found = false;
          
            List<string> validations = new List<string>();
            for (int dashboard_loop = 0; dashboard_loop < dashboard_items.Length; dashboard_loop++)
            {
                string val_text = "";
                foreach (IWebElement element in elementList)
                {
                    val_text = element.Text;
                    Console.WriteLine(val_text);

                    try
                    {
                        if (val_text.ToLower().Contains(dashboard_items[dashboard_loop].ToLower()))
                        {
                            found = true;
                            break;
                        }
                    }catch(Exception e)
                    {
                        Capture_Screenshot("DashboardError", "Fail", dashboard_items[dashboard_loop] + " Not Displayed");
                    }
                }
                if (found)
                {
                    extentTest.Log(LogStatus.Pass, dashboard_items[dashboard_loop] + " Displayed Successfully with data :" + val_text);
                    found = false;
                }
                else
                {
                    extentTest.Log(LogStatus.Fail, dashboard_items[dashboard_loop] + " Not Displayed Successfully");
                    Capture_Screenshot("DashboardError", "Fail", dashboard_items[dashboard_loop] + " Not Displayed");
                }
            }
                  // Wellness shop link
                    try
                    {
                        driver.FindElement(By.LinkText("Visit Welness Shop"));
                        extentTest.Log(LogStatus.Pass, "Visit Welness Shop Link displayed Successfully");
                    }
                    catch (Exception e)

                    {
                        Capture_Screenshot("WellnessShopLinkError", "Fail", "Visit Wellness Shop Link Not Displayed");
                    }               
            
        }
         public void VerifyShop()
        {
            Click(By.LinkText("My Shop"));
           // driver_wait()
        }

        public void VerifyMyClaims()
        {
            extentTest.Log(LogStatus.Info, "MY CLAIMS-Accounts Overview");

            Click(By.LinkText("My Claims"));
            try
            {
                driver_wait(By.LinkText("Flexible Spending Account"));
                // SelectDropdown(By.Id("benefitPeriod"), "2017");
                Thread.Sleep(3000);

                Capture_Screenshot("MyClaimsAccountsOverview", "Pass", "Accounts Overview");
                /**** Accounts Overview ****/

                SelectDropdown(By.Id("benefitPeriod"), "2017");
                int rowcount = GetTableCount("Xpath", "//table[@class='card-table']/tbody/tr");
                
                for (int i = 1; i < 7; i++)
                {
                    Console.WriteLine("i VALUE IS " + i);
                    Console.WriteLine(GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[" + i + "]")));
                    extentTest.Log(LogStatus.Pass, "Accounts Overview data :" + GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[" + i + "]")));
                    Accountvalues[i-1] = GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[" + i + "]"));
                    Console.WriteLine("Account added values are " + Accountvalues[i-1] +" at location "+(i-1));
                }
            }
            catch (Exception e)
            {
                Capture_Screenshot("FlexibleSpendingamountlinkError", "Fail", "FSA Error :"+e.Message);
            }

            /**** New Claims ****/
            try
            {
                extentTest.Log(LogStatus.Info, "MY CLAIMS-NEW Claims");
                Click(By.LinkText("New Claims"));
                driver_wait(By.Id("benefitPeriod"));
                Click(By.LinkText("Traditional Chinese Medicine"));
                driver_wait(By.Id("provider"));
                SendKeys(By.Id("provider"), "Aviva");
                SendKeys(By.Id("receiptNo"), "AutoClaim1");
               
                Thread.Sleep(2000);
                SendKeys(By.Id("receiptAmount"), "10.00");
                Click(By.Id("receipts"));
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.FileName = "C:\\CXA_Automation\\CXAPortal\\Employee_NewClaims.exe";
                process.StartInfo = startInfo;
                process.Start();
                Thread.Sleep(3000);
                driver.SwitchTo().DefaultContent();
                SendKeys(By.Id("remarks"), "New Claim Submission");
                EnterDate(By.Id("receiptDate"), GetDate("MM/dd/yyyy"));
                Thread.Sleep(3000);
                Capture_Screenshot("NewClaimDetailsPage", "Pass", "New Claim Details Page");
                Click(By.XPath("//div[contains(text(),'Submit')]"));
                driver_wait(By.XPath("//div[contains(text(),'Print')]"));
                ClaimNo = GetText(By.XPath("//div[@class='u-margin-bottom']/h2"));
                ClaimNo = ClaimNo.Replace("Claim #", "");
                Capture_Screenshot("ClaimSubmissionPage", "Pass", "Claim Created Successfully with ID:" + ClaimNo);

                /***Verify Claim submission details ***/
                extentTest.Log(LogStatus.Info, "MY CLAIMS-Claims Submission Successful page");
                VerifyClaimDetails();

                Thread.Sleep(2000);

                /**** Pending Claims ****/

                extentTest.Log(LogStatus.Info, "MY CLAIMS-Pending Claims");
                Click(By.LinkText("Pending Claims"));
                driver_wait(By.LinkText(ClaimNo));
                try
                {
                    driver_wait(By.XPath("//div[contains(text(),'Export All')]"));
                    extentTest.Log(LogStatus.Pass, "Export All button displayed fine");

                }
                catch (Exception e)
                {
                    Capture_Screenshot("PendingClaimsExportAllbutton", "Fail", "Export All button Missing on Pending Claims page");
                }
                int PendClaims_rowcount = GetTableCount("xpath", "//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr");
                Console.WriteLine("PendClaims_rowcount count is " + PendClaims_rowcount);
                for (int claimsloop = 1; claimsloop <= PendClaims_rowcount; claimsloop++)
                {
                    Boolean claim_found = false;
                    String claimsId = GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[1]"));
                    if (claimsId.ToLower() == ClaimNo.ToLower())
                    {
                        claim_found = true;
                        Capture_Screenshot("PendingSubmissionPage", "Pass", "New Claim displayed in Pending claims with ID :" + ClaimNo);

                        if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[2]")) == "Traditional Chinese Medicine")
                        {
                            extentTest.Log(LogStatus.Pass, "Claim Item :Traditional Chinese Medicine displayed successfully");
                        }
                        else
                        {
                            extentTest.Log(LogStatus.Fail, "Claim Item :Traditional Chinese Medicine Not displayed with data:" + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[2]")));
                        }
                        if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[5]")) == "Aviva")
                        {
                            extentTest.Log(LogStatus.Pass, "Provider :Aviva displayed successfully");
                        }
                        else
                        {
                            extentTest.Log(LogStatus.Fail, "Provider :Aviva Chinese Medicine Not displayed with data:" + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[5]")));
                        }
                        if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[6]")) == "SGD10.00")
                        {
                            extentTest.Log(LogStatus.Pass, "Receipt Amount :SGD 10.00 displayed successfully");
                        }
                        else
                        {
                            extentTest.Log(LogStatus.Fail, "Receipt Amount :SGD 10.00 Not displayedwith data:" + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[6]")));
                        }
                        if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[8]")) == "Pending Verification")
                        {
                            extentTest.Log(LogStatus.Pass, "Status :Pending Verification displayed successfully");
                        }
                        else
                        {
                            extentTest.Log(LogStatus.Fail, "Status :Pending Verification Not displayed with data:" + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[2]/div/table/tbody/tr[" + claimsloop + "]/td[8]")));
                        }
                        break;
                    }
                    if (claimsloop == PendClaims_rowcount)
                    {
                        if (claim_found == false)
                        {
                            Capture_Screenshot("PendingSubmissionPageError", "Pass", "New Claim Not displayed on Pending Claim Items");
                        }
                    }
                }
            
                Click(By.LinkText(ClaimNo));                
                driver_wait(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[5]/td[1]"));
                extentTest.Log(LogStatus.Info, "VERIFYING PENDING CLAIMS DETAILS FOR : " + ClaimNo);
                Capture_Screenshot("PendingClaimsClaimsdetails", "Pass", "Pending Claims Claims details Page");
                VerifyClaimDetails();

                /**** Claims Adjudication ****/
                try
                {
                    ClaimAdjudication(ClaimNo);
                }
                catch (Exception e)
                {
                    Capture_Screenshot("ClaimAdjudicaitonError", "Fail", "Error during Claim Adjudication process" + e.Message);
                }

            }
            catch (Exception e)
            {
                Capture_Screenshot("NewCLaimsError", "Fail", "Unable to create new Claim:" + e.Message);
            }
        }    
            public void VerifyClaimDetails()
        {
            if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/p")).Contains("Traditional Chinese Medicine"))
            {
                extentTest.Log(LogStatus.Pass, "Claim Item :Traditional Chinese Medicine displayed successfully");
            }
            else
            {
                extentTest.Log(LogStatus.Fail, "Claim Item :Traditional Chinese Medicine Not displayed with data:" + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/p")));
            }
            Emp_Username = ConfigurationManager.AppSettings["SmokeTest_EmpUsername"];

            if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]")) == Emp_Username)
            {
                extentTest.Log(LogStatus.Pass, "Claimant: " + Emp_Username + " displayed successfully");
            }
            else
            {
                extentTest.Log(LogStatus.Fail, "Claimant: " + Emp_Username + " Not displayed and the value displayed is " + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[1]/td[2]")));
            }
            if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[2]/td[2]")) == "Aviva")
            {
                extentTest.Log(LogStatus.Pass, "Provider:Aviva displayed successfully");
            }
            else
            {
                extentTest.Log(LogStatus.Fail, "Provider:Aviva  Not displayed and the value displayed is " + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[2]/td[2]")));
            }
            if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[3]/td[2]")) == "AutoClaim1")
            {
                extentTest.Log(LogStatus.Pass, "Receipt No:AutoClaim1 displayed successfully");
            }
            else
            {
                extentTest.Log(LogStatus.Fail, "Receipt No:AutoClaim1  Not displayed and the value displayed is " + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[3]/td[2]")));
            }
            if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[4]/td[2]")) == GetDate("M/dd/yyyy"))
            {
                extentTest.Log(LogStatus.Pass, "Receipt Date:" + GetDate("M/dd/yyyy") + " displayed successfully");
            }
            else
            {
                extentTest.Log(LogStatus.Fail, "Receipt Date:" + GetDate("M/dd/yyyy") + " Not displayed and the value displayed is " + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[4]/td[2]")));
            }
            if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[5]/td[2]")) == "10")
            {
                extentTest.Log(LogStatus.Pass, "Receipt Amount:10 displayed successfully");
            }
            else
            {
                extentTest.Log(LogStatus.Fail, "Receipt Amount: 10  Not displayed and the value displayed is " + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[5]/td[2]")));
            }
            if (IsElementPresent(By.XPath("//figure[@class='file file--img u-margin-top']/a/img")))
            {
                extentTest.Log(LogStatus.Pass, "Receipt Upload displayed successfully");
            }
            else
            {
                extentTest.Log(LogStatus.Fail, "Receipt Upload Not displayed and the value displayed is ");
            }
            if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[7]/td[2]")) == "New Claim Submission")
            {
                extentTest.Log(LogStatus.Pass, "Remarks:New Claim Submission displayed successfully");
            }
            else
            {
                extentTest.Log(LogStatus.Fail, "Remarks:New Claim Submission  Not displayed and the value displayed is " + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[7]/td[2]")));
            }
        }
        
        public void ClaimAdjudication(string clmno)
        {
            Employee_logout();
            extentTest.Log(LogStatus.Info, "Logint to Admin Portal");

            CXALogin("SmokeTest_URL", "SmokeTest_CompanyId", "SmokeTest_Username", "SmokeTest_Password", "SmokeTest_Browser", "SmokeTest_Client");
            Console.WriteLine("Admin Portal login");
            AccessClient(Client);
            Java_ClickElement("xpath", "//a[@rel='ClaimsMenu']", "Xpath", "//div[@id='ClaimsMenu']/ul/li[3]/a");
            driver_wait(By.LinkText("Claim Adjudication"));
            Click(By.LinkText("Claim Adjudication"));
            driver_wait(By.XPath("//input[@value='Create New Batch']"));
            Click(By.XPath("//input[@value='Create New Batch']"));
            driver_wait(By.Id("BatchTitle"));
            SendKeys(By.Id("BatchTitle"), clmno);
            
            SendKeys(By.Id("ReceiveDate"), GetDate("dd MMM yyyy"));
            try
            {
                driver.FindElement(By.XPath("//*[@id='ui-datepicker-div']/div[2]/button[2]")).Click();
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {

            }
            SendKeys(By.Id("Remarks"), "SmokeTest Automation Testing");
            Click(By.Id("Create"));
            driver_wait(By.XPath("//input[@value='Adjudicate']"));
            Capture_Screenshot("ClaimAdministraionPage", "Pass", "Claim Batch process creation");
            Click(By.XPath("//input[@value='Adjudicate']"));
            driver_wait(By.XPath("//input[@value='Search']"));
            SendKeys(By.Id("SearchClaim"), clmno);
            //Click(By.XPath("//input[@value='Search']"));
            Click(By.XPath("//*[@id='form1']/div[3]/input[3]"));
            driver_wait(By.XPath("//input[@value='Approve']"));
            Capture_Screenshot("ClaimApprovePage", "Pass", "Claim Batch Approval Page");
            Click(By.XPath("//input[@value='Approve']"));
            driver_wait(By.XPath("//img[@title='Undo Approval']"));
            Capture_Screenshot("ClaimApproved", "Pass", "Claim Approved");

            //Process the Claims
            Java_ClickElement("xpath", "//a[@rel='ClaimsMenu']", "Xpath", "//div[@id='ClaimsMenu']/ul/li[3]/a");
            driver_wait(By.LinkText("Claim Adjudication"));
            Click(By.LinkText("Process Approve Flex Claims By Employee (deduct points from accounts)"));
            driver_wait(By.Id("SearchValue"));
            SendKeys(By.Id("SearchValue"), Emp_Username);
            Click(By.Id("bthSearch"));
            driver_wait(By.LinkText(Emp_Username));
            Click(By.LinkText(Emp_Username));
            driver_wait(By.XPath("//input[@value='Process']"));
            Capture_Screenshot("ProcessEmployeeClaimPage", "Pass", "Process Employee Claim Page");
            Click(By.XPath("//input[@value='Process']"));
            HandleAlert();
            driver_wait(By.XPath("//*[@id='form1']/table/tbody/tr/td[2]/table/tbody/tr[2]/td"));
            Capture_Screenshot("ProcessEmployeeClaimPage", "Pass", "CLaims Processed Page");

            // Generate Payroll
            try
            {
                Java_ClickElement("xpath", "//a[@rel='PayrollMenu']", "Xpath", "//*[@id='PayrollMenu']/ul/li[1]/a");
                driver_wait(By.Id("PaymentFrom"));
                Click(By.XPath("//input[@value='Generate New Payment File']"));
                driver_wait(By.Id("PaymentTitle"));
                Click(By.Id("GenerateFlexClaimSelfFunded"));
                SendKeys(By.Id("PaymentTitle"), ClaimNo + " payroll");
                EnterDate(By.Id("PaymentDate"), GetDate("dd MMM yyyy"));
                EnterDate(By.Id("GIRODate"), GetDate("dd MMM yyyy"));
                SendKeys(By.Id("PaymentDescription"), "Payroll Processing for " + ClaimNo);
                Capture_Screenshot("PayrollGeneratePage", "Pass", "Payroll Generate Page");
                Click(By.XPath("//input[@value='Generate']"));
                driver_wait(By.Id("PaymentFrom"));
                Capture_Screenshot("PayrollGeneratedSucessPage", "Pass", "Payroll Generate Success Page");
            }
            catch(Exception e)
            {
                Capture_Screenshot("PayrollGeneratePageError", "Fail", "Payroll Generate Page Error and value is :"+e.Message);
            }
            Logout();

            //Login to EMployee portal
            // driver.Quit();    
            
            Thread.Sleep(4000);
            CXAEmpLogin("Employee_Portal", "SmokeTest_EmpCompanyId", "SmokeTest_EmpUsername", "SmokeTest_EmpPassword", "SmokeTest_Browser");
            driver_wait(By.LinkText("My Claims"));
            Click(By.LinkText("My Claims"));
            driver_wait(By.LinkText("Claims History"));
            Click(By.LinkText("Claims History"));            
            driver_wait(By.Id("benefitPeriod"));
            driver_wait(By.LinkText(clmno));
            int Clms_htry_count = GetTableCount("Xpath", "//table[@class='card-table']/tbody/tr");
            Console.WriteLine("CLaims History total values are : " + Clms_htry_count);

            for(int i=1;i<=Clms_htry_count;i++)
            {
                string claims_histry_value = GetText(By.XPath("//table[@class='card-table']/tbody/tr["+i+"]/td[1]"));
                if (claims_histry_value.ToLower()==clmno.ToLower())
                {
                    Console.WriteLine("Found policy on claims histry");
                    if (GetText(By.XPath("//table[@class='card-table']/tbody/tr[" + i + "]/td[6]")) == "SGD10.00")
                    {
                        Console.WriteLine("Reimbursed Amount is 10");
                        Capture_Screenshot("ReimbursedAmount", "Pass", "Reimbursed Amount SGD10.00 displayed successfully on Claims History Page for ClaimID: "+ClaimNo);
                    }
                    else
                    {
                        Console.WriteLine("Status Not Matching :" + GetText(By.XPath("//table[@class='card-table']/tbody/tr[" + i + "]/td[6]")));
                        Capture_Screenshot("ReimbursedAmountError", "Fail", "ReimbursedAmount Not matching with claim amount and the values are :"+ GetText(By.XPath("//table[@class='card-table']/tbody/tr[" + i + "]/td[6]")));
                    }
                    if (GetText(By.XPath("//table[@class='card-table']/tbody/tr[" + i + "]/td[8]"))== "Paid")
                            {
                        Console.WriteLine("Status is Approved");
                        Capture_Screenshot("EmployeeClaimApproved", "Pass", "Claim Paid Status on Employee POrtal");
                    }
                    else
                    {
                        Console.WriteLine("Status Not Matching :"+ GetText(By.XPath("//table[@class='card-table']/tbody/tr[" + i + "]/td[8]")));
                        Capture_Screenshot("EmployeeClaimApprovedError", "Fail", "Claim Paid Status Not displayed for "+ClaimNo);
                    }
                }
            }
            Click(By.LinkText(ClaimNo));
            extentTest.Log(LogStatus.Info, "VERIFYING CLAIMS HISTORY DETAILS FOR : " + ClaimNo);
            driver_wait(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div/div[2]/table/tbody/tr[5]/td[1]"));
            VerifyClaimDetails();
            Click(By.LinkText("Pending Claims"));
            driver_wait(By.XPath("//div[contains(text(),'Export All')]"));
            Thread.Sleep(5000);
            try
            {
                IsElementPresent(By.LinkText(ClaimNo));
                Capture_Screenshot("PendingClaimsError", "Fail", "Claimno:" + ClaimNo + " still displays on Pending Claims page after Claim Adjudication");
            }
            catch(Exception e)
            {
                Capture_Screenshot("PendingClaims", "Pass", "Claimno:" + ClaimNo + " not displayed successfully on Pending Claims page after Claim Adjudication");
            }

            ///Verifying the values on Accounts Overview page
            try
            {
                Click(By.LinkText("Accounts Overview"));
                driver_wait(By.LinkText("Flexible Spending Account"));
                extentTest.Log(LogStatus.Info, "VERIFYING ACCOUNTS OVERVIEW  FOR : " + ClaimNo);
                Click(By.LinkText("Flexible Spending Account"));
                driver_wait(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[4]/div/table/tbody/tr[1]"));
                int FSA_rcount=GetTableCount("Xpath", "//*[@id='root']/div/div[1]/main/div/div/div/div[4]/div/table/tbody/tr");
                for(int i=2;i<= FSA_rcount;i++)
                {
                    if(GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[4]/div/table/tbody/tr["+i+"]/td[2]")).Contains(ClaimNo))
                    {
                        Capture_Screenshot("AccountsOverviewTransactionPage", "Pass", "Accounts Overview Trasaction details Page");
                        if (GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[4]/div/table/tbody/tr[" + i + "]/td[6]"))== "SGD -10")
                        {
                            extentTest.Log(LogStatus.Pass, "Amount displayed value matches with Approved amount for "+ClaimNo +" as "+ GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[4]/div/table/tbody/tr[" + i + "]/td[6]")));
                        }
                        else
                        {
                            extentTest.Log(LogStatus.Fail, "Amount displayed value not matching with Approved amount for " + ClaimNo + " as " + GetText(By.XPath("//*[@id='root']/div/div[1]/main/div/div/div/div[4]/div/table/tbody/tr[" + i + "]/td[6]")));
                        }
                    }
                }

                Click(By.XPath("//div[contains(text(),'Back')]"));
                driver_wait(By.LinkText("Flexible Spending Account"));
                Thread.Sleep(2000);
                Capture_Screenshot("AccountsOverviewPage", "Pass", "Accounts Overview details Page");

                if (GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[4]"))==Accountvalues[3])
                {
                    extentTest.Log(LogStatus.Pass, "Entitled Amount displayed value matches with Actual Entitled amount for " + ClaimNo + " as " + GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[4]")));
                }
                else
                {
                    extentTest.Log(LogStatus.Fail, "Entitled Amount displayed value Not matches with Actual Entitled amount for " + ClaimNo + " as :" + GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[4]")) + " and actual:"+ Accountvalues[3]);
                }
                float utilised_amount,bal_amount;
                Accountvalues[4] = Accountvalues[4].Replace("SGD -", "");
                Accountvalues[4] = Accountvalues[4].Replace(",", "");
                float.TryParse(Accountvalues[4], out utilised_amount);
                utilised_amount = utilised_amount + 10;  //10 is the receipt amount
                Accountvalues[4] = "SGD -" + utilised_amount.ToString();
                if (GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[5]")).Replace(",", "") == Accountvalues[4])
                {
                    extentTest.Log(LogStatus.Pass, "Utilised Amount displayed value matches with Actual Entitled amount for " + ClaimNo + " as " + GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[5]")));
                }
                else
                {
                    extentTest.Log(LogStatus.Fail, "Utilised Amount displayed value Not matches with Actual Entitled amount for " + ClaimNo + " as :" + GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[5]")) + " and actual:" + Accountvalues[4]);
                }
                Accountvalues[5] = Accountvalues[5].Replace("SGD ", "");
                Accountvalues[5] = Accountvalues[5].Replace(",", "");
                float.TryParse(Accountvalues[5], out bal_amount);
                bal_amount = bal_amount - 10;  //10 is the receipt amount
                Accountvalues[5] = "SGD " + bal_amount.ToString();
                if (GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[6]")).Replace(",","") == Accountvalues[5])
                {
                    extentTest.Log(LogStatus.Pass, "Balance Amount displayed value matches with Actual Entitled amount for " + ClaimNo + " as " + GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[6]")));
                }
                else
                {
                    extentTest.Log(LogStatus.Fail, "Balance Amount displayed value Not matches with Actual Entitled amount for " + ClaimNo + " as :" + GetText(By.XPath("//table[@class='card-table']/tbody/tr[1]/td[6]")) + " and actual:" + Accountvalues[5]);
                }

            }
            catch(Exception e)
            {
                Capture_Screenshot("AccountsOverviewError", "Fail", "Accounts Overview details error: "+e.Message);
            }
        }

        public void Employee_logout()
        {
            driver_wait(By.XPath("//div[@class='header__subnav']"));           
            Click(By.XPath("//div[@class='header__subnav']"));
            driver_wait(By.LinkText("Logout"));
            Click(By.LinkText("Logout"));
            driver_wait(By.Id("username"));
            driver.Manage().Cookies.DeleteAllCookies();
            Thread.Sleep(2000);
            //driver.Quit();
            //Java_ClickElement("linktext", "//div[@class='header__subnav']", "Xpath", "//*[@id='root']/div/div[1]/div/div/header/div[1]/div/nav[2]/div/div/div/a[3]");
        }
        [Test]
        public static void Testing()
        {
            DateTime todayDay = DateTime.Today;
            Console.WriteLine(DateTime.Today);
            string formattedDate = todayDay.ToString("dd Mmm yyyy");
            Console.WriteLine("Todays date is " + formattedDate);
            float j;
            string val = "SGD - 982.03";
            val = val.Replace("SGD - ", "");
            Console.WriteLine(val);
            if (float.TryParse(val, out j))
                Console.WriteLine(j-10);
            else
                Console.WriteLine("String could not be parsed.");
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
            extent.EndTest(extentTest);

            // writing everything to document.
            extent.Flush();
           // Logout();
           
        }

    }
}
