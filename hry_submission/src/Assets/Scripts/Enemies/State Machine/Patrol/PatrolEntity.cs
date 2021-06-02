using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;

public class PatrolEntity : Entity {
	/*
	 * Patrol AI skelet, it's supposed to go from wall/ledge to wall/ledge
	 * Further use in slime and slime only
	 */
	[SerializeField]
	private Transform wallCheck, ledgeCheck, leftBound, rightBound, playerCheck;
	public bool isChargeReady { get; private set; } = true;

	public D_PatrolEntity entityData;

	public override void FixedUpdate() {
		base.FixedUpdate();
	}

	public override void SetVelocity(Vector2 direction, float velocity) {
		base.SetVelocity(direction, velocity);
	}

	public override void Start() {
		base.Start();
	}

	public override void Update() {
		base.Update();
	}

	public virtual bool CheckWall() {
		return Physics2D.Raycast(wallCheck.position, -aliveGO.transform.right, entityData.wallCheckDistance, baseData.whatIsGround);
	}

	public virtual bool CheckLedge() {
		return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, baseData.whatIsGround);
	}

	/*
	 * If left and right boundaries are given, checks whether AI is inside of them.
	 * (Slime can charge out of bounds, so this is used to help him get back to avoid breaking the AI)
	 */
	public virtual bool IsOutOfBounds() {
		if(leftBound == null || rightBound == null) {
			return false;
		}
		if (wallCheck.position.x < leftBound.position.x || wallCheck.position.x > rightBound.position.x) {
			return true;
		}
		return false;
	}
	// Basically pointless, one !IsOutOfBounds() should handle it just fine.
	public virtual bool IsInsideBounds() {
		if (leftBound == null || rightBound == null) {
			return true;
		}
		if(playerCheck.position.x > leftBound.position.x && wallCheck.position.x < rightBound.position.x) {
			return true;
		}
		return false;
	}

	/*
	 * Check if player is in detection range (shorter than aggro)
	 */
	public virtual bool CheckPlayerMinRange() {
		return Physics2D.Raycast(playerCheck.position, -aliveGO.transform.right, entityData.minAgroDistance, baseData.whatIsPlayer);
	}

	/*
	 * Checks if player is outside of aggro range
	 */
	public virtual bool CheckPlayerMaxRange() {
		return Physics2D.Raycast(playerCheck.position, -aliveGO.transform.right, entityData.maxAgroDistance, baseData.whatIsPlayer);
	}

	public int CheckBoundsDir() {
		if (IsInsideBounds()) {
			return 0;
		} else if (playerCheck.position.x < leftBound.position.x) {
			return 1;
		} else if(playerCheck.position.x > rightBound.position.x) {
			return -1;
		}
		return 0;
	}

	public virtual void OnDrawGizmos() {
		Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facing * entityData.wallCheckDistance));
		Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
		Gizmos.color = Color.green;
		Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facing * entityData.maxAgroDistance));
		Gizmos.color = Color.red;
		Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facing * entityData.minAgroDistance));
		Gizmos.color = Color.white;
	}

	IEnumerator StartChargeCD() {
		isChargeReady = false;

		yield return new WaitForSeconds(entityData.chargeCD);
		isChargeReady = true;
	}
}
