using ConsoleApp1.CodeDomTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //CodeComplier.Test();
            CodeDom_try.Test();

            //Application.Run(new Form());

            //new System.Windows.Forms.Form().ShowDialog();
            Console.WriteLine("end.......................................");
            Console.ReadKey();

        }
    }
}
