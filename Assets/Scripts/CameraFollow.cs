using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private int startTime; // from LevelController
    private int currentTime; // from LevelController

    LevelController levelController;




    // Transform of the GameObject you want to shake
    private new Transform transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.04f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 0.4f;

    // The initial position of the GameObject
    Vector3 initialPosition;





    void Start()
    {
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();
        startTime = levelController.startTime;
        currentTime = levelController.currentTime;

        cam = GetComponent<Camera>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            targets.Add(player.transform);
        }
    }

    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void Update()
    {
        ScreenShake();
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }


    void OnEnable()
    {
        initialPosition = transform.localPosition;
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
        float newSize = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime);
    }


    float GetGreatestDistance()
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

    void ScreenShake()
    {
        currentTime = levelController.currentTime;
        if (currentTime <= 1)
        {
            shakeDuration = 0.1f;
        }
    }
}
