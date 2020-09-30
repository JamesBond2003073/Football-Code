using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
public GameObject gameOverDisplay;
public Rigidbody rbBall;
public float force = 2f;
public float gameOverTimer = 0.8f;
public int flag=0;
public GameObject[] go;
 public GameObject[] Goal;
 public GameObject[] Player;
 public int playerLengthAdd;
public GameObject PlayerDetection;
public BallPossesion possessScript;
public MenuControl1 control;
public GameObject active;
public int flagActive = 12;


void Start()
{
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

}

void OnCollisionEnter(Collision other)
{
   if(other.gameObject.tag == strings.Ball)
   {
     

       flag=1;
       rbBall=other.gameObject.GetComponent<Rigidbody>();
      // rbBall.AddForce(transform.forward * force,ForceMode.Impulse);
   }

}

void Update()
{

for(int i=0;i<go.Length;i++)
   {     if(go[i].GetComponent<EnemyDetection>().possessBall == true && i != flagActive )
        { PlayerDetection = go[i].transform.GetChild(2).gameObject;
         possessScript = PlayerDetection.GetComponent<BallPossesion>();
         active = go[i];
         flagActive = i;
        }
   }

    if(flag==1)
    gameOverTimer -= Time.deltaTime;
    if(gameOverTimer <= 0f && active.GetComponent<EnemyDetection>().possessBall == false)
    GameOver();
    if(gameOverTimer <= 0.3f && gameOverTimer > 0f && active.GetComponent<EnemyDetection>().possessBall == true)
    {
        gameOverTimer = 0.8f;
        flag = 0;   
    }
}
 void GameOver()
    {  Debug.Log("Game Over" + ( control.currentIndex));
      Time.timeScale = 0.2f;
      Time.fixedDeltaTime = 0.2f * 0.02f;
      gameOverDisplay.SetActive(true);

    }
}
