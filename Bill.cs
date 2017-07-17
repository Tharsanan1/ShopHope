using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHope
{
    class Bill
    {
        static List<Bill> billList = new List<Bill>();
        List<string> idList;
        List<string> quantityList;
        List<string> priceList;
        static int count = 0;
        double total;
        public Bill(List<string> idList, List<string> quantityList, List<string> priceList, double total) {
            this.idList = idList;
            this.priceList = priceList;
            Console.WriteLine("prie list in bill count quantity at constructor ==== " + quantityList.Count);
            this.quantityList = quantityList;
            Console.WriteLine("prie list in bill count this.quantity at constructor ==== " + this.quantityList.Count);
            this.total = total;
            billList.Add(this);
            count++;
        }
        public static Bill getbill() {
            count--;
            if(count==-1) {
                count = billList.Count - 1;
            }
            return billList[count];
            
        }
        public static List<Bill> getBillList() {
            return billList;
        }
        public List<string> getIdlist() {
            return idList;
        }
        public List<string> getQuantityList() {
            return quantityList;
        }
        public List<string> getPriceList()
        {
            return priceList;
        }
        public double getTotal() {
            return total;
        }
    }
}
