using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AlarmTrigger : MonoBehaviour
{
    private bool _isActive = false;

    public bool IsActive => _isActive;

    public event Action Activate;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Criminal criminal))
        {
            _isActive = true;
            Activate?.Invoke();
        }
    }

    private void OnTriggerExit()
    {
        _isActive = false;
        Activate?.Invoke();
    }
}
