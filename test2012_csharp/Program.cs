#pragma warning disable 0168, 0219

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DebugNamespace;
using SL = System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace test2012_csharp
{
struct Color
{
    public static readonly Color WHITE = new Color(1, 1, 1);
    //public const Color WHITE2 = new Color(1, 1, 1);
    //public static readonly Color BLACK = null;//new Color(0, 0, 0);
    //public static readonly Color RED = null;//new Color(1, 0, 0);

    public static Color WHITE2 { get { return new Color(1, 1, 1); } }

    private static Color white = new Color(1, 1, 1);

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
        color = Color.WHITE2;
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

class ClassColor
{
    public static readonly ClassColor WHITE = new ClassColor(1, 1, 1);

    public float r;
    public float g;
    public float b;

    public ClassColor(float r, float g, float b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
    }

    void DoDo(ClassColor c = null)//ClassColor.WHITE)
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

class SomeReference
{
    public int i = 7;
}

struct LetsSeeCtor
{
    public int i;
    public SomeReference s;

    //public LetsSeeCtor()//string s)
    //{
    //    i = 1;
    //}
}

class Some1
{
    public void Do()
    {
        Console.WriteLine(1);
    }
}
class Some2
{
    public void Do()
    {
        Console.WriteLine(2);
    }
}
class GenericSome<T>
{
    public List<T> list = new List<T>();

    public void Exe(T obj)
    {
        list.Add(obj);
        //obj.Do();
        T obj2 = obj;
        //obj2 = obj + obj2;
    }
}

class Mammals { }
class Dogs : Mammals { public int i; }
class Cats : Mammals { public int j; }

class Coroutine
{
    public IEnumerable<int> Something()
    {
        yield return 111;
        yield return 112;
        yield return 113;
    }
}

class Program
{
    public delegate R DVariant<in A, out R>(A a);

    class MyClass1
    {
        public int i = 0;
    }
    static void thehell(ref object ob)
    {
        ob = null;
    }
    static void thehell2(object ob)
    {
        //ob = null;
        ob = 1;
    }
    static void thehell3(MyClass1 m)
    {
        m.i = 999;
    }

    //static void TestOut(out List<int> list)
    //{
    //    list.Add(1);
    //}
    static void TestRef(ref List<int> list)
    {
        list.Add(1);
    }
    static void passByObject(ref object o)
    {
        int i = (int)o;
        i = 99;
    }
    static void passByNormalObject(object o)
    {
        int x = (int)o;
        x = 10;
    }

    public delegate void Del(string s);
    static Del dAll;
    static void D1(string s) { Console.WriteLine(s + 1); }
    static void D2(string s) { Console.WriteLine(s + 2); }
    static void D3(string s) { Console.WriteLine(s + 3); }
    static bool D4(string s) { return s.Length > 1; }

    public delegate Mammals HandlerMethod();

    public static Mammals MammalsHandler()
    {
        return new Mammals();
    }

    public static Dogs DogsHandler()
    {
        return new Dogs();
    }

    public static Cats CatsHandler()
    {
        return new Cats();
    }

    static private Dictionary<Type, HandlerMethod> typeToMethod = new Dictionary<Type, HandlerMethod>();

    static void Test()
    {
        HandlerMethod handlerMammals = MammalsHandler;
        HandlerMethod handlerDogs = DogsHandler; // Covariance enables this assignment.
        Mammals m = handlerDogs(); // this’s ok since Dogs can covert to Mammals implicitly
        Dogs d = (Dogs)m;
        d.i = 5000;
        Console.WriteLine(m.ToString());

        typeToMethod[typeof(Dogs)] = DogsHandler;
        typeToMethod[typeof(Cats)] = CatsHandler;

        Dogs dd = Create<Dogs>();
        dd.i = 1234;
        Cats cc = Create<Cats>();
        cc.j = 9999;

        Console.WriteLine(1);
    }

    static T Create<T>() where T : Mammals
    {
        return (T)(typeToMethod[typeof(T)]());
    }

    static void TestCoroutine()
    {
        Coroutine c = new Coroutine();
        foreach (int i in c.Something())
        {
            Console.WriteLine(i);
        }
        IEnumerator<int> it = c.Something().GetEnumerator();
        for (int i = 0; i < 2; ++i)
        {
            if (it.MoveNext() == false)
            {
                break;
            }

            Console.WriteLine(it.Current);
        }
        //Console.WriteLine(c.Something().GetEnumerator().Current);
        //Console.WriteLine(c.Something().GetEnumerator().Current);
        //Console.WriteLine(c.Something().GetEnumerator().Current);
        Console.WriteLine(1);
    }

    static string ConcatExample0(int[] intArray)
    {
        string line = intArray[0].ToString();

        for (int i = 1; i < intArray.Length; i++)
        {
            line += ", " + intArray[i];
        }

        return line;
    }

    static string ConcatExample(int[] intArray)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 1; i < intArray.Length; i++)
        {
            sb.Append(", ");
            sb.Append(intArray[i]);
        }
        return sb.ToString();
    }

    class SomeClass
    {

    }
    //class SomeGeneric<T> where T : typeof(SomeClass)
    //{

    //}

    class UseDelegate
    {
        public delegate void DEL();
        public DEL del;
    }
    class UseEvent
    {
        public delegate void DEL();
        public event DEL eve;
        public void Call() { eve(); }
    }
    static public void DDD1()
    {
        Console.WriteLine(1);
    }
    static public void DDD2()
    {
        Console.WriteLine(2);
    }

    class SBase1
    {

    }
    class SBase2 : SBase1
    {

    }

    class GBase<T> : T
    {
        public int Doo()
        {
            return 1;
        }
    }

    static void Main(string[] args)
    {
        {
            GBase<SBase1> gb1 = new GBase<SBase1>();
            gb1.Doo();
        }
        {
            //var op1 = x => x += 1; // compile error
            Action<int> op2 = x => x += 1;
            Action<int> op3 = (int x) => x += 1;

            List<int> list = new List<int>(1);
            list.Add(777);
            list[0] = 888;

            Console.WriteLine(1);
        }
        {
            UseDelegate ud = new UseDelegate();
            UseEvent ue = new UseEvent();

            ud.del += DDD1;
            ud.del += DDD2;
            ud.del();

            ue.eve += DDD1;
            ue.eve += DDD2;
            //ue.eve();
            ue.Call();

            Console.WriteLine(1);
        }
        {
            List<int> ints = new List<int>(20000);
            Random rd = new Random();
            //int[] ints = {10, 20, 30, 100, 200, 300, 666};
            for (int i = 0; i < ints.Capacity; ++i)
            {
                ints.Add(rd.Next());
            }
            int[] intsarray = ints.ToArray();
            string[] stringsarray = new string[intsarray.Length];
            for (int i = 0; i < intsarray.Length; ++i)
            {
                stringsarray[i] = intsarray[i].ToString();
            }

            Stopwatch sw = Stopwatch.StartNew();
            string x1 = ConcatExample0(intsarray);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);

            Stopwatch sw2 = Stopwatch.StartNew();
            string x2 = ConcatExample(intsarray);
            sw2.Stop();
            Console.WriteLine(sw2.Elapsed);

            Console.WriteLine(1);
        }
        {
            List<int> list1 = new List<int>(100);
            //list1.Count;
            Dictionary<int, int> dic1 = new Dictionary<int, int>();
            //list1.MyReset<List<int>, int>();
            list1.MyReset();
            //dic1.MyReset();
            int x = 1234;
            x.MyReset();
            Coroutine c = new Coroutine();
            c.MyReset();
            ControlIdSet cis = new ControlIdSet();
            cis.MyReset();
            Color cl = new Color();
            cl.MyReset();

            TestAction<List<int>> ta1 = new TestAction<List<int>>();
            //ta1.action = ExtentionMethods.MyReset<List<int>, int>;
            ta1.action = ExtentionMethods.MyReset;
            TestAction<Dictionary<int, int>> ta2 = new TestAction<Dictionary<int, int>>();
            //ta2.action = ExtentionMethods.MyReset;
            ta1.action(list1);
            //ta2.action(dic1);
            TestAction<Coroutine> ta3 = new TestAction<Coroutine>();
            ta3.action = ExtentionMethods.MyReset;
            ta3.action(new Coroutine());

            
            Console.WriteLine(1);
        }
        {
            float[] fs = new float[3];
            fs[0] = 1.0f;

            List<float> lf = new List<float>();
            Console.WriteLine("empty");
            Console.WriteLine(lf.Count);
            Console.WriteLine(lf.Capacity);
            lf.Add(1.0f);
            Console.WriteLine("1 element");
            Console.WriteLine(lf.Count);
            Console.WriteLine(lf.Capacity);
            lf.Add(1.0f);
            Console.WriteLine("2 elements");
            Console.WriteLine(lf.Count);
            Console.WriteLine(lf.Capacity);
            lf.Add(1.0f);
            Console.WriteLine("3 elements");
            Console.WriteLine(lf.Count);
            Console.WriteLine(lf.Capacity);
            lf.Add(1.0f);
            Console.WriteLine("4 elements");
            Console.WriteLine(lf.Count);
            Console.WriteLine(lf.Capacity);
            lf.Add(1.0f);
            Console.WriteLine("5 elements");
            Console.WriteLine(lf.Count);
            Console.WriteLine(lf.Capacity);
            lf.Clear();
            Console.WriteLine("cleared");
            Console.WriteLine(lf.Count);
            Console.WriteLine(lf.Capacity);
            
            Console.WriteLine(1);
        }
        {
            TestCoroutine();
        }
        {
            int i = 123;
            object o = i;
            int j = (int)o; // ok
            //long k = (long)o; // error
            long m = j; // ok...
            Console.WriteLine(1);
        }
        {
            Test();
        }
        {
            dAll += D1;
            dAll += D2;
            dAll += D3;

            Delegate[] ds = dAll.GetInvocationList();
            for (int i = 0; i < ds.Length; ++i)
            {
                var m = ds[0].Method;
                var t = ds[0].Target;
                Console.WriteLine(1);
            }

            dAll("x");
            dAll -= D2;
            dAll("no d2");
            dAll = D1;
            dAll("y");

            Action<string> ddd = D1;
            ddd("zzz");
            Func<string, bool> fff = D4;
            bool r = D4("kr");

            Console.WriteLine(1);
        }
        {
            GenericSome<Some1> gs1 = new GenericSome<Some1>();
            Some1 s1 = new Some1();
            gs1.Exe(s1);
        }
        {
            Dictionary<int, int> id = new Dictionary<int, int>();
            id[0] = 0;
            id[1] = 1;
            id[2] = 2;
            id[0] = 1000;
            //int x = id[3];
            int o;
            bool b = id.TryGetValue(3, out o);
            if (b)
            {
                Console.WriteLine(o);
            } 
            else
            {
                Console.WriteLine(o);
            }

            Console.WriteLine(1);
        }
        {
            string x;
            string y = null;
            //bool b = string.IsNullOrEmpty(x);
            string s1 = "xyz";
            string s2 = s1;
            s2 = "abc";
        }
        {
            object o1 = new object();
            object o2 = null;
            object[] p = new object[] { o1, o2 };
            bool bo2 = (p[1] != null);

            string s = o2.ToString();

            Console.WriteLine(1);
        }
        {
            string s = @"{0}
{0}<{1}> {2}<{3}> {4}<{5}> {6}<{7}>
bool = {8}, string = {9}, EX = {10}, delay = {11}-{12}, use time scale = {13}";

            string s1 = "zzz{0}xxx___{7}aa{8}zzzhahaboo";
            string s2 = "xxx";

            int index = 0;
            int counter = 0;
            string result = string.Empty;

            string inp = s1;
            while (true)
            {
                int start = inp.IndexOf('{', index);
                if (start == -1)
                {
                    result += inp.Substring(index, inp.Length - index);
                    break;
                }

                int match = inp.IndexOf('}', start + 1);
                if (match == -1)
                {
                    break;
                }

                result += inp.Substring(index, (start - index + 1)) + counter + "}";

                index = match + 1;
                ++counter;
            }

            Console.WriteLine(1);
        }
        {
            //string text = ",1,2,3,";
            string text = "11111111111112222222";
            char[] keywordSeparators = { ',' };
            string[] keywords = text.Split(keywordSeparators, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(1);
        }
        {
            string s = @"Source = <GaameObject> Instance Id = <9527> Event Id = <ooxx_1122-8866_z/z1\23> Listener = <zzz>";
            string EVENT_ID_HEAD = @"Event Id = ";
            {
                string pattern = @"(?<=" + EVENT_ID_HEAD + @")<" + ".*" + "(?=>*?)";
                Regex regex = new Regex(pattern);
                var m = regex.Match(s);
                string r = m.ToString();
                Console.WriteLine(1);
            }
            {
                string pattern = @"(?<=Source = [\w\.\-\/\\]+\sInstance Id = )([\w\.\-\/\\]+)";
                Regex regex = new Regex(pattern);
                var m = regex.Match(s);
                string r = m.ToString();
                Console.WriteLine(1);
            }
            {
                float zzzz = 3.141592f;
                string xxxxxx = zzzz.ToString();
                float f = Convert.ToSingle(xxxxxx);
                Console.WriteLine(1);
            }

            Console.WriteLine(1);
        }
        {
            List<string> ss = new List<string>();
            ss.Add("sent from 1");
            ss.Add("sent from 2");
            ss.Add("rec by 3 sent from 1 time = 555.0501");
            ss.Add("rec by 4 sent from 2 time = 666.0501");
            ss.Add("rec by 1 sent from 3 time = 777.0600");
            ss.Add("rec by 1 sent from 3 time = 778.0777");
            ss.Add("rec by 1 sent from 3 time = 779.0888");

            ss.Add("rec by 1\n sent from 3\n time = 779");

            //Regex r = new Regex (".*(3).*(1).*");
            //Regex r = new Regex("^(?=.*1).*3.*$");
            Regex r = new Regex(@"^(?=.*1)(?=.*3)(?=.*555)");
            
            
            //bool containsAny = r.IsMatch (s);
            List<string> result = ss.FindAll(x => r.IsMatch(x));


            string text = "1,3,5.05";
            char[] seperator = { ',' };
            string[] keywords = text.Split(seperator, StringSplitOptions.RemoveEmptyEntries);

            // 2
            string pattern = @"^";
            for (int i = 0, size = keywords.Length; i < size; ++i)
            {
                pattern += @"(?=.*" + Regex.Escape(keywords[i]) + @")";
            }
            //pattern = Regex.Escape(pattern);
            Regex regex = new Regex(pattern, RegexOptions.Singleline);
            var xoxoxo = ss.Where(x => regex.IsMatch(x));
            Console.WriteLine(xoxoxo.Count<string>());
            foreach (var x in xoxoxo)
            {
                Console.WriteLine(x);
            }

            // 1
            var yoyoyo = ss.Where(
            (string x) =>
            {
                //Console.WriteLine(x);
                for (int i = 0, size = keywords.Length; i < size; ++i)
                {
                    if (x.Contains(keywords[i]) == false)
                    {
                        //Console.WriteLine(x + " not matched!");
                        return false;
                    }
                }
                //Console.WriteLine(x + " matched!");
                return true;
            });
            Console.WriteLine(yoyoyo.Count<string>());
            foreach (var x in yoyoyo)
            {
                Console.WriteLine(x);
            }

            Console.WriteLine("1");
        }
        {
            int x = 1;
            //passByObject(x);
            passByNormalObject((object)x);

            Console.WriteLine("1");
        }
        {
            string s = string.Empty;
            s.Insert(s.Length, "xxxxx");
            s.Insert(s.Length, "yyy");

            Console.WriteLine("1");
        }
        {
            OrderedDictionary od = new OrderedDictionary();
            //od.Add(9, 1);
            od.Add("123", 1);
            if (od.Contains("123") == false)
            {
                od.Add("123", 2);
            }
            od.Add("456", 2);
            od.Add("789", 3);
            od.Add("101112", 4);
            od.Add("count", od.Count);
            //var x = od[(object)9];
            var e1 = od[0];
            var e3 = od[3];
            var e4 = od[4];
            var e11 = od["123"];
            var e31 = od["101112"];
            var e41 = od["xxx"];

            List<string> results = new List<string>();
            var keys = od.AsReadOnly().Keys;
            foreach (object o in keys)
            {
                string s = (string)(o);
                results.Add(s);
            }

            Console.WriteLine("1");
        }
        {
            HashSet<string> hs = new HashSet<string>();
            hs.Add("123");
            hs.Add("123");
            hs.Add("456");
            hs.Add("789");
            hs.Add("000");
            int hscount = hs.Count;
            string first = hs.First<string>();
            //first = hs[0];
            //System.Collections.Generic.HashSetDebugView<string> o; // holy shit thing
            

            StringCollection sc = new StringCollection();
            sc.Add("123");
            sc.Add("123");
            sc.Add("123");
            bool c = sc.Contains("123");

            Console.WriteLine("1");
        }
        {
            //LetsSeeCtor l = new LetsSeeCtor();//this initialize i to 0
            LetsSeeCtor l;// if you do this, you cant use l.i
            //Console.WriteLine(l.i);
            l.i = 999;
            Console.WriteLine("1");
        }
        {
            const string path = @"Assets/Resources/ResourcesAB/testCube.prefab";
            //var e = File.Exists(path);
            //var x = File.GetAttributes(path);
            //bool isdir = (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
            string yo = Path.GetExtension(path);

            Console.WriteLine("1");
        }
        {
            object ob = new object();
            //thehell(ref ob);
            thehell2(ob);
            MyClass1 m = new MyClass1();
            m.i = 1;
            thehell3(m);

            Console.WriteLine("1");
        }
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
