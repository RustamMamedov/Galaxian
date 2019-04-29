using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Better to use pool for bullets
public class Enemybullet : MonoBehaviour
{
	[SerializeField] private float speed = .1f;
	private Camera camera;

	private void Awake()
	{
		camera = Camera.main;
	}

	public void Die()
	{
		Destroy(gameObject);
	}


	public void OnUpdate()
	{
		transform.Translate(-Vector2.up * speed);

		Vector2 screenPosition = camera.WorldToScreenPoint(transform.position);

		if (screenPosition.y < 0)
			Die();
	}
}
