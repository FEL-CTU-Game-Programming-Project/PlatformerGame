using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]

// Constants for characters, it can be changed also in unity, it is just for better overview of these attributes
public class PlayerData : ScriptableObject
{
    public float movementSpeed = 2.0f;
    public float jumpHeight = 5.0f;
    public float groundCheckRadius = 0.1f;
    public float dashTime = 0.25f;
    public float dashSpeed = 10f;
    public float dashCooldown = 3f;
    public float knockbackDuration = 0.25f;
    public Vector2 knockbackPower = new Vector2(2,0);
    public float invincibilityAfterKnockback = 2;
    public float boostDuration = 10; // k boostum

    public float inputTimer = 0.1f;
    public float lightAttackDamage = 20;
    public float lightAttackComboDamage = 40;
    public float heavyAttackDamage = 60;

    public float flashWhiteDuration = 0.5f;

    public float immortalDuration = 3.0f;
    public float immortalCooldown = 10.0f;
}
