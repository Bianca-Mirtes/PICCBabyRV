using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public static StateController Instance { get; private set; }

    protected State StateNow = State.VerificarCasoPicc;

    private void Awake() { Instance = this; }

    public void SetState(State state) { StateNow = state; }

    public State GetState() { return StateNow; }

    public bool CompareStates(State state) {
        if(state.ToString() == StateNow.ToString())
            return true;
        else
            return false;
    }
}