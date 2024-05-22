using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private Criminal _thief;

    void Update()
    {
        transform.LookAt(_thief.transform);
    }
}
