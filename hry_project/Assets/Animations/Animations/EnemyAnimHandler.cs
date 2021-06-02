using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimHandler : MonoBehaviour
{
    private Entity entity;
    void Start()
    {
        entity = transform.parent.GetComponent<Entity>();
    }
    public void AnimationEnd(string message) {
        entity.AnimEvent(message);
	}
}
