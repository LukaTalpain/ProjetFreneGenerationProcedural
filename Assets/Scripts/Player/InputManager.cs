using UnityEngine;

public class InputManager : MonoBehaviour
{
    public RSE_InputEvent inputEvent;
    [SerializeField] private RSE_ToggleMovementInput toggleMovementInput;
    private bool isInputEnabled = true;

    private void OnEnable()
    {
        toggleMovementInput.ToggleMovementInputEvent += ToggleInput;
    }
    private void OnDisable()
    {
        toggleMovementInput.ToggleMovementInputEvent -= ToggleInput;
    }
    private void Awake()
    {
        isInputEnabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inputEvent.InvokeEchapPressed();
        }
        if (!isInputEnabled) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            inputEvent.InvokeZPressed();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            inputEvent.InvokeQPressed();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputEvent.InvokeSPressed();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            inputEvent.InvokeDPressed();
        }
        
    }

    private void ToggleInput ()
    {
        isInputEnabled = !isInputEnabled;
    }

}
