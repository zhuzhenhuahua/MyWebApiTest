using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tester.设计模式.行为型.策略模式
{
    /// <summary>
    /// 策略接口，使具体类抽象化
    /// </summary>
    public interface IStrategy
    {
        int doOperation(int num1, int num2);
    }
}
