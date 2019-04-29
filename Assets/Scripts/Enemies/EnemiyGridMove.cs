using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiyGridMove : MonoBehaviour
{
	[SerializeField, Range(.005f, .05f)] private float speed = .01f;
	[SerializeField, Range(1f, 5f)] private float maxOffset = 3f;
	[SerializeField] private BoolValue isGamePlayStarted;
	private int direction = 1;

	public void OnUpdate()
	{
		if (!isGamePlayStarted.Value)
			return;

		transform.Translate(direction * speed * Vector2.right);

		if (transform.position.x * direction > maxOffset || transform.position.x * direction < -maxOffset)
			direction *= -1;
	}

	public void ResetPosition()
	{
		transform.position = new Vector2(0, transform.position.y);
	}
}
