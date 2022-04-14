using UnityEngine;

public class Game : MonoBehaviour {
    UIManager uiManager;
    LevelManager levelManager;
    Player player;

    public bool startFromTitleScreen;
    public string startingLevel;

    private void Awake() {
        
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Start() {
        TestBench.Test();
        
        if (startFromTitleScreen) {
            StartGameFromTitleScreen();
        } else {
            StartGameFromLevel(startingLevel);
        }
    }

    public void EndGame(GameResult gameResult) {
        if (gameResult == GameResult.Win) {
            uiManager.ActivateScreen("YouWin");
        } else if (gameResult == GameResult.Lose) {
            uiManager.ActivateScreen("GameOver");
        }
    }

    public void StartGameFromTitleScreen() {
        uiManager.ActivateScreen("TitleScreen");
    }

    public void StartGameFromLevel(string startingLevel) {
        uiManager.ActivateScreen("HUD");
        player.Reset();
        levelManager.StartLevel(startingLevel);
    }
}