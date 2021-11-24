using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour
{
    [SerializeField] private BoxCollider punchCollider; 
    public void setPunchTriggerState(bool active)
    {
        punchCollider.enabled = active; 
    }
}
