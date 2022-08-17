using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{

    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button backButton;
    public Button quitButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    public GameObject gameOver;
    public GameObject victory;

    [Header("Text")]
    public Text volSliderText;

    [Header("Slider")]
    public Slider volSlider;

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        gameOver.SetActive(false);
        victory.SetActive(false);
    }

    public void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        gameOver.SetActive(false);
        victory.SetActive(false);
        settingsMenu.SetActive(true);
        
    }

    public void ShowgameOver()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        victory.SetActive(false);
        gameOver.SetActive(true);
    }

    public void Showvictory()
    {
        gameOver.SetActive(false);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        victory.SetActive(true);
    }

    void OnSliderValueChanged(float value)
    {
        if (volSliderText)
            volSliderText.text = value.ToString();
    }

    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(() => StartGame());

        if (settingsButton)
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());

        if (backButton)
            backButton.onClick.AddListener(() => ShowMainMenu());

        if (volSlider)
        {
            volSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value));
            if (volSliderText)
                volSliderText.text = volSlider.value.ToString();

        }
        if (quitButton)
            quitButton.onClick.AddListener(() => QuitGame());

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => ReturnToMenu());

        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(() => ReturnToGame());
    }
    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);


                if (pauseMenu.activeSelf)
                {
                    Time.timeScale = 0;
                    GameManager.instance.playerInstance.GetComponent<Character>().enabled = false;
                    GameManager.instance.playerInstance.GetComponent<Projectile>().enabled = false;
                }
                else
                {
                    Time.timeScale = 1;
                    GameManager.instance.playerInstance.GetComponent<Character>().enabled = true;
                    GameManager.instance.playerInstance.GetComponent<Projectile>().enabled = true;

                }
            }
        }
    }

    public void ReturnToGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameManager.instance.playerInstance.GetComponent<Character>().enabled = true;
        GameManager.instance.playerInstance.GetComponent<Projectile>().enabled = true;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}