using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
 public GameObject ball;
 public Rigidbody rbBall;
 public Vector3 dir;
 public float enemySpeed = 1f;
 public float enemyKickForce = 2f;
 public int enemyKickFlag = 0;
 public GameObject parent;
 public Animator enemyAnim;
 public GameObject gameOverDisplay;
 public GameObject[] go;
 public GameObject[] Goal;
 public GameObject[] Player;
 public int playerLengthAdd;
 public GameObject active;
 public int flagActive = 12;
public bool isActive;
 public GameObject PlayerDetection;
 public GameObject levelCompleteDis;
 public BallPossesion possessScript;
 public EnemyDetection enemyDetectionScript;
 public FollowActive activeScript;
public float timerStatic = 0.1f;
public int over = 0;
public int kick = 0;
public MenuControl1 control;

 void Start()
 {    activeScript = transform.GetChild(2).GetComponent<FollowActive>();
      Player = GameObject.FindGameObjectsWithTag("Player");
      Goal = GameObject.FindGameObjectsWithTag("PlayerGoal");
      playerLengthAdd = GameObject.FindGameObjectsWithTag("Player").Length ;
      go = new GameObject[Player.Length + Goal.Length];
      for(int i=0;i<Player.Length;i++)
      {
           go[i] = Player[i];
      }
      for(int j=0;j<Goal.Length;j++)
      {
           go[playerLengthAdd] = Goal[j];
           playerLengthAdd++;
      }


    rbBall = ball.GetComponent<Rigidbody>();  
    enemyAnim = GetComponent<Animator>(); 
 }

  void OnTriggerEnter(Collider other)
  { 
      if(other.gameObject.tag == strings.Ball && enemyKickFlag == 0 && over == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ) && enemyDetectionScript.possessBall == true)
     {  Debug.Log("ball");
         
         enemyKickFlag = 1;
         for(int i=0;i<go.Length;i++)
         {
            // go[i].transform.GetChild(2).gameObject.GetComponent<BallPossesion>().enabled = false;
         }
         StartCoroutine("KickEnemy");
       
     }
     else if(other.gameObject.tag == strings.Ball && enemyKickFlag == 0 && over == 0)
     {  Debug.Log("ball1");
         enemyKickFlag = 1;
        rbBall.AddForce(rbBall.transform.right * enemyKickForce,ForceMode.Impulse);
        GameOver();
        
     }
    
  }

  void FixedUpdate()
  { transform.position = new Vector3(transform.position.x,Mathf.Clamp(transform.position.x,0.0f,0.05f),transform.position.z);
      if(isActive == false)
  {
      this.gameObject.SetActive(false);
  }
      if(over == 1)
      {
          enemyAnim.SetBool("isRunning",false);
      }
      
      
      if(gameOverDisplay.activeSelf == true || levelCompleteDis.activeSelf == true)
  {
      over = 1;
  }
 
      for(int i=0;i<go.Length;i++)
         {      
             if(go[i].GetComponent<EnemyDetection>().possessBall == true && i != flagActive)
             {   enemyDetectionScript = go[i].GetComponent<EnemyDetection>();
                 PlayerDetection = go[i].transform.GetChild(2).gameObject;
                 possessScript = PlayerDetection.GetComponent<BallPossesion>();
                 active = go[i];  
                 flagActive = i;
             }
             
             }

     if(enemyKickFlag == 0 && over == 0 && activeScript.activeBehaviour == 1)
    {    enemyAnim.SetBool("isRunning",true);
         transform.LookAt(ball.transform);
         transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
    } 
    
  }
/*  void LateUpdate()
  {
        if(enemyKickFlag == 0 && over == 0 && activeScript.activeBehaviour == 1)
    {    enemyAnim.SetBool("isRunning",true);
         transform.LookAt(ball.transform);
         transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
    } 
  }
*/

 IEnumerator KickEnemy()
    { // timerStatic -= Time.deltaTime;
         
      // if(timerStatic < 0f)
      //  this.gameObject.tag = strings.EnemyStatic;


       enemyAnim.SetBool("isKicking",true);
       enemyAnim.SetBool("isRunning",false);
     
            
           YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.3f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
          }
         enemyAnim.SetBool("isKicking",false);
          if(enemyDetectionScript.possessBall == true && kick == 0)
          {   kick = 1;
              rbBall.AddForce(transform.forward * enemyKickForce,ForceMode.Impulse);
         GameOver();
         
        }
       
        enemyKickFlag = 0;
    }  

void GameOver()
    {  Debug.Log("Game Over" + (control.currentIndex));
      Time.timeScale = 0.2f;
      Time.fixedDeltaTime = 0.2f * 0.02f;
      gameOverDisplay.SetActive(true);

    }

}
