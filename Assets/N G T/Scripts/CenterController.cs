using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private OVRCameraRig _XRrig;
    

    //public TeleportationObject currentTeleportationObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._VREnabled)
        {
            Vector2 axis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
            _XRrig.transform.Translate(new Vector3(axis.x, 0 , axis.y) * (4 * Time.deltaTime));
            
        }
    }


   
}
