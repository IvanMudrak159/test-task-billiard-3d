using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform Ball;

    private void LateUpdate()
    {
        transform.position = Ball.position;
    }
}
