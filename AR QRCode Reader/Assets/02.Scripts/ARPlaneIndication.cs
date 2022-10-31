using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaneIndication : MonoBehaviour
{
    ARRaycastManager arRaycastManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public Camera ARCam;
    public GameObject ARIndicator;

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        PlaneIndication();
    }

    private void PlaneIndication()
    {
        var screenCenter = ARCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        if (arRaycastManager.Raycast(screenCenter, hits, TrackableType.All))
        {
            Pose hitPos = hits[0].pose;

            var cameraForward = ARCam.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            hitPos.rotation = Quaternion.LookRotation(cameraBearing);
            ARIndicator.SetActive(true);
            ARIndicator.transform.SetPositionAndRotation(hitPos.position, hitPos.rotation);
        }
        else
        {
            ARIndicator.SetActive(false);
        }
    }
}