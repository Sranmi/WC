using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 四则运算
{
   public class Number
    {
        public int Son
        {
            get;
            set;
        }
        public int Mother
        {
            get;
            set;
        }

        public char Symbol;
        public Number (int son,int mother,char symbol)
        {
            this.Son = son;
            this.Mother = mother;
            this.Symbol = symbol;
        }
    }
}
