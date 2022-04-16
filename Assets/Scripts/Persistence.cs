using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence {
    public static void SaveObjectToFile(object obj, string filename) {
        string json = JsonUtility.ToJson(obj);
        string path = Application.persistentDataPath + "/" + filename;
        System.IO.File.WriteAllText(path, json);
        Debug.Log("\"" + filename + "\" saved, in path: " + Application.persistentDataPath);
    }
}
