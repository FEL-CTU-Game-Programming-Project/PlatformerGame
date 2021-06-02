using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Similar to JeffStats (almost same)
 */
public class EllieStats : MonoBehaviour
{
    [SerializeField] private float baseHP;
    public float currentHP;


    [SerializeField] private bool dying = false;
    private Animator anim;
    private EllieController ec;

    private SpriteRenderer jeffSprite;

    public HealthBar healthBar;

    public GameObject DeathUI;

    // types of attack, lightAttack0,1 are from combo and heavyAttack is heavyAttack
    public enum AnimationTags
    {
        none, lightAttack0, lightAttack1, heavyAttack, consumableUsed
    }

    // at the beginning hp is set and it is also set to UI health bar
    private void Start()
    {
        anim = GetComponent<Animator>();
        ec = GetComponent<EllieController>();
        jeffSprite = GetComponent<SpriteRenderer>();
        currentHP = baseHP;
        healthBar.SetMaxHealth(baseHP);
    }

    // decreases HP of character 
    public void DecreaseHP(float damageTaken)
    {
        currentHP -= damageTaken;
        StartCoroutine("Flash");
        if (currentHP <= 0.00f)
        {
            Die();
        }
    }

    // starts die animation and destroy character after 4 seconds and pops up window with restart/exit
    private void Die()
    {
        dying = true;
        anim.SetBool("isDying", dying);
        Destroy(gameObject, 4f);
        DeathUI.gameObject.SetActive(true);
    }

    // Blicks red and back to default 
    IEnumerator Flash()
    {
        jeffSprite.color = new Color(90, 0, 0);
        yield return new WaitForSeconds(0.05f);

        if (!ec.boosting) // this condition is to dont go white too soon if player is boosting/usingRage. If there was not this condition, character would be both white and rage boosting.
        {
            jeffSprite.color = Color.white;
        }
    }

    public bool GetDieStatus()
    {
        return dying;
    }

    // increases HP on health bar UI
    public void Heal(int healNum)
    {
        if (currentHP == baseHP)
        {
            return;
        }

        if (currentHP + healNum >= baseHP)
        {
            healthBar.SetHealth(baseHP);
            currentHP = baseHP;
        }
        else
        {
            healthBar.SetHealth(currentHP + healNum);
            currentHP += healNum;
        }
    }
}
