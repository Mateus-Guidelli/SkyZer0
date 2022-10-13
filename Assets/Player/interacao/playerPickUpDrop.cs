using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPickUpDrop : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            float pickUpDistance = 2f;
            if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask ))
            {
               if(raycastHit.transform.TryGetComponent(out ObjectGrabbable objectGrabbable))
               {
                 Debug.Log(objectGrabbable);    
               }
            }
        }   
    }
}
