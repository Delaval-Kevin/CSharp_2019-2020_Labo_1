using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathFunctions;

namespace MyCartographyObjects
{
    [Serializable]
    public class Coordonnees : CartoObj
    {
        #region VARIABLES MEMBRES
        private double _latitude;
        private double _longitude;
        #endregion


        #region PROPRIETES
        public double Latitude
        {
            get{ return _latitude; }
            set{ _latitude = value; }
        }

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }
        #endregion


        #region CONSTRUCTEURS
        public Coordonnees() :this(0, 0) { }

        public Coordonnees(double latitude, double longitude) : base()
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        #endregion


        #region METHODES
        public override string ToString()
        {
            return base.ToString() + " (" + Latitude.ToString("0.000") + " : " + Longitude.ToString("0.000") + ")";
        }

        public override bool IsPointClose(Coordonnees coorTmp, double precision)
        {
           if(MathUtil.Dist2Points(Latitude, Longitude, coorTmp.Latitude, coorTmp.Longitude) <= precision)
                return true;

            return false;
        }
        #endregion
    }
}
