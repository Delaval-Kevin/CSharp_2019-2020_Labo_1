using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace MathFunctions
{
    public  class MathUtil
    {
        #region METHODES
        //Calcul la distance entre 2 points
        public static double Dist2Points(double x1, double y1, double x2, double y2)
        {
            double x = x2 - x1;
            double y = y2 - y1;
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        //Calcule la distance entre un point et un segment de droite
        public static double DistPointSeg(double x1, double y1, double x2, double y2, double x0, double y0)
        {
            double A = x0 - x1;
            double B = y0 - y1;
            double C = x2 - x1;
            double D = y2 - y1;

            double dot = A * C + B * D;

            double len_sq = C * C + D * D;
            double param = -1;

            if (len_sq != 0)
                param = dot / len_sq;

            double xx, yy;

            if (param < 0)
            {
                xx = x1;
                yy = y1;
            }
            else
            if (param > 1)
            {
                xx = x2;
                yy = y2;
            }
            else
            {
                xx = x1 + param * C;
                yy = y1 + param * D;
            }

            double dx = x0 - xx;
            double dy = y0 - yy;
            return Math.Sqrt(dx * dx + dy * dy);

        }
        #endregion
    }
}
