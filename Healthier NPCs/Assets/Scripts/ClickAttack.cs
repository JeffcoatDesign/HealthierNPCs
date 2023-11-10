using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAttack : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 5;
    private Ray _ray;
    private RaycastHit _raycastHit;

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _raycastHit))
        {
            if (Input.GetMouseButtonDown(0)) {
                if (_raycastHit.collider.CompareTag("NPC"))
                {
                    _raycastHit.collider.GetComponent<NPC>().TakeDamage(_damageAmount);
                }
            }
        }
    }
}
