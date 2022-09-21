using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour
{
	public float speed = 90.0f;

	private void Update()
	{
		//CAMERA WILL NOT ROTATE CLOCKWISE/COUNTER CLOCKWISE
		Vector3 moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f; //transform is relative, vector is universal.
		float bias = 0.96f;
		Camera.main.transform.position = Camera.main.transform.position * bias + moveCamTo * (1.0f - bias);
		Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f);

		//PLANE SPEED
		transform.position += transform.forward * Time.deltaTime * speed;
		speed -= transform.forward.y * Time.deltaTime * 50.0f;

		if(speed < 50.0f)
		{
			speed = 50.0f;
		}

		//PLANE MOVEMENT
		transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));

		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

		if(terrainHeightWhereWeAre > transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, terrainHeightWhereWeAre, transform.position.z);
		}
	}
}