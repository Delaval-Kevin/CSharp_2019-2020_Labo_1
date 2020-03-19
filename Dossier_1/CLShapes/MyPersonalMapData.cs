using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyCartographyObjects
{
    [Serializable]
    public class MyPersonalMapData : INotifyPropertyChanged
    {
        #region VARIABLES
        private string _path = @"C:\Users\delav\Documents\2eme annee\C#\labo-phase-1-et-2-Head-Splitter\Dossier_1\Data\";
        private string _ext = ".dat";

        private string _nom;
        private string _prenom;
        private string _email;
        private ObservableCollection<ICartoObj> _liste;
        #endregion

        #region EVENT
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region PROPRIETES
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Email"));
                    }
                }
            }
        }

        public string Path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                {
                    _path = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Path"));
                    }
                }
            }
        }

        public string Ext
        {
            get { return _ext; }
            set
            {
                if (_ext != value)
                {
                    _ext = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Ext"));
                    }
                }
            }
        }

        public string Nom
        {
            get { return _nom; }
            set
            {
                if (_nom != value)
                {
                    _nom = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Nom"));
                    }
                }
            }
        }

        public string Prenom 
        {
            get { return _prenom; }
            set
            {
                if (_prenom != value)
                {
                    _prenom = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Prenom"));
                    }
                }
            }
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

        //Constructeur d'initialisation partiel
        public MyPersonalMapData(string nom, string prenom) : this(nom, prenom, "Pas d'e-mail") { }

        //Constructeur d'initialisation complet
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
            string filename = Path + Nom + Prenom + Ext;
            BinaryFile.Save(this,filename);
        }

        //Charge l'objet 'MyPersonalMapData' dans un fichier 
        public void Load()
        {
            string filename = Path + Nom + Prenom + Ext;
            MyPersonalMapData data = BinaryFile.Load(filename);
            this.Email = data.Email;
            this.Liste = data.Liste;
        }
        #endregion
    }
}