using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public string musicEventName;
	public bool playMusicFromStart = true;


	void Start () {
		if (playMusicFromStart) {
			startMusic();
		}
	}

	public void startMusic() {
		AudioManager.PlaySound (musicEventName);
	}

	public void stopMusic() {
		AudioManager.PostEvent (musicEventName, Fabric.EventAction.StopSound);
	}

	public void pauseMusic() {
		AudioManager.PostEvent (musicEventName, Fabric.EventAction.PauseSound);
	}

	public void PlayNext(GameObject nextMusic) {
		string nextMusicName = nextMusic.name;
		AudioManager.PostEventWithParam (musicEventName, Fabric.EventAction.SetSwitch, nextMusicName);
	}

	public void SetMusicVolume(float volume) {
		AudioManager.PostEventWithParam (musicEventName, Fabric.EventAction.SetVolume, volume);
	}
}
