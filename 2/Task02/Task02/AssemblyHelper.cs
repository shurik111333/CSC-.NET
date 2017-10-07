using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task02
{
    public static class AssemblyHelper
    {
        public static IEnumerable<Assembly> GetAssemblies(string path)
        {
            var dir = new DirectoryInfo(path);
            foreach (var file in dir.GetFiles("*.dll"))
            {
                Assembly a = null;
                try
                {
                    a = Assembly.LoadFrom(file.FullName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (a != null)
                {
                    yield return a;
                }
            }
        }
    }
}
