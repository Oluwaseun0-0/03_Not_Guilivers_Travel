using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportaionObjectController : MonoBehaviour
{
    public List<TeleportationObject> telepads;
    public int counter = 0;
    public TeleportationObject currentTelePad;

    [SerializeField] private GameObject _teleportationPrefab;
    [SerializeField] private Transform _spawnPos;

    private GameManager _gameManager;
    private int noOFObject = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (telepads.Count != 0)
        {
            currentTelePad = telepads[counter];
        }
        
    }

    
    [Button]
    public void CreatePlatform()
    {
        if (_gameManager._VREnabled || _gameManager._MREnabled)
        {
            noOFObject++;
            GameObject platform = Instantiate(_teleportationPrefab, _spawnPos.position, Quaternion.identity);

            platform.gameObject.name = $"platform {noOFObject}";
            telepads.Add(platform.GetComponent<TeleportationObject>());
            
            _gameManager._VRObjects.Add(platform);

        }
    }

    /*public void CurrentTelepad()
    {
        currentTelePad = telepads[counter];
    }*/
    
    
    [Button]
    public void CycleTelepadsNo()
    {
        
        if (counter !=0 && counter > telepads.Count - 2)
        {
            counter = 0;
        }
        else
        {
            counter++;
        }
        

        
    }
}
