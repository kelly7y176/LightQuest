using System.Collections;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Playing,
    Pause,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameObject MainMenuUi;
    public GameObject InGameMenuUi;
    public GameObject PauseMenuUi;
    public GameObject GameOverMenuUi;

    public int delay = 1;
    public static GameState CurrentState {  get; private set; }
    private void Awake()
    {
       if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);  
        }
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    // Update is called once per frame
    public void ChangeState(GameState newState)
    {
       // if (CurrentState == newState) return;
        StartCoroutine(TransitionToState(newState));
        
    }

    public void ChangeToMainMenu()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeToPlayingu()
    {
        ChangeState(GameState.Playing);
    }

    public void ChangeToPause()
    {
        ChangeState(GameState.Pause);
    }

    public void ChangeToGameOver()
    {
        ChangeState(GameState.GameOver);
    }

    private IEnumerator TransitionToState(GameState newState)
    {
        if(newState != GameState.MainMenu)
        yield return new WaitForSecondsRealtime(delay);

        CurrentState = newState;
        HeadleStateChange();
    }

    private void HeadleStateChange()
    {
        HideAllMenu();
        switch (CurrentState)
        {
            case GameState.MainMenu:
                Time.timeScale = 0;
                MainMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                InGameMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.inGameMusic);
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                PauseMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                GameOverMenuUi.SetActive(true);
                AudioManager.instance.PlayMusic(AudioManager.instance.menuMusic);
                break;
        }
    }

    private void HideAllMenu()
    {
        MainMenuUi.SetActive(false);
        InGameMenuUi.SetActive(false);
        PauseMenuUi.SetActive(false);
        GameOverMenuUi.SetActive(false);
    }
}
