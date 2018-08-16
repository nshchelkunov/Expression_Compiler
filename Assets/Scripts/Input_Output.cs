
using UnityEngine;
using UnityEngine.UI;

public class Input_Output : MonoBehaviour
{
    public InputField inputField;
    public Text output;

    private string input;

    public void OnSubmitProcessing()
    {
        input = inputField.text;
        inputField.DeactivateInputField();

        output.text = Application.Processing(input);

        inputField.ActivateInputField();
    }
}


