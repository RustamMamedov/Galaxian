using UnityEngine;
using UnityEngine.UI;

public class UIValueObserver : MonoBehaviour
{
    [SerializeField] private IntegerValue observableBalue;
    [SerializeField] private Text text;
    private int prevValue;

    public void OnUpdate()
    {
        if (prevValue == observableBalue.Value)
            return;

        text.text = observableBalue.Value.ToString();
    }
}
