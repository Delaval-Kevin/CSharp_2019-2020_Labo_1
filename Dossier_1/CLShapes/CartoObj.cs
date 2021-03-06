﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Maps.MapControl.WPF;

namespace MyCartographyObjects
{
    [Serializable]
    public abstract class CartoObj : IIsPointClose
    {
        #region VARIABLES MEMBRES
        private int _id;
        private static int _compteur = 0;
        #endregion


        #region PROPRIETES
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private static int Compteur
        {
            get { return _compteur; }
            set { _compteur = value; }
        }
        #endregion


        #region CONSTRUCTEURS
        public CartoObj ()
        {
            Compteur++;
            Id = Compteur;
        }
        #endregion


        #region METHODES
        public override string ToString()
        {
            return "Id : " + Id;
        }

        public virtual void Draw()
        {
            Console.WriteLine(this.ToString());
        }

        public abstract bool IsPointClose(Coordonnees coorTmp, double precision);

        public abstract bool IsPointClose(Location coorTmp, double precision);

        #endregion
    }
}
