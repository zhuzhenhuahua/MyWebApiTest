using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tester.设计模式.行为型.策略模式
{
    /// <summary>
    /// 策略角色类，将各个算法实现同一个接口，具体实现交给客户端。（给哪个实例，执行哪个方法。）
    /// </summary>
   public class StrategyContext
    {
        private IStrategy _strategy;
        private StrategyContext()
        {

        }
        public StrategyContext(IStrategy strategy)
        {
            this._strategy = strategy;
        }
        public int executeStrategy(int num1, int num2)
        {
           return this._strategy.doOperation(num1, num2);
        }
    }
}
