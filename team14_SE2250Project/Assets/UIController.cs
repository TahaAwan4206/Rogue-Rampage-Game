using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Image fadeScreen;
    private bool fadingToBlack, fadingFromBlack;
    public float fadeSpeed = 2f;
    public string mainMenuScene;
    public GameObject pauseScreen;

    private void Start()
    {
        updateHealth(PlayerHealthController.instance.maxHealth, PlayerHealthController.instance.maxHealth);
    }
    private void Awake()
    {
        pauseScreen.SetActive(false);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // instance = this;
    }
    public Slider health;

    // Update is called once per frame
    void Update()
    {
        if (fadingToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                fadingToBlack = false;
            }
        }
        else if (fadingFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                fadingFromBlack = false;
            }
        }





        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }


public void updateHealth(int currentHealth, int maxHealth)
    {
        health.maxValue = maxHealth;
        health.value = currentHealth;
    }

    public void StartFadeToBlack()
    {
        fadingToBlack = true;
        fadingFromBlack = false;
    }

    public void StartFadeFromBlack()
    {
        fadingFromBlack = true;
        fadingToBlack = false;
    }

    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;
        }

    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        Destroy(PlayerHealthController.instance.gameObject);
        PlayerHealthController.instance = null;

        Destroy(RespawnController.instance.gameObject);
        RespawnController.instance = null;

        Destroy(MapController.instance.gameObject);
        MapController.instance = null;

        instance = null;
        Destroy(gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }

   

    
}

