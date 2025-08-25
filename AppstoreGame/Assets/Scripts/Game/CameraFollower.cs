using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        transform.position = new Vector3(_target.position.x + 1.44f, 0, -10);
    }
}
