using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItem : MonoBehaviour
{
    [SerializeField] private float _healAmmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealthSystem _health))
        {
            if (_health.Heal(_healAmmount))
            {
                Destroy(gameObject);
            }
        }
    }
}
