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
        bool isModelOn = true;
        for (float i = 0; i < _invTime; i += 0.1f)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (isModelOn)
            {
                _model.SetActive(false);
            }
            else
            {
                _model.SetActive(true);
            }
            isModelOn = !isModelOn;
            yield return new WaitForSeconds(0.1f);
        }
        _isInvincible = false;
    }
}
