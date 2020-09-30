using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
   void Awake()
   {  if(PlayerPrefs.GetInt("ExitLevel") >= 1 && PlayerPrefs.GetInt("ExitLevel") <= 6)
       SceneManager.LoadScene(PlayerPrefs.GetInt("ExitLevel"));
       else
       SceneManager.LoadScene(1);
   }

    
}
