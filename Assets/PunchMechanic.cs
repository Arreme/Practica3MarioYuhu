using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchMechanic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out GoombaStateFollow _goomba))
        {
            _goomba.DeathEvent(GetComponent<BoxCollider>());
        }
    }
}
