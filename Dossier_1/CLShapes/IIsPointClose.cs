using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;

namespace MyCartographyObjects
{
    public interface IIsPointClose
    {
        #region METHODES
        bool IsPointClose(Coordonnees coorTmp, double precision);

        bool IsPointClose(Location coorTmp, double precision);
        #endregion
    }
}
