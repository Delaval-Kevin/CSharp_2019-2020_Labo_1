using System;
using System.Linq;
using System.Text;
using MyCartographyObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PersonelMap_Manager
{
    interface IPageChange
    {
        #region EVENEMENTS
        event Action <int, MyPersonalMapData> PageChange;
        #endregion
    }
}
