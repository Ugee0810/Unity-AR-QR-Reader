using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    public static ObjectPlacement Instance;

    private ARRaycastManager raycastManager;
    private Pose placementPose;
    private bool placementPoseisValid;
    private bool isObjectPlaced;

    public GameObject positionIndicator;
    public GameObject[] prefabToPlace;
    public Camera ARCamera;

    public Text txt;
    private GameObject PlacedObj;

    [HideInInspector]
    public string qrcode;

    private void Awake()
    {
        Instance = this;
        raycastManager = GetComponent<ARRaycastManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ObjectPlacement>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isObjectPlaced)
        {
            UpdatePlacementPose();
            if (placementPoseisValid && qrcode != "") PlaceObject(qrcode);
            {

            }
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = ARCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        raycastManager.Raycast(screenCenter, hits, TrackableType.All);
        placementPoseisValid = hits.Count > 0;

        if (placementPoseisValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = ARCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            positionIndicator.SetActive(true);
            positionIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            positionIndicator.SetActive(false);
        }
    }

    private void PlaceObject(string qrcode)
    {
        if (qrcode == "Object")
        {
            PlacedObj = Instantiate(prefabToPlace[0], placementPose.position, placementPose.rotation);
            txt.text = "오브젝트가 생성되었습니다!";
        }
        isObjectPlaced = true;
    }
}