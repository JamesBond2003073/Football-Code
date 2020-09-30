using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlayers : MonoBehaviour
{
public GameObject playerRight;



    void OnTriggerEnter(Collider other)
    {  GameObject parent = other.gameObject;
  //  Debug.Log(this.name);
   // Debug.Log(child.name);
        if(parent.gameObject.tag == strings.Player || parent.gameObject.tag == strings.PlayerGoal)
       { 
         playerRight = parent.gameObject;

       }
    }
}
