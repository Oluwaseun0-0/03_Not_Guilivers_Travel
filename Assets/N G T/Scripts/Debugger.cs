using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{

    public bool isOnTestBool;

    private bool runON = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (runON)
        {
            Debug.Log("SELECTED");
        }
    }

    public void DebugVisual()
    {
       // runON = true;
        Debug.Log("SELECTED");
    }
    
    public void DebugVisual2()
    {
        Debug.Log("Selected 2");
    }

    public void BoolSwitch()
    {
        isOnTestBool = !isOnTestBool;
    }
}
