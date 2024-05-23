using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private Transform _thief;

    private void Update()
    {
        transform.LookAt(_thief);
    }
}
