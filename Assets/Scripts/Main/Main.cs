using UnityEngine;
using System.Collections;

public class Main : MonoSingleton<Main> {
	[Header("Skips the intro (editor only)")]
	public bool skipIntro = true;
	public bool fastIntro = false;

	[HideInInspector]
	public MainState currentState;
	public IntroState introState;
	public GameState gameState;
	public EndState endState;

	private bool _paused = false;
	public bool paused {
		get {
			return _paused;
		}
		set {
			if(_paused == value)
				return;
			_paused = value;
			Time.timeScale = _paused ? 0 : 1;
			if(paused) {
				gameState.settingsView.Show();
				backgroundAmbienceController.QuietMode();
			} else {
				gameState.settingsView.Hide();
				backgroundAmbienceController.NormalMode();
			}
		}
	}

	public BackgroundAmbienceController backgroundAmbienceController;
	public CreditsView creditsView;

	void Awake () {
		introState.OnComplete += OnCompleteIntro;
		gameState.OnComplete += OnCompleteGame;
		endState.OnComplete += OnCompleteEndState;

		creditsView.Hide();
	}

	void Start () {
		if(skipIntro && Application.isEditor) {
			OnCompleteIntro();
		} else {
			ChangeState(introState);
			if(fastIntro && Application.isEditor)
				introState.PlayShortIntro();
			else
				introState.PlayLongIntro();
		}
	}

	void OnCompleteIntro () {
		ChangeState(gameState);
	}

	void OnCompleteGame () {
		ChangeState(endState);
	}

	void OnCompleteEndState () {
		Restart();
	}

	void ChangeState (MainState state) {
		if(currentState != null) {
			currentState.Exit();
		}
		currentState = state;
		currentState.Enter();
	}

	public void Restart () {
		introState.StopAllCoroutines();
		gameState.Clear();
		paused = false;
		ChangeState(introState);
		introState.PlayLongIntro();
	}

	public void Quit () {
		Application.Quit();
	}
}