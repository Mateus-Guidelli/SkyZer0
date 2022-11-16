using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhealth : MonoBehaviour
{
    public WeapomController wc;
    public CollisionDetection cd;
    public ragDollOnOff rg;
    

    public float health;
    public float maxHealth;

    public void Start()
    {
        health = maxHealth;
    }
    public void Update()
    {
        if(tag == "Enemy")
        {
                if(health <= 0)
            {
                rg.RagdollModeOn();
            }
        
            if(health > maxHealth)
            {
                health = maxHealth;
            }

        }
       
    }

}
