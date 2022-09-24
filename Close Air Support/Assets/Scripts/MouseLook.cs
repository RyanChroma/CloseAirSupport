using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform camPivot;
    public Transform cam;
    public float zoomDepth = 0.0f;
    public Vector3 camPos;
    public Quaternion defaultPivot;
    public float rotateBackSpeed = 0.0f;

	void Start()
	{
        defaultPivot = camPivot.localRotation;
        camPos = cam.localPosition;
	}

	void Update()
    {
        //CAMERA WILL NOT ROTATE CLOCKWISE/COUNTER CLOCKWISE
        Vector3 mainCameraController = transform.position - transform.forward * 15.0f + Vector3.up * 1.0f; //transform is relative, vector is universal.
		float bias = 0.96f;
		Camera.main.transform.position = Camera.main.transform.position * bias + mainCameraController * (1.0f - bias);
		Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);

        if (Input.GetKey(KeyCode.C))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            camPivot.Rotate(Vector3.up * mouseX);
            camPivot.Rotate(Vector3.left * mouseY);
        }
        else
		{
            //WHEN LET GO OF C, IT WILL GO BACK TO DEFAULT CAM ROTATION
            camPivot.localRotation = Quaternion.Lerp(camPivot.localRotation, defaultPivot, rotateBackSpeed * Time.deltaTime);
		}

        //RIGHT CLICK TO ZOOM
        /*if(Input.GetKey(KeyCode.Mouse1))
		{
            Vector3 targetCamPos = camPos + cam.forward * zoomDepth;
            cam.localPosition = Vector3.Lerp(cam.localPosition, targetCamPos, 5 * Time.deltaTime);
        }
        else
		{
            cam.localPosition = Vector3.Lerp(cam.localPosition, camPos, 5 * Time.deltaTime);
        }*/
    }
}