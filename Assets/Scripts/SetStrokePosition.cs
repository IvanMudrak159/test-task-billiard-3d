using UnityEngine;
using UnityEngine.EventSystems;

public class SetStrokePosition : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public float radius;
    public int offset;
    private RectTransform _parentRectTransform;
    private RectTransform _rectTransform;
    private Vector2 _startPosition;
    
    [SerializeField] private Cue cue; 
    private void OnEnable()
    {
        _rectTransform.position = _startPosition;
    }

    private void Start()
    {
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        radius = _parentRectTransform.rect.width / 2 - offset;

        _rectTransform = GetComponent<RectTransform>();
        _startPosition = _rectTransform.position;
        Debug.Log(_startPosition);

    }

    public void OnDrag(PointerEventData eventData)
    {
        float distance = Vector2.Distance(_rectTransform.position, _startPosition);
        if (distance < radius)
        {
            transform.position = eventData.pointerCurrentRaycast.screenPosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float xOffset = (_rectTransform.position.x - _startPosition.x) / radius;
        float yOffset = (_rectTransform.position.y - _startPosition.y) / radius;
        cue.ChangeRotation(xOffset, yOffset);
    }
}
