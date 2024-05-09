using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static OVRInput;

public class ControllerInput : MonoBehaviour
{
    [SerializeField] private Controller _leftController;
    [SerializeField] private Controller _righttController;
    
    
    
     private Vector3 _leftTouchPos;
     private Quaternion _leftTouchRot;
     private Vector3 _rightTouchPos;
     private Quaternion _rightTouchRot;
    [SerializeField] private TeleportationObject _leftTeleportationObject;
    [SerializeField] private TeleportationObject _rightTeleportationObject;

    [Header("Left Controller")] 
    public UnityEvent buttonXPressed;
    public UnityEvent buttonYPressed;
    public UnityEvent leftTriggerPressed;
    public UnityEvent leftGripPressed;
    
    [Header("Right Controller")]
    public UnityEvent buttonAPressed;
    public UnityEvent buttonBPressed;
    public UnityEvent rightTriggerPressed;
    public UnityEvent rightGripPressed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (OVRInput.GetUp(OVRInput.RawButton.X))
        {
            buttonXPressed.Invoke();
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.Y))
        {
            buttonYPressed.Invoke();
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            buttonAPressed.Invoke();
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            buttonBPressed.Invoke();
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            Debug.Log("RIGHT TRIGGER ");
            rightTriggerPressed.Invoke();
            /*if (!_leftTeleportationObject)
            {
                _leftTeleportationObject.TeleportXRRig();
            }*/

        }
        else if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        {
            Debug.Log("LEFT TRIGGER ");
            leftTriggerPressed.Invoke();
            /*if (!_rightTeleportationObject)
            {
                _rightTeleportationObject.TeleportXRRig();
            }*/

        }
        else if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger))
        {
            rightGripPressed.Invoke();
        }
        else if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            leftGripPressed.Invoke();
        }
        
        



       



    }
}
