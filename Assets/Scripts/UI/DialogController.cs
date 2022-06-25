using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {
    
    public static DialogController Instance { get; private set; }

    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text textField;
    [SerializeField] private int lettersPerSecond;
    
    private Queue<string> messages;
    private bool waitingForProceed;
    private bool proceed;

    private void Awake() {
        Instance = this;
        messages = new Queue<string>();
        waitingForProceed = false;
        proceed = false;
        
        dialogBox.SetActive(false);
    }

    private void Update() {
        CheckStartDialog();
        CheckForProceed();
    }

    public void AddMessageToQueue(string message) {
        messages.Enqueue(message);
    }

    private void CheckStartDialog() {
        if (!dialogBox.activeInHierarchy && messages.Count > 0) {
            GameController.Instance.CurrentState = GameState.Dialog;
            StartCoroutine(ShowMessage(messages.Dequeue()));
        }
    }

    private void CheckForProceed() {
        if (waitingForProceed) {
            if (Input.GetButtonDown("Proceed")) {
                proceed = true;
                waitingForProceed = false;
            }
        }
    }

    private IEnumerator ShowMessage(string message) {
        dialogBox.SetActive(true);
        textField.text = "";
        
        yield return TypeLine(message);

        waitingForProceed = true;
        yield return new WaitUntil(() => proceed);
        proceed = false;

        if (messages.Count == 0) {
            ExitDialog();
        }
    }

    private IEnumerator TypeLine(string line) {
        foreach (var c in line.ToCharArray()) {
            textField.text += c;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    private void ExitDialog() {
        GameController.Instance.CurrentState = GameState.PlayerControl;
        dialogBox.SetActive(false);
    }
}