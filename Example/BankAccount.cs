using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example
{
    public class BankAccount
    {
        public object locked = new object();
        public int Balance { get; set; }

        public void Deposit(int amount)
        {
            lock (locked)
            {
                Balance += amount;
            }
        }

        public void Draw(int amount)
        {
            lock (locked)
            {
                Balance -= amount;
            }
        }


    }
}
