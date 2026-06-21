using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance {get; private set;}

    private static int levelNumber = 1;
    private static float totalScore = 0f;

  [SerializeField] private List<GameLevel> gameLevelList;
  [SerializeField] private CinemachineCamera cinemachineCamera;

  private int score;
  private float time;
  private bool isTimerActive;

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

  private void Awake()
    {
        Instance = this;
    }
  private void Start()
    {
        Lander.Instance.OnCoinPickup += Lander_OnCoinPickup;
        Lander.Instance.OnLanded += Lander_OnLanded;
        Lander.Instance.OnStateChanged += Lander_OnStateChanged;
        GameInput.Instance.OnMenuButtonPressed += GameInput_OnMenuButtonPressed;
        LoadCurrentLevel();
    }
    public static void ResetStaticData()
    {
        levelNumber = 1;
        totalScore = 0;

    }

    private void GameInput_OnMenuButtonPressed(object sender, System.EventArgs e)
    {
        PauseGame();
    }
    private void Update()
    {
        if (isTimerActive)
        {
            time += Time.deltaTime;
            return;
        }
    }

    private void LoadCurrentLevel()
    {
        GameLevel gameLevel = GetGameLevel();
        GameLevel spawnedGameLevel = Instantiate(gameLevel, Vector3.zero, Quaternion.identity);
        Lander.Instance.transform.position = spawnedGameLevel.GetLanderStartPosition();
        cinemachineCamera.Target.TrackingTarget = spawnedGameLevel.GetCameraStartTargetTransform();
        CinemachineCameraZoom2D.Instance.setTargetOrthographicSize(spawnedGameLevel.GetZoomOutOrthographicSize());
    }
    private GameLevel GetGameLevel()
    {
        foreach(GameLevel gameLevel in gameLevelList)
        {
            if(gameLevel.GetLevelNumber() == levelNumber)
            {
                return gameLevel;
            }
        }
        return null;
    }

    private void Lander_OnCoinPickup(object sender, EventArgs e)
    {
        AddScore(500);
    }

     private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnStateChanged(object sender, Lander.OnStateChangedEventArgs e)
    {
        isTimerActive = e.state == Lander.State.Normal;

        if(e.state == Lander.State.Normal)
        {
            cinemachineCamera.Target.TrackingTarget = Lander.Instance.transform;
            CinemachineCameraZoom2D.Instance.setNormalOrthographicSize();
        }
    }

  public void AddScore(int scoreToBeAdded)
    {
        score += scoreToBeAdded;
    }

    public int GetScore()
    {return score;}

    public float GetTime()
    {
        return time;
    }

    public void GoToNextLevel()
    {
        levelNumber++;
        totalScore += score;
        if(GetGameLevel() == null)
        {
            // load gameover
            SceneLoader.LoadScene(SceneLoader.Scene.GameOverScene);
        }
        else
            //We still have more levels
            SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }
    public void RetryLevel()
    {
        SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
    }
    public float GetTotalScore()
    {
        return totalScore;
    }

    public int GetLevelNumber()
    {
        return levelNumber;
    }

    public void PauseUnpauseGame()
    {
        if(Time.timeScale == 1f)
        {
            PauseGame();
        }
        else
        UnPauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }
     public void UnPauseGame()
    {
        Time.timeScale = 1f;
        OnGameUnPaused?.Invoke(this, EventArgs.Empty);
    }
}
