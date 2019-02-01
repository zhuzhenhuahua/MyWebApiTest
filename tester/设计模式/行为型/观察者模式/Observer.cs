using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tester.设计模式.行为型.观察者模式
{
    public abstract class Observer
    {
        protected Subject subject;
        public abstract void update();
    }
    public class BinaryObserver : Observer
    {
        public BinaryObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }
        public override void update()
        {
            var res = Convert.ToString(this.subject.getState(), 2);
        }
    }

    public class OctalObserver : Observer
    {
        public OctalObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }
        public override void update()
        {
            var res = Convert.ToInt32(this.subject.getState().ToString(), 8);
        }
    }
}
