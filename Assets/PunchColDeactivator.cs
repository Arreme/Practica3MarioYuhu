using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchColDeactivator : MonoBehaviour
{
    [SerializeField] private Collider _punchDeactivator;

    public void DeactivateCollider()
    {
        _punchDeactivator.enabled = false;
    }
}
