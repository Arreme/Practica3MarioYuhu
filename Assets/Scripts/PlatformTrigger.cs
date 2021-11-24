using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private float maxAttachingAngle; 
    GameObject attachedMario; 

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<MarioPlayerController>() != null)
        {
            attachMario(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<MarioPlayerController>() != null)
        {
            detachMario();
        }
    }

    private void Update()
    {
        if (Vector3.Angle(transform.up, Vector3.up) > maxAttachingAngle)
        {
            detachMario();
        }
    }


    private void attachMario(GameObject mario)
    {
        mario.transform.parent = transform;
        animator.SetBool("isMarioOn", true);
        attachedMario = mario; 
    }

    private void detachMario()
    {
        if(attachedMario != null)
        {
            attachedMario.transform.parent = null;
            animator.SetBool("isMarioOn", false);
            attachedMario = null; 
        }
       
    }


    
}
