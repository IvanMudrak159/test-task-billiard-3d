using UnityEngine;

public class CalculateTrajection : MonoBehaviour
{
    [SerializeField] private Transform ball;
    [SerializeField] private LineRenderer trajectoryPathLineRenderer;
    [SerializeField] private LineRenderer trajectoryHitLineRenderer;
    
    [SerializeField] private MeshRenderer projectionBall;
    
    private float _projectBallOffset;
    
    private void Start()
    {
        _projectBallOffset = projectionBall.bounds.extents.x;
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 rayDirection = ball.position - transform.position;
        rayDirection.y = 0;
        if (Physics.SphereCast(ball.position, _projectBallOffset, rayDirection, out hit))
        {
            Vector3 reflectPosition = hit.point + _projectBallOffset * hit.normal;
            projectionBall.transform.position = reflectPosition;
            Vector3 reflectDir = Vector3.Reflect(rayDirection, hit.normal);
            trajectoryPathLineRenderer.SetPosition(0, ball.position);
            trajectoryPathLineRenderer.SetPosition(1, reflectPosition);
            trajectoryPathLineRenderer.SetPosition(2, reflectPosition + reflectDir * 0.1f);
            if (!hit.collider.CompareTag("Ball"))
            {
                trajectoryHitLineRenderer.gameObject.SetActive(false);
                return;
            }
            trajectoryHitLineRenderer.gameObject.SetActive(true);
            Vector3 normal = (hit.collider.bounds.center - hit.point).normalized;
            normal.y = 1;
            trajectoryHitLineRenderer.SetPosition(0, hit.point + Vector3.up);
            trajectoryHitLineRenderer.SetPosition(1, hit.point + normal);
        }
    }
}
