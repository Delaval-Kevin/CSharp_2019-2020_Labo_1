using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelMap_Manager
{
    interface IPageChange
    {
        event Action <int> pageChange;
    }
}
