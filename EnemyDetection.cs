using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
public bool enemyClose = false;
public bool enemyDynamicClose = false;
public bool possessBall = false;
public bool tackled = false;
public GameObject ball;
public Rigidbody rbBall;
private Vector3 refvel;
public BallPossesion possessScript;
public GameObject enemy;
public float enemySpeed = 2f;
public GameObject enemyStatic;
public float stopFactor = 0.065f;
public float timerStatic = 0.1f;


   void OnTriggerEnter(Collider other)
   {
       if(other.gameObject.tag==strings.EnemyStatic)
      { enemyClose=true;
       enemyStatic = other.gameObject;
      }
       if(other.gameObject.tag==strings.Ball)
       possessBall=true;
       if(other.gameObject.tag == strings.Enemy)
       enemyDynamicClose = true;
          
   }
   void OnTriggerStay(Collider other)
   {
        if(other.gameObject.tag==strings.EnemyStatic)
      { enemyClose=true;
       enemyStatic = other.gameObject;
       enemyDynamicClose = false;
      }
       if(other.gameObject.tag==strings.Ball)
       possessBall=true;
       if(other.gameObject.tag == strings.Enemy)
       enemyDynamicClose = true;
   }
   void OnTriggerExit(Collider other)
   {     
       if(other.gameObject.tag==strings.Ball )
       {
       possessBall=false;
       Time.timeScale = 1f;
       Time.fixedDeltaTime = 0.02f;
       }
       if(other.gameObject.tag==strings.EnemyStatic)
       enemyClose=false;
        if(other.gameObject.tag == strings.Enemy)
       enemyDynamicClose = false;
   }


    void Start()
    {   
        possessScript = GetComponentInChildren<BallPossesion>();
        ball = GameObject.FindWithTag(strings.Ball);
        rbBall = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(possessBall == true)
        {    transform.GetChild(5).gameObject.SetActive(true);
            if(this.name != strings.player && enemy != null ) 
             {Time.timeScale = 1f;
            Time.fixedDeltaTime = 1f * 0.02f;
             }
            if(enemy != null && enemyDynamicClose==false && tackled==false)
          { 
              enemy.GetComponent<Animator>().SetBool("isRunning",true);
             enemy.transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
          }

          if(enemyDynamicClose==true && tackled == false && enemy != null)
          { 
         //  possessScript.kickRestrict = 1;       
           StartCoroutine("Tackle");
          }  
        }

        if(possessBall==true && possessScript.kickActive == false)
        {
            rbBall.velocity= Vector3.SmoothDamp(rbBall.velocity,Vector3.zero,ref refvel,stopFactor);
            rbBall.rotation = Quaternion.identity;
        }
       

        if(tackled == true)
        {
            enemy.transform.LookAt(rbBall.transform);
            
        }
        if(possessBall == false )
      {  transform.GetChild(5).gameObject.SetActive(false);
         enemy.GetComponent<Animator>().SetBool("isTackling",false);
         enemy.GetComponent<Animator>().SetBool("isRunning",false);
      }
    }
   
    IEnumerator Tackle()
    {   timerStatic -= Time.deltaTime;
        
      //  if(timerStatic < 0f)
        enemy.tag = strings.EnemyStatic;

         enemy.GetComponent<Animator>().SetBool("isTackling",true);
         enemy.GetComponent<Animator>().SetBool("isRunning",false);
        yield return new WaitForSeconds(1f);
        tackled=true;
       // enemy.transform.position = new Vector3(enemy.transform.position.x + enemy.transform.forward.x/30,enemy.transform.position.y,enemy.transform.position.z + enemy.transform.forward.z/30);
        enemy.GetComponent<Animator>().SetBool("isTackling",false);
        
       
    }
}
