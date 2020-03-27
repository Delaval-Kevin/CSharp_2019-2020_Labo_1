using System;
using System.Windows.Media;
using MyCartographyObjects;
using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;

namespace Dossier_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Coordonnees coordonnees = new Coordonnees(1, 1);


            Polygon polygon = new Polygon(coordonnees, Colors.Black, Colors.Black, 1);
            polygon.Coordonnees.Add(new Coordonnees(2, 4));
            polygon.Coordonnees.Add(new Coordonnees(5, 3));
            polygon.Coordonnees.Add(new Coordonnees(6, 1));
            Console.WriteLine(polygon.ToString());

            Console.WriteLine("Le point est il proche selon la precision : {0}", polygon.IsPointClose(new Coordonnees(3, 1), 1));
            Console.WriteLine("Le point est il proche selon la precision : {0}", polygon.IsPointClose(new Coordonnees(8, 3), 1));
            Console.WriteLine("Nombre de point du polygon : {0}", polygon.NbPoints);
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


            Console.WriteLine("\n\n polygon 1 : ");
            Console.WriteLine(polygon1.ToString());
            Console.WriteLine("\n\n polygon 2 : ");
            Console.WriteLine(polygon2.ToString());

            Console.WriteLine("\n\n polyline 1 : ");
            Console.WriteLine(polyline1.ToString());
            Console.WriteLine("\n\n polyline 2 : ");
            Console.WriteLine(polyline2.ToString());

            Console.WriteLine("Le point est il proche selon la precision : {0}", polygon2.IsPointClose(new Coordonnees(1, 5), 1));
            Console.WriteLine("Le point est il proche selon la precision : {0}", polygon2.IsPointClose(new Coordonnees(5, 1), 1));

            List<CartoObj> ListCart = new List<CartoObj>
            {
                polyline1,
                polyline2,
                polygon2,
                new Coordonnees(),
                polygon1,
                poi
            };

            Console.WriteLine("\n\n\n-- Liste de CartoObj --");
            foreach (CartoObj cart in ListCart)
            {
                Console.WriteLine(cart.ToString());
            }

            List<IPointy> IP = new List<IPointy>
            {
                polygon1,
                polyline2,
                polygon2,
                polyline1
            };
            Console.WriteLine("\n\n\n-- Liste de IPointy --");
            foreach (IPointy ip in IP)
            {
                Console.WriteLine(ip.ToString());
            }


            List<Polyline> t = new List<Polyline>
            {
                polyline1,
                polyline2
            };

            Console.WriteLine("\n\n\n-- Liste de Polyline avant sort --");
            foreach (CartoObj cart in t)
            {
                Console.WriteLine(cart.ToString());
            }

            t.Sort();

            Console.WriteLine("\n\n\n-- Liste de Polyline apres sort --");
            foreach (CartoObj cart in t)
            {
                Console.WriteLine(cart.ToString());
            }

            MyPolylineBoundingBoxComparer mpoly = new MyPolylineBoundingBoxComparer();

            Console.WriteLine("\n\n\n-- Tri par la longueur du bounding --");
            t.Sort(mpoly);

            foreach (Polyline cart in t)
            {
                Console.WriteLine(cart.ToString());
            }

            Console.WriteLine("\n\n\n-- Find --");
            Console.WriteLine(t.Find(x => x.CalculLongueur() == 0));


            Console.WriteLine("\n\n\n-- FindAll --");
            List<Polyline> poly = t.FindAll(x => x.CalculLongueur() == 3);
            Affiche(poly);


            Console.WriteLine("\n\n\n-- FindAll ISPointClose --");
            poly = t.FindAll(x => x.IsPointClose(new Coordonnees(1, 0.5), 1));
            Affiche(poly);

            Console.WriteLine("\n\n\n-- Compare CartoObj --");
            Console.WriteLine("\n-- UnSort --");
            CartoComparer carte = new CartoComparer();
            foreach (CartoObj obj in ListCart)
            {

                Console.WriteLine(obj.ToString());
                Console.WriteLine("----------------------------------------------------------");
            }

            Console.WriteLine("\n\n-- Sort --");
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