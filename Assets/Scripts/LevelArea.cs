using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelArea : MonoBehaviour {
    SpriteRenderer sr;

    public LevelAreaData levelAreaData;

    public void Awake() {
        Transform wallWest = transform.Find("WallWest");
        wallWest.localPosition = new Vector3(-(levelAreaData.width / 2 + levelAreaData.wallThickness / 2), 0, 0);
        wallWest.localScale = new Vector3(levelAreaData.wallThickness, levelAreaData.height + levelAreaData.wallThickness * 2, 1);

        Transform wallEast = transform.Find("WallEast");
        wallEast.localPosition = new Vector3(levelAreaData.width / 2 + levelAreaData.wallThickness / 2, 0, 0);
        wallEast.localScale = new Vector3(levelAreaData.wallThickness, levelAreaData.height + levelAreaData.wallThickness * 2, 1);

        Transform wallNorth = transform.Find("WallNorth");
        wallNorth.localPosition = new Vector3(0, levelAreaData.height / 2 + levelAreaData.wallThickness / 2, 0);
        wallNorth.localScale = new Vector3(levelAreaData.width + levelAreaData.wallThickness * 2, levelAreaData.wallThickness, 1);

        Transform wallSouth = transform.Find("WallSouth");
        wallSouth.localPosition = new Vector3(0, -(levelAreaData.height / 2 + levelAreaData.wallThickness / 2), 0);
        wallSouth.localScale = new Vector3(levelAreaData.width + levelAreaData.wallThickness * 2, levelAreaData.wallThickness, 1);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.size = new Vector2(levelAreaData.width, levelAreaData.height);
    }

    public void Start() {
        
    }
}
