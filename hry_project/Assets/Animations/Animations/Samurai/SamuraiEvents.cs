using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiEvents : MonoBehaviour {
    private Samurai samurai;

    void Start() {
        samurai = transform.parent.GetComponent<Samurai>();
    }

    public void AnimEvent(string message) {
        samurai.AnimationEvent(message);
	}
}
