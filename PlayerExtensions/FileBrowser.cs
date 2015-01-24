using System;
using System.IO;
using System.Text;

namespace Mpdn.PlayerExtensions
{
    public static class FileBrowser
    {
        public static string LoadFiles(string currentDirectory)
        {
            var directories = Directory.EnumerateDirectories(currentDirectory);
            var files = Directory.EnumerateFiles(currentDirectory);

            StringBuilder sb = new StringBuilder();

            var parentDir = Directory.GetParent(currentDirectory);
            if (!String.IsNullOrEmpty(parentDir.FullName))
                sb.Append("p:UP");

            int counter = 1;
            foreach (var dir in directories)
            {
                if (counter > 1)
                    sb.Append(">>");
                sb.Append("d:" + dir);
                counter++;
            }

            counter = 1;
            foreach (var file in files)
            {
                if (counter > 1)
                    sb.Append(">>");
                sb.Append("f:" + file);
                counter++;
            }

            return sb.ToString();
        }
    }
}