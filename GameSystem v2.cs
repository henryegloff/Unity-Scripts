
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
  public static bool levelActive = false;

  // Screen Animators
  public Animator screenBackgroundAnimator;
  public Animator startScreenAnimator;
  public Animator startScreenBackground;
  public Animator pauseScreenAnimator;
  public Animator levelScreenAnimator;
  public Animator levelCompletedScreenAnimator;
  public Animator gameOverScreenAnimator;
  public Animator controlsAnimator;

  // General UI Elements
  public Text levelUI;
  public Text timeUI;
  public Text fastestTimeUI;
  public Text energyUI;

  // End Level Elements
  public Text levelCompletedUI;
  public Text levelCompletedTimeUI;
  public Text levelCompletedFastestTimeUI;

  // Game Over Elements
  public Text gameOverCompletedTimeUI;
  public Text gameOverFastestTimeUI;

  // Timer
  public static TimeSpan timePlaying;
  public static bool timerGoing;
  public static float elapsedTime;

  // Game Values
  public static float energy = 100;

  // Level Time Records (array)
  public static TimeSpan [] levelRecords;
  
  /* 
  Level soundtracks are set to play on awake
  When a level resets it turns the volume on or off 
  depending on the boolean value of 'audioEnabled'
  */


  void Start()
  {
    gameSystem = this;

    // Initial Start Screen
    if(startScreen)
    {
      /* 
      Define the amount of items in the 'records' array based on the 
      amount of scenes in the build
      */
      levelRecords = new TimeSpan [SceneManager.sceneCountInBuildSettings];

      Time.timeScale = 0f;
      screenBackgroundAnimator.SetBool("Start", true);
      startScreenBackground.SetBool("StartBackground", true);
      startScreenAnimator.SetBool("Active", true);

      // Controls on by default
      controlsAnimator.SetBool("Active", true);

      startScreen = false; 
      /* 
      Once the game has "started" we don't want the start screen to
      automatically load for each new level
      */

    } else {       

      Time.timeScale = 1;
      gamePaused = false;
      AudioListener.volume = 1f;
      GameSystem.energy = 100;

      BeginTimer(); 
      levelActive = true;

      if(controlsEnabled)
      {
        controlsAnimator.SetBool("Active", true);
      }

      if(!audioEnabled)
      {
        AudioListener.volume = 0f; 
      }

      /*
      Add the fastest time record to the UI if there is a record greater than the default 00:00:00
      */

      if (levelRecords[SceneManager.GetActiveScene().buildIndex] > TimeSpan.FromSeconds(1)) {
        fastestTimeUI.text = "Fastest Time: " + levelRecords[SceneManager.GetActiveScene().buildIndex].ToString("mm':'ss'.'ff");
      }

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
      }

      //Test level complete *** Comment out in final build
      if (Input.GetKeyUp(KeyCode.Y))
      {
        LevelCompleted();
      }

      //Test game over *** Comment out in final build
      if (Input.GetKeyUp(KeyCode.T))
      {
        GameOver();
      }

      // Test energy *** Comment out in final build
      if (Input.GetKeyUp(KeyCode.RightBracket))
      {
        energy += 10;
      }
      if (Input.GetKeyUp(KeyCode.LeftBracket))
      {
        energy -= 10;
      }

    }

    // Update UI
    if (energy < 100)
    {
      energyUI.text = energy.ToString()+"%";
    }

    // Energy Out
    if (energy < 1)
    {
      GameOver();
    }

    levelUI.text = SceneManager.GetActiveScene().name;
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
    levelActive = true;
  }




  /* Timer
  -------------------------------*/

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




  /* Toggles
  -------------------------------*/

  public void ToggleAudio()
  {
    if(!audioEnabled)
    {
      AudioListener.volume = 1f; 
    }
    else
    {
      AudioListener.volume = 0f; 
    }
    audioEnabled = !audioEnabled;
  }



  public void ToggleControls() {
    if(!controlsEnabled)
    {
      controlsAnimator.SetBool("Active", true);
    }
    else
    {
      controlsAnimator.SetBool("Active", false);
    }
    controlsEnabled = !controlsEnabled;
  }


  public void TogglePause()
  {
    if(!gamePaused)
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
      gameOverScreenAnimator.SetBool("Active", false);
      startScreenBackground.SetBool("StartBackground", false);
      Time.timeScale = 1;
      gamePaused = false;
      timerGoing = true;
      StartCoroutine(UpdateTimer());
    }
  }




  /* Levels
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
    levelCompletedScreenAnimator.SetBool("Active", false);

    levelScreenAnimator.SetBool("Active", true);
  }

  public void CancelLevelSelection()
  {
    startScreenBackground.SetBool("StartBackground", false);
    screenBackgroundAnimator.SetBool("Start", false);
    TogglePause();
    levelActive = true;
  }

  public void BackFromLevelSelection()
  {
    if (!levelActive) {
      screenBackgroundAnimator.SetBool("Start", true);
      startScreenBackground.SetBool("StartBackground", true);
      startScreenAnimator.SetBool("Active", true);
    }

    else if (gamePaused) {
      pauseScreenAnimator.SetBool("Active", true);
      screenBackgroundAnimator.SetBool("Active", true);
    }
    levelScreenAnimator.SetBool("Active", false);
  }

  public void LoadLevel()
  {
    var levelName = EventSystem.current.currentSelectedGameObject.name;
    /* 
    Gets the name of button clicked 
    The button name needs to match the name of scene to be loaded
    */

    levelScreenAnimator.SetBool("Active", false);
    SceneManager.LoadScene(levelName);

    Start(); 
  }






  /* Level Completed
  -------------------------------*/

  public void LevelCompleted()
  {

    levelCompletedScreenAnimator.SetBool("Active", true);
    screenBackgroundAnimator.SetBool("Active", true);
    Time.timeScale = 0f;
    gamePaused = true;
    timerGoing = false;


    var currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    var currentLevelNumber = currentLevelIndex + 1;
    Debug.Log("Current Level Number = " + currentLevelNumber);


    /* 
    Get the record from the records array
    that corresponds to the current level/scene in build (remember first is 0)
    */

    var currentLevelRecord = levelRecords[currentLevelIndex];
    Debug.Log("Current Level Record = " + currentLevelRecord);

    var currentLevelTime = timePlaying;
    Debug.Log("Time Playing = " + timePlaying);


    // Update Screen Text
    levelCompletedUI.text = "Level " + currentLevelNumber + " Completed";
    levelCompletedTimeUI.text = "Time: " + timePlaying.ToString("mm':'ss'.'ff");


    if (currentLevelRecord < TimeSpan.FromSeconds(1)) { // First level play when current record = 00:00:00

      levelCompletedFastestTimeUI.text = "Record set: " + timePlaying.ToString("mm':'ss'.'ff");
      levelRecords[currentLevelIndex] = timePlaying;
      fastestTimeUI.text = "Fastest time: " + timePlaying.ToString("mm':'ss'.'ff");

    }

    else if (timePlaying < currentLevelRecord) {  // New record

      levelCompletedFastestTimeUI.text = "New record! (Old Record: " + currentLevelRecord.ToString("mm':'ss'.'ff") + ")";
      levelRecords[currentLevelIndex] = timePlaying;
      fastestTimeUI.text = "Fastest time: " + timePlaying.ToString("mm':'ss'.'ff");

    } 

    else {
      levelCompletedFastestTimeUI.text = "Current Record: " + currentLevelRecord.ToString("mm':'ss'.'ff");
    }

  }





  /* Game Over
  -------------------------------*/

  public void GameOver()
  {
    gameOverScreenAnimator.SetBool("Active", true);
    screenBackgroundAnimator.SetBool("Active", true);
    Time.timeScale = 0f;
    gamePaused = true;
    timerGoing = false;

    var currentLevelRecord = levelRecords[SceneManager.GetActiveScene().buildIndex];

    gameOverCompletedTimeUI.text = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
    

    if (levelRecords[SceneManager.GetActiveScene().buildIndex] < TimeSpan.FromSeconds(1)) {
      gameOverFastestTimeUI.text = "Fastest Time: not recorded";
    }

    else {
      gameOverFastestTimeUI.text = "Current Record: " + currentLevelRecord.ToString("mm':'ss'.'ff");
    }

  }



  /* Quit
  -------------------------------*/

  public void Quit()
  {
    UnityEditor.EditorApplication.isPlaying = false; // *** Remove in final build
    Application.Quit();
  }


}
