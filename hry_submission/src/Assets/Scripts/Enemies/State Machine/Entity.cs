using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;

public class Entity : MonoBehaviour
{
	/*
	 * Parent class of every AI
	 * Stores basic information about hp, stagger state, facing direction
	 */
	public FiniteStateMachine stateMachine;

	[SerializeField]
	public D_Entity baseData;

	protected float maxHealth;
	protected float currentHealth;

	public int knockbackDirection { get; protected set; }
	public int facing { get; protected set; } = -1;

	private bool staggerable = true;
	protected bool startStagger = false;

	protected bool deathCalled = false;

	public AttackProperties touchDamage { get; protected set; }
	public Rigidbody2D rb { get; private set; }
	public Animator animator { get; private set; }
	// Idea behind aliveGO was so enemies could have particle system instantiated inside parent GO..
	// Well, now it's just an extremely hindering idea :)
	public GameObject aliveGO { get; private set; }


	public virtual void Start() {
		aliveGO = transform.Find("Alive").gameObject;
		rb = aliveGO.GetComponent<Rigidbody2D>();
		animator = aliveGO.GetComponent<Animator>();
		maxHealth = baseData.maxHealth;
		currentHealth = maxHealth;

		touchDamage = new AttackProperties(baseData.touchDamage);

		stateMachine = new FiniteStateMachine();
	}

	public virtual void Update() {
		stateMachine.currentState.LogicUpdate();
		if(currentHealth <= 0 && !deathCalled) {
			deathCalled = true;
			Death();
		}
	}

	public virtual void FixedUpdate() {
		stateMachine.currentState.PhysicsUpdate();
	}

	public virtual void SetVelocity(Vector2 direction, float velocity) {
		rb.velocity = direction.normalized * velocity + rb.velocity.y * Vector2.up;
	}


	/*
	 * Receiving damage from other sources. Sets knockback direction based on position of other gameobject
	 * Every enemy can have their own stagger cd. Staggers are usually pretty long (bad design)
	 */
	protected virtual void Damage(AttackProperties attack) {
		currentHealth -= attack.damage + attack.elementDamageBonus;
		Debug.Log("Received damage: " + attack.damage);
		if(attack.direction > aliveGO.transform.position.x) {
			knockbackDirection = -1;
		} else {
			knockbackDirection = 1;
		}
		if (staggerable) {
			StartCoroutine("StartStaggerCD");
			startStagger = true;
		}
	}
	
	// Can be overriden by inherited classes in order to do some other action upon death
	protected virtual void Death() {
		Destroy(gameObject);
	}

	/*
	 * Function called by BasicHitEvent script. It's responsible for dealing damage to the player
	 * Binary "or" and "shift" wizardry was stole..*cough* borrowed from random forum (didn't save the link)
	 * Function can be overriden to enable special rules for damage
	 */
	public virtual void HitBoxTrigger(Collider2D other) {
		if (baseData.whatIsPlayer == (baseData.whatIsPlayer | (1 << other.gameObject.layer))) {
			touchDamage.direction = aliveGO.transform.position.x;
			other.gameObject.SendMessage("Damage", touchDamage);
		}
	}

	/*
	 * Function that is called by EnemyAnimHandler script.
	 * Used check for end of animation or any keyframes
	 */
	public virtual void AnimEvent(string name) {

	}

	IEnumerator StartStaggerCD() {
		staggerable = false;

		yield return new WaitForSeconds(baseData.staggerCD);
		staggerable = true;
	}

	public virtual void Flip() {
		facing *= -1;
		aliveGO.transform.Rotate(0f, 180f, 0f);
	}
}
