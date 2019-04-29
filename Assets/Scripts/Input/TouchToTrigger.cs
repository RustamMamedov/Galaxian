using UnityEngine;
using UnityEngine.Events;


public class TouchToTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent OnTouch;

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
		if (Input.GetMouseButtonDown(0))
			OnTouch?.Invoke();
	}

	private void AndroidUpdate()
	{
        if (Input.touchCount == 0)
            return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
            OnTouch?.Invoke();
	}    
}
