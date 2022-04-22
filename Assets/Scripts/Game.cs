using UnityEngine;

public class Game : MonoBehaviour {
    UIManager uiManager;
    LevelManager levelManager;

    public bool automaticTestMode = false;
    public bool startFromTitleScreen;
    public string startingLevel;

    private void Awake() {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    void Start() {        
        if (startFromTitleScreen) {
            StartGameFromTitleScreen();
        } else {
            StartRandomizedLevel();
            //StartGameFromLevel(startingLevel);
        }
    }

    public void EndGame(GameResult gameResult) {
        if (gameResult == GameResult.Win) {
            uiManager.ActivateScreen("YouWin");
        } else if (gameResult == GameResult.Lose) {
            uiManager.ActivateScreen("GameOver");
        }
    }

    public void NextLevel() {
        uiManager.ActivateScreen("NextLevel");
        GameObject.Find("NextLevel").GetComponent<NextLevel>().toBlack = true;
    }

    public void StartGameFromTitleScreen() {
        uiManager.ActivateScreen("TitleScreen");
    }

    public void StartRandomizedLevel() {
        uiManager.ActivateScreen("HUD");
        levelManager.RemoveLevel();

        // fires bullet
        levelManager.GetComponent<LevelConstructor>().Spawn();
    }

    public void StartGameFromLevel(string startingLevel) {
        uiManager.ActivateScreen("HUD");
        levelManager.StartLevel(startingLevel);
    }
}