
using UnityEngine;
using System.Collections;

public class SlideshowController : MonoBehaviour {
	
	public GraphicalEffect[] effects;
	public int currentEffect = 0;

	public bool effectLock = false;
	public float effectLockDuration = 1;

	
	void Update () {
		if (!effectLock) {
			if (Input.GetMouseButton (0) || Input.GetKeyDown("left")) {
				NextEffect ();
			} else if (Input.GetMouseButton (1) || Input.GetKeyDown("right")) {
				PreviousEffect ();
			}
		}
	}

	private void NextEffect() {
		if (currentEffect < effects.Length-1) {
			currentEffect++;
			effects [currentEffect].EnqueueToScheduler ();
			StartCoroutine("EffectLock");
		}
	}

	private void PreviousEffect() {
		if (currentEffect > 0 && currentEffect <= effects.Length) {
			currentEffect--;
			effects[currentEffect].EnqueueToScheduler();
			StartCoroutine("EffectLock");
		}
	}

	private IEnumerator EffectLock() {
		effectLock = true;

		yield return new WaitForSeconds(effectLockDuration);

		effectLock = false;
	}
}
