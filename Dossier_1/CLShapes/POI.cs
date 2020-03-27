using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyCartographyObjects
{
    [Serializable]
    public class POI : Coordonnees, ICartoObj
    {
        #region VARIABLES MEMBRES
        private string _description;
        #endregion


        #region PROPRIETES
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion


        #region CONSTRUCTEURS
        //Constructeur par défaut
        public POI() : this(50.620796, 5.581418, "HEPL") { }

        //Constructeur d'initialisation
        public POI(double latitude, double longitude, string description) : base(latitude, longitude)
        {
            Description = description;
        }
        #endregion


        #region METHODES
        public override string ToString()
        {
            return  "POI : " + Description;
        }
        #endregion
    }
}
