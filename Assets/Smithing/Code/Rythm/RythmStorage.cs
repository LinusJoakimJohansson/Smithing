using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RythmStorage {

	const string FOLDER = @"\Output\Rythm\";
	const string FILE = "Analyzed.txt";
	AudioProcessor _source;

	float _startTime;
	List<float> _beats = new List<float>();
	public void Init (AudioProcessor source) {
		_source = source;
		_source.onBeat.AddListener(CollectData);
		CreateFolder();
		ClearFile();
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

	public void SetStartTime() {
		_startTime = Time.time;
	}

	public void CollectData() {
		_beats.Add(Time.time - (_startTime));
	}

	public void WriteDataToFile() {
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		//if (System.IO.File.Exists(Application.dataPath + FOLDER + "Analyzed.txt")) {
		//	System.IO.File.AppendAllText(Application.dataPath + FOLDER +"Analyzed.txt", output);
		//}
		for (int i = 0; i < _beats.Count; i++){
			sb.Append( _beats[i] + ":");
		}
		//System.IO.File.WriteAllText(Application.dataPath + FOLDER + "Analyzed.txt", output);
		System.IO.File.AppendAllText(Application.dataPath + FOLDER + FILE, sb.ToString());
	}

	public static List<float> LoadBeatsForSong(){
		if (!System.IO.File.Exists(Application.dataPath + FOLDER + FILE)) {
			return null;
		}
		string file = System.IO.File.ReadAllText(Application.dataPath + FOLDER + FILE);	
		string[] beats = file.Split(':');
		List<float> ret = new List<float>();
		for (int i = 0; i < beats.Length; i++){
			ret.Add(float.Parse(beats[i]));
		}
		return ret;
	}
}
