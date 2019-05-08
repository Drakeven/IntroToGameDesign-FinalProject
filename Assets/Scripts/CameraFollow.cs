using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    public Transform target;

    //Smaller = smoother
    public float smoothSpeed = 4f;
    public Vector3 offset;

    void Start()
    {
        
    }

    //Similar to update, except run straight after
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
