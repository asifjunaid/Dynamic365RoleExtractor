using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_365_Role_Extractor.Utilities
{
    public class FileManager
    {
        public static void WriteToFile<T>(T lst, string fileName)
        {
            string filePath = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();
            using (Stream stream = File.Open(filePath + @"\" + fileName, FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bformatter.Serialize(stream, lst);
            }
        }
        public static T ReadFromFile<T>(string fileName)
        {
            T lst = default(T) ;
            string filePath = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();
            fileName = filePath + @"\" + fileName;
            if (File.Exists(fileName))
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    if (stream.Length > 0)
                        lst = (T)bformatter.Deserialize(stream);
                }
            return lst;
        }
        public static void WriteToCSVFile(StringBuilder sb, string fileName)
        {
            string filePath = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();
            fileName = filePath + @"\" + fileName;
            // Check if file already exists. If yes, delete it.     
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            // Create a new file     
            using (FileStream fs = File.Create(fileName))
            {
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes(sb.ToString());
                fs.Write(title, 0, title.Length);
            }
        }
        public static string[] ReadFromCSVFile(string fileName)
        {
            StringBuilder sb = new StringBuilder();
            string filePath = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();
            fileName = filePath + @"\" + fileName;
            if (!File.Exists(fileName))
                throw new Exception($"{fileName} not exist!");

            return File.ReadAllLines(fileName);
        }
    }
}
