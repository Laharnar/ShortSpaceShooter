using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour {

    public static GameplayManager m;

    public Text scoreUi;
    public Score score;
    public Unit playerUnit;// reference to player, note: reference is lost when player is destroyed.

    GameState gameState;
    public enum GameState {
        Running,
        Paused,
        Win,
        Lose,
        ExitMenu,
    }

    void Awake() {
        m = this;

        StartGame();
    }

    internal static void AddScore(int amount) {
        m.score.AddScore(amount);
        m.score.UpdateUi(m.scoreUi);
    }

    void Update() {
        switch (gameState) {
            case GameState.Running:
                // win and lose check
                if (playerUnit.isDead) {
                    Debug.Log("Player dead ...");
                    SetGameplayState(GameState.Lose);
                    playerUnit.Explode(Vector2.zero);
                } else if (score.IsGoalMet()) {
                    Debug.Log("Score achieved!");
                    SetGameplayState(GameState.Win);
                }

                // pause game
                else if (Input.GetKeyDown(KeyCode.P)) {
                    Debug.Log("Pause.");
                    SetGameplayState(GameState.Paused);
                    Pause();
                } else if (Input.GetKeyDown(KeyCode.Escape)) {
                    Debug.Log("Exit menu.");
                    SetGameplayState(GameState.ExitMenu);
                    Pause();
                }
                break;
            case GameState.Paused:
                // unpause
                if (Input.GetKeyDown(KeyCode.P)) {
                    Debug.Log("Unpause!");
                    SetGameplayState(GameState.Running);
                    UnPause();
                }
                break;
            case GameState.ExitMenu:
                // unpause
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    Debug.Log("Resume.");
                    UnPause();
                    SetGameplayState(GameState.Running);
                }
                break;
            default:
                break;
        }
    }

    public void SetGameplayState(GameState newState) {
        // Open required menus once
        this.gameState = newState;
        // game loop logic
        switch (gameState) {
            case GameState.Win:
                // show win menu
                MenuManager.m.ShowMenu("win");
                break;
            case GameState.Lose:
                // show lose menu
                MenuManager.m.ShowMenu("lose");
                break;
            case GameState.ExitMenu:
                MenuManager.m.ShowMenu("exit");
                break;
            case GameState.Running:
                MenuManager.m.ShowMenu("gameplay");
                break;
            case GameState.Paused:
                MenuManager.m.ShowMenu("pause");
                break;
            default:
                Debug.LogError("Unhandled state "+gameState);
                break;
        }
    }

    internal void StartGame() {
        print("whooo       (starting game)");
        gameState = GameState.Running;

        if (playerUnit == null) {
            Debug.LogWarning("Set player", this);
            playerUnit= GameObject.Find("Player").GetComponent<Unit>();
        }
    }

    internal static void Pause() {
        Time.timeScale = 0;
    }

    public static void UnPause() {
        Time.timeScale = 1;
    }
}
