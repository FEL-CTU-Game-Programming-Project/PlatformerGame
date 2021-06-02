using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAttack : MonoBehaviour
{
    public JeffStats.AnimationTags gotInput = JeffStats.AnimationTags.none;
    private JeffStats.AnimationTags activeAttack;
    [SerializeField] private float lastBuffer;

    public bool isAttacking;
    public int attackDirection;

    private Animator anim;
    private PlayerController pc;
    private JeffStats stats;
    private AttackProperties lightAttack;
    private AttackProperties heavyAttack;
    public HealthBar healthBar;

    private float randomShakePower;

    private int comboCounter = 0;
    private float maxDelayBetweenInputAttacks = 1;

    public LayerMask whatIsDamageable;


    private void Start()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();
        stats = GetComponent<JeffStats>();
        lightAttack = new AttackProperties(pc.data.lightAttackDamage, AttackProperties.DamageType.Neutral, 0);
        heavyAttack = new AttackProperties(pc.data.heavyAttackDamage, AttackProperties.DamageType.Neutral, 0);
    }

    private void Update()
    {
        ResetCombo();

        // this condition is here for not attacking after jump, it feels weird, like if in the middle of jump player click on attack, then character starts attacking when jump is finished, with this condition
        // player has to press attack again when he finishes jump
        if(pc.jeff.velocity.y == 0)
        {
            CheckCombatInput();
            CheckAttacks();
        }
        
    }

    // All possible input for combat
    private void CheckCombatInput()
    {
        if (isAttacking)
        {
            return;
        }

        if(Time.time > lastBuffer + pc.data.inputTimer)
        {
            gotInput = JeffStats.AnimationTags.none;
        }

        // COMBO
        if (Input.GetButtonDown("Fire1"))
        {
            
            if(comboCounter == 0)
            {
                gotInput = JeffStats.AnimationTags.lightAttack0;
                lastBuffer = Time.time;
                comboCounter++;
            }
            else if(comboCounter == 1)
            {
                gotInput = JeffStats.AnimationTags.lightAttack1;
                lastBuffer = Time.time;
                comboCounter++;
            }
            else if (comboCounter == 2)
            {
                gotInput = JeffStats.AnimationTags.lightAttack2;
                comboCounter++;
            }
        }

        // HEAVY ATTACK
        if (Input.GetButtonDown("Fire2"))
        {
            gotInput = JeffStats.AnimationTags.heavyAttack;
            lastBuffer = Time.time;
        }
    }

    // Setup attack animations
    private void CheckAttacks()
    {
        if (isAttacking)
        {
            return;
        }

        if(gotInput == JeffStats.AnimationTags.lightAttack0)
        {
            isAttacking = true;
            anim.SetBool("defaultAttack0", true);
            anim.SetBool("isAttacking", isAttacking);
        }
        else if (gotInput == JeffStats.AnimationTags.lightAttack1)
        {
            isAttacking = true;
            anim.SetBool("defaultAttack1", true);
            anim.SetBool("isAttacking", isAttacking);
        }
        else if (gotInput == JeffStats.AnimationTags.lightAttack2)
        {
            isAttacking = true;
            anim.SetBool("defaultAttack2", true);
            anim.SetBool("isAttacking", isAttacking);
        }

        if (gotInput == JeffStats.AnimationTags.heavyAttack)
        {  
            isAttacking = true;
            anim.SetBool("isAttacking", isAttacking);
            anim.SetBool("heavyAttack", true);
        }
    }

    // If it takes too long between attacks, player has to start with combo again
    private void ResetCombo()
    {
        if (Time.time - lastBuffer > maxDelayBetweenInputAttacks)
        {
            comboCounter = 0;
        }
    }

    // Is called at the end of attack animation
    public void FinishedAnimation(int animation)
    {
        // resets combo if it is finished
        if(comboCounter == 3)
        {
            comboCounter = 0;
        }

        // sets everthing to false so we can attack again
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("defaultAttack0", false);
        anim.SetBool("defaultAttack1", false);
        anim.SetBool("defaultAttack2", false);
        anim.SetBool("heavyAttack", false);

        gotInput = JeffStats.AnimationTags.none;
    }


    // this is received damage from enemy (if enemy hits me, this function is called)
    private void Damage(AttackProperties ap)
    {
        if (!pc.isInvincible && stats.currentHP > 0) // if I am not invincible and have some HP left
        {
            stats.DecreaseHP(ap.damage); // lowers my HP based on damage I received
            healthBar.SetHealth(stats.currentHP); // lowers HP on UI
            attackDirection = (ap.direction < transform.position.x) ? 1 : -1;
            pc.StartCoroutine("ApplyKnockback"); // makes small knockback (this function is in PlayerController)
        }
    }

    // SENDING DAMAGE TO ENEMY
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMask.NameToLayer("layerName") != LayerMask.NameToLayer("whatIsDamageable"))
        {
            return;
        }

        if(gotInput == JeffStats.AnimationTags.none)
        {
            return;
        }

        // Selects correct attack
        AttackProperties ap;
        switch (gotInput)
        {
            case JeffStats.AnimationTags.lightAttack0:
                ap = lightAttack;
                break;

            case JeffStats.AnimationTags.lightAttack1:
                ap = lightAttack;
                break;

            case JeffStats.AnimationTags.lightAttack2:
                ap = lightAttack;
                break;

            case JeffStats.AnimationTags.heavyAttack:
                ap = heavyAttack;
                break;

            default:
                ap = null;
                break;

        }
        // This condition seems to be useless but there was some problems without it
        if(ap.damage == lightAttack.damage || ap.damage == heavyAttack.damage)
        {
            ap.direction = transform.position.x;
            randomShakePower = Random.Range(-1f, 3f); // creates shake of rundom power
            ScreenShakeController.Instance.ShakeCamera(randomShakePower, 0.1f); // this is the camera shake
            collision.transform.parent.SendMessage("Damage", ap); // sending all information concerning attack here
        }   
    }
}
