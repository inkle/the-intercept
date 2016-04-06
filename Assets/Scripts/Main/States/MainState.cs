using UnityEngine;
using System.Collections;

// Enter > Complete > Exit. Enter and Exit are called externally (by Main)
public class MainState : MonoBehaviour {

	public delegate void OnCompleteEvent ();
	public event OnCompleteEvent OnComplete;

	public virtual void Enter () {

	}

	public virtual void Complete () {
		if(OnComplete != null)
			OnComplete();
	}

	public virtual void Exit () {

	}
}