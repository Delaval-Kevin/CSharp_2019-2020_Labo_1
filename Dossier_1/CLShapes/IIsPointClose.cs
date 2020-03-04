using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects
{
    public interface IIsPointClose
    {
        #region METHODES
        bool IsPointClose(Coordonnees coorTmp, double precision);
        #endregion
    }
}
