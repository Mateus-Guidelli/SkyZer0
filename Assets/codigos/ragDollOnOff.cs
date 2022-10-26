using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragDollOnOff : MonoBehaviour
{
    public BoxCollider mainCollider;
    public GameObject Rig;
    public Animator Animator;

    void Start()
    {
        GetRagdoll();
        RagdollModeOff();
          
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "canHit")
        {
           RagdollModeOn();
            
        }

    }
   
    Collider[] ragDollColliders;
    Rigidbody[] limbsRigidbodies;
    
    void GetRagdoll()
    {
       ragDollColliders = Rig.GetComponentsInChildren<Collider>();
       limbsRigidbodies = Rig.GetComponentsInChildren<Rigidbody>();  
    }

    void RagdollModeOn()
    {
         Animator.enabled = false;
          foreach(Collider col in ragDollColliders)
        {
            col.enabled = true;
        }
        
        foreach(Rigidbody rigid in limbsRigidbodies)
        {
            rigid.isKinematic = false;
        }

       
        mainCollider.enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        

    }

    void RagdollModeOff()
    {
        foreach(Collider col in ragDollColliders)
        {
            col.enabled = false;
        }
        
        foreach(Rigidbody rigid in limbsRigidbodies)
        {
            rigid.isKinematic = true;
        }

        Animator.enabled = true;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

    }

    

    
}
