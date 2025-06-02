using Naninovel;
using UnityEngine;
using UnityEngine.UI;

public class MapButtonController : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetInteractable(string variableName)
    {
        Engine.GetService<ICustomVariableManager>().TryGetVariableValue(variableName, out bool value);
        _button.interactable = value;
    }
}
