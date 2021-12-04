using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoombaStateFollow : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMesh;
    [SerializeField] private Transform _target;
    [SerializeField] private MonoBehaviour _prevState;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _damageAmmount;
    private bool _canAttack = true;
    private void Update()
    {
        _navMesh.destination = _target.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _anim.SetTrigger("NotAlerted");
            _prevState.enabled = true;
            enabled = false;
        }
    }
    public void AttackTrigger_TriggerStayEvent(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealthSystem _pHealth) && _canAttack)
        {
            if (!_pHealth.isInvincible())
            {
                _pHealth.GetHit(_damageAmmount);
            }
            StartCoroutine(attackCooldown());
        }
    }

    public void DeathEvent(Collider other)
    {
        if (other.CompareTag("Player"))
            Destroy(gameObject);
    }

    private IEnumerator attackCooldown()
    {
        _canAttack = false;
        yield return new WaitForSeconds(1f);
        _canAttack = true;
    }
}
