using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test2012_csharp
{

class TestAction<T> where T : class
{
    public Action<T> action;
}

}
