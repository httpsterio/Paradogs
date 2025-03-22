
using UnityEngine;

public class StartLevelMenuAction : MainMenuAction {

	public string levelName;

	public override void Perform() {
		Application.LoadLevel (levelName);
	}
}
