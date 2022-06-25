using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {
    
    public static DialogBox Instance { get; private set; }

    [SerializeField] private Text textField;

    private void Awake() {
        Instance = this;
    }
}