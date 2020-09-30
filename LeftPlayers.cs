using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPlayers : MonoBehaviour
{
   public GameObject playerLeft;



    void OnTriggerEnter(Collider other)
    {  GameObject parent = other.gameObject;
  //  Debug.Log(this.name);
   // Debug.Log(child.name);
        if(parent.gameObject.tag == strings.Player || parent.gameObject.tag == strings.PlayerGoal)
       { 
         playerLeft = parent.gameObject;

       }
    }

void Start()
{

}


}
