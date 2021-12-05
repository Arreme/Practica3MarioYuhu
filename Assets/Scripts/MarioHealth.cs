using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarioHealth : MonoBehaviour
{
    [SerializeField] private Image _healthImage;
    [SerializeField] private Transform _shown;
    private Vector3 _pHidden;
    private Vector3 _pShown;
    private float _uiCD = 0;
    private float _currentHealth = 1;
    private float _targetHealth = 1;
    private float i = 0;

    private void Awake()
    {
        _pHidden = gameObject.transform.position;
        _pShown = _shown.position;
    }
    private void Update()
    {
        _uiCD -= Time.deltaTime;
        if (_uiCD >= 0)
        {
            i += Time.deltaTime * 2;
            i = Mathf.Min(i, 1);
            _healthImage.fillAmount = Mathf.Lerp(_currentHealth, _targetHealth, i);
        } else
        {
            i -= Time.deltaTime;
            i = Mathf.Max(i, 0);
            _currentHealth = _targetHealth;
        }
        transform.position = Vector3.Lerp(_pHidden, _pShown, i);

    }

    public void OnHealthChange(float health)
    {
        _targetHealth = health/80;
        _uiCD = 2.5f;
    }
}
