using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Console.WriteLine(folderPath);


            string fileName = "example.txt";

            string fullPath = Path.Combine(folderPath, fileName);
            Console.WriteLine(fullPath);

            File.Create(fullPath).Close();
            File.WriteAllText(fullPath, "HEllo" + Environment.NewLine);
            File.AppendAllText(fullPath, "WOOORLD!");
            File.WriteAllText(fullPath, "hi, world!");

            string[] words = new string[] { "aaa", "bbb", "ccc" };
            File.WriteAllLines(fullPath, words);
            File.AppendAllLines(fullPath, words);
            File.AppendAllText(fullPath, "slovo");

            string content = File.ReadAllText(fullPath);
            string[] lines = File.ReadAllLines(fullPath);

            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }

            string folderPath2 = Path.Combine(folderPath, "ExampleFolder");
            string filePath = Path.Combine(folderPath2, "anotherExampleFolder.txt");

            if(!Directory.Exists(folderPath2))
            {
                Directory.CreateDirectory(folderPath2);
            }

            string ext = "txt";

            if(!Directory.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            else
            {
                File.WriteAllText(filePath, "");
            }
            
       
            //знаем только полный путь
            string FolderPath3 = Path.GetDirectoryName(filePath);
            string fileName2 = Path.GetFileName(filePath);

            Console.WriteLine(fileName2);
            Console.WriteLine(FolderPath3);

            
        }
    }
}
