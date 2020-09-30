using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

[System.Serializable]
public class MenuControl1 : MonoBehaviour {
public Camera main;
// Menu Animation Parameters
public  float pauseCheck = 0;
public  float playCheck = 0;

public GameObject buttonRestart;
public GameObject buttonQuit;
public Animator animRestart;
public Animator animQuit;
public float timerPause = 0f;
public float timerPlay = 0f;
public GameObject pauseButton;
public GameObject playButton;
public int currentIndex = 0;
public TextMeshProUGUI textBox;

void Load()
{
	SceneManager.LoadScene(PlayerPrefs.GetInt("ExitLevel"));
}
void Start()
{    	if(PlayerPrefs.GetInt("LevelNumber") < 1)
         PlayerPrefs.SetInt("LevelNumber",1);

     //  textBox.text = "Level "+ PlayerPrefs.GetInt("LevelNumber").ToString();
	 currentIndex = SceneManager.GetActiveScene().buildIndex;
	Debug.Log("Level Start - " + (currentIndex));
}
public void SaveGame()
{
	 PlayerPrefs.SetInt("ExitLevel",SceneManager.GetActiveScene().buildIndex + 1);
		 PlayerPrefs.Save();
}
	public void PauseMenu()
	{
 pauseButton.SetActive(false);
 playButton.SetActive(true);
 //main.GetComponent<DepthOfFieldDeprecated>().enabled = true;
 pauseCheck = 1;
 playCheck = 0;
 Time.timeScale = 0;
	}
	void OnApplicationQuit()
	{
	 
	}
	public void QuitGame()
	{  
		Application.Quit();
	}

	public void PlayMenu()
	{   	
		playButton.SetActive(false);
	    pauseButton.SetActive(true);
	
		//main.GetComponent<DepthOfFieldDeprecated>().enabled = false;
		Time.timeScale = 1;
		pauseCheck = 0;
		playCheck = 1;
	}

	public void RestartGame()
	{  currentIndex = SceneManager.GetActiveScene().buildIndex;
	   //Debug.Log("Level Start - " + (currentIndex + 1));
		SceneManager.LoadScene(currentIndex);
	}

	public void NextLevel()
	{  currentIndex = SceneManager.GetActiveScene().buildIndex;
	     if(currentIndex < 6)
		{SceneManager.LoadScene(currentIndex + 1);
		if(PlayerPrefs.GetInt("LevelNumber") >= 1 && PlayerPrefs.GetInt("LevelNumber") <= 400)
		PlayerPrefs.SetInt("LevelNumber",PlayerPrefs.GetInt("LevelNumber") + 1);
		
		  SaveGame();
		}
		else 
		{SceneManager.LoadScene(1);
			if(PlayerPrefs.GetInt("LevelNumber") >= 1 && PlayerPrefs.GetInt("LevelNumber") <= 400)
		    PlayerPrefs.SetInt("LevelNumber",PlayerPrefs.GetInt("LevelNumber") + 1);
		  SaveGame();
		}
	}

	void Update()
	{
		//Pause Menu Animations
		if(pauseCheck == 1)
		{
			timerPause += Time.unscaledDeltaTime;
      if(timerPause >= 0.0f)
		{
          buttonRestart.SetActive(true);
		}
		if(timerPause >= 0.08f)
		{
			buttonQuit.SetActive(true);
      	pauseCheck = 0;
			timerPause = 0f;
		}
	
		}
		//Play Menu Animations
		if(playCheck == 1)
		{  
			timerPlay += Time.unscaledDeltaTime;
			if(timerPlay >= 0.0f)
			{
			 animQuit.SetBool("play",true);
			}
			if(timerPlay >= 0.08f)
			{
			 animRestart.SetBool("play",true);
			}
		
			if(timerPlay >= 0.4f)
			{
				 animQuit.SetBool("play",false);
				
				 animRestart.SetBool("play",false);
				 buttonRestart.SetActive(false);
				
				 buttonQuit.SetActive(false);
				 playCheck = 0f;
			 timerPlay = 0f;
			}
            }
	}
}
