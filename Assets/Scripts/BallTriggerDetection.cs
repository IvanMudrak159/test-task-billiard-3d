using UnityEngine;

public class BallTriggerDetection : MonoBehaviour
{
    public delegate void OnPocket(Transform ball);

    public static event OnPocket onPocket;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pocket"))
        {
            onPocket?.Invoke(transform);
        }
    }
}
