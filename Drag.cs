using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour,IEndDragHandler,IDragHandler
{public int dir;
public int dirBack;
 public GameObject[] go;
 public GameObject[] Goal;
 public GameObject[] Player;
 public GameObject PlayerDetection;
 public BallPossesion possessScript;
public int playerLengthAdd;
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
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
{
Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
for(int i=0;i<go.Length;i++)
{ 
     PlayerDetection = go[i].transform.GetChild(2).gameObject;
     possessScript = PlayerDetection.GetComponent<BallPossesion>();
     possessScript.swipeDir = GetDragDirection(dragVectorDirection);
     possessScript.swipeDirBack = GetDragDirectionBack(dragVectorDirection);
     if(possessScript.goalFlag == 1 && possessScript.enemyScript.possessBall == true)
     {
          possessScript.swipeDirGoal = dragVectorDirection;
     }
}
}

private int GetDragDirection(Vector3 dragVector)
{
float positiveX = Mathf.Abs(dragVector.x);
float positiveY = Mathf.Abs(dragVector.y);
if (positiveX/positiveY > 0.4f && dragVector.y > 0f)
{
dir = (dragVector.x > 0 ) ? 1 : -1;
}
else
{
dir = (dragVector.y > 0) ? 2 : -2;
}


     if(positiveY/positiveX < 0.3f)
{
     dir = (dragVector.x > 0) ? 3 : -3;
}

return dir;
}

private int GetDragDirectionBack(Vector3 dragVector)
{
float positiveX = Mathf.Abs(dragVector.x);
float positiveY = Mathf.Abs(dragVector.y);
if (positiveX/positiveY > 0.4f && dragVector.y < 0f)
{
dirBack = (dragVector.x > 0 ) ? 1 : -1;
}
else
{
dirBack = (dragVector.y < 0) ? 2 : -2;
}
return dirBack;
}

}
