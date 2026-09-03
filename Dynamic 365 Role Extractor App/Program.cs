using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dynamic_365_Role_Extractor_App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
    public class ConsoleWriter : System.IO.TextWriter
    {
        System.Windows.Forms.ListView lst;
        public ConsoleWriter(System.Windows.Forms.ListView lst) {
            this.lst = lst;
        }
        public override Encoding Encoding =>  System.Text.Encoding.UTF8;
        public override void WriteLine(string value)
        {
            lst.BeginInvoke((Action)(() =>
                        {
                            lst.Items.Add(value.ToString());
                        }
            ));
        }
    }
}
