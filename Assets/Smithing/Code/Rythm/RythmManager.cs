using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RythmManager : OverridableMonoBehaviour {

	// Use this for initialization

	[SerializeField]
	private  AudioSource _source;

	[SerializeField]
	private  AudioClip _clip;
	[SerializeField]
	private  AudioProcessor _processor;
	RythmStorage _storage;
	private bool _started = false;
	void Start () {
		_source.clip = _clip;
		_storage = new RythmStorage();
		_storage.Init(_processor);
	}

	public void StartPlaying() {
		_source.Play();
		_started = true;
	}

	public void StopPlaying() {
		_source.Stop();
		_started = false;
	}
	
	// Update is called once per frame
	public override void FixedUpdateMe () {
		if (!_source.isPlaying && _started) {
			_storage.WriteDataToFile();
			_started = false;
		}
	}
}
