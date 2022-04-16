using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    List<GameObject> screens = new List<GameObject>();
    Game game;

    void Awake() {
        game = GameObject.Find("Game").GetComponent<Game>();
    }

    void Start() {
        GetAllScreens();
        DeactivateAllScreens();
    }

    void GetAllScreens() {
        foreach (Transform t in GameObject.Find("Screens").transform) {
            screens.Add(t.gameObject);
        }
    }

    void DeactivateAllScreens() {
        foreach (GameObject screen in screens) {
            screen.SetActive(false);
        }
    }

    public void ActivateScreen(string name) {
        foreach (GameObject screen in screens) {
            screen.SetActive(false);
            if (screen.name == name) {
                screen.SetActive(true);
            }
        }
    }

    public void StartGame() {
        DeactivateAllScreens();
        game.StartGameFromLevel("TestLevel");
    }
}
