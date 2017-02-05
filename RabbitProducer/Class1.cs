using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace RabbitProducer
{
   public static class Class1
    {
        public static Func<T1, Func<T2, Func<T3, TResult>>>
         CurryMethodFunc<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> function)
        {
            return a => b => c => function(a, b, c);
        }
    }

    public class Stuff
    {
        public int MathStuff(int x, int y, int z)
        {
            return x * y + z;
        }
        public double SlightlyDiffMathStuff(double x, double y, double z)
        {
            return x * y + z;
        }
        public void Dostuff()
        {
            Func<double, double, double, double> asAFunc = SlightlyDiffMathStuff;
            var newAwesomeCurrying = asAFunc.CurryMethodFunc();
            double answer = newAwesomeCurrying(3)(4)(12);

            Func<int, int, int, int> mathFunc = MathStuff;
            var mathWithCurrying = mathFunc.CurryMethodFunc();
            int another = mathWithCurrying(3)(4)(12);
        }
    }

}
