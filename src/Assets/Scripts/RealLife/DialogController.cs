using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{

    public Text hudTextBox;

	public GraphicalEffect fadeInEffect;
	public GraphicalEffect fadeOutEffect;
	public GraphicalEffect replaceEffect;
	public float replaceEffectTime = 0;
    
    public void updateText(string text, float duration)
    {
		float delay = 0f;

		if (TextCurrentlyDisplayed()) {
			replaceEffect.gameObject.GetComponent<HUDTextTextChangeEffect> ().newText = text;
			replaceEffect.EnqueueToScheduler ();
			delay += replaceEffectTime;

			stopFadeOutEffect();
		} else {
			hudTextBox.text = text.Replace ("\\n", "\n");
			fadeInEffect.EnqueueToScheduler();
		}
		
		fadeOutEffect.occurTimeDelay = duration + delay;
		fadeOutEffect.EnqueueToScheduler();
    }

	private bool TextCurrentlyDisplayed() {
		return hudTextBox.text != "";
	}

	private float getEffectDuration(GraphicalEffect effect) {
		if (effect is ContinuousGraphicalEffect) {
			return ((ContinuousGraphicalEffect)effect).duration;
		} else {
			return 0;
		}
	}

	private void stopFadeOutEffect() {
		// try to unschedule. If fails, deactivating and activating object should stop coroutine
		if (!fadeOutEffect.Unschedule ()) {
			fadeOutEffect.gameObject.SetActive(false);
			fadeOutEffect.gameObject.SetActive(true);
		}
	}
}