using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public List<Transform> targets;

    public float smoothTime = 1f;

    public Vector3 offset;
    private Vector3 velocity;

    public float minZoom = 20f;
    public float maxZoom = 5f;
    public float zoomLimiter = 50f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }

    void Zoom()
    {
        float newSize = Mathf.Lerp(maxZoom, minZoom, GetGratestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime);
    }


    float GetGratestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        
        return bounds.size.x;
    }

    void Move()
    { 
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
