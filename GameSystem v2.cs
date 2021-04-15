
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class GameSystem : MonoBehaviour
{


    // Game System
	public static GameSystem gameSystem;

    // Initial Settings
	public static bool gamePaused = true;
	public static bool audioEnabled = true;
	public static bool controlsEnabled = true;
	public static bool startScreen = true;

    // Animators
	public Animator screenBackgroundAnimator;
	public Animator startScreenAnimator;
    public Animator startScreenBackground;
	public Animator pauseScreenAnimator;
	public Animator levelScreenAnimator;
    public Animator controlsAnimator;
    public Animator toggleControlsAnimator;

    // UI Elements
    public Text scoreUI;
    public Text topScoreUI;
    public Text timeUI;
    public Text fastestTimeUI;
    public Text energyUI;
    public Text levelUI;

    // Audio Button
    public GameObject AudioButton;
    public Sprite AudioSpriteOn;
    public Sprite AudioSpriteOff;

    // Timer
    public static TimeSpan timePlaying;
    public static bool timerGoing;
    public static float elapsedTime;

    // Game Values
    public static float energy = 100;
    public static int score = 0;

    /* 

    All level soundtracks are set to play on awake
    When a level resets it turns the volume on or off 
    depending on the boolean value of 'audioEnabled'

    */


    void Start()
    {
        gameSystem = this;

        // Initial Start Screen
        if(startScreen)
        {
            Time.timeScale = 0f;

            screenBackgroundAnimator.SetBool("Start", true);
            startScreenBackground.SetBool("StartBackground", true);
            startScreenAnimator.SetBool("Active", true);
            controlsAnimator.SetBool("Active", true);
            toggleControlsAnimator.SetBool("Active", true);
            AudioButton.GetComponent<Image>().sprite = AudioSpriteOn;
            startScreen = false; 
            /* 
            Once the game has "started" we won't see the start screen
            again when we replay or load new levels etc
            */
        } else {       

            Time.timeScale = 1;
            gamePaused = false;
            AudioListener.volume = 1f;
            GameSystem.energy = 100;
            GameSystem.score = 0;
            BeginTimer(); 

            if(controlsEnabled)
            {
                controlsAnimator.SetBool("Active", true);
                toggleControlsAnimator.SetBool("Active", true);
            }

            if(!audioEnabled)
            {
                AudioListener.volume = 0f; 
                AudioButton.GetComponent<Image>().sprite = AudioSpriteOff;
            }
        }
    }



    void Update()
    {   
        if (Input.GetKeyUp(KeyCode.Escape))
        {
           TogglePause();
        }

        // Test energy
        if (Input.GetKeyUp(KeyCode.RightBracket))
        {
           energy += 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftBracket))
        {
           energy -= 1;
        }

        // Test score
        if (Input.GetKeyUp(KeyCode.Y))
        {
           score += 1;
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
           score -= 1;
        }


        if (energy < 1)
        {
           RestartLevel();
        }

        // Update UI
        levelUI.text = SceneManager.GetActiveScene().name;
        scoreUI.text = "Score: " + score.ToString();
        energyUI.text = energy.ToString()+"%";
        
        

    }



    public void PlayGame() // When game is played from Start Screen
    {
        startScreenAnimator.SetBool("Active", false); 
        screenBackgroundAnimator.SetBool("Start", false);
        startScreenBackground.SetBool("StartBackground", false);
        Time.timeScale = 1;
        gamePaused = false;
        timerGoing = true;
        BeginTimer();
    }




    /* Timer
    -------------------------------*/

    public void BeginTimer() 
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTime()
    {
        timerGoing = false;
        Debug.Log("Timer Ended");
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr= "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeUI.text = timePlayingStr;
            yield return null;
        }
    }




    /* Toggles
    -------------------------------*/

	public void ToggleAudio()
    {
        if(!audioEnabled)
        {
            AudioListener.volume = 1f; 
            AudioButton.GetComponent<Image>().sprite = AudioSpriteOn;

        }
        else
        {
            AudioListener.volume = 0f;
            AudioButton.GetComponent<Image>().sprite = AudioSpriteOff;
            
        }
        audioEnabled = !audioEnabled;
        Debug.Log("Toggled Audio Enabled = " + audioEnabled);
    }



    public void ToggleControls() {
        if(!controlsEnabled)
        {
            controlsAnimator.SetBool("Active", true);
            toggleControlsAnimator.SetBool("Active", true);
        }
        else
        {
            controlsAnimator.SetBool("Active", false);
            toggleControlsAnimator.SetBool("Active", false);
        }
        controlsEnabled = !controlsEnabled;
        Debug.Log("Toggled Controls Enabled = " + controlsEnabled);
    }


    public void TogglePause()
    {
        if(gamePaused == false)
        {
            pauseScreenAnimator.SetBool("Active", true);
            screenBackgroundAnimator.SetBool("Active", true);
            Time.timeScale = 0f;
            gamePaused = true;
            timerGoing = false;
        }

        else 
        {
            screenBackgroundAnimator.SetBool("Active", false);
            pauseScreenAnimator.SetBool("Active", false);
            levelScreenAnimator.SetBool("Active", false);
            startScreenAnimator.SetBool("Active", false);
            Time.timeScale = 1;
            gamePaused = false;
            timerGoing = true;
            StartCoroutine(UpdateTimer());
        }
    }




    /* Levels & Quit
    -------------------------------*/

    public void RestartLevel()
    {
    	Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        Start(); 
    }

    public void SelectLevel()
    {
    	pauseScreenAnimator.SetBool("Active", false);
    	startScreenAnimator.SetBool("Active", false);
    	levelScreenAnimator.SetBool("Active", true);
    }

    public void LoadLevel()
    {
        var buttonName = EventSystem.current.currentSelectedGameObject.name;
        /* 
        Gets the name of button clicked 
        (the button name needs to match the name of scene to be loaded)
        */
        levelScreenAnimator.SetBool("Active", false);
        SceneManager.LoadScene(buttonName);
        Start(); 
    }

    public void Quit()
    {
    	UnityEditor.EditorApplication.isPlaying = false; // * Remove for final build
    	Application.Quit();
    }


}
