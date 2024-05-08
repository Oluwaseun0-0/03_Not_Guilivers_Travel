using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Prerequisites")]

    [SerializeField] private OVRPassthroughLayer _passthroughLayer;
    [SerializeField] private OVRManager _ovrManager;
    [SerializeField] private Camera _cameraMR;
    [SerializeField] private Camera _cameraVR;
    [SerializeField] private List<GameObject> _VRObjects;
    [SerializeField] private List<GameObject> _MRObjects;

    public bool _MREnabled = true;
    public bool _VREnabled = false;

    public UnityEvent debuggerEvent;
    public UnityEvent debuggerEventTwo;
    
    // Start is called before the first frame update
    void Start()
    {
        SwitchToMR();
        _MRObjects.Add(FindObjectOfType<MRUKRoom>().gameObject); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    [Button]
    public void SwitchReality()
    {
        if (_MREnabled)
        {
            SwitchToVR();
            
        }
        else
        {
            SwitchToMR();
        }
    }

    private void SwitchToMR()
    {
        //Reality bool flags
        _MREnabled = true;
        _VREnabled = false;
        
        //Camera Type controls
        DisableCamera(_cameraVR);
        EnableCamera(_cameraMR);
        
        
        //Passthrough Controler
        _passthroughLayer.enabled = true;
        _ovrManager.isInsightPassthroughEnabled = true;
        
        
        //GameObject controller
        //MR Gameobjects
        foreach (var model in _MRObjects)
        {
            model.SetActive(true);
        }
        
        //VR Gameobjects
        foreach (var model in _VRObjects)
        {
            model.SetActive(false);
        }
        

        
    }

    private void SwitchToVR()
    {
        //Reality bool flags
        _MREnabled = false;
        _VREnabled = true;
        
        //Camera Type controls
        DisableCamera(_cameraMR);
        EnableCamera(_cameraVR);
        
        //Passthrough Controller
        _passthroughLayer.enabled = false;
        _ovrManager.isInsightPassthroughEnabled = false;
        
        //GameObject controller
        //MR Gameobjects
        foreach (var model in _MRObjects)
        {
            model.SetActive(false);
        }
        
        //VR Gameobjects
        foreach (var model in _VRObjects)
        {
            model.SetActive(true);
        }
        
    }


    private void EnableCamera(Camera cameraName)
    {
        cameraName.TryGetComponent(out AudioListener audioListener);
        audioListener.enabled = true;
        cameraName.enabled = true;
        
        cameraName.gameObject.SetActive(true);
    }
    
    
    private void DisableCamera(Camera cameraName)
    {
        cameraName.TryGetComponent(out AudioListener audioListener);
        audioListener.enabled = false;
        cameraName.enabled = false;
        
        cameraName.gameObject.SetActive(false);
    }
    

    [Button]
    private void DebuggerEventStarter()
    {
        debuggerEvent.Invoke();
    }
    
    [Button]
    private void DebuggerEventTwoStarter()
    {
        debuggerEventTwo.Invoke();
    }
}
