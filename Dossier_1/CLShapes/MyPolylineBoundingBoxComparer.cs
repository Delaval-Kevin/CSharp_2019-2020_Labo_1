using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathFunctions;

namespace CLShapes
{
    public class MyPolylineBoundingBoxComparer : IComparer<Polyline>
    {
        public int Compare(Polyline x, Polyline y)
        {
            return BoundingBoxArea(x).CompareTo(BoundingBoxArea(y));
        }

        public double BoundingBoxArea(Polyline polyTmp)
        {
            double area;
            double xMin, xMax, yMin, yMax;

            if (!polyTmp.Coordonnees.Any())
                return 0;

            xMin = polyTmp.Coordonnees[0].Latitude;
            xMax = polyTmp.Coordonnees[0].Latitude;
            yMin = polyTmp.Coordonnees[0].Longitude;
            yMax = polyTmp.Coordonnees[0].Longitude;

            foreach (Coordonnees coor in polyTmp.Coordonnees)
            {
                if (xMin > coor.Latitude)
                    xMin = coor.Latitude;
                if (xMax < coor.Latitude)
                    xMax = coor.Latitude;
                if (yMin > coor.Longitude)
                    yMin = coor.Longitude;
                if (yMax < coor.Longitude)
                    yMax = coor.Longitude;
            }

            area = MathUtil.Dist2Points(xMin, yMin, xMin, yMax);
            area = area + MathUtil.Dist2Points(xMin, yMin, xMax, yMin);

            return area;
        }
    }
}
