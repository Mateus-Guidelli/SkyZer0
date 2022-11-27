using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class pegaArma : MonoBehaviour
{
    public GameObject weapon;
    public CollisionDetection cd;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, weaponHolder, Mcamera;
    public Transform weaponT;
    public WeapomController wc;
    public BoxCollider collDrop;
    public Camera Weaponcamera;
    public Camera MainCamera;
    

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;
    
    Animator anim;
    
    private void Start()
    {
       
        anim = weapon.GetComponent<Animator>();
        //Setup
        if (!equipped)
        {
            cd.enabled = false;
            collDrop.enabled = true;
            rb.isKinematic = false;
            coll.isTrigger = false;
            anim.enabled = false;
            wc.CanAttack = false;
            coll.isTrigger = false;
            coll.enabled = false;
            Weaponcamera.cullingMask = LayerMask.GetMask("nothing");
            MainCamera.cullingMask = -1;
        }
        if (equipped)
        { 
            cd.enabled = true;
            Weaponcamera.cullingMask = LayerMask.GetMask("weapom");

            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
               
        }
    }

    private void Update()
    {
       
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && !slotFull) PickUp();

        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        collDrop.enabled = false;
        anim.enabled = true;
        equipped = true;
        slotFull = true;
        Weaponcamera.cullingMask = LayerMask.GetMask("weapom");
       
        transform.SetParent(weaponHolder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
    
        rb.isKinematic = true;
        coll.isTrigger = true;

        cd.enabled = true;
        wc.CanAttack = true;
        
        coll.enabled = true;
    }

    
    private void Drop()
    {
        collDrop.enabled = true;
        Weaponcamera.cullingMask = LayerMask.GetMask("nothing");
        MainCamera.cullingMask = -1;
        anim.enabled = false;
        wc.CanAttack = false;
        equipped = false;
        slotFull = false;
        
        coll.enabled = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(Mcamera.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(Mcamera.up * dropUpwardForce, ForceMode.Impulse);
        
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        cd.enabled = false;
    }

}
