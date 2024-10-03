using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public Camera cam;
    private float _camFOV;
    public float zoomSpeed;
    private float _mouseScroolInput;
    // Start is called before the first frame update
    void Start()
    {
        _camFOV = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        _mouseScroolInput = Input.GetAxis(Common.mouseScroll);
        _camFOV -= _mouseScroolInput * zoomSpeed;
        _camFOV = Mathf.Clamp(_camFOV, 30, 60);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _camFOV, zoomSpeed);
    }
}
