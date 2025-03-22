using UnityEngine;
using System.Collections;

public class DialogChanger : MonoBehaviour {

    private DialogController dialogController;
    public string text = "";
    public float duration = 0;
    public bool triggerOnce = true;

    private bool triggered = false;

	// Use this for initialization
	void Start () {
        dialogController = GameObject.Find("DialogController").GetComponent<DialogController>();
	}
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (triggerOnce && !triggered)
            dialogController.updateText(text, duration);

        triggered = true;
    }
}
