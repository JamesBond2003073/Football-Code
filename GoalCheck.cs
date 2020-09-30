using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCheck : MonoBehaviour
{
   public GameObject LevelCompleteUI;
   public GameObject Over;
   public SaveCheck save;
   public int goalCheck = 0;
   public GoalKickCheck Kick;
   public float timerOverPost = 1f;
   public float timerOverSave = 1f;
   public MenuControl1 control;
   public ParticleSystem vfx;
   public int vfxFlag = 0;
   void Awake()
   {
       vfx = GameObject.Find("Confetti").GetComponent<ParticleSystem>();
      vfx.Stop();
   }
   void Start()
   { 
     
   }
   void Update()
   {
      if(save.saveCheck == 1)
      {
           timerOverSave -= Time.deltaTime;
      }
      if(timerOverSave <= 0f && goalCheck == 0 && !LevelCompleteUI.activeSelf)
      {Over.SetActive(true);
       Debug.Log("Game Over" + (control.currentIndex));
      }
      if(Kick.postCheck == 1)
      {
         timerOverPost -= Time.deltaTime; 
      }
      if(timerOverPost > 0f && timerOverSave > 0f && goalCheck == 1 && !Over.activeSelf)
      {  Debug.Log("Level Complete" + (control.currentIndex));
         LevelCompleteUI.SetActive(true);
         if(vfxFlag == 0)
        { vfx.Play();
         vfxFlag = 1;
        }
      }
      if(timerOverPost <= 0f && goalCheck == 0 && !LevelCompleteUI.activeSelf)
      {Over.SetActive(true);
      Debug.Log("Game Over" + (control.currentIndex));
      }
   }


   void OnTriggerEnter(Collider other)
   {    
       if(other.gameObject.tag == strings.Ball)
       {   goalCheck = 1;
          if(Kick.postCheck == 0 && save.saveCheck == 0 && !Over.activeSelf)
          {LevelCompleteUI.SetActive(true);
           if(vfxFlag == 0)
        { vfx.Play();
         vfxFlag = 1;
        }
           Debug.Log("Level Complete" + (control.currentIndex));
          }
       }
   }
}
