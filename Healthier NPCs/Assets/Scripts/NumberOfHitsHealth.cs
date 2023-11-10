using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfHitsHealth : MonoBehaviour, IHealth
{
    [SerializeField] int healthHits = 5;
    [SerializeField] float invulnerabilityTimeAfterEachHit = 5f;
    [SerializeField] Material _iFrameMat;
    private Material _defaultMaterial;

    private int hitsRemaining;
    private bool _canTakeDamage = true;
    private bool _timerRunning = false;
    public event Action<float> OnHPPctChanged = delegate (float f) { };
    public event Action OnDied = delegate { };

    public float CurrentHpPct { get { return hitsRemaining / (float)healthHits; } }

    private void Start()
    {
        _defaultMaterial = GetComponent<MeshRenderer>().material;
        hitsRemaining = healthHits;
    }

    public void TakeDamage(int amount)
    {
        if (_canTakeDamage)
        {
            StartCoroutine(InvulnerabilityTimer());

            hitsRemaining--;

            OnHPPctChanged(CurrentHpPct);

            if (hitsRemaining <= 0)
                Die();
        }
    }

    private IEnumerator InvulnerabilityTimer()
    {
        if (!_timerRunning)
        {
            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.material = _iFrameMat;
            _canTakeDamage = false;
            yield return new WaitForSeconds(invulnerabilityTimeAfterEachHit);
            mr.material = _defaultMaterial;
            _canTakeDamage = true;
            _timerRunning = false;
        }
        yield return null;
    }
    private void Die()
    {
        OnDied();
        Destroy(gameObject);
    }
}
