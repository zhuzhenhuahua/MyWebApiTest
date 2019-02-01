using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tester.设计模式.行为型.观察者模式
{
   public class Subject
    {
        public List<Observer> observers = new List<Observer>();
        private int _state;
        public int getState()
        {
            return _state;
        }
        public void setState(int state)
        {
            this._state = state;
            notifyAllObservers();
        }
        public void attach(Observer observer)
        {
            observers.Add(observer);
        }
        public void notifyAllObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.update();
            }
        }
    }
}
