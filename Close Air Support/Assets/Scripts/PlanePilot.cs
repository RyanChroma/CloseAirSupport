using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePilot : MonoBehaviour
{
	public float speed = 90.0f;

	private void Update()
	{
		//PLANE SPEED
		transform.position += transform.forward * Time.deltaTime * 90.0f;
		speed -= transform.forward.y * Time.deltaTime * 2.0f;
		transform.Rotate(Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));

		float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

		if(terrainHeightWhereWeAre > transform.position.y)
		{
			transform.position = new Vector3(transform.position.x, terrainHeightWhereWeAre, transform.position.z);
		}
	}
}