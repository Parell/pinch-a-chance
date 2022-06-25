using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private int menuScene = 1;
    [SerializeField] private int sceneToStart = 2;
    [Space]
    [SerializeField] private bool isPaused;
    [SerializeField] private bool inMainMenu = true;
    //[Space]
    //[SerializeField] private int musicToChangeTo = 0;
    [Space]
    [SerializeField] private GameObject tint;
    [SerializeField] private GameObject menuMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
        LoadManager.Instance.LoadScene(menuScene, LoadSceneMode.Single);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !isPaused && !inMainMenu)
        {
            Pause();
        }
        else if (Input.GetButtonDown("Cancel") && isPaused && !inMainMenu)
        {
            UnPause();
        }
    }

    public void Pause()
    {
        isPaused = true;

        ShowPauseMenu();
    }

    public void UnPause()
    {
        isPaused = false;

        HidePauseMenu();
    }

    public void ShowMenu()
    {
        menuMenu.SetActive(true);
    }

    public void HideMenu()
    {
        menuMenu.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        optionsMenu.SetActive(true);
        tint.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
        tint.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        tint.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        tint.SetActive(false);
    }

    // public void PlayNewMusic()
    // {
    //     playMusic.FadeUp(fastFadeIn);
    //     playMusic.PlaySelectedMusic(musicToChangeTo);
    // }

    public void StartStopGame()
    {
        StartCoroutine(OnStartStopGame());
    }

    IEnumerator OnStartStopGame()
    {
        if (inMainMenu)
        {
            StartCoroutine(LoadManager.Instance.FadeAndLoadScene(LoadManager.FadeDirection.In, sceneToStart, LoadSceneMode.Single));
            HideMenu();
        }
        else
        {
            StartCoroutine(LoadManager.Instance.FadeAndLoadScene(LoadManager.FadeDirection.In, menuScene, LoadSceneMode.Single));
            yield return new WaitForSeconds(1);
            ShowMenu();
            HidePauseMenu();
            UnPause();
        }

        inMainMenu = !inMainMenu;
    }

    public void Quit()
    {
        StartCoroutine(OnQuit());
    }

    IEnumerator OnQuit()
    {
        yield return LoadManager.Instance.Fade(LoadManager.FadeDirection.In);

        Debug.Log("Quit application");
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}