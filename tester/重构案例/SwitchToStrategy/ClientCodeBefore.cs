using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tester.重构案例.SwitchToStrategy.Before
{
    public class ClientCodeBefore
    {
        public decimal GetPrice(State state)
        {
            ShippingInfo shippingInfo = new ShippingInfo();
           return shippingInfo.CalculateShippingAmount(state);
        }
    }

    public class ShippingInfo
    {
        public decimal CalculateShippingAmount(State shipToState)
        {
            switch (shipToState)
            {
                case State.Alaska:
                    return GetAlaskaShippingAmount();
                case State.NewYork:
                    return GetNewYorkShippingAmount();
                case State.Florida:
                    return GetFloridaShippingAmount();
                default:
                    return 0;
            }
        }
        private decimal GetAlaskaShippingAmount()
        {
            return 15;
        }
        private decimal GetNewYorkShippingAmount()
        {
            return 10;
        }
        private decimal GetFloridaShippingAmount()
        {
            return 3;
        }
    }
    public enum State
    {
        Alaska, NewYork, Florida
    }
}

