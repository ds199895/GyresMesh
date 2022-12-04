using Flowing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Creator
{
    public class testConvert:IApp
    {
        public static void Main(String[] args)
        {
            IApp.main();
        }
        public override void SetUp()
        {
            var s = "8E04775D";
            Print(Convert.ToUInt32(s, 16));
        }

    }
}
