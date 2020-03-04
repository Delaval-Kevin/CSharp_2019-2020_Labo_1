using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyCartographyObjects
{
    public class MyPersonalMapData
    {
        #region VARIABLES
        private string _nom;
        private string _prenom;
        private string _email;
        #endregion

        #region PROPRIETES
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public string Prenom 
        {
            get { return _prenom; }
            set { _prenom = value; }
        }
        #endregion

        #region CONSTRUCTEURS

        #endregion

        #region METHODES
        private static void Save(MyPersonalMapData jbc, string filename)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, jbc);
            }
        }
        private static void Load(string filename)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fstream = File.OpenRead(filename))
            {
                MyPersonalMapData DataFromDisk = (MyPersonalMapData)binFormat.Deserialize(fstream);
                Console.WriteLine("\nFichier bien chargé");
            }
        }
        #endregion
    }
}