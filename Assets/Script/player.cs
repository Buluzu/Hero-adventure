using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Slash;
using static Enemies;
using static MenuScence;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{
    public static player instance;
    //private MenuScence MenuScenceValue;
    public float MaxHP;
    public float MaxMP;
    public float MP;
    public float HP;
    public int star=0;
    private float horizontal;
    public float test;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 8f;   
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 4f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;
    public float damage;
    public float SlashDamage;

    private float TimebtwAttack;
    public float StartTimebtwAttack;
    public Transform attackposition;
    public LayerMask enemyLayer;
    public float attackRange;
    [SerializeField] private Collider2D cl;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Animator anime ;

    private float TimebtwSlash;
    public float StartTimebtwSlash;
    [SerializeField] private Transform slashPos;
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private float slashSpeed;
    [SerializeField] private Slash slashfip;

    public GameObject HPBar;
    public GameObject HPAmount;
    public GameObject MPBar;
    public GameObject MPAmount;
    public GameObject starAmount;

    public AudioSource aus;
    public AudioClip dash;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip attack2;
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        HP = MaxHP;
        MP=MaxMP;
    }
    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            anime.SetBool("isJumping",true);
            if (aus && jump != null)
            {
                aus.PlayOneShot(jump);
            }
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            anime.SetBool("isJumping", true);
            if (aus && jump != null)
            {
                aus.PlayOneShot(jump);
            }
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        if (IsGrounded())
        {
            anime.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        
        Flip();

        if(IsGrounded())
        {
            anime.SetFloat("idlerun", Mathf.Abs(horizontal));
        }

        if (TimebtwAttack <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anime.SetTrigger("Attack");
                if(aus && attack !=null)
                {
                    aus.PlayOneShot(attack);
                }
                Collider2D[] enemiesinAtkRange = Physics2D.OverlapCircleAll(attackposition.position, attackRange, enemyLayer);
                for (int i = 0; i < enemiesinAtkRange.Length ; i++)
                {
                    enemiesinAtkRange[i].GetComponent<Enemies>().EnemiesTakeDamage(damage);
                }
                TimebtwAttack = StartTimebtwAttack;
            }
        }
        else
        {
            TimebtwAttack -= Time.deltaTime;
        }


        if (TimebtwSlash <= 0f)
        {
            if(!isFacingRight)
            {
                slashfip.slashf.flipX = true;

                if(Input.GetMouseButtonDown(1)&&MP>=20)
                {
                    anime.SetTrigger("Slash");
                    if (aus && attack2 != null)
                    {
                        aus.PlayOneShot(attack2);
                    }
                    /*var slash = Instantiate(slashPrefab, slashPos.position, slashPos.rotation);
                    slash.GetComponent<Rigidbody2D>().velocity = -slashSpeed * slashPos.right;*/
                    Instantiate(slashPrefab, slashPos.position, slashPos.rotation);
                    Slash.instance.SpeedRate= -1f;
                    TimebtwSlash = StartTimebtwSlash;
                    MP-=20;

                }
            }
            else
            {
                slashfip.slashf.flipX = false;
                if (Input.GetMouseButtonDown(1)&&MP>=20)
                {
                    anime.SetTrigger("Slash");
                    if (aus && attack2 != null)
                    {
                        aus.PlayOneShot(attack2);
                    }
                    /*var slash = Instantiate(slashPrefab, slashPos.position, slashPos.rotation);
                    slash.GetComponent<Rigidbody2D>().velocity = slashSpeed * slashPos.right;*/
                    Instantiate(slashPrefab, slashPos.position, slashPos.rotation);
                    Slash.instance.SpeedRate = 1f;
                    TimebtwSlash = StartTimebtwSlash;
                    MP-=20;
                }
            }

        }
        else
        {
            TimebtwSlash -= Time.deltaTime;
        }
        if(HP<=0)
        {
            anime.SetBool("Die", true);
        }

        Stat();
        UpdateUI();
        if(star==6)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            anime.SetBool("isDashing", true);
           
            if (aus && dash != null)
            {
                aus.PlayOneShot(dash);
            } 
            return;
        }
        else anime.SetBool("isDashing", false);
       
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);   
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 a = transform.localScale;
            isFacingRight = !isFacingRight;
            a.x *= -1f;
            transform.localScale = a;
        }
    }

   /* private void FlipSlash()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 b = slashPos.localScale;
            isFacingRight = !isFacingRight;
            b.x *= -1f;
            slashPos.localScale = b;
        }
    }*/

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackposition.position, attackRange);
    }
    public void TakeDamage(float enemiesDamage)
    {
       
            HP -= enemiesDamage;
            anime.SetTrigger("Hurt");
        
    }

    public void Healing(float hp)
    {
        HP+=hp;
    }

    public void MPRecovery(float mp)
    { MP += mp;}

   /* public void Die()
    {
        anime.SetBool("Die", true);
        *//*cl.enabled=false;
        rb.gravityScale = 0;*//*
    }*/

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemies enemy = collision.GetComponent<Enemies>();
        if (enemy != null)
        {
            enemy.EnemiesTakeDamage(SlashDamage);
        }
    }*/
    private void Stat()
    {
        if (MP < 0) { MP = 0; }
        if (MP > MaxMP) { MP = MaxMP; }
        if (HP < 0) 
        { 

            HP = 0;
        }
        if (HP>MaxHP) { HP = MaxHP; }
        MP += Time.deltaTime *2;
        //HP += Time.deltaTime/2;

    }
    private void UpdateUI()
    {
        HPBar.GetComponent<Image>().fillAmount= (int)HP/MaxHP;
        HPAmount.GetComponent<TextMeshProUGUI>().text= HP.ToString();
        MPBar.GetComponent<Image>().fillAmount = (int)MP / MaxMP;
        MPAmount.GetComponent<TextMeshProUGUI>().text = MP.ToString();
        starAmount.GetComponent<TextMeshProUGUI>().text = star.ToString();
    }


    
}

