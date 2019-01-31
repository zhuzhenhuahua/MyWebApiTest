using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tester.重构案例.SwitchToStrategy.After
{
    public class ClientCodeAfter
    {
        public decimal GetPrice(State state)
        {
            ShippingInfo shippingInfo = new ShippingInfo();
            return shippingInfo.CalculateShippingAmount(state);
        }
    }
    public enum State
    {
        Alaska, NewYork, Florida
    }
    public class ShippingInfo
    {
        private Dictionary<State, IShippingCalculation> ShippingCalculations { get; set; }
        public ShippingInfo()
        {
            ShippingCalculations = new Dictionary<State, IShippingCalculation>()
            {
                { State.Alaska,new AlaskShippingCalculation()},
                { State.Florida,new FloridaShippingCalculation()},
                { State.NewYork,new NewYorkShippingCalculation()}
            };
        }
        public decimal CalculateShippingAmount(State state)
        {
            return ShippingCalculations[state].Calculate();
        }
    }
    public interface IShippingCalculation
    {
        decimal Calculate();
    }
    public class AlaskShippingCalculation : IShippingCalculation
    {
        public decimal Calculate()
        {
            return 15;
        }
    }
    public class NewYorkShippingCalculation : IShippingCalculation
    {
        public decimal Calculate()
        {
            return 10;
        }
    }
    public class FloridaShippingCalculation : IShippingCalculation
    {
        public decimal Calculate()
        {
            return 3;
        }
    }
}
