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
    private bool _reportingHealth;
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

        if (_reportingHealth)
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
        StartCoroutine(healthDelay());
    }

    private IEnumerator healthDelay()
    {
        _reportingHealth = true;
        yield return new WaitForSeconds(3f);
        _reportingHealth = false;
    }
}
