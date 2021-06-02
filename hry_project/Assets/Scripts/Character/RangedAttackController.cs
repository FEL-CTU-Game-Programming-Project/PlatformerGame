using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    private JeffStats stats;

    public BombBehaviour bombPrefab;
    public Transform bombOffset;

    public ArrowBehaviour arrowPrefab;
    public Transform ArrowLaunchOffset;

    public bool isShooting;
    public bool throwingBomb;

    // how many HP are healed
    public int healNum = 20;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<JeffStats>();
    }

    private void Update()
    {
        CheckInput();
        UpdateAnimation();
    }

    // Checks all possible inputs from inventory. This function chcecks if there is more than 0 bombs/hpPotions/arrows in inventory at the moment and if required key is pressed, then following functions are called
    private void CheckInput()
    {

        // BOMBA (je tady i podminka aby hrac nemohl zaroven bezet a hazet bomby)
        if (Input.GetButtonDown("First Slot Inventory") && FindObjectOfType<BombCountDisplay>().bombCount > 0 && !FindObjectOfType<PlayerController>().isRunning)
        {
            throwingBomb = true;
            FindObjectOfType<BombCountDisplay>().bombCount--; // lowers number of bombs on UI
            Bomb();
        }

        // LUK
        if (Input.GetButtonDown("Second Slot Inventory") && FindObjectOfType<ArrowCountDisplay>().arrowCount > 0)
        {
            BowShot();
        }

        if (Input.GetButtonDown("Third Slot Inventory") && FindObjectOfType<HPPotionCountDisplay>().potionCount > 0)
        {
            FindObjectOfType<HPPotionCountDisplay>().potionCount--;
            stats.Heal(healNum);
        }
    }

    // checks if any animation should be played at the moment
    private void UpdateAnimation()
    {
        anim.SetBool("isShooting", isShooting);
    }

    // Creates bomb
    private void Bomb()
    {
        Instantiate(bombPrefab, bombOffset.position, transform.rotation);
        throwingBomb = false;
    }

    // set bool to true and animation starts playing
    private void BowShot()
    {
        isShooting = true;
    }

    // creates arrow
    public void ShootArrow()
    {
        Instantiate(arrowPrefab, ArrowLaunchOffset.position, transform.rotation);
    }

    // is called at the end of bowShot animation and set bool to not do shooting animation again and lowers number of arrows
    public void FinishBowShot()
    {
        isShooting = false;

        if (FindObjectOfType<ArrowCountDisplay>().arrowCount > 0)
        {
            FindObjectOfType<ArrowCountDisplay>().arrowCount--;
        }
        anim.SetBool("isShooting", isShooting);
    }
}
