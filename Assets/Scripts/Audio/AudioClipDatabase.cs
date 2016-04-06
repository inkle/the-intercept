using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioClipDatabase : MonoSingleton<AudioClipDatabase> {
	public List<AudioClip> keySounds;
	public List<AudioClip> carriageReturnSounds;
	public List<AudioClip> windingClicks;
	public AudioClip attachingPaperSound;
	public AudioClip horrorSting;

	public void PlayKeySound () {
		PlaySound(keySounds[Random.Range(0, keySounds.Count)]);
	}

	public void PlayCarriageReturnSound () {
		PlaySound(carriageReturnSounds[Random.Range(0, carriageReturnSounds.Count)]);
	}

	public void PlayWindingSound (float volume) {
		PlaySound(windingClicks[Random.Range(0, windingClicks.Count)], volume);
	}

	public void PlayAttachingPaperSound () {
		PlaySound(attachingPaperSound);
	}

	public void PlayHorrorSting () {
		PlaySound(horrorSting);
	}

	private void PlaySound (AudioClip audioClip, float volume = 1) {
		AudioSource.PlayClipAtPoint(audioClip, Vector3.zero, volume);
	}
}
