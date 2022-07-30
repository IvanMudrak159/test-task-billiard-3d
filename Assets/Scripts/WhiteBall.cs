using UnityEngine;

public class WhiteBall : MonoBehaviour
{
    private bool _isMoving;
    private Rigidbody _rigidbody;

    public delegate void OnBallStoped();
    public static event OnBallStoped onBallStoped;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        Cue.onBallHit += ChangeState;
    }

    private void OnDisable()
    {
        Cue.onBallHit += ChangeState;
    }

    private void ChangeState()
    {
        _isMoving = true;
    }
    private void Update()
    {
        if (_isMoving && _rigidbody.velocity.magnitude < 0.1f)
        {
            _isMoving = false;
            onBallStoped?.Invoke();
        }
    }
}
