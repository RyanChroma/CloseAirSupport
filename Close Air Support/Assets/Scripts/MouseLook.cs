using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform camPivot;
    public Quaternion defaultPivot;
    public float rotateBackSpeed = 0.0f;

	void Start()
	{
        defaultPivot = camPivot.localRotation;
	}

	void Update()
    {
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
    }
}
