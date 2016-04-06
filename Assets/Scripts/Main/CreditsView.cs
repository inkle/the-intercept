using UnityEngine;
using System.Collections;

public class CreditsView : MonoBehaviour {

	public void Show () {
		gameObject.SetActive(true);
	}

	public void Hide () {
		gameObject.SetActive(false);
	}

	public void OnClickBackButton () {
		Hide ();
	}
}
