using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour {
    public GameObject[] levels;
    public Player player;
    public Bullet bullet;

    void Awake() {
        Prefabs.levels = levels;
        Prefabs.player = player;
        Prefabs.bullet = bullet;
    }
}

public class Prefabs {
    public static GameObject[] levels;
    public static Player player;
    public static Bullet bullet;
}
