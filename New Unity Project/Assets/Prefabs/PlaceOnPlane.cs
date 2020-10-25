using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }
    public GameObject arCamera = null;

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; } = null;

    void Start()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        arCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if (Input.touchCount <= 0) {
            return;
       } else if (Input.touchCount > 1) {
            Debug.Log("2 Fingers Detected");
            if (spawnedObject != null) {   
                Debug.Log("Map Object exists. Resizing...");
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
                // Calculate previous position
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                float pinchAmount = deltaMagnitudeDiff * 0.02f * Time.deltaTime;
                arCamera.transform.localScale -= new Vector3(pinchAmount, pinchAmount, pinchAmount);
                // Clamp scale
                float Min = 0.005f;
                float Max = 3f;
                arCamera.transform.localScale = new Vector3(
                    Mathf.Clamp(arCamera.transform.localScale.x, Min, Max),
                    Mathf.Clamp(arCamera.transform.localScale.y, Min, Max),
                    Mathf.Clamp(arCamera.transform.localScale.z, Min, Max)
                );
                Debug.Log("Finished Resizing Map Object");
            }
        }
        else if (Input.touchCount == 1) {
            if (m_RaycastManager.Raycast(Input.GetTouch(0).position, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    Debug.Log("Adding Map Object");
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    Debug.Log("Moving Map Object");
                    spawnedObject.transform.position = hitPose.position;
                }
            }
        }
    }

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}