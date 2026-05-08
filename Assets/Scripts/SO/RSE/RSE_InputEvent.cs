using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputEvent", menuName = "RSE/InputEvent", order = 0)]
public class RSE_InputEvent : ScriptableObject
{
    public event Action zPressed;
    public void InvokeZPressed ()
    {
        zPressed?.Invoke ();
    }



    public event Action qPressed;
    public void InvokeQPressed ()
    {
        qPressed?.Invoke ();
    }



    public event Action sPressed;
    public void InvokeSPressed ()
    {
        sPressed?.Invoke ();
    }



    public event Action dPressed;
    public void InvokeDPressed ()
    {
        dPressed?.Invoke ();
    }


    public event Action echapPressed;
    public void InvokeEchapPressed ()
    {
        echapPressed?.Invoke ();
    }


}
