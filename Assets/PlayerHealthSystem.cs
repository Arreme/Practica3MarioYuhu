using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IHealthSystem
{
    [SerializeField] private float _maxLife;
    private float _currLife;
    [SerializeField] private float _invTime;
    private bool _isInvincible;
    [SerializeField] private GameObject _model;
    private void Start()
    {
        _currLife = _maxLife;
    }
    public bool GetHit(float dmgAmmount)
    {
        _currLife -= dmgAmmount;
        Debug.Log("ouch!" + _currLife);
        StartCoroutine(InvTimeCoroutine());
        if (_currLife <= 0)
        {
            //DEATH
        }
        return true;
    }

    public bool Heal(float healAmmount)
    {
        if (_currLife == _maxLife) return false;
        _currLife = Mathf.Min(_currLife+healAmmount,_maxLife);
        return true;
    }

    public bool isInvincible()
    {
        return _isInvincible;
    }

    private IEnumerator InvTimeCoroutine()
    {
        _isInvincible = true;
        _model.SetActive(false);
        yield return new WaitForSeconds(_invTime / 8);
        _model.SetActive(true);
        yield return new WaitForSeconds(_invTime / 2);
        _model.SetActive(false);
        yield return new WaitForSeconds(_invTime / 8);
        _model.SetActive(true);
        yield return new WaitForSeconds(_invTime / 2);
        _model.SetActive(false);
        yield return new WaitForSeconds(_invTime / 8);
        _model.SetActive(true);
        _isInvincible = false;
    }
}
