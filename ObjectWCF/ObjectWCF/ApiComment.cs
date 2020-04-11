using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ObjectWCF
{
    public class ApiComment : INterface1
    {
        void INterface1.Add(string column)
        {
            Class1.Add(column);
        }
        void INterface1.Remove(string column)
        {
            Class1.Remove(column);
        }
        void INterface1.Delete(string where, string whereValue)
        {
            Class1.Delete(where, whereValue);
        }
        List<string> INterface1.Check(string check, bool silent)
        {
            return Class1.Check(check, silent);
        }
        void INterface1.Modify(string set, string setValue, string where, string whereValue)
        {
            Class1.Modify(set, setValue, where, whereValue);
        }
        List<string> INterface1.SearchPath(string column, string text)
        {
            return Class1.SearchPath(column, text);
        }
        List<string> INterface1.Search(string column, string text)
        {
            return Class1.Search(column, text);
        }
        List<string> INterface1.Columns()
        {
            return Class1.Columns();
        }
        bool INterface1.Insert(string name, string fullPath, string type, double size, DateTime dateCreated)
        {
            return Class1.Insert(name, fullPath, type, size, dateCreated);
        }
    }
}
