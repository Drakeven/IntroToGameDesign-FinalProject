using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    private LevelController levelController; // the Level Controller of the level

    private Camera cam; // the current camera Component

    public List<Transform> targets; // the targets (players) to track with the camera

    public Vector3 offset; // the offset of the camera in relation to the targets it tracks
    public Vector3 velocity; // the velocity of the camera

    public float smoothTime = 1f; // the time it takes to smooth the camera
    public float minZoom = 20f; // the minimum level of zoom of the camera
    public float maxZoom = 5f; // the maximum level of zoom of the camera
    public float zoomLimiter = 50f;  // the limited level of zoom of the camera

    private new Transform transform; // transform of the camera
    private Vector3 initialPosition; // initial position of the camera
    private float shakeMagnitude = 0.04f; // measure of magnitude for the shake
    private float shakeDuration = 0f; // duration of the shake effect
    private float dampingSpeed = 0.4f; // how quickly the shake effect should stop

    void Start()
    {
        // find the Level Controller in the scene
        levelController = GameObject.Find("Level Controller").GetComponent<LevelController>();

        // get the Camera Component off this GameObject
        cam = GetComponent<Camera>();

        // get all the players in the scene at start
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // loop through all of the players found
        foreach (GameObject player in players)
        {
            // add each of the players into the targets list
            targets.Add(player.transform);
        }
    }

    void Awake()
    {
        // if there isn't currently a transform set
        if (transform == null)
        {
            // find the transform of the current GameObject
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void Update()
    {
        // apply screenshake on the camera
        //ScreenShake(); //disabled because it breaks camera movement
    }

    void OnEnable()
    {
        // set the inital position of the camera
        initialPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        // don't do anything if there aren't any targets
        if (targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }

    void Zoom()
    {
        // calculate the new size based off the locations of the players
        float newSize = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

        // set the camera's size, lerping from the current size to the new size
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime);
    }


    float GetGreatestDistance()
    {
        // calculate the distance between players in the targets list
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    void Move()
    {
        // move the camera's position to be in the center of of all of the targets, adding any offset
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        // if there's only one target, return it's location
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        // calculate the center point between the targets
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

    void ScreenShake()
    {
        // if the current timer's time is 1 second left, add time to the shake duration
        if (levelController.currentTime <= 1)
        {
            shakeDuration = 0.1f;
        }

        // if there is time left in the screenshake's duration
        if (shakeDuration > 0)
        {
            // apply the shaking effect by moving the position of the camera
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            // remove time from the duration left
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // reset the duration counter
            shakeDuration = 0f;

            // reset the position of the camera
            transform.localPosition = initialPosition;
        }
    }
}