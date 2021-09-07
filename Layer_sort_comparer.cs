using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace island
{
    class Layer_sort_comparer:System.Collections.IComparer
    {
        public int Compare(object s1,object s2)
        {
            return ((Layer_sort)s1).y-((Layer_sort)s2).y;
        }
    }
}
