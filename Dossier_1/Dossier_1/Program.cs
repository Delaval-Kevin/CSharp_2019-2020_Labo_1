using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CLShapes;


namespace Dossier_1
{
    class Program 
    {
        static void Main(string[] args)
        {
            Polygon polygon1 = new Polygon(new Coordonnees(2,2), Colors.Black, Colors.Black, 1);
            Polygon polygon2 = new Polygon();

            Polyline polyline1 = new Polyline(new Coordonnees(3,3), Colors.Black, 1);
            Polyline polyline2 = new Polyline();

            polygon2.Coordonnees.Add(new Coordonnees(2, 4));
            polygon2.Coordonnees.Add(new Coordonnees(5, 3));
            polygon2.Coordonnees.Add(new Coordonnees(6, 1));

            polyline2.Coordonnees.Add(new Coordonnees(7, 4));
            polyline2.Coordonnees.Add(new Coordonnees(5, 9));
            polyline2.Coordonnees.Add(new Coordonnees(6, 10));
            polyline1.Coordonnees.Add(new Coordonnees(2, 2));
            polyline1.Coordonnees.Add(new Coordonnees(3, 3));
            polyline2.Coordonnees.Add(new Coordonnees(9, 1));

        //    Console.WriteLine(polygon1.ToString());
        //    Console.WriteLine(polygon2.ToString());

            Console.WriteLine(polyline1.ToString());
            Console.WriteLine(polyline2.ToString());

        //    Console.WriteLine("{0}", polygon2.IsPointClose(new Coordonnees(1, 5), 1));

            List<Polyline> ListCart = new List<Polyline>();

        //    ListCart.Add(polygon1);
            ListCart.Add(polyline2);
        //    ListCart.Add(polygon2);
            ListCart.Add(polyline1);
        //    ListCart.Add(new POI());
            Console.WriteLine("\n\n\nListe de Polyline avant sort:");
            foreach (CartoObj cart in ListCart)
            {
                Console.WriteLine(cart.ToString()); 
            }
            MyPolylineBoundingBoxComparer pcomp = new MyPolylineBoundingBoxComparer();
            ListCart.Sort(pcomp);

            Console.WriteLine("\n\n\nListe de Polyline apres sort:");
            foreach (CartoObj cart in ListCart)
            {
                Console.WriteLine(cart.ToString());
            }

/*            List<IPointy> IP = new List<IPointy>();
            IP.Add(polygon1);
            IP.Add(polyline2);
            IP.Add(polygon2);
            IP.Add(polyline1);
            Console.WriteLine("\n\n\nListe de IPointy :");
            foreach (IPointy ip in IP)
            {
                Console.WriteLine(ip.ToString());
            }

            Console.WriteLine("\n\n\nListe de CartoObj IS IPointy:");
            foreach (CartoObj cart in ListCart)
            {   
                if(cart is IPointy)
                    Console.WriteLine(cart.ToString());
            }

            Console.WriteLine("\n\n\nListe de CartoObj IS NOT IPointy:");
            foreach (CartoObj cart in ListCart)
            {
                if (!(cart is IPointy))
                    Console.WriteLine(cart.ToString());
            }
*/

            Console.ReadKey();
        }
    }
}
