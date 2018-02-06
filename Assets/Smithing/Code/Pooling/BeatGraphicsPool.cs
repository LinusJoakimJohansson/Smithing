using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatGraphicsPool : MonoBehaviour {

	private static BeatGraphicsPool _instance;

	public static BeatGraphicsPool Instance {
		get {
			return _instance;
		}
	}
	[SerializeField]
	private GameObject _prefab;

	[SerializeField]
	private int _pooledCount = 15;
	private GameObject[] _pooledImages;

	public void Awake() {
		_instance = this;
		Init();
	}

	public void Init() {
		if (_prefab != null) {
			_pooledImages = new GameObject[_pooledCount];
			for (int i = 0; i < _pooledCount; i++){
				_pooledImages[i] = Instantiate(_prefab, _instance.transform);
				_pooledImages[i].SetActive(false);
			}
		}
	}

	public static GameObject GetOne() {
		for (int i = 0; i < _instance._pooledCount; i++) {
			if (!_instance._pooledImages[i].activeInHierarchy) {
				_instance._pooledImages[i].SetActive(true);
				return _instance._pooledImages[i];
			}
		} 
		_instance._pooledImages[0].SetActive(true);
		return _instance._pooledImages[0];
	}

	public static void ReturnOne(GameObject obj) {
		for (int i = 0; i < _instance._pooledCount; i++) {
			if (_instance._pooledImages[i] == obj) {
				_instance._pooledImages[i].SetActive(false);
				_instance._pooledImages[i].transform.SetParent(
					_instance.transform);
				_instance._pooledImages[i].transform.localPosition = 
					Vector3.zero;
			}
		} 
	}
}
