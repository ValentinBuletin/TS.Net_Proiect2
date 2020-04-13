using ClassLibrary1.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassLibrary1
{
    /// <summary>The class that contains all the methods used to acces the database.</summary>
    public class Class1
    {
        /// <summary>Adds a new column to the table.</summary>
        /// <param name="column">The name of the column to be added.</param>
        public static void Add(string column)
        {
            using (Model1Container context = new Model1Container())
            {
                var ind = context.Tags;

                Tag tmp_tag = new Tag()
                {
                    Tag_Name = column
                };
                context.Tags.Add(tmp_tag);

                var items = context.Pics_Vids;

                foreach (var x in items)
                {
                    x.Values += "null,";
                }
                context.SaveChanges();
            }
        }

        /// <summary>Removes a column from the table.</summary>
        /// <param name="column">The name of the column to be removed.</param>
        public static void Remove(string column)
        {
            using (Model1Container context = new Model1Container())
            {
                var items = context.Pics_Vids;

                foreach (var x in items)
                {
                    var ind = context.Tags;
                    var values = x.Values.Split(',');

                    int index = 0;
                    Dictionary<int, int> dict = new Dictionary<int, int>();
                    foreach (var y in ind)
                    {
                        dict.Add(y.Tag_Id, index);
                        index += 1;
                    }

                    foreach (var y in ind)
                    {
                        if (column.Equals(y.Tag_Name))
                        {
                            values[dict[y.Tag_Id]] = "TEST!!!PHRASE";
                            var sir = "";
                            foreach (var b in values)
                            {
                                if (!b.Equals("TEST!!!PHRASE"))
                                    sir += b + ",";
                            }
                            x.Values = sir;
                            context.Tags.Remove(y);
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        /// <summary>Deletes all the rows that match the search.</summary>
        /// <param name="where">The name of the column to be searched.</param>
        /// <param name="whereValue">The value that is to be searched.</param>
        public static void Delete(string where, string whereValue)
        {
            int row = 0;
            using (Model1Container context = new Model1Container())
            {
                var items = context.Pics_Vids;

                foreach (var x in items)
                {
                    if (where.Equals("Full_Path") && x.Full_Path == whereValue)
                    {
                        context.Pics_Vids.Remove(x);
                        row++;
                    }

                    var ind = context.Tags;
                    var values = x.Values.Split(',');

                    int index = 0;
                    Dictionary<int, int> dict = new Dictionary<int, int>();
                    foreach (var y in ind)
                    {
                        dict.Add(y.Tag_Id, index);
                        index += 1;
                    }

                    foreach (var y in ind)
                    {
                        if (where.Equals(y.Tag_Name) && whereValue == values[dict[y.Tag_Id]])
                        {
                            context.Pics_Vids.Remove(x);
                            row++;
                        }
                    }
                }
                context.SaveChanges();
            }
            if (row == 0)
                MessageBox.Show("No row deleted");
        }

        /// <summary>Retrieves all the values from a column.</summary>
        /// <param name="check">The name of the column.</param>
        /// <returns>A List of strings with the values.</returns>
        public static List<string> Check(string check, bool silent)
        {
            List<string> query = new List<string>();
            using (Model1Container context = new Model1Container())
            {
                var items = context.Pics_Vids;

                foreach (var x in items)
                {
                    if (check.Equals("Full_Path"))
                    {
                        query.Add(x.Full_Path);
                    }
                }
            }
            if (query.Count == 0)
                query.Add("NOTHING WAS FOUND!");
            else
            {
                if (silent == false)
                {
                    MessageBox.Show("Found: " + query.Count() + " picures/videos!");
                }
            }
            return query;
        }

        #region Modify
        /// <summary>Modifies all the values that match a search.</summary>
        /// <param name="set">The name of the column to be modified.</param>
        /// <param name="setValue">The value to be modified.</param>
        /// <param name="where">The name of the column to be searched.</param>
        /// <param name="whereValue">The value to be searched.</param>
        public static void Modify(string set, string setValue, string where, string whereValue)
        {
            using (Model1Container context = new Model1Container())
            {
                var items = context.Pics_Vids;
                try
                {
                    foreach (var x in items)
                    {
                        if (where.Equals("Name") && x.Name == whereValue)
                        {
                            if (set.Equals("Name"))
                                x.Name = setValue;
                            else if (set.Equals("Full_Path"))
                                x.Full_Path = setValue;
                            x.Date_Modified = DateTime.Now;

                            var ind2 = context.Tags;
                            var values2 = x.Values.Split(',');

                            int index = 0;
                            Dictionary<int, int> dict = new Dictionary<int, int>();
                            foreach (var a in ind2)
                            {
                                dict.Add(a.Tag_Id, index);
                                index += 1;
                            }

                            foreach (var a in ind2)
                            {
                                if (set.Equals(a.Tag_Name))
                                {
                                    values2[dict[a.Tag_Id]] = setValue;
                                    var sir = "";
                                    foreach (var b in values2)
                                    {
                                        sir += b + ",";
                                    }
                                    x.Values = sir;
                                }
                            }
                        }
                        else if (where.Equals("Full_Path") && x.Full_Path == whereValue)
                        {
                            if (set.Equals("Name"))
                                x.Name = setValue;
                            else if (set.Equals("Full_Path"))
                                x.Full_Path = setValue;
                            x.Date_Modified = DateTime.Now;

                            var ind2 = context.Tags;
                            var values2 = x.Values.Split(',');

                            int index = 0;
                            Dictionary<int, int> dict = new Dictionary<int, int>();
                            foreach (var a in ind2)
                            {
                                dict.Add(a.Tag_Id, index);
                                index += 1;
                            }

                            foreach (var a in ind2)
                            {
                                if (set.Equals(a.Tag_Name))
                                {
                                    values2[dict[a.Tag_Id]] = setValue;
                                    var sir = "";
                                    foreach (var b in values2)
                                    {
                                        sir += b + ",";
                                    }
                                    x.Values = sir;
                                }
                            }
                        }
                        else if (where.Equals("Type") && x.Type == whereValue)
                        {
                            if (set.Equals("Name"))
                                x.Name = setValue;
                            else if (set.Equals("Full_Path"))
                                x.Full_Path = setValue;
                            x.Date_Modified = DateTime.Now;

                            var ind2 = context.Tags;
                            var values2 = x.Values.Split(',');

                            int index = 0;
                            Dictionary<int, int> dict = new Dictionary<int, int>();
                            foreach (var a in ind2)
                            {
                                dict.Add(a.Tag_Id, index);
                                index += 1;
                            }

                            foreach (var a in ind2)
                            {
                                if (set.Equals(a.Tag_Name))
                                {
                                    values2[dict[a.Tag_Id]] = setValue;
                                    var sir = "";
                                    foreach (var b in values2)
                                    {
                                        sir += b + ",";
                                    }
                                    x.Values = sir;
                                }
                            }
                        }
                        else if (where.Equals("Size") && x.Size.ToString() == whereValue)
                        {
                            if (set.Equals("Name"))
                                x.Name = setValue;
                            else if (set.Equals("Full_Path"))
                                x.Full_Path = setValue;
                            x.Date_Modified = DateTime.Now;

                            var ind2 = context.Tags;
                            var values2 = x.Values.Split(',');

                            int index = 0;
                            Dictionary<int, int> dict = new Dictionary<int, int>();
                            foreach (var a in ind2)
                            {
                                dict.Add(a.Tag_Id, index);
                                index += 1;
                            }

                            foreach (var a in ind2)
                            {
                                if (set.Equals(a.Tag_Name))
                                {
                                    values2[dict[a.Tag_Id]] = setValue;
                                    var sir = "";
                                    foreach (var b in values2)
                                    {
                                        sir += b + ",";
                                    }
                                    x.Values = sir;
                                }
                            }
                        }
                        else if (where.Equals("Date_Created") && x.Date_Created.ToString() == whereValue)
                        {
                            if (set.Equals("Name"))
                                x.Name = setValue;
                            else if (set.Equals("Full_Path"))
                                x.Full_Path = setValue;
                            x.Date_Modified = DateTime.Now;

                            var ind2 = context.Tags;
                            var values2 = x.Values.Split(',');

                            int index = 0;
                            Dictionary<int, int> dict = new Dictionary<int, int>();
                            foreach (var a in ind2)
                            {
                                dict.Add(a.Tag_Id, index);
                                index += 1;
                            }

                            foreach (var a in ind2)
                            {
                                if (set.Equals(a.Tag_Name))
                                {
                                    values2[dict[a.Tag_Id]] = setValue;
                                    var sir = "";
                                    foreach (var b in values2)
                                    {
                                        sir += b + ",";
                                    }
                                    x.Values = sir;
                                }
                            }
                        }
                        else if (where.Equals("Date_Modified") && x.Date_Modified.ToString() == whereValue)
                        {
                            if (set.Equals("Name"))
                                x.Name = setValue;
                            else if (set.Equals("Full_Path"))
                                x.Full_Path = setValue;
                            x.Date_Modified = DateTime.Now;

                            var ind2 = context.Tags;
                            var values2 = x.Values.Split(',');

                            int index = 0;
                            Dictionary<int, int> dict = new Dictionary<int, int>();
                            foreach (var a in ind2)
                            {
                                dict.Add(a.Tag_Id, index);
                                index += 1;
                            }

                            foreach (var a in ind2)
                            {
                                if (set.Equals(a.Tag_Name))
                                {
                                    values2[dict[a.Tag_Id]] = setValue;
                                    var sir = "";
                                    foreach (var b in values2)
                                    {
                                        sir += b + ",";
                                    }
                                    x.Values = sir;
                                }
                            }
                        }

                        var ind = context.Tags;
                        var values = x.Values.Split(',');

                        int index2 = 0;
                        Dictionary<int, int> dict2 = new Dictionary<int, int>();
                        foreach (var y in ind)
                        {
                            dict2.Add(y.Tag_Id, index2);
                            index2 += 1;
                        }

                        foreach (var y in ind)
                        {
                            if(where.Equals(y.Tag_Name) && values[dict2[y.Tag_Id]].ToString() == whereValue)
                            {
                                if (set.Equals("Name"))
                                    x.Name = setValue;
                                else if (set.Equals("Full_Path"))
                                    x.Full_Path = setValue;
                                x.Date_Modified = DateTime.Now;

                                var ind2 = context.Tags;
                                var values2 = x.Values.Split(',');

                                int index = 0;
                                Dictionary<int, int> dict = new Dictionary<int, int>();
                                foreach (var a in ind2)
                                {
                                    dict.Add(a.Tag_Id, index);
                                    index += 1;
                                }

                                foreach (var a in ind2)
                                {
                                    if (set.Equals(a.Tag_Name))
                                    {
                                        values2[dict[a.Tag_Id]] = setValue;
                                        var sir = "";
                                        foreach (var b in values2)
                                        {
                                            sir += b + ",";
                                        }
                                        Console.WriteLine(sir);
                                        x.Values = sir;
                                    }
                                }
                            }
                                
                        }
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR Modify:" + ex.Message);
                }
                
            }
        }
        #endregion

        #region SearchPath
        /// <summary>Searches for the "Full_Path" after a criteria.</summary>
        /// <param name="column">The name of the column to be seached.</param>
        /// <param name="text">The value to be searched.</param>
        /// <returns>A List of strings with the paths of the pictures/videos found.</returns>
        public static List<string> SearchPath(string column, string text)
        {
            List<string> query = new List<string>();
            using (Model1Container context = new Model1Container())
            {
                var items = context.Pics_Vids;

                foreach (var x in items)
                {
                    if (column.Equals("Name") && x.Name == text)
                    {
                        query.Add(x.Full_Path);
                    }
                    else if (column.Equals("Full_Path") && x.Full_Path == text)
                    {
                        query.Add(x.Full_Path);
                    }
                    else if (column.Equals("Type") && x.Type == text)
                    {
                        query.Add(x.Full_Path);
                    }
                    else if (column.Equals("Size") && x.Size.ToString() == text)
                    {
                        query.Add(x.Full_Path);
                    }
                    else if (column.Equals("Date_Created") && x.Date_Created.ToString() == text)
                    {
                        query.Add(x.Full_Path);
                    }
                    else if (column.Equals("Date_Modified") && x.Date_Modified.ToString() == text)
                    {
                        query.Add(x.Full_Path);
                    }

                    var ind = context.Tags;
                    var values = x.Values.Split(',');

                    int index = 0;
                    Dictionary<int, int> dict = new Dictionary<int, int>();
                    foreach (var y in ind)
                    {
                        dict.Add(y.Tag_Id, index);
                        index += 1;
                    }

                    foreach ( var y in ind)
                    {                        
                        if (column == y.Tag_Name)
                        {
                            if(text == values[dict[y.Tag_Id]])
                            {
                                query.Add(x.Full_Path);
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Found: " + query.Count() + " picures/videos!");
            if (query.Count == 0)
                query.Add("NOTHING WAS FOUND!");
            return query;
        }
        #endregion

        /// <summary>Searches after a criteria.</summary>
        /// <param name="column">The name of the column to be seached.</param>
        /// <param name="text">The value to be searched.</param>
        /// <returns>A List of strings with the all the values of the pictures/videos found.</returns>
        public static List<string> Search(string column, string text)
        {
            List<string> query = new List<string>();
            using (Model1Container context = new Model1Container())
            {
                var items = context.Pics_Vids;
                
                foreach (var x in items)
                {
                    if (x.Full_Path == text)
                    {
                        query.Add("Name" + ":\t" + x.Name.ToString());
                        query.Add("Full_Path" + ":\t" + x.Full_Path.ToString());
                        query.Add("Type" + ":\t" + x.Type.ToString());
                        query.Add("Size" + ":\t" + x.Size.ToString() + " MB");
                        query.Add("Date_Created" + ":\t" + x.Date_Created.ToString());
                        query.Add("Date_Modified" + ":\t" + x.Date_Modified.ToString());

                        var ind = context.Tags;
                        var values = x.Values.Split(',');

                        int index = 0;
                        foreach (var y in ind)
                        {
                            var sir = y.Tag_Name.ToString() + ":\t";
                            sir += values[index];
                            index += 1;
                            query.Add(sir);
                        }
                        query.Add(Environment.NewLine);
                    }
                }
            }
            if (query.Count == 0)
                query.Add("NOTHING WAS FOUND!");
            return query;
        }

        /// <summary>Searches for the columns of the table "Pics_Vids and Tags".</summary>
        /// <returns>A List of strings with names of the columns.</returns>
        public static List<string> Columns()
        {
            List<string> tmp = new List<string>();
            using (Model1Container context = new Model1Container())
            {
                tmp = typeof(Pic_Vid).GetProperties().Select(property => property.Name).ToList<string>();
                var items = context.Tags;
                foreach(var x in items)
                {
                    tmp.Add(x.Tag_Name);
                }
            }
            return tmp;
        }

        private static bool select(string fullPath)
        {
            bool found = false;
            using (Model1Container context = new Model1Container())
            {
                var all = context.Pics_Vids;
                foreach (var x in all)
                {
                    if (fullPath == x.Full_Path)
                    {
                        found = true;
                    }
                }
            }

            return found;
        }

        /// <summary>Inserts a new row (if it doesn't exist) in the table.</summary>
        /// <param name="name">The name of the picture/video.</param>
        /// <param name="fullPath">The full path of the picture/video.</param>
        /// <param name="type">The type of extension.</param>
        /// <param name="dateCreated">The date it has been created.</param>
        public static bool Insert(string name, string fullPath, string type, double size, DateTime dateCreated)
        {

            if (select(fullPath) == false)
            {
                using (Model1Container context = new Model1Container())
                {
                    string values = "";
                    int index = context.Tags.ToList<Tag>().Count();
                    

                    for (int i = 0; i<=index;i++)
                    {
                        values += "null,";
                    }

                    Pic_Vid poza_video = new Pic_Vid()
                    {
                        Name = name,
                        Full_Path = fullPath,
                        Type = type,
                        Size = size,
                        Date_Created = dateCreated,
                        Date_Modified = DateTime.Now,
                        Values = values
                    };
                    context.Pics_Vids.Add(poza_video);
                    context.SaveChanges();
                }
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
