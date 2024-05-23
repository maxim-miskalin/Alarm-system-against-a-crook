using System.Collections.Generic;
using UnityEngine;

public class Criminal : MonoBehaviour
{
    [SerializeField] private Transform _wayPoint;
    [SerializeField, Min(0f)] private float _speed = 4f;

    private List<Transform> _way = new();
    private int _index = 0;

    private void Start()
    {
        for(int i = 0; i < _wayPoint.childCount; i++)
            _way.Add(_wayPoint.GetChild(i));
    }

    private void Update()
    {
        Transform _point = _way[_index];
        transform.position = Vector3.MoveTowards(transform.position, _point.position, _speed * Time.deltaTime);

        if (transform.position == _point.position)
            _index = ++_index % _way.Count;
    }
}