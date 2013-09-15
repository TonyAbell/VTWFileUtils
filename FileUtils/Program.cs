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
            var prefixpattern = ConfigurationManager.AppSettings["prefixpattern"];
            var sufixpattern = ConfigurationManager.AppSettings["sufixpattern"];
            var numberpattern = ConfigurationManager.AppSettings["numberpattern"];
            var fileStartWith = ConfigurationManager.AppSettings["fileStartWith"];
            var bookIndex = ConfigurationManager.AppSettings["bookIndex"];
         
            if (Directory.Exists(dir))
            {
                var files = Directory.EnumerateFiles(dir)
                                     .Select(s=> Tuple.Create(Path.GetFileName(s),s))
                                     .Where(w => !w.Item1.Trim().StartsWith(prefixpattern))
                                     .Where(w => w.Item1.Trim().StartsWith(fileStartWith))
                                     .Select(s => 
                                     {                                       
                                         var fileIndex = s.Item1.Substring(fileStartWith.Length).TrimEnd(Path.GetExtension(s.Item2).ToCharArray());
                                         var i = int.Parse(fileIndex);
                                         return Tuple.Create<int, string>(i, s.Item2);
                                     })
                                     .OrderBy(o => o.Item1)                                
                                     .ToList();

                foreach (var item in files)
                {

                    var fileName = Path.GetFileName(item);
                    var fileIndex = int.Parse(fileName.Substring(fileStartWith.Length).TrimEnd(Path.GetExtension(item).ToCharArray()));
                    var end = string.Format(numberpattern, fileIndex);
                  
                    var newFileName = string.Format("{0}_{1}_{2}_{3}",
                                                    prefixpattern,
                                                    bookIndex, 
                                                    sufixpattern, 
                                                    end);
                    var filePath = Path.GetDirectoryName(item);
                    
                    var newFileNameWithPath = filePath + "\\" + newFileName;
                    File.Move(item, newFileNameWithPath);

                }


            }

            Console.WriteLine("Enter to exit");
            Console.ReadLine();

        }
    }
}
