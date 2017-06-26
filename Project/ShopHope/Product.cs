using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHope
{
    public class Product
    {
        int  quantity, warningLevel;
        string name, catagory, brand;
        double price, weight;
        bool warningLevelReached;
        public Product(int quantity, int warningLevel, string name, string catagory, string brand, double price, double weight) {
            //this.id = id;
            this.quantity = quantity;
            this.warningLevel = warningLevel;
            this.name = name;
            this.catagory = catagory;
            this.brand = brand;
            this.price = price;
            this.weight = weight;
            this.warningLevelReached = false;
            Connection.performConnection("INSERT INTO shophope.stocks(quantity,warningLevel,name,catagory,brand,price,weight) VALUES('"+quantity+"','"+warningLevel+"','"+name+"','"+catagory+"','"+brand+"','"+price+"','"+weight+"');");
        }
    }
}
