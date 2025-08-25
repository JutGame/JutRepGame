using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public static Action<bool> OnEnd;
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private bool _isPushing = false;
    private TrailRenderer _trail;
    private Vector3 _startPosition;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _trail = GetComponent<TrailRenderer>();
        _trail.enabled = false;
        _startPosition = transform.position;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, _startPosition) > 11)
        {
            OnEnd?.Invoke(false);
            Destroy(gameObject);
        }
    }

    public void PushArrow()
    {
        transform.SetParent(null);
        _trail.enabled = true;
        _isPushing = true;
        _rb.AddForce(transform.right * _speed, ForceMode2D.Impulse);
        SFXController.Instance.Play(SoundType.Shot);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isPushing == false)
            return;
        _rb.linearVelocity = Vector3.zero;
        transform.SetParent(collision.transform);
        _trail.enabled = false;
        _isPushing = false;
        transform.SetParent(collision.transform);

        if(collision.gameObject.TryGetComponent(out Target target))
        {
            target.ShowNumbers(transform.localPosition);
            OnEnd?.Invoke(true);
            SFXController.Instance.Play(SoundType.Hit);
        }
    }
}
