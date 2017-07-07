using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHope
{
    class Labour : Employee
    {
        private Labour(string name, string id, string mail, long phoneNo, int age, string password) : base(name, id, mail, phoneNo, age,password,"Labour")
        {
        }

        public static void getEmployee(string name, string id, string mail, long phoneNo, int age, string password, string post)
        {
            if(post.Equals("Labour")) {
                new Labour(name,id,mail,phoneNo,age,password);
            }
        }
    }
}
