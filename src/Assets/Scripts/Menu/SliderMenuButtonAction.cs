

using UnityEngine;

public class SliderMenuButtonAction : MainMenuAction {
	public enum SliderDirection {left, right };

	public SlidingMenu menu;
	public SliderDirection direction = SliderDirection.left;

	public GraphicalEffect deactivationEffect;
	public GraphicalEffect activationEffect;

	private bool actived = true;

	public void Start() {
		menu.RegisterMenuButton (this);
	}

	public override void Perform() {
		menu.ButtonClicked (this);
	}

	public void EnsureDeactivated() {
		if (actived) {
			actived = false;
			if (deactivationEffect != null) {
				deactivationEffect.EnqueueToScheduler();
			} else {
				gameObject.SetActive(false);
			}
		}
	}

	public void EnsureActivated() {
		if (!actived) {
			actived = true;
			if (activationEffect != null) {
				activationEffect.EnqueueToScheduler();
			} else {
				gameObject.SetActive(true);
			}
		}
	}
}
