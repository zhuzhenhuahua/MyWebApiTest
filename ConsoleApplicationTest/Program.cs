﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //webService调用
            WebReference.ServiceDemo demo = new WebReference.ServiceDemo();
           string str= demo.GetStr("s");
            Console.Read();
        }
    }
    public class Test
    {
     
    }
}
