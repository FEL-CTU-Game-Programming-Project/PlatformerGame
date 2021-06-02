using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Almost same as Jeff (except for special ability)
 */
public class EllieController : MonoBehaviour
{
    // FLIP
    private bool canFlip = true;

    // MOVEMENT
    private float horizontalMovementInputDirection;
    public float facingDirection = 1;

    // JUMP
    private bool isFacingRight = true; // on default character is facing right
    public bool isRunning;
    public bool canJump;

    // GROUND AND WALL CHECK
    public bool isGrounded;
    public bool touchingWall;
    private bool onlyOneJumpNextToWall = true;

    public Rigidbody2D jeff; 
    public PlayerData data;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private EllieStats stats;
    private EllieDefaultAttack attackInformation;

    public Image dashImage1;
    public bool isCooldown = false;

    public Transform groundCheck;

    // DASH
    public bool isDashing { get; private set; }
    private bool immortalReady = true;

    // CROUCH
    private float verticalMovementInputDirection;
    public bool isCrouching;

    // RECEIVE DAMAGE 
    private bool knockback;

    // !!! RAGE !!!
    private SpriteRenderer jeffSprite;
    public bool boosting = false;

    public ParticleSystem dust;

    private GhostController afterImage;

    private RangedAttackController rac;

    public LayerMask whatIsGround;

    public bool isInvincible { get; private set; }

    // At the beginning movementSpeed and jumpHeight is always same
    private void Start()
    {
        data.movementSpeed = 2.0f;
        data.jumpHeight = 5.0f;

        jeff = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        stats = GetComponent<EllieStats>();
        attackInformation = GetComponent<EllieDefaultAttack>();
        jeffSprite = GetComponent<SpriteRenderer>();
        afterImage = GetComponent<GhostController>();
        rac = GetComponent<RangedAttackController>();
    }

    private void FixedUpdate()
    {
        if (!stats.GetDieStatus())
        {
            CheckGroundAndWall();
        }
    }


    private void Update()
    {
        Move();
        CheckInput();
        UpdateAnimation();
        noDustWhileJumping();
        ImmortalCooldown();
    }


    // Checks all possible inputs
    private void CheckInput()
    {
        CheckMovementDirection();

        verticalMovementInputDirection = Input.GetAxisRaw("MPvertical");
        horizontalMovementInputDirection = Input.GetAxisRaw("MPhorizontal"); 

        if (verticalMovementInputDirection == -1)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        if (Input.GetButtonDown("MPJump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("MPimmortal"))
        {
            if (immortalReady)
            {
                dashImage1.fillAmount = 1;
                StartCoroutine("Immortal");
            }

        }
    }

    // This function is to for visual cooldown of immortal special skill on UI
    private void ImmortalCooldown()
    {
        if (!immortalReady)
        {
            dashImage1.fillAmount -= 1 / data.immortalCooldown * Time.deltaTime;

            if (dashImage1.fillAmount <= 0)
            {
                dashImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    public float GetFacingDirection()
    {
        return facingDirection;
    }

    private void UpdateAnimation()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", jeff.velocity.y);
        anim.SetBool("isCrouching", isCrouching);
    }


    public void Move()
    {
        // knockback of character
        if (knockback)
        {
            jeff.velocity = new Vector2(data.knockbackPower.x * attackInformation.attackDirection, data.knockbackPower.y);
        }
        // slower run while crouching
        else if (isCrouching && !knockback)
        {
            CreateDust(); // small dust when character is crouching
            jeff.velocity = new Vector2(data.movementSpeed / 2 * horizontalMovementInputDirection, jeff.velocity.y);
        }
        else if (!knockback)
        {
            // this weird condition is here to stop movement of character if player starts attacking
            if (attackInformation.isAttacking && jeff.velocity.y == 0 || stats.GetDieStatus())
            {
                DestroyDust(); // destroy dust behind character
                jeff.velocity = new Vector2(0 * horizontalMovementInputDirection, jeff.velocity.y);
            }
            // default movement if everything is normal
            else
            {
                CreateDust(); // creates dust behind character
                jeff.velocity = new Vector2(data.movementSpeed * horizontalMovementInputDirection, jeff.velocity.y);
            }

        }

    }

    // all possible pickupable things
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Powerups
        if (other.tag == "Rage")
        {
            StartCoroutine("Rage");
            Destroy(other.gameObject);
        }
        else if (other.tag == "SpeedBoost")
        {
            StartCoroutine("SpeedBoost");
            Destroy(other.gameObject);
        }

        // Ranged Attack (just for picking up bomb from the ground)

        else if (other.tag == "Bomb")
        {
            FindObjectOfType<BombCountDisplay>().bombCount++;
            Destroy(other.gameObject);
        }
 

        // Ellie can pickup keys too
        else if (other.tag == "Key")
        {
            Destroy(other.gameObject);
        }

        // Healing Potion
        else if (other.tag == "HealingPot")
        {
            FindObjectOfType<HPPotionCountDisplay>().potionCount++;
            Destroy(other.gameObject);
        }
        // instant death if player falls into the water
        else if (other.tag == "Water")
        {
            stats.DecreaseHP(stats.currentHP);
            Destroy(gameObject, 2f);
        }
    }


    public void Jump()
    {

        if (isGrounded)
        {
            jeff.velocity = new Vector2(jeff.velocity.x, data.jumpHeight);
        }
    }

    // Flipping character and checks if player is in movement or not
    private void CheckMovementDirection()
    {
        // if moventInputDirection is lower than 0 then we want to look left
        if (isFacingRight && horizontalMovementInputDirection < 0 && canFlip)
        {
            Flip();
        }
        else if (!isFacingRight && horizontalMovementInputDirection > 0 && canFlip)
        {
            Flip();
        }

        if (jeff.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            DestroyDust(); // do not create dust when player stops running
            isRunning = false;
        }
    }

    private void noDustWhileJumping()
    {
        if (!isGrounded)
        {
            DestroyDust();
        }
    }

    // creates circle that detects if player is on the ground or not
    private void CheckGroundAndWall()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, data.groundCheckRadius, whatIsGround);
    }

    // Flip the character
    private void Flip()
    {
        if (!knockback && !attackInformation.isAttacking)
        {
            CreateDust();
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    public void CreateDust()
    {
        dust.Play();
    }

    public void DestroyDust()
    {
        dust.Stop();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, data.groundCheckRadius);
    }

    // special ability immortal
    IEnumerator Immortal()
    {
        immortalReady = false;
        isInvincible = true;
        jeffSprite.color = Color.yellow;
        yield return new WaitForSeconds(data.immortalDuration);
        jeffSprite.color = Color.white;
        isInvincible = false;

        yield return new WaitForSeconds(data.immortalDuration - data.dashTime);
        immortalReady = true;
    }

    // little knockback if enemy hits player and short invincibility to get possiblity of recover and strike back after taking damage
    IEnumerator ApplyKnockback()
    {
        knockback = true;
        isInvincible = true;
        yield return new WaitForSeconds(data.knockbackDuration);
        knockback = false;
        attackInformation.attackDirection = 0;

        yield return new WaitForSeconds(data.invincibilityAfterKnockback);
        isInvincible = false;
    }

    IEnumerator Rage()
    {
        // Setup all improvements that comes with Rage
        boosting = true;
        data.jumpHeight = 7.5f;
        anim.SetFloat("rageModeHeavy", 5.0f);
        jeffSprite.color = Color.red;
        yield return new WaitForSeconds(data.boostDuration);
        // Set all improvements back to default
        jeffSprite.color = Color.white;
        data.jumpHeight = 5.0f;
        anim.SetFloat("rageModeDefault", 1.0f);
        anim.SetFloat("rageModeHeavy", 1.0f);
        boosting = false;
    }

    IEnumerator SpeedBoost()
    {
        data.movementSpeed *= 1.5f;
        yield return new WaitForSeconds(data.boostDuration);
        data.movementSpeed /= 1.5f;
    }
}
