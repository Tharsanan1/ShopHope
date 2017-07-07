using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHope
{
    class SalesMan : Employee
    {
        private SalesMan(string name, string id, string mail, long phoneNo, int age, string password) : base(name, id, mail, phoneNo, age, password,"SalesMan")
        {
            
        }

        public static void getEmployee(string name, string id, string mail, long phoneNo, int age, string password, string post)
        {
            if (post.Equals("SalesMan"))
            {
                new SalesMan(name, id, mail, phoneNo, age, password);
            }
        }
    }
}
