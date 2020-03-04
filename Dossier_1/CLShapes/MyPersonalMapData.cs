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
        //Constructeur par défaut
        public MyPersonalMapData() : this("Pas de nom", "Pas de prénom", "Pas d'e-mail") { }

        //Constructeur d'initialisation
        public MyPersonalMapData(string nom, string prenom, string email)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
        }
        #endregion

        #region METHODES
        //Sauvegarde l'objet 'MyPersonalMapData' dans un fichier 
        private static void Save(MyPersonalMapData jbc, string filename)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, jbc);
            }
        }

        //Charge l'objet 'MyPersonalMapData' dans un fichier 
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