using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCartographyObjects;

namespace PersonelMap_Manager
{
    interface IPageChange
    {
        #region EVENEMENTS
        event Action <int, MyPersonalMapData> pageChange;
        #endregion
    }
}
