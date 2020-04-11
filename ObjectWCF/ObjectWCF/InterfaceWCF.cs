using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ObjectWCF
{
    [ServiceContract]
    interface INterface1
    {
        [OperationContract]
        void Add(string column);
        [OperationContract]
        void Remove(string column);
        [OperationContract]
        void Delete(string where, string whereValue);
        [OperationContract]
        List<string> Check(string check, bool silent);
        [OperationContract]
        void Modify(string set, string setValue, string where, string whereValue);
        [OperationContract]
        List<string> SearchPath(string column, string text);
        [OperationContract]
        List<string> Search(string column, string text);
        [OperationContract]
        List<string> Columns();
        [OperationContract]
        bool Insert(string name, string fullPath, string type, double size, DateTime dateCreated);
    }
}
