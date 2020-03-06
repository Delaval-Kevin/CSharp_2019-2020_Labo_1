using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyCartographyObjects
{
    public class BinaryFile
    {
        #region METHODES
        //Sauvegarde l'objet 'MyPersonalMapData' dans un fichier 
        public static void Save(MyPersonalMapData myData, string filename)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, myData);
            }
        }

        //Charge l'objet 'MyPersonalMapData' dans un fichier 
        public static MyPersonalMapData Load(string filename)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fstream = File.OpenRead(filename))
            {
                MyPersonalMapData DataFromDisk = (MyPersonalMapData)binFormat.Deserialize(fstream);
                return DataFromDisk;
            }

        }
        #endregion
    }
}
