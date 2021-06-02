using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceController : MonoBehaviour
{
	public float moveSpeed = 5f;
	public float aliveTime = 2f;

	private int travelDir;

	public void Start() {
		Destroy(gameObject, aliveTime);
		travelDir = transform.rotation.y == 180 ? -1 : 1 ;
	}

	void FixedUpdate() {
		transform.Translate(Vector3.right * travelDir * moveSpeed *Time.fixedDeltaTime);
    }
}
