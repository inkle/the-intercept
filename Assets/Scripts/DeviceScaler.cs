using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeviceScaler : MonoBehaviour {

	public DeviceScaleSettings handheldScaleSettings;
	public DeviceScaleSettings desktopScaleSettings;

	public CanvasScaler[] canvasScalers;

	void Update () {
		DeviceScaleSettings currentSettings = SystemInfo.deviceType == DeviceType.Handheld ? handheldScaleSettings : desktopScaleSettings;
		foreach(CanvasScaler canvasScaler in canvasScalers) {
			canvasScaler.uiScaleMode = currentSettings.uiScaleMode;
			canvasScaler.scaleFactor = currentSettings.scaleFactor;
			canvasScaler.referenceResolution = currentSettings.referenceResolution;
			canvasScaler.matchWidthOrHeight = currentSettings.matchWidthOrHeight;
		}
	}

	[System.Serializable]
	public class DeviceScaleSettings {
		public CanvasScaler.ScaleMode uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		public Vector2 referenceResolution = new Vector2(800, 600);
		public float matchWidthOrHeight = 0.5f;
		public float scaleFactor = 1f;
	}
}
