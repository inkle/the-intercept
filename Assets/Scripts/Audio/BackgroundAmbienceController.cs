using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BackgroundAmbienceController : MonoBehaviour {
	
	private AudioSource audioSource {
		get {
			return GetComponent<AudioSource>();
		}
	}

	public FloatTween volumeTween = new FloatTween();

	public float quietVolume = 0.1f;
	public float normalVolume = 0.2f;

	public void QuietMode () {
		StopAllCoroutines();
		volumeTween.Tween(audioSource.volume, quietVolume, 1, AnimationCurve.EaseInOut(0,0,1,1));
	}

	public void NormalMode () {
		StopAllCoroutines();
		volumeTween.Tween(audioSource.volume, normalVolume, 1, AnimationCurve.EaseInOut(0,0,1,1));
	}

	private void Awake () {
		audioSource.volume = 0;
		volumeTween.OnChange += OnChangeVolumeTween;
	}

	private void Start () {
		StartCoroutine(DelayedStart());
	}

	private IEnumerator DelayedStart () {
		audioSource.volume = 0;
		yield return new WaitForSeconds(4);
		volumeTween.Tween(audioSource.volume, normalVolume, 12, AnimationCurve.EaseInOut(0,0,1,1));
	}

	private void Update () {
		volumeTween.Loop(Time.unscaledDeltaTime);
	}

	private void OnChangeVolumeTween (float currentValue) {
		audioSource.volume = currentValue;
	}
}
