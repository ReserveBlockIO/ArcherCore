using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcherCore.CSV
{
    public static class CSVUtility
    {
        public static List<string[]> ReadCSV(string path)
        {
            List<string[]> fieldArrayList = new List<string[]>();
            try
            {
                var fileExist = File.Exists(path);
                if(fileExist)
                {
                    using (TextFieldParser parser = new TextFieldParser(@"D:\presalelist.csv"))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        // Skip the header if needed
                        if (!parser.EndOfData)
                        {
                            parser.ReadLine();
                        }

                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();
                            if(fields != null)
                                fieldArrayList.Add(fields);
                        }
                    }
                }
            }
            catch { }

            return fieldArrayList;
        }
    }
}
