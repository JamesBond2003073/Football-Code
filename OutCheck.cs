using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCheck : MonoBehaviour
{
  public GameObject GameOverDis;
  void OnCollisionEnter(Collision other)
  {
      if(other.gameObject.tag == strings.out1)
      GameOverDis.SetActive(true);
  }
}
