using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = ConfigurationManager.AppSettings["dir"];
             var prfixpattern = ConfigurationManager.AppSettings["prfixpattern"];

            if (Directory.Exists(dir))
            {
                var files = Directory.EnumerateFiles(dir);
                foreach (var item in files)
                {

                    var fileName = Path.GetFileName(item);
                    if (!fileName.Trim().StartsWith(prfixpattern))
                    {
                        var newFileName = prfixpattern + fileName;
                        var filePath = Path.GetDirectoryName(item);
                        var newFileNameWithPath = filePath + "\\" + newFileName;
                        File.Move(item, newFileNameWithPath);
                    }                                  
                }
            }
            
            Console.WriteLine("Enter to exit");
           Console.ReadLine();

        }
    }
}
