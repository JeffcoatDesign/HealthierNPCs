using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BleedHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int _startingHealth = 100;
    [SerializeField] private float _bleedTime = 5f;
    [SerializeField] private Material _bleedingMat;
    private Material _normalMat;
    private int _currentHealth;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        _currentHealth = _startingHealth;
        _normalMat = GetComponent<MeshRenderer>().material;
    }

    public float CurrentHPPct { get { return _currentHealth / (float)_startingHealth; } }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        StartCoroutine(Bleed(amount));
    }

    private IEnumerator Bleed(int amount)
    {
        float startTime = Time.time;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = _bleedingMat;
        while (Time.time - startTime < _bleedTime) {
            _currentHealth -= 1;
            OnHPPctChanged(CurrentHPPct);

            if (CurrentHPPct <= 0)
                Die();
            yield return new WaitForSeconds(0.1f);
        }
        meshRenderer.material = _normalMat;
    }

    private void Die()
    {
        OnDied();
        Destroy(gameObject);
    }
}
