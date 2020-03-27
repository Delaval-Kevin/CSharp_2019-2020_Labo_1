using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyCartographyObjects
{
    public class CartoComparer : IComparer<CartoObj>
    {
        #region METHODES
        public int Compare(CartoObj x, CartoObj y)
        {
            if (x is IPointy && y is IPointy)
            {
                IPointy ip1 = x as IPointy;
                IPointy ip2 = y as IPointy;
                return ip1.NbPoints.CompareTo(ip2.NbPoints);
            }
            else
                if (x is IPointy)
                return 1;
            else
                return -1;
        }
        #endregion
    }
}

