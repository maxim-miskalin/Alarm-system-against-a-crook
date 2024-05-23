using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private Transform _thief;

    void Update()
    {
        transform.LookAt(_thief);
    }
}
