using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicTarget : MonoBehaviour
{
	[SerializeField] Camera m_camera = null;

	private void Start()
	{
		m_camera = (m_camera == null) ? Camera.main : m_camera;
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector2 mousePosition = Input.mousePosition;
			Vector2 position = m_camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
			position = new Vector3(position.x, position.y, -2);

			transform.position = position;
		}
	}
}
