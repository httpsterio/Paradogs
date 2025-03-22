
using UnityEngine;

public class ExitMenuAction : MainMenuAction {

	public override void Perform() {
		Debug.Log ("App quit.");
		Application.Quit ();
	}
}
