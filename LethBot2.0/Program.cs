using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethBot2._0
{
    class Program
    {
        static void Main(string[] args)
        {
            DbConnect db = new DbConnect();
            db.Insert("Sean", "Connery");

            //AsyncAwaitTestClass testClass = new AsyncAwaitTestClass();
            //Task task = new Task(testClass.ProcessDataAsync);
            //task.Start();
            //task.Wait();

            //MyBot bot = new MyBot();
        }
    }
}
