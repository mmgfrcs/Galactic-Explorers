using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalacticExplorers.Utilities
{
    public enum ConditionOperation
    {
        Equals, NotEquals, Greater, Less, GreaterEqual, LessEqual
    }
    public abstract class Condition<T>
    {
        protected T obj;
        protected ConditionOperation operation;


        public abstract bool IsConditionFullfilled();
    }
}
