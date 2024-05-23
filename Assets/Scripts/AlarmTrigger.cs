using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private AlarmSystem _alarmSystem;

    private bool _isActive = false;

    public bool IsActive => _isActive;

    public event Action Activate;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Criminal criminal))
        {
            _isActive = true;
            Activate?.Invoke();
            _alarmSystem.TurnUp();
        }
    }

    private void OnTriggerExit()
    {
        _alarmSystem.TurnDown();
        _isActive = false;
    }
}
