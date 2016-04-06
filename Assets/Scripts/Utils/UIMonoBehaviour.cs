using UnityEngine;
using System.Collections;

public class UIMonoBehaviour : MonoBehaviour {

	public RectTransform rectTransform {
		get {
			return (RectTransform) transform;
		}
	}
}
