using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class RightControllerInput : MonoBehaviour
{

    [SerializeField] private ControllerInput _defaultControllerInput;
    [SerializeField] private TeleportaionObjectController _teleportaionObjectController;
    [SerializeField] private LineRenderer _RightLineRenderer;
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
        Ray rightRay = new Ray(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch),
            OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward);
        if (Physics.Raycast(rightRay, out RaycastHit hit))
        {
            //Debug.Log($"RIGHT HIT {hit.collider.gameObject.name}");
           // TeleportationObject rightTeleportationObject = hit.collider.gameObject.GetComponent<TeleportationObject>();
            if(hit.collider.gameObject.TryGetComponent(out TeleportationObject rightTeleportationObject))
            {
                _telePad = rightTeleportationObject;
                _telePad.canTeleport = true;
                _telePad.startScalling = true;

            }
            /*else if (!hit.collider.gameObject.TryGetComponent(out TeleportationObject testt))
            {
                if (!_rightTeleportationObject) return;
                _rightTeleportationObject.canTeleport = false;
                _rightTeleportationObject.ResetScale();
                _rightTeleportationObject = null;


            }*/

        }
        

    }

    [Button]
    public void RightControllerTeleportXRrig()
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
