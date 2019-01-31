using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zzh.Model.DB.NewFolder1
{
    #region Refactoring Day1
    /*
     * Refactoring Day1
     *向类的使用者隐藏类中的完整集合是一个很好的做法，比如对集合的add/remove操作中包含了其他的相关逻辑时（每次增加、移除时要改变_orderTotal的值）
     *因此，可以迭代但不直接在集合上进行操作的方式来暴露集合，是个不错的注意
     */
    public class Order
    {
        private int _orderTotal;
        private List<OrderLine> _orderLines;
        public IEnumerable<OrderLine> OrderLines
        {
            get { return this._orderLines; }
        }
        public void AddOrderLine(OrderLine line)
        {
            _orderTotal += line.total;
            _orderLines.Add(line);
        }
        public void RemoveLine(OrderLine line)
        {
            var model = _orderLines.Find(p => p == line);
            if (model == null) return;
            _orderTotal -= line.total;
            _orderLines.Remove(model);
        }
    }
    public class OrderLine
    {
        public int total { get; set; }
    }
    #endregion

    #region Refactoring Day8
    public class Sanition
    {
        public string WashHands()
        {
            return "Cleaned";
        }
    }
    public class Child : Sanition
    {
    }
    /*在该实例中，Child并不是Sanition，因此这样的继承层次是毫无意义的，我们可以这样重构，在Child的构造函数中实现一个Sanition实例，
    *并将方法的调用委托给这个实例。继承只能用于严格的继承场景，并不是用来快速编写代码的工具
    */
    public class Sanition2
    {
        public string WashHands()
        {
            return "Cleaned";
        }
    }
    public class Child2
    {
        private Sanition2 sanition { get; set; }
        public Child2()
        {
            sanition = new Sanition2();
        }
        public string WashHands()
        {
            return sanition.WashHands();
        }
    }
    #endregion
}
