using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKickCheck : MonoBehaviour
{  public GoalCheck goal;
   public int postCheck = 0;
   void OnCollisionEnter(Collision other)
   {   
       if(other.gameObject.tag == strings.Ball)
       postCheck = 1;
        
   }
}
