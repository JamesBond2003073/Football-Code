using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{ public GameObject gameOverDisplay;
public GameObject completeDisplay;
public MenuControl1 control;

   void OnTriggerExit(Collider other)
   {
       if(other.gameObject.tag == strings.Ball)
       {   if(!completeDisplay.activeSelf)
           gameOverDisplay.SetActive(true);
           Debug.Log("Game Over" + ( control.currentIndex));
       }
   }
}
