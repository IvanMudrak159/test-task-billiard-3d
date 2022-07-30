using UnityEngine;

public class CameraFit : MonoBehaviour {

    public MeshRenderer outline;
    
    void Start () {
        float screenRatio = (float)Screen.safeArea.width / Screen.safeArea.height;
        float targetRatio = outline.bounds.size.x / outline.bounds.size.y;

        if (!Camera.main)
        {
            Debug.LogError("There is no main camera on the scene");
            return;
        }
        
        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = outline.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = outline.bounds.size.y / 2 * differenceInSize;
        }
    }
}