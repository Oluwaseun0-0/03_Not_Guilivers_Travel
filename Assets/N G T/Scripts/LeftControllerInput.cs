using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class LeftControllerInput : MonoBehaviour
{

    [SerializeField] private ControllerInput _defaultControllerInput;
    [SerializeField] private LineRenderer _LeftLineRenderer;
    [SerializeField] private TeleportaionObjectController _teleportaionObjectController;
    [SerializeField] private TeleportationObject _telePad;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        _telePad = _teleportaionObjectController.currentTelePad;
        TelePadIndicator();
        Ray leftRay = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch),
            OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch) * Vector3.forward);
        
        if (Physics.Raycast(leftRay, out RaycastHit hit))
        {
            //Debug.Log($"LEFT HIT {hit.collider.gameObject.name}");
            if(hit.collider.gameObject.TryGetComponent(out TeleportationObject leftTeleportationObject))
            {
                _telePad = leftTeleportationObject;
                _telePad.canTeleport = true;
                _telePad.startScalling = true;

            }
            /*else if (!hit.collider.gameObject.TryGetComponent(out TeleportationObject testt))
            {
                if (!_leftTeleportationObject) return;
                _leftTeleportationObject.canTeleport = false;
                _leftTeleportationObject.ResetScale();
                _leftTeleportationObject = null;


            }*/
            
        }
        

    }
    

    [Button]
    public void LeftControllerTeloportXRrig()
    {
        if (!_telePad) return;
        _telePad.TeleportXRRig();

        //StartCoroutine(MakingTeleporterNull(1));
    }

    private IEnumerator MakingTeleporterNull(float duration)
    {
        yield return new WaitForSeconds(duration);
        _telePad.canTeleport = false;
        _telePad.ResetScale();
        _telePad = null;
    }
    
    public void TelePadIndicator()
    {
        if (!_telePad) return;
        _telePad.canTeleport = true;
        _telePad.startScalling = true;
    }

    public void FailedRayVisual()
    {
        /*Debug.Log("Reading");
        var _leftTouchPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        var _leftTouchRot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        var leftRayDirection = _leftTouchRot * Vector3.forward;

        var _rightTouchPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        var _rightTouchRot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        Vector3 rightRayDirection = _rightTouchRot * Vector3.forward;




        if (Physics.Raycast(_leftTouchPos, leftRayDirection, out RaycastHit leftHit))
        {
            _LeftLineRenderer.SetPosition(0, _leftTouchPos);

            if (leftHit.collider.gameObject.TryGetComponent(out TeleportationObject leftTeleportationObject))
            {

                if (!leftTeleportationObject)
                {
                    leftTeleportationObject.canTeleport = true;
                    _leftTeleportationObject = leftTeleportationObject;
                    _LeftLineRenderer.SetPosition(1, leftTeleportationObject.transform.position);
                }
                else
                {
                    _leftTeleportationObject = null;

                }
            }

        }

        if (Physics.Raycast(_rightTouchPos, rightRayDirection, out RaycastHit rightHit))
        {

            if (rightHit.collider.gameObject.TryGetComponent(out TeleportationObject rightTeleportationObject))
            {
                _RightLineRenderer.SetPosition(0, _rightTouchPos);
                _RightLineRenderer.SetPosition(1, rightTeleportationObject.transform.position);
                rightTeleportationObject.canTeleport = true;
                _rightTeleportationObject = rightTeleportationObject;
            }
            else
            {
                _rightTeleportationObject = null;
            }
        }*/
    }

    
}
