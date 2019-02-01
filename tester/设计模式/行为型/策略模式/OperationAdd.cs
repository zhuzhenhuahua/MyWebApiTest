using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tester.设计模式.行为型.策略模式
{
    public class OperationAdd : IStrategy
    {
        public int doOperation(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
