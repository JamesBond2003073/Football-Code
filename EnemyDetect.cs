using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
   public int enemyCheck = 0;

   void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag == strings.Enemy)
       enemyCheck = 1;
   }

}
