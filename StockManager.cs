using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHope
{
    class StockManager : Employee
    {
        private StockManager(string name, string id, string mail, long phoneNo, int age, string password) : base(name, id, mail, phoneNo, age, password,"StockManager")
        {
        }

        public static void getEmployee(string name, string id, string mail, long phoneNo, int age, string password, string post)
        {
            if(post.Equals("StockManager")) {
                new StockManager(name,id,mail,phoneNo,age,password);
            }
        }
    }
}
