using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeapomController wc;
    public Enemyhealth HP; 
    public float damage;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && wc.IsAttacking )
        {
            other.GetComponent<Animator>().SetTrigger("hit");
            other.GetComponent<Enemyhealth>().health -= damage;
        }
    }
    





}
