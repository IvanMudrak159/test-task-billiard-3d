using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalculateStroke : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Cue cue;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        cue.Hit(_slider.value);
        _slider.value = 0;
    }
}