using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCheck : MonoBehaviour
{ public int saveCheck = 0;
void OnCollisionEnter(Collision other)
{ if(other.gameObject.tag == strings.Ball)
    saveCheck = 1;
}
}
