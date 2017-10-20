using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RythmAnalyzer {

	const string FOLDER = @"\Output\Rythm\";
	const string FILE = "Analyzed.txt";
	AudioClip _clip;
	AudioSource _source;

	int _sampleCount;
	int _timeSamples;
	float _seconds;

	public void Init (AudioSource source) {
		_source = source;
		CreateFolder();
		ClearFile();
		_sampleCount = _source.clip.samples;
		_seconds = _source.clip.length;
	}

	void CreateFolder() {
		if (!System.IO.Directory.Exists(Application.dataPath + FOLDER)) {
			if (!System.IO.Directory.Exists(Application.dataPath + "Output")) {
				System.IO.Directory.CreateDirectory(Application.dataPath + "OutPut");
			}
			System.IO.Directory.CreateDirectory(Application.dataPath + FOLDER);
		}
	}
	
	void ClearFile() {
		if (System.IO.File.Exists(Application.dataPath + FOLDER + FILE)) {
			System.IO.File.WriteAllText(Application.dataPath + FOLDER + FILE, "");
		}
	}

	public void SyncTime() {
		_timeSamples = _source.timeSamples;
	}

	public void CollectData() {
		float[] samples = new float[16]; 
		_source.GetOutputData(samples, 0);
		WriteDataToFile(samples);
	}

	public void WriteDataToFile(float[] data) {
		string output = "";
		//if (System.IO.File.Exists(Application.dataPath + FOLDER + "Analyzed.txt")) {
		//	System.IO.File.AppendAllText(Application.dataPath + FOLDER +"Analyzed.txt", output);
		//}
		output += System.Environment.NewLine + Time().ToString() +  " [";
		for (int i = 0; i < data.Length; i++) { 
			output += data[i] + ",";
		}
		output += "]";
		//System.IO.File.WriteAllText(Application.dataPath + FOLDER + "Analyzed.txt", output);
		System.IO.File.AppendAllText(Application.dataPath + FOLDER + FILE, output);
	}

	float Time () {
		float percent = (float)_timeSamples/(float)_sampleCount;
		float time = percent*_seconds;
		Debug.Log (percent + "Time passed: " + time);
		return time;
	}
}
