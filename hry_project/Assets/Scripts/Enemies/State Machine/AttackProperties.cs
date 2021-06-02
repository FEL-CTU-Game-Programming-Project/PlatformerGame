using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProperties
{
    public float direction;
    public float damage { get; private set; }
    public enum DamageType
    {
        Neutral, Fire, Lightning, Poison, Freeze
    }

    public DamageType damageType { get; private set; }
    public float elementDamageBonus { get; private set; }

    public AttackProperties(float damage, DamageType damageType, float elementDamageBonus)
    {
        this.damage = damage;
        this.damageType = damageType;
        this.elementDamageBonus = elementDamageBonus;
    }
    public AttackProperties(float damage) {
        this.damage = damage;
        this.damageType = DamageType.Neutral;
        this.elementDamageBonus = 0;
    }
}
