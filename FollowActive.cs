using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowActive : MonoBehaviour
{
  public int activeBehaviour = 0;

  void OnTriggerEnter(Collider other)
  {
      if(other.gameObject.tag == strings.Ball)
      {
          activeBehaviour = 1;
      }
  }
}
