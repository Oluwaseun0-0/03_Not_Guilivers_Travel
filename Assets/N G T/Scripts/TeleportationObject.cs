using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class TeleportationObject : MonoBehaviour
{
    public bool canTeleport;
    public bool canOrient = false;
    public bool startScalling = false;
    [SerializeField] GameObject XRrig;
    

    private float _timer = 4;
    private float _scalingTimer = 3;
    private float scaleFactor = 3f;
    private Vector3 originalScale;
    private Vector3 targetScale;
    [SerializeField]private Renderer _renderer;
    private Color _originalColor;
    private Rigidbody _rb;
    [SerializeField] private Vector3 rigOffset = Vector3.zero;

    private void OnEnable()
    {
        _renderer = GetComponent<Renderer>();
        _rb = GetComponent<Rigidbody>();
        XRrig = FindObjectOfType<OVRCameraRig>().gameObject;
        canOrient = true;
        
        originalScale = transform.localScale;
        targetScale = originalScale * scaleFactor;

        _originalColor = _renderer.material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canOrient)
        {
            OrientationAdjuster();
        }

        if (startScalling)
        {
            //LerpScale();
            _scalingTimer -= Time.deltaTime;
            if (_scalingTimer > 0)
            {
                    float lerpFactor = 1 - (_scalingTimer / 3f); // 3f is the duration of scaling
                    transform.localScale = Vector3.Lerp(originalScale, targetScale, lerpFactor);
                    _renderer.material.color = Color.red;
            }
            else
            {
                    // Reset scale
                    _renderer.material.color = _originalColor;
                    startScalling = false;
                    _scalingTimer = 3;
                    transform.localScale = originalScale;
            }
        }
        else
        {
            ResetScale();
        }

    }

    void OrientationAdjuster()
    {
        canOrient = false;
        StartCoroutine(OrientingObject(_timer));
    }

    private IEnumerator OrientingObject(float timer)
    {
        
        yield return new WaitForSeconds(timer);
        _rb.isKinematic = true;

        gameObject.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(0.5f);
        _rb.isKinematic = false;
        canOrient = true;
    }


    public void TeleportXRRig()
    {
        //if (!canTeleport) return;
        
        StartCoroutine(MovingRigRoutine(2f));
    }

    private IEnumerator MovingRigRoutine(float duration)
    {
        float time = 0;
        _rb.isKinematic = true;
        Vector3 originalPos = XRrig.transform.position;
        Vector3 targetPos = transform.position + rigOffset;

        while (time < duration)
        {
            float lerpFac = time / duration;
            XRrig.transform.position = Vector3.Lerp(originalPos, targetPos, lerpFac);
            time += Time.deltaTime;
            yield return null;
        }

        XRrig.transform.position = targetPos;
        _rb.isKinematic = false;

    }

    public void LerpScale()
    {
        startScalling = false;
        StartCoroutine(LerpingScale(_scalingTimer));
    }

    private IEnumerator LerpingScale(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            _renderer.material.color = Color.red;
            float lerpFactor = time / duration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, lerpFactor);
            time += Time.deltaTime;
            
            yield return null;
        }
        transform.localScale = targetScale;
        
        // Wait for a brief period
        yield return new WaitForSeconds(0.25f);

        // Lerp back to the original scale
        time = 0f;
        while (time < duration)
        {
            float lerpFactor = time / duration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, lerpFactor);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localScale = originalScale;
    }

    public void ResetScale()
    {
        transform.localScale = originalScale;
        _renderer.material.color = _originalColor;

    }
}
