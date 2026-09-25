using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;

    [SerializeField] InputActionAsset inputAction;
    InputActionMap UI;
    InputAction submit;

    private void Awake()
    {
        UI = inputAction.FindActionMap("UI");
        submit = UI.FindAction("Submit");
    }
    private void OnEnable()
    {
        UI.Enable();
    }
    private void OnDisable()
    {
        UI.Disable();
    }

    private void Update()
    {
        if (submit.WasCompletedThisFrame() && (nameInput.text != null))
        {
            PlayerPrefs.SetString("userName", nameInput.text);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Intro");
        }
    }
}
