using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    PlayerControl,
    ScriptControl,
    Dialog,
}

public class GameController : MonoBehaviour {

    public static GameController Instance { get; private set; }
    
    public GameState CurrentState { get; set; }

    private void Awake() {
        Instance = this;
        CurrentState = GameState.PlayerControl;
    }
}