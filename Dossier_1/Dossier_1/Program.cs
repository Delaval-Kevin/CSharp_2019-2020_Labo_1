using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MyCartographyObjects;


namespace Dossier_1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Coordonnees C = new Coordonnees(100, 200);

            POI P = new POI("LOUVEIGNE", 200.254411536, 50.221452);
            POI P1 = new POI();
            Console.WriteLine(C.ToString());

            Console.WriteLine(P.ToString());
            Console.WriteLine(P1.ToString());

            Coordonnees coordonnees = new Coordonnees(123.3, 15);

            Console.WriteLine(coordonnees.ToString());

            POI poi = new POI();

            Console.WriteLine(poi.ToString());

            Polyline polyline = new Polyline(coordonnees, Colors.Black, 12);
            polyline.Coordonnees.Add(new Coordonnees(12, 123));
            Console.WriteLine(polyline.ToString());

            Polygon polygon = new Polygon();
            Console.WriteLine(polygon.ToString());

            Console.ReadKey();
            */

            Coordonnees coordonnees = new Coordonnees(1, 1);


            Polygon polygon = new Polygon(coordonnees, Colors.Black, Colors.Black, 1);
            polygon.Coordonnees.Add(new Coordonnees(2, 4));
            polygon.Coordonnees.Add(new Coordonnees(5, 3));
            polygon.Coordonnees.Add(new Coordonnees(6, 1));
            Console.WriteLine(polygon.ToString());

            Console.WriteLine("{0}", polygon.IsPointClose(new Coordonnees(3, 1), 1));
            Console.WriteLine("{0}", polygon.NbPoints);
            POI poi = new POI();

            Polygon polygon1 = new Polygon(new Coordonnees(2, 2), Colors.Black, Colors.Black, 1);
            Polygon polygon2 = new Polygon();

            Polyline polyline1 = new Polyline(null, Colors.Black, 1);
            Polyline polyline2 = new Polyline();

            polygon2.Coordonnees.Add(new Coordonnees(2, 4));
            polygon2.Coordonnees.Add(new Coordonnees(5, 3));
            polygon2.Coordonnees.Add(new Coordonnees(6, 1));




            polyline2.Coordonnees.Add(new Coordonnees(0, 0));
            polyline2.Coordonnees.Add(new Coordonnees(0, 1));
            polyline2.Coordonnees.Add(new Coordonnees(1, 1));
            polyline2.Coordonnees.Add(new Coordonnees(1, 0));



            polyline1.Coordonnees.Add(new Coordonnees(10, 0));
            polyline1.Coordonnees.Add(new Coordonnees(8, 8));
            polyline1.Coordonnees.Add(new Coordonnees(10, 10));



            Console.WriteLine(polygon1.ToString());
            Console.WriteLine(polygon2.ToString());

            Console.WriteLine(polyline1.ToString());
            Console.WriteLine(polyline2.ToString());

            Console.WriteLine("{0}", polygon2.IsPointClose(new Coordonnees(1, 5), 1));

            List<CartoObj> ListCart = new List<CartoObj>();

            ListCart.Add(polyline1);
            ListCart.Add(polyline2);
            ListCart.Add(polygon2);
            ListCart.Add(new Coordonnees());
            ListCart.Add(polygon1);
            ListCart.Add(poi);

            Console.WriteLine("\n\n\nListe de CartoObj :");
            foreach (CartoObj cart in ListCart)
            {
                Console.WriteLine(cart.ToString());
            }

            List<IPointy> IP = new List<IPointy>();
            IP.Add(polygon1);
            IP.Add(polyline2);
            IP.Add(polygon2);
            IP.Add(polyline1);
            Console.WriteLine("\n\n\nListe de IPointy :");
            foreach (IPointy ip in IP)
            {
                Console.WriteLine(ip.ToString());
            }


            List<Polyline> t = new List<Polyline>();

            //    ListCart.Add(polygon1);
            t.Add(polyline2);
            //    ListCart.Add(polygon2);
            t.Add(polyline1);
            //    ListCart.Add(new POI());
            Console.WriteLine("\n\n\nListe de Polyline avant sort:");
            foreach (CartoObj cart in t)
            {
                Console.WriteLine(cart.ToString());
            }

            t.Sort();

            Console.WriteLine("\n\n\nListe de Polyline apres sort:");
            foreach (CartoObj cart in t)
            {
                Console.WriteLine(cart.ToString());
            }

            MyPolylineBoundingBoxComparer mpoly = new MyPolylineBoundingBoxComparer();

            Console.WriteLine("\n Tri par la longueur du bounding");
            t.Sort(mpoly);

            foreach (Polyline cart in t)
            {
                Console.WriteLine(cart.ToString());
            }

            Console.WriteLine("\n Find");
            Console.WriteLine(t.Find(x => x.CalculLongueur() == 0));


            Console.WriteLine("\n FindAll");
            List<Polyline> poly = t.FindAll(x => x.CalculLongueur() == 3);
            Affiche(poly);


            Console.WriteLine("\n FindAll ISPointClose");
            poly = t.FindAll(x => x.IsPointClose(new Coordonnees(1, 0.5), 1));
            Affiche(poly);

            Console.WriteLine("\n Compare CartoObj");
            CartoComparer carte = new CartoComparer();
            foreach (CartoObj obj in ListCart)
            {

                Console.WriteLine(obj.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }
            ListCart.Sort(carte);
            foreach (CartoObj obj in ListCart)
            {

                Console.WriteLine(obj.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }








            Console.ReadKey();



        }

        private static void Affiche(List<Polyline> poly)
        {
            foreach (Polyline cart in poly)
            {
                Console.WriteLine(cart.ToString());
            }
        }
    }
}