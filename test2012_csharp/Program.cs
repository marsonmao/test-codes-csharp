using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DebugNamespace;
using SL = System.Linq;

namespace test2012_csharp
{
struct Color
{
    public static readonly Color WHITE = new Color(1, 1, 1);
    //public const Color WHITE2 = new Color(1, 1, 1);
    //public static readonly Color BLACK = null;//new Color(0, 0, 0);
    //public static readonly Color RED = null;//new Color(1, 0, 0);

    public static Color WHITE2 { get { return new Color(1, 1, 1); } }

    public float r;
    public float g;
    public float b;

    //public float GetR() { return r; }

    public Color(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    public void Init(Color? color = null)//Color.WHITE2)
    {
        Color c0 = color.GetValueOrDefault();

        color = Color.WHITE;
        //color = Color.WHITE2;
        color = color ?? Color.WHITE;
        //Color c = (Color)(color);
        //Color c = color.GetValueOrDefault();
        Color c = color.Value;
        //r = color.GetR();
        //r = color.r;
        //g = color.g;
        //b = color.b;
        r = c.r;
        g = c.g;
        b = c.b;
        c.r = 100;
        c.g = 100;
    }

    public void Dispose()
    {
        
    }
}

struct ControlIdSet : IEquatable<ControlIdSet>
{
    public const int DONT_CARE = -1;

    public int textField;
    public int button;
    public int popup;

    public ControlIdSet(int t, int b, int p)
    {
        textField = t;
        button = b;
        popup = p;
    }

    public bool Equals(ControlIdSet other)
    {
        if (textField != DONT_CARE && other.textField != DONT_CARE)
        {
            return textField == other.textField;
        }

        if (button != DONT_CARE && other.button != DONT_CARE)
        {
            return button == other.button;
        }

        return 
            (textField == other.textField) && 
            (button == other.button) && 
            (popup == other.popup);
    }

    public override bool Equals(Object obj)
    {
        if (obj == null)
            return false;

        ControlIdSet personObj = (ControlIdSet)obj;
        return Equals(personObj);
    }

    public override int GetHashCode()
    {
        return textField;
    }
};

class Program
{
    public delegate R DVariant<in A, out R>(A a);

    static void Main(string[] args)
    {
        {
            //var d = ((String str) => str + " "); // var cant do with lambda

            DVariant<String, String> dvariant = (String str) => str + " ";
            dvariant("test");
        }
        {
            //Debug.Log("");

            //Array a = new Array();
            //a.Yay();

            //SL::ParallelQuery p = null;
            //SL.ParallelQuery p2 = null;
        }
        {
            Color c1 = new Color(0, 0, 0);
            c1.Init();
            c1.Init(Color.WHITE);
            Color c2 = c1;
            c2.r = 123;
            Console.WriteLine(c1.r);
        }
        {
            SingleTon s = SingleTon.Instance;
            //SingleTon s2 = new SingleTon();
        }
        {
            //int c = Convert.ToInt32("TextField");
            string s1 = "abcd";
            string s2 = s1;
            s1 = "jj";
            Console.WriteLine("x");
        }
        {
            List<int> origin = new List<int>();
            object o = origin;
            List<int> t = (List<int>)o;
            List<int> t2 = t;
            origin = null;

            Console.WriteLine("x");
        }
        {
            SortedDictionary<int, int> d = new SortedDictionary<int, int>();
            //int k = d[0];
            d[0] = 1;
            d[-1] = 1;
            d[-5] = 1;
            d[2] = 1;
            d[20] = 1;
            d[-20] = 1;
            //d = d;
        }
        {
            Dictionary<ControlIdSet, string> cs = new Dictionary<ControlIdSet, string>();
            ControlIdSet c1 = new ControlIdSet(1,2,3);
            ControlIdSet c2 = new ControlIdSet(4,5,6);
            ControlIdSet c3 = new ControlIdSet(7,8,9);
            ControlIdSet c11 = new ControlIdSet(-1, 11, 12);
            cs.Add(c1, "qq");
            cs.Add(c2, "ww");
            cs.Add(c3, "ee");
            cs.Add(c11, "rr");
            ControlIdSet c4 = new ControlIdSet(1,9999,9999);
            ControlIdSet c5 = new ControlIdSet(-1, 5, 9999);
            ControlIdSet c6 = new ControlIdSet(-1, 11, 9999);
            var r1 = cs[c1];
            bool b1 = cs.ContainsKey(c1);
            bool b2 = cs.ContainsKey(c4);
            bool b3 = cs.ContainsKey(c5);
            bool b4 = cs.ContainsKey(c6);
            var r2 = cs[c4];
            //bool e = (c1 == c4);

            List<ControlIdSet> lis = new List<ControlIdSet>();
            lis.Add(c1);
            lis.Add(c2);
            lis.Add(c3);
            ControlIdSet f1 = lis.Find(x => x.textField == c5.textField);
            //ControlIdSet f2 = lis.Find(c2);

            Console.WriteLine("1");
        }
        {
            Console.WriteLine("end of your test project");
        }
    }
}
} // end of namespace

class Something
{
    public void Ha()
    {
        Debug.Log("");
        //Array a = new Array();
        //a.Yay();
    }
}

class SingleTon
{
    SingleTon()
    {
    }

    private static SingleTon instance = null;

    public static SingleTon Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SingleTon();
            }
            return instance;
        }
    }
}
