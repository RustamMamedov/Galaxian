using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	[SerializeField] private FloatValue horizontalInput;
	[SerializeField, Range(1f, 10f)] private float maxSpeed = 6;
	[SerializeField, Range(10f, 100f)] private float acceleration = 60f;
	[SerializeField] private BoolValue isGamePlayStarted;
	[SerializeField] private FloatValue playerXPosition;
    
	private Rigidbody2D rigidBody;

	private void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	public void Move()
	{
		if (!isGamePlayStarted.Value)
			return;

		if (Mathf.Abs(rigidBody.velocity.x) >= maxSpeed)
			return;

		rigidBody.AddForce(Vector2.right * horizontalInput.value * acceleration);

		playerXPosition.value = transform.position.x;
	}
}
