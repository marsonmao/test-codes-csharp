using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2012_csharp
{

static class ExtentionMethods
{
    //static public void MyReset<T>(this T col)
    //{
    //    Console.WriteLine("Not a collection! / " + col.GetType() + " / " + typeof(T));
    //}

    //static public void MyReset<T, U>(this T col) where T : ICollection<U>
    //{
    //    col.Clear();
    //    Console.WriteLine("Cleared! / " + col.GetType() + " / " + typeof(T));
    //}

    static public void MyReset(this object col)
    {
        Console.WriteLine("Not a collection!");// / " + col.GetType() + " / " + typeof(T));
    }

    static public void MyReset<T>(this ICollection<T> col)
    {
        col.Clear();
        Console.WriteLine("Cleared!");// / " + col.GetType() + " / " + typeof(T));
    }

    static public void MyReset<T>(this IEquatable<T> col)
    {
        bool result = col.Equals(null);
        Console.WriteLine("Equatable {0}!", result);
    }
}

}
