using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmBar : OverridableMonoBehaviour {

	public AudioProcessor processor;
	[SerializeField]
	private RectTransform _container;
	private List<BeatGraphic> _beatGraphics = new List<BeatGraphic>();
	private float _pixelsPerFrame = -2f;
	void Start () {
		//TODO: Sign up to beat detection events
		processor.onBeat.AddListener (onOnbeatDetected);
		processor.onSpectrum.AddListener (onSpectrum);
	}

	private void onSpectrum(float[] arg0)
	{
	}

	private void onOnbeatDetected()
	{
		GameObject temp = BeatGraphicsPool.GetOne();
		BeatGraphic graph = temp.GetComponent<BeatGraphic>();
		if (graph != null) {
			_beatGraphics.Add(graph);
			temp.transform.SetParent(_container);
			temp.transform.SetPositionAndRotation(_container.position, 
				_container.rotation);
		}
	}

	public override void FixedUpdateMe () {
		float move = _pixelsPerFrame * Time.deltaTime;
		for (int i = 0; i < _beatGraphics.Count; i++) {
			_beatGraphics[i].Move(move);
			if (_beatGraphics[i].transform.localPosition.x < 0) {
					BeatGraphicsPool.ReturnOne(_beatGraphics[i].gameObject);
					_beatGraphics.Remove(_beatGraphics[i]);
				}
		}
	}
}
