using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.EventSystems;
 
public class BallPossesion : MonoBehaviour
{
 //touch parameters
 public Vector3 initialTouchPosition;
 public Vector3 finalTouchPosition;
 public int swipeDir;
 public int swipeDirBack;
 public Vector3 swipeDirGoal = Vector3.zero;

public List<GameObject> playersFront;
public List<GameObject> playersBack;
 public int[] check = new int[5];
 public int[] checkBack = new int[3];
public GameObject ball;
public GameObject playerToPassPlayerDetection;
public GameObject proximityPlayerDetection;
public int assignFlag = 0;
public int assignFlagBack = 0;
public int checkFlag = 0;
public int kickFlag = 0;
public int kickEnemyFlag = 0;
public int flagRotate = 0;
public int gameOver = 0;
public GameObject gameOverDisplay;
public int goalFlag = 0;
public int goalKicked = 0;
public Collider ballCollider;
public PhysicMaterial ballMaterial;
public float enemyKickForce = 5f;
public float timerOver = 1.5f;
public Vector3 refvel;
public int kickRestrict = 0;
public int flagLeft = 0;
public int flagRight = 0;
public float noSideGoal;
public float sideGoal;


//animators
public Animator animplay;
public Animator enemyAnim;
public Animator keeperAnim;

//ball kick params
 public float force = 2f;
 public float forceFar = 3f;
 public float forceTooFar = 2.6f;
 public float forceClose = 2.3f;
 public float forceGoal = 2f;
    public int angle = 45;
    public Rigidbody rbBall;
    private Vector3 dir;
    public Vector3 dirLeft;
    public Vector3 dirRight;
    public Vector3 dirCenter;
    public Vector3 dirLeftBack;
    public Vector3 dirRightBack;
    public Vector3 dirCenterBack;
    public Vector3 dirRightSide;
    public Vector3 dirLeftSide;
    public bool kickActive =false;
    public int sideCheck = 0;
    public int overDone = 0;

//players 
public GameObject playerLeft;
public GameObject playerLeftSide;
public GameObject playerRight;
public GameObject playerRightSide;
public GameObject playerCenter;
public GameObject playerLeftBack;
public GameObject playerRightBack;
public GameObject playerCenterBack;   
public GameObject playerToPass;
public GameObject playerToPassNotOverRight;
public GameObject playerToPassNotOverLeft;
public GameObject playerToPassNotOverCenter;
public GameObject playerToPassNotOverRightBack;
public GameObject playerToPassNotOverLeftBack;
public GameObject playerToPassNotOverCenterBack;
public GameObject playerToPassNotOverBack;
public GameObject playerToPassNotOverSideLeft;
public GameObject playerToPassNotOverSideRight;

//scripts 
public EnemyDetection enemyScript;
public EnemyFollow follow;
public BallPossesion proximityPossessScript;
public RightPlayers right;
public LeftPlayers left;
public MenuControl1 control;

//playerrotate
     public Vector3 axis = Vector3.up;
     public Vector3 desiredPosition;
     public float radius = 2.0f;
     public float radiusSpeed = 0.5f;
     public float rotationSpeed = 80.0f;
     public GameObject rotateObject;
 
void Awake()
{
   Application.targetFrameRate = 300;
}
  // Start is called before the first frame update
    void Start()
    {  
       left = transform.parent.transform.GetChild(6).GetComponent<LeftPlayers>();
       right = transform.parent.transform.GetChild(7).GetComponent<RightPlayers>();
      
      rotateObject = transform.parent.transform.GetChild(4).gameObject;
     // follow = GameObject.Find("EnemyFollow").GetComponent<EnemyFollow>();
      if(transform.parent.gameObject.tag == strings.PlayerGoal)
    {
      goalFlag = 1;
    }
      
      Time.timeScale=1f;   
       enemyScript=GetComponentInParent<EnemyDetection>();
         ball = GameObject.FindWithTag(strings.Ball);
          rbBall= ball.GetComponent<Rigidbody>();

    }

    void OnCollisionEnter(Collision other)
    {  GameObject parent = other.gameObject;
  //  Debug.Log(this.name);
   // Debug.Log(child.name);
        if(parent.gameObject.tag == strings.Player || parent.gameObject.tag == strings.PlayerGoal)
       { 
         playersFront.Add(parent.gameObject);

       proximityPlayerDetection = other.gameObject.transform.GetChild(2).gameObject; 
       proximityPossessScript = proximityPlayerDetection.GetComponent<BallPossesion>();  
 
       proximityPossessScript.playersBack.Add(transform.parent.gameObject);  

       }
    }
     
    // Update is called once per frame
    void Update()
    {  if(flagLeft == 0 && left.playerLeft != null)
      { playerLeftSide = left.playerLeft;
       flagLeft = 1;
      }
      if(flagRight == 0 && right.playerRight != null)
     {
       playerRightSide = right.playerRight;
       flagRight = 1;
     }
      if(gameOverDisplay.activeSelf)
     { overDone = 1;}

    }



    void FixedUpdate()
    {  
          if(enemyScript.possessBall == false)
          {
            kickFlag = 0;
            kickActive = false;
            checkFlag = 0;
            assignFlag = 0;
            playerToPassNotOverCenter = null;
            playerToPassNotOverCenterBack = null;
            playerToPassNotOverLeft = null;
            playerToPassNotOverLeftBack = null;
            playerToPassNotOverRight = null;
            playerToPassNotOverRightBack = null;
          
          }

       //GameOverCheck

       if(gameOver == 1 && playerToPass.GetComponent<EnemyDetection>().possessBall == true && kickEnemyFlag == 0)
       { //playerToPass.GetComponent<EnemyDetection>().enabled = false;
        kickEnemyFlag = 1;
        enemyAnim = playerToPass.GetComponent<EnemyDetection>().enemyStatic.GetComponent<Animator>();
       playerToPassPlayerDetection = playerToPass.transform.GetChild(2).gameObject;
       playerToPassPlayerDetection.GetComponent<BallPossesion>().kickRestrict = 1;
        StartCoroutine("KickEnemy");
        
       }

        if(timerOver <= 0f)
        {
         // rbBall.velocity = Vector3.SmoothDamp(rbBall.velocity,Vector3.zero,ref refvel,0.5f);
        }

        if(enemyScript.possessBall==true && assignFlag==0)
    { 
       StartCoroutine("AssignDelay");
       StartCoroutine("AssignDelayBack");     
    }
     
      //kick goal
      
     if(sideCheck == 1)
     forceGoal = sideGoal;
     if(sideCheck == 0)
     forceGoal = noSideGoal;

     if(swipeDirGoal.y > 0.3f && goalFlag == 1 && goalKicked ==0  && kickRestrict == 0 && enemyScript.possessBall == true && overDone == 0 )
     {  goalKicked=1;
        ballCollider.material.bounciness = 0.3f;

      StartCoroutine("KickGoal");
     }
     if(goalKicked == 1)
     {
       rbBall.velocity = Vector3.ClampMagnitude(rbBall.velocity,13f);
     }

     //KICK BALL SIDE RIGHT
      if (swipeDir == 3)
       //if(Input.GetKeyDown(KeyCode.C))
        { 
              swipeDir = 0;
            
          if(playerRightSide != null && kickFlag==0 && kickRestrict == 0  && enemyScript.possessBall == true && overDone == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ) && overDone == 0)
         { 
             kickFlag=1;

           if(check[4]==1)
           {
             gameOver=1;
             playerToPass = playerRightSide; 
           }
           
               ball.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0f,0f,1f));
                dir = new Vector3(dirRightSide.normalized.x,ball.transform.up.y,dirRightSide.normalized.z) ;
           
            kickActive = true;
            playerToPassNotOverSideRight = playerRightSide;
       
            
              sideCheck = 1;
          StartCoroutine("KickRightSide");

           
        
         }
         }

          //KICK BALL SIDE LEFT
      if (swipeDir == -3)
       //if(Input.GetKeyDown(KeyCode.C))
        { 
              swipeDir = 0;
            
          if(playerLeftSide != null && kickFlag==0 && kickRestrict == 0  && enemyScript.possessBall == true && overDone == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ) && overDone == 0)
         { 
             kickFlag=1;

           if(check[3]==1)
           {
             gameOver=1;
             playerToPass = playerLeftSide; 
           }
           
               ball.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0f,0f,1f));
                dir = new Vector3(dirLeftSide.normalized.x,ball.transform.up.y,dirLeftSide.normalized.z) ;
           
            kickActive = true;
            playerToPassNotOverSideLeft = playerLeftSide;
       
            sideCheck = 1;
          StartCoroutine("KickLeftSide");

           
        
         }
         }

//kick ball center

        if (swipeDir==2)
       //if(Input.GetKeyDown(KeyCode.C))
        { 
              swipeDir = 0;
            
          if(playerCenter != null && kickFlag==0 && kickRestrict == 0  && enemyScript.possessBall == true && overDone == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ) && overDone == 0)
         { 
             kickFlag=1;

           if(check[1]==1)
           {
             gameOver=1;
             playerToPass = playerCenter; 
           }
           
               ball.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0f,0f,1f));
                dir = new Vector3(dirCenter.normalized.x,ball.transform.up.y,dirCenter.normalized.z) ;
           
            kickActive = true;
            playerToPassNotOverCenter = playerCenter;
            playerToPassNotOverCenter.transform.GetChild(2).gameObject.GetComponent<BallPossesion>().sideCheck = 0;
            
          StartCoroutine("KickCenter");

           
        
         }
         }
     
       
       //kick ball left

      if (swipeDir==-1)
      //if(Input.GetKeyDown(KeyCode.A))
        { 
              swipeDir = 0;
            
          if(playerLeft != null && kickFlag==0 && kickRestrict == 0 && enemyScript.possessBall == true && overDone == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ) && overDone == 0)
         { 
           kickFlag=1;

           if(check[0]==1)
          { 
            gameOver=1;
            playerToPass=playerLeft;
          } 
                 ball.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0f,0f,1f));
                dir =   new Vector3(dirLeft.normalized.x,ball.transform.up.y,dirLeft.normalized.z) ;
        
            kickActive = true;
            playerToPassNotOverLeft = playerLeft;
            playerToPassNotOverLeft.transform.GetChild(2).gameObject.GetComponent<BallPossesion>().sideCheck = 0;
            
          StartCoroutine("KickLeft");
        
           
        
         }
        }

         //kick ball right

         if (swipeDir==1)
         //if(Input.GetKeyDown(KeyCode.D))
        { 
              swipeDir = 0;
        
          if(playerRight != null && kickFlag==0 && kickRestrict == 0  && enemyScript.possessBall == true && overDone == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ) && overDone == 0 )
           {  
              kickFlag=1;

             if(check[2]==1)
          { 
            gameOver=1;
            playerToPass=playerRight;
          }

               ball.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0f,0f,1f));
                dir = new Vector3(dirRight.normalized.x,ball.transform.up.y,dirRight.normalized.z) ;
           
            kickActive = true;
            playerToPassNotOverRight = playerRight;
           playerToPassNotOverRight.transform.GetChild(2).gameObject.GetComponent<BallPossesion>().sideCheck = 0;
            
         StartCoroutine("KickRight");
         
        
        
           }

        }

//BACKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK

//kick ball center

        if (swipeDirBack == 2)
       //if(Input.GetKeyDown(KeyCode.C))
        {  
              swipeDirBack = 0;
            
          if(playerCenterBack != null && kickFlag == 0 && kickRestrict == 0  && enemyScript.possessBall == true && overDone == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ) && overDone == 0)
         { 
             kickFlag=1;

           if(checkBack[1]==1)
           {
             gameOver=1;
             playerToPass = playerCenterBack; 
           }
           
               ball.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0f,0f,1f));
                dir = new Vector3(dirCenterBack.normalized.x,ball.transform.up.y,dirCenterBack.normalized.z) ;
           
            kickActive = true;
            playerToPassNotOverCenterBack = playerCenterBack;
            playerToPassNotOverBack = playerCenterBack;
          
       StartCoroutine("KickCenterBack");
            
        
         }
         
         }
     
       
       //kick ball left

      if (swipeDirBack == -1)
      //if(Input.GetKeyDown(KeyCode.A))
        { 
              swipeDirBack = 0;
            
          if(playerLeftBack != null && kickFlag==0 && kickRestrict == 0 && enemyScript.possessBall == true && overDone == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ) && overDone == 0 )
         { 
           kickFlag=1;

           if(checkBack[0] == 1)
          { 
            gameOver=1;
            playerToPass=playerLeftBack;
          } 
                 ball.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0f,0f,1f));
                dir =   new Vector3(dirLeftBack.normalized.x,ball.transform.up.y,dirLeftBack.normalized.z) ;
        
            kickActive = true;
            playerToPassNotOverLeftBack = playerLeftBack;
             playerToPassNotOverBack = playerLeftBack;
         //    StartCoroutine("Rotate");
          StartCoroutine("KickLeftBack");
        
           
        
         }
        }

         //kick ball right

         if (swipeDirBack==1)
         //if(Input.GetKeyDown(KeyCode.D))
        { 
              swipeDirBack = 0;
        
          if(playerRightBack != null && kickFlag==0 && kickRestrict == 0  && enemyScript.possessBall == true && overDone == 0 && (rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  )  && overDone == 0)
           {  
              kickFlag=1;

             if(checkBack[2]==1)
          { 
            gameOver=1;
            playerToPass=playerRightBack;
          }

               ball.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0f,0f,1f));
                dir = new Vector3(dirRightBack.normalized.x,ball.transform.up.y,dirRightBack.normalized.z) ;
           
            kickActive = true;
            playerToPassNotOverRightBack = playerRightBack;
             playerToPassNotOverBack = playerRightBack;
          //   StartCoroutine("Rotate");
         StartCoroutine("KickRightBack");
         
        
        
           }

        }

    }
   
  
     IEnumerator KickCenter()
    {   
             

             playerCenter.GetComponent<EnemyDetection>().stopFactor = 0.06f;

         animplay.SetBool("isPassing",true);
         YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();
       for (float duration = 0.18f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
            }
       
        
            Debug.Log(Vector3.Distance(playerCenter.transform.position,transform.parent.transform.position));
           animplay.SetBool("isPassing",false);

            if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))
           { if(Vector3.Distance(playerCenter.transform.position,transform.parent.transform.position)> 2.3f)
            rbBall.AddForce(dir*forceTooFar,ForceMode.Impulse);
         if(Vector3.Distance(playerCenter.transform.position,transform.parent.transform.position)> 2f && Vector3.Distance(playerCenter.transform.position,transform.parent.transform.position) <= 2.3f)
           rbBall.AddForce(dir*forceFar,ForceMode.Impulse);
           else if( Vector3.Distance(playerCenter.transform.position,transform.parent.transform.position) <= 2f && Vector3.Distance(playerCenter.transform.position,transform.parent.transform.position) >= 1.7f )
          rbBall.AddForce(dir*force,ForceMode.Impulse);
          else if(Vector3.Distance(playerCenter.transform.position,transform.parent.transform.position) < 1.7f)
              rbBall.AddForce(dir*forceClose,ForceMode.Impulse);
           }
    }

    IEnumerator KickLeft()
    {
           playerLeft.GetComponent<EnemyDetection>().stopFactor = 0.06f;

       animplay.SetBool("isPassing",true);

           YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.18f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
          }
  Debug.Log(Vector3.Distance(playerLeft.transform.position,transform.parent.transform.position));
         //yield return new WaitForSeconds(0.25f);
           animplay.SetBool("isPassing",false);

            if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))
         { if(Vector3.Distance(playerLeft.transform.position,transform.parent.transform.position)> 2.3f)
            rbBall.AddForce(dir*forceTooFar,ForceMode.Impulse);
         if(Vector3.Distance(playerLeft.transform.position,transform.parent.transform.position)> 2f && Vector3.Distance(playerLeft.transform.position,transform.parent.transform.position) <= 2.3f)
           rbBall.AddForce(dir*forceFar,ForceMode.Impulse);
            else if( Vector3.Distance(playerLeft.transform.position,transform.parent.transform.position) <= 2f && Vector3.Distance(playerLeft.transform.position,transform.parent.transform.position) >= 1.7f )
          rbBall.AddForce(dir*force,ForceMode.Impulse);
          else if(Vector3.Distance(playerLeft.transform.position,transform.parent.transform.position) < 1.7f)
              rbBall.AddForce(dir*forceClose,ForceMode.Impulse);
         }
         
    }

     IEnumerator KickRight()
    {   
         playerRight.GetComponent<EnemyDetection>().stopFactor = 0.06f;

       animplay.SetBool("isPassing",true);
        //yield return new WaitForSeconds(0.25f);

          YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.18f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
            }
  
          Debug.Log(Vector3.Distance(playerRight.transform.position,transform.parent.transform.position));
           animplay.SetBool("isPassing",false);

            if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))
          { if(Vector3.Distance(playerRight.transform.position,transform.parent.transform.position)> 2.3f)
            rbBall.AddForce(dir*forceTooFar,ForceMode.Impulse);
         if(Vector3.Distance(playerRight.transform.position,transform.parent.transform.position)> 2f && Vector3.Distance(playerRight.transform.position,transform.parent.transform.position) <= 2.3f)
           rbBall.AddForce(dir*forceFar,ForceMode.Impulse);
           else if( Vector3.Distance(playerRight.transform.position,transform.parent.transform.position) <= 2f && Vector3.Distance(playerRight.transform.position,transform.parent.transform.position) >= 1.7f )
          rbBall.AddForce(dir*force,ForceMode.Impulse);
          else if(Vector3.Distance(playerRight.transform.position,transform.parent.transform.position) < 1.7f)
              rbBall.AddForce(dir*forceClose,ForceMode.Impulse);
          }
         
    }

     IEnumerator KickRightSide()
    {   
         playerRightSide.GetComponent<EnemyDetection>().stopFactor = 0.043f;

       animplay.SetBool("isPassing",true);
        //yield return new WaitForSeconds(0.25f);

          YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.18f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
            }
  
          Debug.Log(Vector3.Distance(playerRightSide.transform.position,transform.parent.transform.position));
           animplay.SetBool("isPassing",false);

            if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))
            {
           if(Vector3.Distance(playerRightSide.transform.position,transform.parent.transform.position)> 2.3f)
            rbBall.AddForce(dir*forceTooFar,ForceMode.Impulse);
         if(Vector3.Distance(playerRightSide.transform.position,transform.parent.transform.position)> 2f && Vector3.Distance(playerRightSide.transform.position,transform.parent.transform.position) <= 2.3f)
           rbBall.AddForce(dir*forceFar,ForceMode.Impulse);
           else if( Vector3.Distance(playerRightSide.transform.position,transform.parent.transform.position) <= 2f && Vector3.Distance(playerRightSide.transform.position,transform.parent.transform.position) >= 1.7f )
          rbBall.AddForce(dir*force,ForceMode.Impulse);
          else if(Vector3.Distance(playerRightSide.transform.position,transform.parent.transform.position) < 1.7f)
              rbBall.AddForce(dir*forceClose,ForceMode.Impulse);
            }
         
    }

    IEnumerator KickLeftSide()
    {
           playerLeftSide.GetComponent<EnemyDetection>().stopFactor = 0.043f;
           
          

       animplay.SetBool("isPassing",true);

           YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.18f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
          }
  Debug.Log(Vector3.Distance(playerLeftSide.transform.position,transform.parent.transform.position));
         //yield return new WaitForSeconds(0.25f);
           animplay.SetBool("isPassing",false);

            if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))
            {
          if(Vector3.Distance(playerLeftSide.transform.position,transform.parent.transform.position)> 2.3f)
            rbBall.AddForce(dir*forceTooFar,ForceMode.Impulse);
         if(Vector3.Distance(playerLeftSide.transform.position,transform.parent.transform.position)> 2f && Vector3.Distance(playerLeftSide.transform.position,transform.parent.transform.position) <= 2.3f)
           rbBall.AddForce(dir*forceFar,ForceMode.Impulse);
            else if( Vector3.Distance(playerLeftSide.transform.position,transform.parent.transform.position) <= 2f && Vector3.Distance(playerLeftSide.transform.position,transform.parent.transform.position) >= 1.7f )
          rbBall.AddForce(dir*force,ForceMode.Impulse);
          else if(Vector3.Distance(playerLeftSide.transform.position,transform.parent.transform.position) < 1.7f)
              rbBall.AddForce(dir*forceClose,ForceMode.Impulse);
            }

    }
    

    //BACKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK COROUTINESSSSSSSSSSSSSSSSSSSSSS

     IEnumerator KickCenterBack()
    {    playerCenterBack.GetComponent<EnemyDetection>().stopFactor = 0.025f;

         animplay.SetBool("isPassing",true);
         YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();
       for (float duration = 0.18f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
            }
       
        // yield return new WaitForSeconds(0.25f);
            Debug.Log(Vector3.Distance(playerCenterBack.transform.position,transform.parent.transform.position));
           animplay.SetBool("isPassing",false);

            if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))
            {
         if(Vector3.Distance(playerCenterBack.transform.position,transform.parent.transform.position)> 2.3f)
            rbBall.AddForce(dir*forceTooFar,ForceMode.Impulse);
         if(Vector3.Distance(playerCenterBack.transform.position,transform.parent.transform.position)> 2f && Vector3.Distance(playerCenterBack.transform.position,transform.parent.transform.position) <= 2.3f)
           rbBall.AddForce(dir*forceFar,ForceMode.Impulse);
           else if( Vector3.Distance(playerCenterBack.transform.position,transform.parent.transform.position) <= 2f && Vector3.Distance(playerCenterBack.transform.position,transform.parent.transform.position) >= 1.7f )
          rbBall.AddForce(dir*force,ForceMode.Impulse);
          else if(Vector3.Distance(playerCenterBack.transform.position,transform.parent.transform.position) < 1.7f)
              rbBall.AddForce(dir*forceClose,ForceMode.Impulse);
            }
    }

    IEnumerator KickLeftBack()
    {
           playerLeftBack.GetComponent<EnemyDetection>().stopFactor = 0.025f;
       animplay.SetBool("isPassing",true);

           YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.18f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
          }
  Debug.Log(Vector3.Distance(playerLeftBack.transform.position,transform.parent.transform.position));
         //yield return new WaitForSeconds(0.25f);
           animplay.SetBool("isPassing",false);

            if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))
            {
           if(Vector3.Distance(playerLeftBack.transform.position,transform.parent.transform.position)> 2.3f)
            rbBall.AddForce(dir*forceTooFar,ForceMode.Impulse);
         if(Vector3.Distance(playerLeftBack.transform.position,transform.parent.transform.position)> 2f && Vector3.Distance(playerLeftBack.transform.position,transform.parent.transform.position) <= 2.3f)
           rbBall.AddForce(dir*forceFar,ForceMode.Impulse);
            else if( Vector3.Distance(playerLeftBack.transform.position,transform.parent.transform.position) <= 2f && Vector3.Distance(playerLeftBack.transform.position,transform.parent.transform.position) >= 1.7f )
          rbBall.AddForce(dir*force,ForceMode.Impulse);
          else if(Vector3.Distance(playerLeftBack.transform.position,transform.parent.transform.position) < 1.7f)
              rbBall.AddForce(dir*forceClose,ForceMode.Impulse);
            }
         
    }

     IEnumerator KickRightBack()
    {   
          playerRightBack.GetComponent<EnemyDetection>().stopFactor = 0.025f;

       animplay.SetBool("isPassing",true);
        //yield return new WaitForSeconds(0.25f);

          YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.18f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
            }
  
          Debug.Log(Vector3.Distance(playerRightBack.transform.position,transform.parent.transform.position));
           animplay.SetBool("isPassing",false);

            if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))
            {
           if(Vector3.Distance(playerRightBack.transform.position,transform.parent.transform.position)> 2.3f)
            rbBall.AddForce(dir*forceTooFar,ForceMode.Impulse);
         if(Vector3.Distance(playerRightBack.transform.position,transform.parent.transform.position)> 2f && Vector3.Distance(playerRightBack.transform.position,transform.parent.transform.position) <= 2.3f)
           rbBall.AddForce(dir*forceFar,ForceMode.Impulse);
           else if( Vector3.Distance(playerRightBack.transform.position,transform.parent.transform.position) <= 2f && Vector3.Distance(playerRightBack.transform.position,transform.parent.transform.position) >= 1.7f )
          rbBall.AddForce(dir*force,ForceMode.Impulse);
          else if(Vector3.Distance(playerRightBack.transform.position,transform.parent.transform.position) < 1.7f)
              rbBall.AddForce(dir*forceClose,ForceMode.Impulse);
            }
    }

     IEnumerator Rotate()
     {    YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 2f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;

         if(flagRotate == 0)
          {
         transform.parent.transform.RotateAround (ball.transform.position, axis, rotationSpeed * Time.deltaTime);
         desiredPosition = (transform.parent.transform.position - ball.transform.position).normalized * radius + ball.transform.position;
         transform.parent.transform.position = Vector3.MoveTowards(transform.parent.transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
          }
         if((transform.position - ball.transform.position).normalized == (ball.transform.position-playerToPassNotOverBack.transform.position).normalized)
          flagRotate = 1;

         
            }
     }

      IEnumerator KickGoal()
    {
        
       animplay.SetBool("isKicking",true);
        //yield return new WaitForSeconds(0.25f);

          YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.45f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
            }
        
             if((rbBall.velocity.magnitude < 0.2f && rbBall.velocity.magnitude > -0.2f  ))     
            { rbBall.AddForce(new Vector3(-swipeDirGoal.y,0.25f,swipeDirGoal.x) * forceGoal,ForceMode.Impulse);
             if(swipeDirGoal.x < 0f)
             keeperAnim.SetBool("divingRight",true);
             else if(swipeDirGoal.x > 0f)
             keeperAnim.SetBool("divingLeft",true);
            }
           animplay.SetBool("isKicking",false);  
    }


     IEnumerator KickEnemy()
    {

       enemyAnim.SetBool("isKicking",true);

           YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
      for (float duration = 0.4f; duration > 0; duration -= Time.fixedDeltaTime) {
                yield return waitForFixedUpdate;
          }
        
         //yield return new WaitForSeconds(0.25f);
           enemyAnim.SetBool("isKicking",false);
           rbBall.AddForce(playerToPass.GetComponent<EnemyDetection>().enemyStatic.transform.forward * enemyKickForce,ForceMode.Impulse);
         GameOver();
    }  

    IEnumerator AssignDelay()
    {  // yield return new WaitForSeconds(0f);

          YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
     for (float duration = 0f; duration > 0; duration -= Time.fixedDeltaTime) {
              yield return waitForFixedUpdate;
          }
  
          
        if(playerLeftSide != null)
          dirLeftSide = playerLeftSide.transform.position - transform.parent.transform.position;

          if(playerRightSide != null)
          dirRightSide = playerRightSide.transform.position - transform.parent.transform.position;

          if( playerLeftSide != null && playerLeftSide.GetComponent<EnemyDetection>().enemyClose)
          check[3]=1;
          else
          check[3]=0;

          if(playerRightSide != null && playerRightSide.GetComponent<EnemyDetection>().enemyClose)
          check[4]=1;
          else
          check[4]=0;

        for(int j=0;j<playersFront.Count;j++)
        {
              Vector3 dirplayer = playersFront[j].transform.position-transform.parent.transform.position;
             if(dirplayer.z <= 0.45f && dirplayer.z >= -0.45f)
         {  dirCenter=dirplayer;
             if(playersFront[j].GetComponent<EnemyDetection>().enemyClose)
           {
               check[1]=1;
           }
           else
           {
               check[1]=0;
           }
                 playerCenter = playersFront[j];
         }
             if(dirplayer.z > 0.45f)
             {  dirRight=dirplayer;
                  if(playersFront[j].GetComponent<EnemyDetection>().enemyClose)
           {
               check[2]=1;
           }
           else
           {
               check[2]=0;
           }

                 playerRight = playersFront[j];
             }
             if(dirplayer.z < -0.45f)
             {      dirLeft=dirplayer;
                     if(playersFront[j].GetComponent<EnemyDetection>().enemyClose)
           {
               check[0]=1;
           }
           else
           {
               check[0]=0;
           }

                 playerLeft = playersFront[j];
             }
        }
        assignFlag=1;
    }

 IEnumerator AssignDelayBack()
    {  // yield return new WaitForSeconds(0f);

          YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();   
     for (float duration = 0f; duration > 0; duration -= Time.fixedDeltaTime) {
              yield return waitForFixedUpdate;
          }
  
        
        for(int j=0;j<playersBack.Count;j++)
        {
             Vector3 dirplayer = playersBack[j].transform.position-transform.parent.transform.position;
             Debug.Log(dirplayer.z);
             if(dirplayer.z <= 0.45f && dirplayer.z >= -0.45f)
         {  dirCenterBack = dirplayer;
             if(playersBack[j].GetComponent<EnemyDetection>().enemyClose)
           {
               checkBack[1]=1;
           }
           else
           {
               checkBack[1]=0;
           }
                 playerCenterBack = playersBack[j];
         }
             if(dirplayer.z > 0.45f)
             {  dirRightBack = dirplayer;
                  if(playersBack[j].GetComponent<EnemyDetection>().enemyClose)
           {
               checkBack[2]=1;
           }
           else
           {
               checkBack[2]=0;
           }

                 playerRightBack = playersBack[j];
             }
             if(dirplayer.z < -0.45f)
             {      dirLeftBack = dirplayer;
                     if(playersBack[j].GetComponent<EnemyDetection>().enemyClose)
           {
               checkBack[0]=1;
           }
           else
           {
               checkBack[0]=0;
           }

                 playerLeftBack = playersBack[j];
             }
        }
        assignFlagBack=1;
    }

    void GameOver()
    {  Debug.Log("Game Over" + (control.currentIndex));
      Time.timeScale = 0.2f;
      Time.fixedDeltaTime = 0.2f * 0.02f;
      gameOverDisplay.SetActive(true);

    }
}
