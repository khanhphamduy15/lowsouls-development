using UnityEngine;
using System;
using System.IO;
using UnityEditor.Overlays;

namespace LS
{
    public class SaveFileDataWriter
    {
        public string saveDataDirectoryPath = "";
        public string saveFileName = "";

        //before create, make sure character slot exist
        public bool CheckIfFileExist()
        {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //del char save file
        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
        }

        //create char save file (upon starting new game)
        public void CreateNewCharSaveFile(CharacterSaveData characterData)
        {
            //make save path
             string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

            try
            {
                //Create directory the file will be written to, if not exist
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("CREATING SAVE FILE AT PATH : " + savePath);

                //serialize the c# game data object to json
                string dataToStore = JsonUtility.ToJson(characterData, true);

                //write file to sys
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("ERROR WHILE TRYING TO SAVE, GAME NOT SAVED " + savePath + "\n" + e);
            }
        }   

         //load save file from previous game
         public CharacterSaveData LoadSaveFile()
        {
            CharacterSaveData characterData = null;
            //make load path
            string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

            if (File.Exists(loadPath))
            {
                try {
                    string dataToLoad = "";
                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            dataToLoad = reader.ReadToEnd();
                        }
                    }
                    //deserialize the data from json to unity
                    characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("ERROR WHILE TRYING TO LOAD, GAME NOT LOADED " + loadPath + "\n" + e);
                }
            }

            return characterData;   
        }
    }
}
