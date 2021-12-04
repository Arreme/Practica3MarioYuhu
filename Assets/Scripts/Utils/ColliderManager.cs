using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider> _onTriggerEnter;
    [SerializeField] private UnityEvent<Collider> _onTriggerExit;
    [SerializeField] private UnityEvent<Collider> _onTriggerStay;

    private void OnTriggerEnter(Collider other)
    {
        if (_onTriggerEnter != null)
        {
            _onTriggerEnter.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_onTriggerExit != null)
        {
            _onTriggerExit.Invoke(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_onTriggerExit != null)
        {
            _onTriggerStay.Invoke(other);
        }
    }
}
