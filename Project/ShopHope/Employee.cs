using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHope
{
    abstract class Employee
    {
        string name;
        string id;
        string mail;
        string post;
        long phoneNo;
        int age;
        public Employee(string name, string id, string mail, long phoneNo, int age , string password , string post) {
            this.name = name;
            this.id = id;
            this.mail = mail;
            this.phoneNo = phoneNo;
            this.age = age;
            this.post = post;
            
            Connection.performConnection("INSERT INTO shophope.employeetable(userName,passWord,age,nIC,emailAdress,phoneNum,post) VALUES('" + name + "','" + password + "','" + age + "','" + id + "','" + mail + "','" + phoneNo + "','"+post+"');");

        }

        public Employee(string name, string id, string mail, long phoneNo, int age)
        {
            this.name = name;
            this.id = id;
            this.mail = mail;
            this.phoneNo = phoneNo;
            this.age = age;
        }
    }
}
