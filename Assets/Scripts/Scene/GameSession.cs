using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public int HighScore { get; set; } = 0;

    public TMP_Text healthUI;
    public TMP_Text scoreUI;
    public TMP_Text highScoreUI;
    //public TMP_Text livesUI;

    public GameObject gameOverScreen;
    public GameObject winGameScreen;

    GameObject playChar;
    Player player;
    bool gameWon = false;

    static GameSession instance = null;
    public static GameSession Instance
    {
        get
        {
            return instance;
        }
    }

    public enum eState
    {
        Load,
        StartSession,
        Session,
        EndSession,
        GameOver,
        WinGame
    }

    public eState State { get; set; } = eState.Load;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HighScore = GameController.Instance.highScore;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameController.Instance.OnPause();
        }

        switch (State)
        {
            case eState.Load:
                Score = 0;
                if (highScoreUI != null) highScoreUI.text = string.Format("{0:D4}", HighScore);
                playChar = GameObject.FindGameObjectWithTag("Player");
                player = playChar.GetComponent<Player>();
                State = eState.StartSession;
                break;
            case eState.StartSession:
                if (gameOverScreen != null) gameOverScreen.SetActive(false);
                if (winGameScreen != null) winGameScreen.SetActive(false);
                GameController.Instance.transition.StartTransition(Color.clear, 1);
                State = eState.Session;
                break;
            case eState.Session:
                if (healthUI != null)
                {
                    healthUI.text = $"Health: {player.m_health.currentHealth} / {player.m_health.maxHealth}";
                }
                CheckDeath();
                break;
            case eState.EndSession:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (gameWon) State = eState.WinGame;
                else State = eState.GameOver;
                break;
            case eState.GameOver:
                if (gameOverScreen != null) gameOverScreen.SetActive(true);
                break;
            case eState.WinGame:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (winGameScreen != null) winGameScreen.SetActive(true);
                break;
            default:
                break;
        }        
    }

    public void AddPoints(int points)
    {
        Score += points;
        if (scoreUI != null) scoreUI.text = string.Format("{0:D4}", Score);

        SetHighScore();
    }

    public void QuitToMainMenu()
    {
        GameController.Instance.OnLoadMenuScene("MainMenu");
    }

    private void SetHighScore()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
            GameController.Instance.SetHighScore(HighScore);
            if (highScoreUI != null) highScoreUI.text = string.Format("{0:D4}", HighScore);
        }
    }

    private void CheckDeath()
    {
        if (player.m_isDead)
        {
            State = eState.EndSession;
            playChar.SetActive(false);
            playChar = null;
            player = null;
        }
    }
}
