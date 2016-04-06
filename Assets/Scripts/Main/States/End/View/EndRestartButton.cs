using UnityEngine;
using System.Collections;

public class EndRestartButton : MonoBehaviour {

	public void OnClickRestartButton () {
		Main.Instance.endState.Complete();
	}
}
