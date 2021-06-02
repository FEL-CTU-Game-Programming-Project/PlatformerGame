using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEllie : MonoBehaviour
{
    private EllieStats stats;

    public BombBehaviour bombPrefab;
    public Transform bombOffset;

    public bool throwingBomb;

    // how many HP are healed
    public int healNum = 20;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<EllieStats>();
    }

    private void Update()
    {
        CheckInput();
    }

    // Checks all possible inputs from inventory. This function chcecks if there is more than 0 bombs/hpPotions/arrows in inventory at the moment 
    // and if required key is pressed, then following functions are called
    private void CheckInput()
    {

        // BOMB
        if (Input.GetButtonDown("First Slot Inventory") && FindObjectOfType<BombCountDisplay>().bombCount > 0 && !FindObjectOfType<EllieController>().isRunning)
        {
            Debug.Log("Bombing");
            throwingBomb = true;
            FindObjectOfType<BombCountDisplay>().bombCount--;
            Bomb();
        }
        // HEALTH POTION
        if (Input.GetButtonDown("Third Slot Inventory") && FindObjectOfType<HPPotionCountDisplay>().potionCount > 0)
        {
            FindObjectOfType<HPPotionCountDisplay>().potionCount--;
            stats.Heal(healNum);
        }
    }

    // Creates bomb
    private void Bomb()
    {
        Instantiate(bombPrefab, bombOffset.position, transform.rotation);
        throwingBomb = false;
    }
}
