using UnityEngine;

public class Pipe : MonoBehaviour
{
    private float _movingSpeed = 2f;
    [SerializeField] private Transform _topPoint;
    [SerializeField] private Transform _bottomPoint;
    [SerializeField] private Target _target;

    private bool _movingToTop = false;

    private void Start()
    {
        _movingSpeed = Random.Range(1f, 3f);
    }

    private void Update()
    {
        Vector3 targetPosition = _movingToTop ? _topPoint.position : _bottomPoint.position;
        targetPosition = new Vector3(targetPosition.x, targetPosition.y, -1);
        _target.transform.position = Vector3.MoveTowards(
            _target.transform.position,
            targetPosition,
            _movingSpeed * Time.deltaTime
        );

        if (Vector3.Distance(_target.transform.position, targetPosition) < 0.01f)
        {
            _movingToTop = !_movingToTop;
        }
    }
}
