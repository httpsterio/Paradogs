using UnityEngine;
using System.Collections;

public class MenuButtonController : MonoBehaviour {

	public GraphicalEffect mouseEnterEffect;
	public GraphicalEffect mouseExitEffect;
	public GraphicalEffect onClickEffect;
	

	void OnMouseEnter() {
		NullCheckRun (mouseEnterEffect);
	}

	void OnMouseExit() {
		NullCheckRun (mouseExitEffect);
	}

	void OnMouseUpAsButton() {
		NullCheckRun (onClickEffect);

		MainMenuAction[] actions = GetComponents<MainMenuAction> ();
		foreach (MainMenuAction action in actions) {
			action.Activate();
		}
	}

	private void NullCheckRun(GraphicalEffect effect) {
		if (effect != null) {
			effect.EnqueueToScheduler();
		}
	}
}
