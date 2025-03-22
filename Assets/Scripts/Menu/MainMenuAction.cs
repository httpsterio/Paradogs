using UnityEngine;
using System.Collections;

public abstract class MainMenuAction : MonoBehaviour {

	public float actionDelay = 0f;

	public void Activate() {
		StartCoroutine("menuCoroutine");
	}

	private IEnumerator menuCoroutine() {

		yield return new WaitForSeconds(actionDelay);
		Perform ();
	}

	public abstract void Perform();
}
