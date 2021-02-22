using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
	public static int score = 0;
    public Text scoreUI;

	
    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.RightBracket))
        {
           score += 1;
           
        }

        if (Input.GetKeyUp(KeyCode.LeftBracket))
        {
           score -= 1;
        }

        if (score >= 20) 
        {
          SceneManager.LoadScene(sceneName:"Enemys");
        }

        scoreUI.text = score.ToString();

    }
}
