using UnityEngine;
using System.Collections;

public class SlidingMenu : MonoBehaviour {

	public GameObject[] menuItems;
	public int currentItem = 0;

	public SliderMenuButtonAction buttonLeft;
	public SliderMenuButtonAction buttonRight;

	public GraphicalEffect slideOutLeftEffect;
	public GraphicalEffect slideOutRightEffect;

	public GraphicalEffect slideInLeftEffect;
	public GraphicalEffect slideInRightEffect;

	void Start () {
		initActivation ();
	}
	
	public void ButtonClicked(SliderMenuButtonAction action) {
		if (action == buttonLeft) {
			ScrollLeft();
		} else if (action == buttonRight) {
			ScrollRight();
		} else {
			Debug.LogWarning("An unrecognized button was clicked on SlidingMenu. " + gameObject);
		}
	}

	public void RegisterMenuButton(SliderMenuButtonAction action) {
		SliderMenuButtonAction.SliderDirection direction = action.direction;

		if (direction == SliderMenuButtonAction.SliderDirection.left) {
			buttonLeft = action;
		} else {
			buttonRight = action;
		}
		UpdateButtonsActive ();
	}

	private void ScrollLeft() {
		MenuItemAnimation (slideOutLeftEffect, false);
		currentItem--;
		MenuItemAnimation (slideInLeftEffect, true);

		UpdateButtonsActive ();
	}

	private void ScrollRight() {
		MenuItemAnimation (slideOutRightEffect, false);
		currentItem++;
		MenuItemAnimation (slideInRightEffect, true);

		UpdateButtonsActive ();
	}

	private void MenuItemAnimation(GraphicalEffect animationEffect, bool targetItemActive) {
		GameObject target = menuItems [currentItem];
		if (animationEffect != null) {
			animationEffect.targetObject = target;
			animationEffect.EnqueueToScheduler ();
		} else {
			target.SetActive(targetItemActive);
		}
	}

	private void UpdateButtonsActive() {
		if (buttonLeft != null) {
			if (currentItem == 0) {
				buttonLeft.EnsureDeactivated ();
			} else {
				buttonLeft.EnsureActivated ();
			}
		}

		if (buttonRight != null) {
			if (currentItem == (menuItems.Length - 1)) {
				buttonRight.EnsureDeactivated ();
			} else {
				buttonRight.EnsureActivated ();
			}
		}
	}

	private void initActivation() {
		for (int i = 0; i < menuItems.Length; i++) {
			if (i == currentItem) {
				menuItems[i].SetActive(true);
			} else {
				menuItems[i].SetActive(false);
			}
		}
	}


}