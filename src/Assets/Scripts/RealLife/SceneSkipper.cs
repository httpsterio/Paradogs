using UnityEngine;
using System.Collections;

public class SceneSkipper : MonoBehaviour {

	public InteractionController sceneSkipper;


	// Update is called once per frame
	void Update () {
		string input = Input.inputString;

		if (input.Contains (" ")) {
			sceneSkipper.OnPlayerInteraction();
			gameObject.SetActive(false);
		}
	}
}
