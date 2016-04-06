using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GraphicPulser : MonoBehaviour {
	public Graphic graphic;
	public float timer = 0;
	public float speed = 1;
	public AnimationCurve pulseCurve = new AnimationCurve(new Keyframe(0.0f,0.5f), new Keyframe(0.5f,1.0f), new Keyframe(1f,0.5f));

	void Update () {
		timer += speed * Time.deltaTime;
		graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, pulseCurve.Evaluate(timer % 1));
	}
}