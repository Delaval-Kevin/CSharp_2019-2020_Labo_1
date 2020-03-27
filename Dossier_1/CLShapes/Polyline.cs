using System;
using System.Linq;
using System.Text;
using MathFunctions;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;

namespace MyCartographyObjects
{
    [Serializable]
    public class Polyline : CartoObj, IPointy, ICartoObj, IComparable<Polyline>, IEquatable<Polyline>
    {
        #region VARIABLES MEMBRES
        private List<Coordonnees>   _coordonnees;
        private string              _couleur;
        private int                 _epaisseur;
        private string              _description;
        #endregion


        #region PROPRIETES
        public List<Coordonnees> Coordonnees
        {
            get { return _coordonnees; }
            set { _coordonnees = value; }
        }

        public int Epaisseur
        {
            get { return _epaisseur; }
            set { _epaisseur = value; }
        }

        public Color Couleur
        {
            get 
            {
                if (_couleur != null)
                {
                    return (Color)ColorConverter.ConvertFromString(_couleur);
                }
                else 
                {
                    return Colors.Blue;
                }
            }
            set { _couleur = value.ToString(); }
        }

        public int NbPoints
        {
            get { return Coordonnees.Count; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion


        #region CONSTRUCTEURS
        //Constructeur par défaut
        public Polyline() : this(null, Colors.Black, 1) { }

        //Constructeur d'initialisation
        public Polyline(Coordonnees coordonnees) : this(coordonnees, Colors.Black, 1) { }

        //Constructeur d'initialisation
        public Polyline(Coordonnees coordonnees, Color couleur, int epaisseur)
        {
            Coordonnees = new List<Coordonnees>();
            if (coordonnees != null)
            {
                Coordonnees.Add(coordonnees);
            }
            Couleur = couleur;
            Epaisseur = epaisseur;
        }
        #endregion


        #region METHODES
        public override string ToString()
        {
            return "Polyline : " + Description;
        }

        public override void Draw()
        {
            Console.WriteLine(this.ToString());
        }

        //Vérifie si la coordonnée reçue en paramètre est proche de la Polyline selon la précisoin
        public override bool IsPointClose(Coordonnees coorTmp, double precision)
        {
            if (!Coordonnees.Any())
            {
                return false;
            }
            else if (Coordonnees.Count < 2)
            {
                if (MathUtil.Dist2Points(Coordonnees[0].Latitude, Coordonnees[0].Longitude, coorTmp.Latitude, coorTmp.Longitude) <= precision)
                    return true;
                else
                    return false;
            }

            double x0, x1, x2, y0, y1, y2;

            x0 = coorTmp.Latitude;
            y0 = coorTmp.Longitude;
            for( int i = 0 ; i < Coordonnees.Count-1 ; i++)
            {
                x1 = Coordonnees[i].Latitude;
                y1 = Coordonnees[i].Longitude;
                x2 = Coordonnees[i+1].Latitude;
                y2 = Coordonnees[i+1].Longitude;

                if (MathUtil.Dist2Points(x1, y1, x0, y0) <= precision)
                    return true;

                if (MathUtil.Dist2Points(x2, y2, x0, y0) <= precision)
                    return true;

                if (MathUtil.DistPointSeg(x1, y1, x2, y2, x0, y0) <= precision)
                    return true;
            }

            return false;
        }

        //Vérifie si la location reçue en paramètre est proche du Polyline selon la précisoin donnée
        public override bool IsPointClose(Location locTmp, double precision)
        {
            Coordonnees coorTmp = new Coordonnees(locTmp.Latitude, locTmp.Longitude);

            return this.IsPointClose(coorTmp, precision);
        }

        //Calcule la longueur totale des segments de la Polyline
        public double CalculLongueur()
        {
            double taille = 0;

            for (int i = 0; i < Coordonnees.Count - 1; i++)
            {
                taille += MathUtil.Dist2Points(Coordonnees[i].Latitude, Coordonnees[i].Longitude, Coordonnees[i + 1].Latitude, Coordonnees[i + 1].Longitude);
            }
            return taille;
        }

        public int CompareTo(Polyline other)
        {
            return CalculLongueur().CompareTo(other.CalculLongueur());
        }

        public bool Equals(Polyline other)
        {
            return this.NbPoints == other.NbPoints;
        }
        #endregion
    }
}
