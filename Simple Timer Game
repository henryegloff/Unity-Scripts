
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{

    public static GameSystem gameSystem;
    public static TimeSpan levelRecord;
    public static bool gamePaused = true;
    public static bool levelActive = false;
    public static bool initalStart = true;

    public Text fastestTimeUI;
    public Text timeUI;


    // Timer
    public static TimeSpan timePlaying;
    public static bool timerGoing;
    public static float elapsedTime;

    //Animators   
    public Animator startScreenAnimator;
    public Animator pauseScreenAnimator;
    public Animator gameOverScreenAnimator;


    // public Text fastestTimeUI;


    void Start()
    {
        gameSystem = this;

        if(initalStart)
        {
            Time.timeScale = 0f;
            startScreenAnimator.SetBool("Active", true);
            initalStart = false; 
        }
        else {
            Time.timeScale = 1;
            BeginTimer();
            gamePaused = false;
            levelActive = true;
        }
    }

    void Update()
    {
        
            if (levelActive) // Only when level is being played
            {
              // Toggle Pause
              if (Input.GetKeyUp(KeyCode.Escape))
              {
                TogglePause();

                Debug.Log("Pause Toggled");


              }

            //Test level complete *** Comment out in final build
              if (Input.GetKeyUp(KeyCode.Y))
              {
                GameOver();
              }


          }
        
    }




    public void PlayGame() 
    {
        Time.timeScale = 1;
        startScreenAnimator.SetBool("Active", false);
        BeginTimer();
        levelActive = true;
        gamePaused = false;
    }


    public void BeginTimer() 
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timeUI.text = timePlayingStr;
            yield return null;
        }
    }



    public void TogglePause() 
    {
        if(!gamePaused)
        {
            pauseScreenAnimator.SetBool("Active", true);
            Time.timeScale = 0f;
            timerGoing = false;
            gamePaused = true;
        }

        else 
        {
            pauseScreenAnimator.SetBool("Active", false);
            Time.timeScale = 1;
            timerGoing = true;
            StartCoroutine(UpdateTimer());
            gamePaused = false;
        }
    }
  

    public void Replay() 
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        Start(); 
    }

    public void Quit() 
    {
        UnityEditor.EditorApplication.isPlaying = false; // *** Remove in final build
        Application.Quit();
    }

    public void GameOver() 
    {
        gameOverScreenAnimator.SetBool("Active", true);
        Time.timeScale = 0f;
        gamePaused = true;
        timerGoing = false;
    }

    


}
