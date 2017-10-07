using System;
using System.IO;
using System.Linq;
using System.Reflection;
using MyNUnitAnnotations;

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("There is no path to assembly in arguments.");
                return;
            }
            try
            {
                foreach (var a in AssemblyHelper.GetAssemblies(args[0]))
                {
                    var test = new MyNUnit();
                    a.ExportedTypes.ToList().ForEach(t => test.RunTests(t, Console.WriteLine));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}