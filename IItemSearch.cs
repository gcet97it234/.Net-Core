using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection
{
    public interface IItemSearch
    {
        int GetItem(string itemNo);
    }

    public class EStelItemSearch : IItemSearch
    {

        public int GetItem(string itemNo)
        {
            return 0;
        }

        
            
    }

    public class EMJItemSearch : IItemSearch
    {

        public int GetItem(string itemNo)
        {
            return 1;
        }

    }
}
