using UnityEngine;

public class InputManager : MonoBehaviour
{
	private readonly string Horizontal = "Horizontal";
	[SerializeField] private FloatValue horizontalInput;
	[SerializeField] private GameEvent OnScreenRightSideTouch;
	private int screenHalfWidth;
	private int moveAnchorPoint = int.MinValue;

	private void OnEnable()
	{
		screenHalfWidth = Screen.width / 2;
		ResetInputValues();
	}

	private void ResetInputValues()
	{
		horizontalInput.value = 0;
	}

	public void OnUpdate()
	{
#if UNITY_EDITOR
		EditorUpdate();
#elif UNITY_ANDROID
		AndroidUpdate();
#endif
	}

	private void EditorUpdate()
	{
		horizontalInput.value = Input.GetAxis(Horizontal);

		if (Input.GetMouseButtonDown(0))
			OnScreenRightSideTouch?.InvokeEvent();
	}

	private void AndroidUpdate()
	{
		if (Input.touchCount == 0)
			return;

		for (int i = 0; i < Mathf.Min(Input.touchCount, 2); i++)
			HandleTouch(Input.GetTouch(i));
	}

	private void HandleTouch(Touch touch)
	{
		if (touch.position.x > screenHalfWidth)
			HandleShootTouch(touch);
		else
			HandleMoveTouch(touch);

	}
	private void HandleMoveTouch(Touch touch)
	{
		if (touch.phase == TouchPhase.Began)
		{
			moveAnchorPoint = (int)touch.position.x;
			return;
		}

		if (touch.phase == TouchPhase.Ended)
		{
			moveAnchorPoint = int.MinValue;
			horizontalInput.value = 0;
			return;
		}

		if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
		{
			horizontalInput.value = touch.position.x > moveAnchorPoint ? 1 : -1;
			moveAnchorPoint = (int)touch.position.x;			
		}
	}

	private void HandleShootTouch(Touch touch)
	{
		if (touch.phase != TouchPhase.Began)
			return;

		OnScreenRightSideTouch?.InvokeEvent();
	}
}
