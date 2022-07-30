using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cue : MonoBehaviour
{
    private const float Pi = Mathf.PI;
    
    private bool _uiTouch;
    
    private float _power;
    public float cueMoveSpeed = 1f;
    [SerializeField] private float force;

    private Vector3 _startCuePosition;
    private Vector3 _startRotation;
    [SerializeField] private Transform rotateAround;
    [SerializeField] private Vector3 offset;
    
    private Coroutine _moveCoroutine;
    
    public delegate void OnBallHit();
    public static event OnBallHit onBallHit;

    public void OnEnable()
    {
        WhiteBall.onBallStoped += ResetRotation;
    }
    public void OnDisable()
    {
        WhiteBall.onBallStoped -= ResetRotation;
    }

    private void Awake()
    {
        _startCuePosition = transform.localPosition;
        _startRotation = transform.eulerAngles;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
            {
                _uiTouch = true;
            }
            if (_uiTouch)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    _uiTouch = false;
                }
                return;
            }
            
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 direction = touchPosition - rotateAround.position;
            float angle = Mathf.Atan2(direction.z, direction.x) * (180 / Pi);
            rotateAround.eulerAngles = new Vector3(0, -angle, 0);
        }
        
    }

    public void Hit(float power)
    {
        _power = power;
        _moveCoroutine = StartCoroutine(MoveCue(power));
    }

    private IEnumerator MoveCue(float power)
    {
        Vector3 endPosition = _startCuePosition - new Vector3(2,0,0) * power;
        
        while (Vector3.Distance(transform.localPosition, endPosition) > 0.1f)
        {
            var step = cueMoveSpeed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, endPosition, step);
            yield return null;
        }
        
        while (Vector3.Distance(transform.localPosition, offset) > 0.1f)
        {
            var step = 3 * cueMoveSpeed * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, offset, step);
            yield return null;
        }
    }
    

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("White Ball"))
        {
            StopCoroutine(_moveCoroutine);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Vector3 collisionPoint = hit.point;
                Vector3 direction = other.transform.position - transform.position;
                direction.y = 0;

                other.attachedRigidbody.AddForceAtPosition(
                    force * _power * direction,
                    collisionPoint,
                    ForceMode.Impulse);
            }

            transform.localPosition = _startCuePosition;
            yield return new WaitForEndOfFrame();
            onBallHit?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void ChangeRotation(float xOffset, float yOffset)
    {
        transform.eulerAngles += new Vector3(xOffset, yOffset, 0);
    }

    public void ResetRotation()
    {
        transform.eulerAngles = _startRotation;
    }
}