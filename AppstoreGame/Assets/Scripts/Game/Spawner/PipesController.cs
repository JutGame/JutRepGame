using System.Collections.Generic;
using UnityEngine;

public class PipesController : MonoBehaviour
{
    [SerializeField] private Transform _startPosition;
    private float _distance = 13;
    public void Initialize(List<Pipe> pipes)
    {
        for (int i = 0; i < pipes.Count; i++)
        {
            Pipe pipe = Instantiate(pipes[i], new Vector3(_startPosition.position.x + (_distance * i), _startPosition.position.y, _startPosition.position.z), Quaternion.identity);
        }
    }
}
