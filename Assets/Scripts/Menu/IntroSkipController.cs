using UnityEngine;
using System.Collections;

public class IntroSkipController : MonoBehaviour {

	public GraphicalEffect skipEffect;
	public GraphicalEffect introEffect;

	void Start() {
		if (ApplicationPersistentData.firstMenuRun) {
			ApplicationPersistentData.firstMenuRun = false;
			introEffect.EnqueueToScheduler ();
		} else {
			skipEffect.EnqueueToScheduler();
		}
	}

	void Update () {
		if (!Input.GetMouseButton (1) && Input.anyKey) {
			skipEffect.EnqueueToScheduler();
		}
	}
}
