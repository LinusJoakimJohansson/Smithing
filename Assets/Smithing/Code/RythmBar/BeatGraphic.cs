using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatGraphic : MonoBehaviour {

	public void Move(float pixels) {
		Vector3 newPos = new Vector3(transform.localPosition.x + pixels, 0,0);
		transform.localPosition = newPos;
	}
}
