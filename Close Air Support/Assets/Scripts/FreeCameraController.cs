using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
	void Update()
	{
		/*Vector3 freeCameraController = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f; //transform is relative, vector is universal.
		float bias = 0.96f;
		Camera.main.transform.position = Camera.main.transform.position * bias + freeCameraController * (1.0f - bias);
		Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);*/
	}
}
