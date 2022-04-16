using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelAreaData", menuName = "ScriptableObjects/LevelAreaData", order = 1)]
public class LevelAreaData : ScriptableObject {
    public float width;
    public float height;
    public float wallThickness;
}
