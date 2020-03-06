using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MathFunctions;

namespace MyCartographyObjects
{
    [Serializable]
    public class Polyline : CartoObj, IPointy, ICartoObj, IComparable<Polyline>, IEquatable<Polyline>
    {
        #region VARIABLES MEMBRES
        private List<Coordonnees>   _coordonnees;
        private Color               _couleur;
        private int                 _epaisseur;
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
            get { return _couleur; }
            set { _couleur = value; }
        }

        public int NbPoints
        {
            get { return Coordonnees.Count; }
        }
        #endregion


        #region CONSTRUCTEURS
        //Constructeur par défaut
        public Polyline() : this(null, Colors.Black, 1) { }

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
            string chaine = base.ToString() + " Epaisseur : " + Epaisseur + " Couleur : " + Couleur + " Longueur : " + CalculLongueur() + " Nombre de points : " + NbPoints +" Liste : ";

            if (!Coordonnees.Any())
            {
                chaine += "VIDE !";
            }
            else
            {
                foreach (Coordonnees coo in Coordonnees)
                {
                    chaine += ("\n\t - " + coo.ToString());
                }
            }

            return chaine;
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

        //Calcule la longueur totale des segments de la Polyline
        public double CalculLongueur()
        {
            double taille = 0;

            for (int i = 0; i < Coordonnees.Count - 1; i++)
            {
                taille = taille + MathUtil.Dist2Points(Coordonnees[i].Latitude, Coordonnees[i].Longitude, Coordonnees[i + 1].Latitude, Coordonnees[i + 1].Longitude);
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
