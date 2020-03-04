using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MathFunctions;

namespace MyCartographyObjects
{
    public class Polygon : CartoObj, IPointy
    {
        #region VARIABLES MEMBRES
        private List<Coordonnees>   _coordonnees;
        private Color               _couleur_contour;
        private Color               _couleur_remplissage;
        private double              _opacite;
        #endregion


        #region PROPRIETES
        public List<Coordonnees> Coordonnees
        {
            get { return _coordonnees; }
            set { _coordonnees = value; }
        }

        public Color CouleurContour
        {
            get { return _couleur_contour; }
            set { _couleur_contour = value; }
        }

        public Color CouleurRemplissage
        {
            get { return _couleur_remplissage; }
            set { _couleur_remplissage = value; }
        }

        public double Opacite
        {
            get { return _opacite; }
            set { _opacite = value; }
        }

        public int NbPoints
        {
            get { return Coordonnees.Count; }
        }
        #endregion


        #region CONSTRUCTEURS
        //Constructeur par défaut
        public Polygon() : this(null, Colors.White, Colors.Black, 0) { }

        //Constructeur d'initialisation
        public Polygon(Coordonnees coordonnees, Color couleurContour, Color couleurRemplissage, double opacite)
        {
            Coordonnees = new List<Coordonnees>();
            if (coordonnees != null)
            {
                Coordonnees.Add(coordonnees);
            }
                CouleurRemplissage = couleurRemplissage;
            CouleurContour = couleurContour;
            Opacite = opacite;
        }
        #endregion


        #region METHODES
        public override string ToString()
        {
            string chaine = base.ToString() + " Couleur de remplissage : " + CouleurRemplissage +
                " Couleur de contour : " + CouleurContour + " Opacite : " + Opacite + " Nombre de points : " + NbPoints + " Liste : ";
           
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

        //Vérifie si la coordonnée reçue en paramètre est proche du Polygon selon la précisoin donnée
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
            double x0, y0, x1, x2, y1, y2;

            x0 = coorTmp.Latitude;
            y0 = coorTmp.Longitude;

            for (int i = 0; i < Coordonnees.Count - 1; i++)
            {

                x1 = Coordonnees[i].Latitude;
                y1 = Coordonnees[i].Longitude;
                x2 = Coordonnees[i + 1].Latitude;
                y2 = Coordonnees[i + 1].Longitude;

                if (MathUtil.Dist2Points(x1, y1, x0, y0) <= precision)
                    return true;

                if (MathUtil.Dist2Points(x2, y2, x0, y0) <= precision)
                    return true;

                if (MathUtil.DistPointSeg(x1, y1, x2, y2, x0, y0) <= precision)
                    return true;
            }

            x1 = Coordonnees[0].Latitude;
            y1 = Coordonnees[0].Longitude;
            x2 = Coordonnees[Coordonnees.Count - 1].Latitude;
            y2 = Coordonnees[Coordonnees.Count - 1].Longitude;

            if (MathUtil.DistPointSeg(x1, y1, x2, y2, x0, y0) <= precision)
                return true;

            if (InBoundingBox(coorTmp))
                return true;

            return false;
        }

        //Vérifie si le point est dans la BoundingBox
        public bool InBoundingBox(Coordonnees coorTmp)
        {
            double xMin, xMax, yMin, yMax;

            xMin = Coordonnees[0].Latitude;
            xMax = Coordonnees[0].Latitude;
            yMin = Coordonnees[0].Longitude;
            yMax = Coordonnees[0].Longitude;

            foreach (Coordonnees coor in Coordonnees)
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

            if (coorTmp.Latitude >= xMin && coorTmp.Latitude <= xMax && coorTmp.Longitude >= yMin && coorTmp.Longitude <= yMax)
                return true;

            return false;
        }
        #endregion
    }
}
