using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour {
    public GameObject[] levels;
    public Player player;
    public Bullet bullet;
    public ExitZone exitZone;
    public PlayerStartingPos playerStartingPos;
    public LevelArea levelArea;
    public Hugger hugger;
    public Snark snark;

    void Awake() {
        Prefabs.levels = levels;
        Prefabs.player = player;
        Prefabs.bullet = bullet;
        Prefabs.exitZone = exitZone;
        Prefabs.playerStartingPos = playerStartingPos;
        Prefabs.levelArea = levelArea;
        Prefabs.hugger = hugger;
        Prefabs.snark = snark;
    }
}

public class Prefabs {
    public static GameObject[] levels;
    public static Player player;
    public static Bullet bullet;
    public static ExitZone exitZone;
    public static PlayerStartingPos playerStartingPos;
    public static LevelArea levelArea;
    public static Hugger hugger;
    public static Snark snark;
    public static GameObject activeLevel = null;
}
