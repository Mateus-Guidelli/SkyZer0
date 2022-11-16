using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    
    public float throwForce = 500f; 
    public float pickUpRange = 5f; 
    private GameObject heldObj; 
    private Rigidbody rb; 
    private bool canDrop = true; 
    private int LayerNumber;
    public PlayerMove pm;
    public Enemyhealth HP; 
    public float damage;
  

    public void Start()
    {
        LayerNumber = LayerMask.NameToLayer("pickUpLayerMask"); 
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (heldObj == null) 
            {
                
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    
                    if (hit.transform.gameObject.tag == "canPickUp")
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                    
                }
            }
            else
            {
                if(canDrop == true)
                {
                    StopClipping(); 
                    DropObject();
                   
                }
            }
        }
        if (heldObj != null) 
        {
            
            MoveObject(); 
            if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) 
            {
                StopClipping();
                ThrowObject();
                
                
            }

        }
    }
    public void PickUpObject(GameObject pickUpLayerMask)
    {
        if (pickUpLayerMask.GetComponent<Rigidbody>())
        {
            heldObj = pickUpLayerMask; 
            rb = pickUpLayerMask.GetComponent<Rigidbody>(); 
            rb.isKinematic = true;
            rb.transform.parent = holdPos.transform; 
            heldObj.layer = LayerNumber; 
            
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    public void DropObject()
    {
        transform.gameObject.tag = "canHit";
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0; 
        rb.isKinematic = false;
        heldObj.transform.parent = null; 
        heldObj = null; 
        
    }
    public void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }
    public void ThrowObject()
    {
        transform.gameObject.tag = "canHit";
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        rb.isKinematic = false;
        heldObj.transform.parent = null;
        rb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    public void StopClipping() 
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }

}
