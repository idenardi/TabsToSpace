using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TabToSpaces
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = args[0];
            var filePaths = Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories);

            int updated = 0;
            foreach (var path in filePaths)
            {
                var content = File.ReadAllLines(path);
                string[] newfile = new string[content.Length];

                int i = 0;
                var bChanged = false;

                foreach (var line in content)
                {
                    newfile[i] = ReplaceTabs(line, out var bChangedLine);
                    i++;

                    if (bChangedLine)
                        bChanged = true;
                }

                if (bChanged)
                {
                    ++updated;
                    Console.WriteLine("fixing " + path);
                    File.WriteAllLines(path, newfile, System.Text.Encoding.UTF8);
                }
            }

            Console.WriteLine("fixed {0} files", updated);
            var x = Console.ReadKey();
        }


        public static string ReplaceTabs(string line, out bool bChanged)
        {
            bChanged = false;
            var i = 0;
            var lstIndex = new List<int>();
            
            foreach(char c in line)
            {
                if (c == ' ')
                    continue;
                else if (c == '\t')
                    lstIndex.Add(i);
                else
                    break;

                i++;
            }

            var lstIndexOrdered = lstIndex.OrderByDescending(c => c);
            foreach (var index in lstIndexOrdered)
            {
                line = line.Remove(index, 1).Insert(index, "    ");
                bChanged = true;
            }

            return line;
        }
    }
}
