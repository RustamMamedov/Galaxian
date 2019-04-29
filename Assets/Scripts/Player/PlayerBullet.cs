using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	[SerializeField, Range(.05f, .2f)] private float speed = .1f;

	public bool Throw(Vector2 startPosition)
	{
		if (gameObject.activeSelf)
			return false;

        transform.position = startPosition;
		gameObject.SetActive(true);
        return true;
	}

	public void OnUpdate()
	{
		if (!gameObject.activeSelf)
			return;

        Move();
	}

	private void Move()
	{
		transform.Translate(0, speed, 0);
	}
}
