using UnityEngine;
using System.Collections;

/// <summary>
/// Toggles properties of a CanvasGroup based on its alpha. 
/// Useful for allowing raycasts to automatically pass through an invisible CanvasGroup.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupOpacityInteractionEnabler : MonoBehaviour {
	
	private CanvasGroup canvasGroup;
	
	public float alphaThreshold = 0;
	public bool blocksRaycasts = true;
	public bool interactable = true;
	
	void OnEnable () {
		canvasGroup = GetComponent<CanvasGroup>();
	}
	
	void Update () {
		if(blocksRaycasts) {
			canvasGroup.blocksRaycasts = canvasGroup.alpha > alphaThreshold;
		}
		if(interactable) {
			canvasGroup.interactable = canvasGroup.alpha > alphaThreshold;
		}
	}
}
