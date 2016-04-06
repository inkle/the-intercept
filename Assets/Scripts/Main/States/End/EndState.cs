using UnityEngine;
using System.Collections;

public class EndState : MainState {
	public EndView endView;

	private void Awake () {
		endView.Init();
	}

	public override void Enter () {
		base.Enter ();
		AudioClipDatabase.Instance.PlayHorrorSting();
		endView.Show();
		Main.Instance.backgroundAmbienceController.QuietMode();
	}

	public override void Exit () {
		base.Exit ();
		endView.Hide();
		Main.Instance.backgroundAmbienceController.NormalMode();
	}
}
