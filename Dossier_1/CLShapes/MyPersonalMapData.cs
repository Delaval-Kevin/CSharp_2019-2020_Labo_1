using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyCartographyObjects
{
    [Serializable]
    public class MyPersonalMapData
    {
        #region VARIABLES
        private string _path = @"Z:\Documents\2eme annee\C#\labo-phase-1-et-2-Head-Splitter\Dossier_1\Data\";
        private string _dat = ".dat";

        private string _nom;
        private string _prenom;
        private string _email;
        private ObservableCollection<ICartoObj> _liste;
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

        public ObservableCollection<ICartoObj> Liste
        {
            get { return _liste; }
            private set { _liste = value; }
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
            _liste = new ObservableCollection<ICartoObj>();
        }
        #endregion

        #region METHODES
        //Sauvegarde l'objet 'MyPersonalMapData' dans un fichier 
        public void Save()
        {
            string filename = _path + Nom + Prenom + _dat;
            BinaryFile.Save(this,filename);
        }

        //Charge l'objet 'MyPersonalMapData' dans un fichier 
        public void Load()
        {
            string filename = _path + Nom + Prenom + _dat;
            MyPersonalMapData data = BinaryFile.Load(filename);
            this.Email = data.Email;
            this.Liste = data.Liste;
        }
        #endregion
    }
}