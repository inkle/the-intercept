using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class TypedText {
	public bool typing = false;

	public string text;
	public string targetText;

	private float nextStringTimer;

	public int currentSplitStringIndex;
	private string[] splitString;

	public string currentString {
		get {
			if(currentSplitStringIndex < splitString.Count()) return splitString[currentSplitStringIndex];
			else return "";
		}
	}

	[Tooltip("Modifies type speed")]
	public float speedMultiplier = 1;
	private Dictionary<string, BaseTimeDelay> customStringTypeTimeDictionary = new Dictionary<string, BaseTimeDelay>();
	private Dictionary<string, BaseTimeDelay> customPostTypePauseDictionary = new Dictionary<string, BaseTimeDelay>();

	private TypedTextSettings settings;

	public delegate void OnTextChangeEvent(string fullText);
	public event OnTextChangeEvent OnTextChange;

	public delegate void OnTypeTextEvent(string newText);
	public event OnTypeTextEvent OnTypeText;

	public delegate void OnCompleteTypingEvent();
	public event OnCompleteTypingEvent OnCompleteTyping;


	public void TypeText (string myText, TypedTextSettings settings) {
		if(string.IsNullOrEmpty(myText)) 
			return;
		// This should be a struct or deep cloned to prevent editing externally
		this.settings = settings;
		CreateDelayDictionary(settings.customTypeDelays, out customStringTypeTimeDictionary);
		CreateDelayDictionary(settings.customPostTypePause, out customPostTypePauseDictionary);
		targetText += myText;
		typing = true;
		currentSplitStringIndex = 0;
		splitString = GetSplitText(myText);
		TypeCurrentString();
	}

	public void Loop () {
		if(!typing) 
			return;
		nextStringTimer -= Time.deltaTime * speedMultiplier;
		while(typing && nextStringTimer < 0) {
			TypeCurrentString();
		}
	}

	public void ShowInstantly () {
		Type(GetRemainingString());
		CompleteTyping();
	}

	private string GetRemainingString () {
		int remainingCount = splitString.Length - currentSplitStringIndex;
		if(remainingCount <= 0) 
			return "";
		string[] remainingSplitString = new string[remainingCount];
		Array.Copy(splitString, currentSplitStringIndex, remainingSplitString, 0, splitString.Length - currentSplitStringIndex);
		return string.Concat(remainingSplitString);
	}

	public void Clear () {
		targetText = text = "";
		TextChanged(text);
		FinishTyping();
	}

	//STATES
	private void TypeCurrentString () {
		Type(currentString);
		FinishTypingString();
	}

	private void Type (string stringToAdd) {
		text += stringToAdd;
		if(OnTypeText != null) OnTypeText(stringToAdd);
		TextChanged(text);
	}

	private void FinishTypingString () {
		if(currentSplitStringIndex >= splitString.Length-1){
			CompleteTyping();
		} else {
			StartTypingNextString();
		}
	}

	private void StartTypingNextString () {
		currentSplitStringIndex++;
		nextStringTimer += GetLastStringPauseTime() + GetNextStringTypeTime();
	}

	private float GetLastStringPauseTime () {
		if(currentSplitStringIndex == 0)
			return 0;
		float delayTime;
		string lastString = splitString[currentSplitStringIndex-1];
		if (customPostTypePauseDictionary.ContainsKey(lastString)) {
			delayTime = customPostTypePauseDictionary[lastString].GetDelay();
		} else {
			delayTime = 0;
		}
		return delayTime;
	}

	private float GetNextStringTypeTime () {
		float typeTime;
		if (customStringTypeTimeDictionary.ContainsKey(currentString)) {
			typeTime = customStringTypeTimeDictionary[currentString].GetDelay();
		} else {
			typeTime = settings.defaultTypeDelay.GetDelay();
		}
		return typeTime;
	}

	private void CompleteTyping () {
		FinishTyping();
		if(OnCompleteTyping != null) OnCompleteTyping();
	}

	private void FinishTyping () {
		splitString = new string[0];
		nextStringTimer = 0;
		typing = false;
	}

	//EVENTS
	private void TextChanged (string text) {
		if(OnTextChange != null) OnTextChange(text);
	}

	//SPLITTING
	private string[] GetSplitText (string myText) {
		if(settings.splitMode == TypedTextSettings.SplitMode.Word) {
			return SplitByWord(myText);
		} else if(settings.splitMode == TypedTextSettings.SplitMode.Character) {
			return SplitByCharacter(myText);
		} else {
			return SplitByCustom(myText);
		}
	}

	private string[] SplitByWord (string _input) {
		string pattern = @"^(\s+|\d+|\w+|[^\d\s\w]+)+$";
		Regex regex = new Regex(pattern);
		List<string> tmpList = new List<string>();
		if (regex.IsMatch(_input)) {
			Match match = regex.Match(_input);
			foreach (Capture capture in match.Groups[1].Captures) {
				tmpList.Add(capture.Value);
			}
		}
		return tmpList.ToArray<string>();
	}

	private string[] SplitByCharacter (string str) {
		if (str == null) return null;
        char[] charArray = str.ToCharArray();
        string[] stringArray = new string[charArray.Length];
        for(int i = 0; i < charArray.Length; i++) {
        	stringArray[i] = charArray[i].ToString();
        }
        return stringArray;
	}

	private string[] SplitByCustom (string _input) {
		return _input.Split(settings.customSplitString.ToArray(), StringSplitOptions.None);
	}

	private void CreateDelayDictionary (List<CustomStringTimeDelay> customTypeDelays, out Dictionary<string, BaseTimeDelay> delayDictionary) {
		delayDictionary = new Dictionary<string, BaseTimeDelay>(StringComparer.OrdinalIgnoreCase);
		if(customTypeDelays == null || customTypeDelays.Count == 0) return;
		for (int i = 0; i < customTypeDelays.Count; i++) {
			delayDictionary.Add(customTypeDelays[i].customString, customTypeDelays[i].textDelay);
		}
	}

	[System.Serializable]
	public class TypedTextSettings {
		public enum SplitMode {
			Word,
			Character,
			Custom
		}

		[Tooltip("The text typing mode")]
		public SplitMode splitMode;
		[Tooltip("The strings used to divide TypedText in Custom TypedTextSplitMode")]
		public List<string> customSplitString;

		[Tooltip("Type delay for standard strings")]
		public BaseTimeDelay defaultTypeDelay;

		[Tooltip("Type delay for custom strings")]
		public List<CustomStringTimeDelay> customTypeDelays;
		[Tooltip("Type delay for custom strings, after the string is printed. Useful for full stops.")]
		public List<CustomStringTimeDelay> customPostTypePause;

		public TypedTextSettings () {
			this.splitMode = SplitMode.Word;
			this.customSplitString = new List<string>() {",", "\n"};
			this.defaultTypeDelay = new TimeDelay(0.1f);
			this.customTypeDelays = new List<CustomStringTimeDelay>();
			this.customPostTypePause = new List<CustomStringTimeDelay>();
		}

		public TypedTextSettings (SplitMode splitMode) {
			this.splitMode = splitMode;
			this.customSplitString = new List<string>() {",", "\n"};
			this.defaultTypeDelay = new TimeDelay(0.1f);
			this.customTypeDelays = new List<CustomStringTimeDelay>();
			this.customPostTypePause = new List<CustomStringTimeDelay>();
		}

//		public TypedTextSettings (TypedTextSettings settings) {
//			this.splitMode = settings.splitMode;
//			this.customSplitString = new List<string>(settings.customSplitString);
//			this.defaultTypeDelay = new TimeDelay(0.1f);
//			this.customTypeDelays = new List<CustomStringTimeDelay>(settings.customSplitString);
//			this.customPostTypePause = new List<CustomStringTimeDelay>(settings.customSplitString);
//		}
	}


	[System.Serializable]
	public class CustomStringTimeDelay {
		public string customString;
		public BaseTimeDelay textDelay;

		public CustomStringTimeDelay (string customString, BaseTimeDelay textDelay) {
			this.customString = customString;
			this.textDelay = textDelay;
		}
	}

	[System.Serializable]
	public abstract class BaseTimeDelay {
		public abstract float GetDelay ();
	}

	[System.Serializable]
	public class TimeDelay : BaseTimeDelay {
		public float delay;
		
		public TimeDelay (float delay) {
			this.delay = delay;
		}
		
		public override float GetDelay () {
			return delay;
		}
	}
	
	[System.Serializable]
	public class RandomTimeDelay : BaseTimeDelay {
		public float minTypeTime;
		public float maxTypeTime;
		
		public RandomTimeDelay (float minTypeTime, float maxTypeTime)  {
			this.minTypeTime = minTypeTime;
			this.maxTypeTime = maxTypeTime;
		}
		
		public override float GetDelay () {
			return UnityEngine.Random.Range(minTypeTime, maxTypeTime);
		}
	}
}
