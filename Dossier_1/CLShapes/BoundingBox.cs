using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCartographyObjects
{
    public class BoundingBox : IComparable<BoundingBox>
    {
        #region VARIABLES MEMBRES
        Coordonnees _max;
        Coordonnees _min;
        #endregion

        #region PROPRIETES
        public Coordonnees Min 
        {
            get { return _min; }
            set { _min = value; }
        }

        public Coordonnees Max
        {
            get { return _max; }
            set { _max = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public BoundingBox() : this(new Coordonnees(0, 0), new Coordonnees(0, 0)) { }

        public BoundingBox(Coordonnees cmin, Coordonnees cmax)
        {
            Min = cmin;
            Max = cmax;
        }
        #endregion

        #region METHODES
        public void InitBox(List<Coordonnees> lcoord)
        {
            if (!lcoord.Any())
                return;

            double x1, x2, y1, y2;
            x1 = x2 = lcoord[0].Latitude;
            y1 = y2 = lcoord[0].Longitude;

            foreach (Coordonnees coo in lcoord)
            {
                if (x1 > coo.Latitude)
                    x1 = coo.Latitude;
                if (y1 > coo.Longitude)
                    y1 = coo.Longitude;
                if (x2 < coo.Latitude)
                    x2 = coo.Latitude;
                if (y2 < coo.Longitude)
                    y2 = coo.Longitude;

            }
            Min = new Coordonnees(x1, y1);
            Max = new Coordonnees(x2, y2);
        }

        public double CalculAir()
        {
            return (Max.Longitude - Min.Longitude) * (Max.Latitude - Min.Latitude);
        }

        public int CompareTo(BoundingBox other)
        {
            return this.CalculAir().CompareTo(other.CalculAir());
        }
        #endregion
    }
}
