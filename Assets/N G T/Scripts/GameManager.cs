using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public bool _MREnabled = true;
    public bool _VREnabled = false;
    public bool _PestySwtiching = false;
    public List<GameObject> _VRObjects;
    
    [Header("Prerequisites")]
    [SerializeField] private float _scaleUpFactor;
    [SerializeField] private float _scaleDownFactor;
    [SerializeField] private float _scaleTimeDuration;
    [SerializeField] private OVRPassthroughLayer _passthroughLayer;
    [SerializeField] private OVRManager _oVRManager;
    [SerializeField] private Camera _cameraMR;
    [SerializeField] private Camera _cameraVR;
    [SerializeField] private AnchorPrefabSpawner _anchorPrefabSpawner;
    [SerializeField] private List<GameObject> _MRObjects;
    

    
    public GameObject _roomController;

    public UnityEvent debuggerEvent;
    public UnityEvent debuggerEventTwo;
    private GameObject _mrukRoomDuplicate;

    // Start is called before the first frame update
    void Start()
    {
        DisableCamera(_cameraVR);
        EnableCamera(_cameraMR);
        //_MRObjects.Add(FindObjectOfType<MRUKRoom>().gameObject); 
        _roomController = FindObjectOfType<MRUKRoom>().gameObject; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_roomController)
        {
            _roomController = FindObjectOfType<MRUKRoom>().gameObject;
        }
    }
    
    public void PestySwitching()
    {
        _PestySwtiching = !_PestySwtiching;
    }
    
    [Button]
    public void SwitchReality()
    {
        
        if (!_PestySwtiching) return;
        if (!_roomController) return;
        if (_MREnabled)
        {
            SwitchToVR();
            
        }
        else
        {
            SwitchToMR();
        }

        _PestySwtiching = false;
    }

    private void SwitchToMR()
    {
        /*if (!_roomController)
        {
            _roomController = FindObjectOfType<MRUKRoom>().gameObject; 
        }*/
        //Reality bool flags
        _MREnabled = true;
        _VREnabled = false;
        
        //Camera Type controls
        /*DisableCamera(_cameraVR); //Causing issue with camera settings
        EnableCamera(_cameraMR);*/
        
        
        //Passthrough Controler
        _passthroughLayer.enabled = true;
        _oVRManager.isInsightPassthroughEnabled = true;
        
        
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
        
        //Prefab Spawner settings
        StartCoroutine(SwitchingToMRRoutine(_roomController,_scaleTimeDuration, _scaleDownFactor));
        
        
        //RoomGuardian setup test
        
        
    }

    private void SwitchToVR()
    {
        //Reality bool flags
        _MREnabled = false;
        _VREnabled = true;
        
        //Camera Type controls
        /*DisableCamera(_cameraMR);  //Causing issue with camera settings
        EnableCamera(_cameraVR);*/
        
        //Passthrough Controller
        _passthroughLayer.enabled = false;
        _oVRManager.isInsightPassthroughEnabled = false;
        
        //GameObject controller
        //MR Gameobjects
        /*foreach (var model in _MRObjects)
        {
            model.SetActive(false);
        }*/
        
        //VR Gameobjects
        foreach (var model in _VRObjects)
        {
            model.SetActive(true);
        }
        
        
        
        
        //Prefab Spawner settings
        
        _anchorPrefabSpawner.SpawnPrefabs();
        StartCoroutine(SwitchingToVRRoutine(_roomController,_scaleTimeDuration, _scaleUpFactor));
        
        //RoomGuardian setup test
        //RoomGuardianSetUp();

        
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
    

    
    private void LerpScaleUp()
    {
        StartCoroutine(SwitchingToVRRoutine(_roomController,_scaleTimeDuration, 2f));
        
    }
    
    

    private IEnumerator SwitchingToVRRoutine(GameObject model, float duration, float scaleFactor)
    {
        float time = 0f;
        Vector3 originalScale = model.transform.localScale;
        Vector3 targetScale = originalScale * scaleFactor;
        //_anchorPrefabSpawner.SpawnPrefabs();

        while (time < duration)
        {
            float lerpFactor = time / duration;
            model.transform.localScale = Vector3.Lerp(originalScale, targetScale, lerpFactor);
            time += Time.deltaTime;
            yield return null;
        }

        model.transform.localScale = targetScale;
        //_roomController.SetActive(true);
        
    }

    private IEnumerator SwitchingToMRRoutine(GameObject model, float duration, float scaleFactor)
    {
        float time = 0f;
        Vector3 originalScale = model.transform.localScale;
        Vector3 targetScale = originalScale * scaleFactor;

        while (time < duration)
        {
            float lerpFactor = time / duration;
            model.transform.localScale = Vector3.Lerp(originalScale, targetScale, lerpFactor);
            time += Time.deltaTime;
            yield return null;
        }

        model.transform.localScale = targetScale;
        _anchorPrefabSpawner.ClearPrefabs();
        //RoomGuardianDecouple();
    }

    public void RoomGuardianSetUp()
    {
         _mrukRoomDuplicate = Instantiate(_roomController);
         _roomController.gameObject.SetActive(false);
         
        
    }
    
    public void RoomGuardianDecouple()
    {
        if (_mrukRoomDuplicate)
        {
            Destroy(_mrukRoomDuplicate);
            _mrukRoomDuplicate = null;
        }
        _roomController.gameObject.SetActive(true);
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
