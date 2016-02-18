using UnityEngine;
using System.Collections;

public class TiledFactory {
    public static void generateTiled(string levelName)
    {
        string filePath = "somethingFolder/" + levelName; 
        TextAsset file = Resources.Load(filePath) as TextAsset;

        
    }
}
