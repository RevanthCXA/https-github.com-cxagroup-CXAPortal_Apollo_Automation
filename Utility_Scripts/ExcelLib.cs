using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CXAPortal.Utility_Scripts
{
    class ExcelLib
    {
        public static DataTable table;
        private static DataTable ExcelToDataTable(string fileName, string sheet)
        {
            //open file and returns as Stream
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //Createopenxmlreader via ExcelReaderFactory
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream); //.xlsx
            //Set the First Row as Column Name
            excelReader.IsFirstRowAsColumnNames = true;
            //Return as DataSet
            DataSet result = excelReader.AsDataSet();
            //Get all the Tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table[sheet];
           // Console.WriteLine("Datatable is : " + resultTable.ToString());

            //return
            return resultTable;
        }

        public class Datacollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }

        public static List<Datacollection> dataCol = new List<Datacollection>();

        public static void PopulateInCollection(string path, string sheet)
        {
            table = ExcelToDataTable(@path, sheet);
          //  Console.WriteLine("Datatable count is : " + table.Rows.Count);
            //Iterate through the rows and columns of the Table
            //for (int row = 1; row <= table.Rows.Count; row++)

            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()                      

                };
                  //  Console.WriteLine("Datatable value is : " + table.Rows[row - 1][col].ToString());
                    //Add all the details for each row
                  //  Console.WriteLine("Datatable is : " + dtTable);
                    dataCol.Add(dtTable);
                }
            }
        }

        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                Console.WriteLine(rowNumber);
                string data = (from colData in dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
              //  Console.WriteLine("Data is : "+data.ToString());
                return data.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static string ReadValues(int rowNumber, int columnno)
        {
            try
            {
               // Console.WriteLine(rowNumber);
                string data = table.Rows[rowNumber-1][columnno].ToString();
                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
              //  Console.WriteLine("Data value is is : " + data.ToString());
                return data.ToString();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
