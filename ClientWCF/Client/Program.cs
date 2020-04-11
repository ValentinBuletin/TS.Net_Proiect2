/// <summary>
/// Nume si prenume: Braneanu Valentin
/// Laborator: Marti 12-14
/// Tema laborator: Proiectul 1
/// Data predare proiect: 14.03.2020
/// Declaratie: Declar pe propria raspundere ca acest proiect nu a fost
/// copiat din alte surse
/// Bibliografie, surse de inspiratie: https://stackoverflow.com/questions/12142806/how-to-insert-records-in-database-using-c-sharp-language,
/// https://docs.microsoft.com/en-us/dotnet/csharp, https://www.sqlservertutorial.net/
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>The main program.</summary>
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
            Application.Run(new Form1());
        }
    }
}
