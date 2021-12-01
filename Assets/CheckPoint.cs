using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform _checkpointPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MarioPlayerController controller))
        {
            controller.setCheckPoint(this);
        }
    }

    public Transform getCheckPointPos()
    {
        return _checkpointPos;
    }
}
