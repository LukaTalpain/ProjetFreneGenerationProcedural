using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ToggleMovementInput", menuName = "RSE/ToggleMovementInput", order = 0)]
public class RSE_ToggleMovementInput : ScriptableObject
{
    public event Action ToggleMovementInputEvent;
    public void InvokeToggleMovementInput ()
    {
        ToggleMovementInputEvent?.Invoke();
    }
}
