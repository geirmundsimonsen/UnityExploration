using UnityEngine;

public class LevelManager : MonoBehaviour {
    public void StartLevel(string name) {
        RemoveLevel();

        foreach (GameObject level in Prefabs.levels) {
            if (level.name == name) {
                GameObject newLevel = Instantiate(level);
                newLevel.name = "ActiveLevel";
            }
        }
    }

    void RemoveLevel() {
        GameObject activeLevel = GameObject.Find("ActiveLevel");
        if (activeLevel != null) {
            Destroy(activeLevel);
        }
    }
}
