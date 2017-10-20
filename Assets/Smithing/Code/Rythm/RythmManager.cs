using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmManager : OverridableMonoBehaviour {

	// Use this for initialization

	public AudioSource Source;
	public AudioClip Clip;

	RythmAnalyzer _analyzer;
	void Start () {
		Source.clip = Clip;
		_analyzer = new RythmAnalyzer();
		_analyzer.Init(Source);
	}

	public void StartPlaying() {
		Source.Play();
	}

	public void StopPlaying() {
		Source.Stop();
	}
	
	// Update is called once per frame
	public override void FixedUpdateMe () {
		if (Source.isPlaying) {
			_analyzer.SyncTime();
			_analyzer.CollectData();
		}
	}
}
