using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private float test1;
    public GameObject HPBar;
    public float MaxHP;
    public float HP;
    public float speed;
    public float SlashDamage;
    public float SpinDamage;
    public Rigidbody2D rb;
    private bool isFacingRight=true;
    [SerializeField] private float a;
    private IEnumerator coroutine;
    public Animator anime;

    public float sigh;
    public float attackRange;
    public Vector2 spinArea;
    public float spinRange;
    private float TimebtwAttack;
    public float StartTimebtwAttack;
    private float TimebtwSpin;
    public float StartTimebtwSpin;

    public Transform spinPos;
    public Transform attackPos;
    public Transform playerVector2;
    public Transform playerCheck;
    public LayerMask playerLayer;
    public static Boss instance;

    private void Awake()
    {
        instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Enemies.instance.health = MaxHP;
        InvokeRepeating("randomPos", 5, 5);
        anime.GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        HP = Enemies.instance.health;
        Stat();
        UpdateUI();
        test1 = transform.position.x;
        if (Boss.instance.HP <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void FixedUpdate()
    {
        if(!IsPlayerComing())
        {
            anime.SetBool("attack3", false);
            move();
            if (a != transform.position.x)
            {
                anime.SetBool("running", true);
            }
            else
            {
                anime.SetBool("running", false);
            }
            flip();
        }
        else
        {
            if(IsPlayerInAttackRange())
            {
                if (TimebtwAttack <= 0f)
                {
                    anime.SetTrigger("attack1");
                    Collider2D[] playerinAtkRange = Physics2D.OverlapCircleAll(attackPos.position, attackRange, playerLayer);
                    for (int i = 0; i < playerinAtkRange.Length; i++)
                    {

                        playerinAtkRange[0].GetComponent<player>().TakeDamage(SlashDamage);
                    }
                    TimebtwAttack = StartTimebtwAttack;

                }
                else
                {
                    TimebtwAttack -= Time.deltaTime;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerVector2.position.x, transform.position.y), speed * Time.deltaTime);
                if(IsPlayerInSpinRange())
                {
                    anime.SetBool("attack3", true);
                    if (TimebtwSpin <= 0f)
                    {
                        //Collider2D[] playerinSpinRange = Physics2D.OverlapBoxAll(attackPos.position, spinArea, playerLayer);
                        Collider2D[] playerinSpinRange = Physics2D.OverlapCircleAll(spinPos.position, spinRange-8, playerLayer);
                        for (int i = 0; i < playerinSpinRange.Length; i++)
                        {

                            playerinSpinRange[i].GetComponent<player>().TakeDamage(SpinDamage);
                        }
                        TimebtwSpin = StartTimebtwSpin;

                    }
                    else
                    {
                        TimebtwSpin -= Time.deltaTime;
                    }
                }


              flip1();
            }
            
        }




    }
    private void Stat()
    {
        
        if (HP < 0)
        {
            HP = 0;
        }
        

    }

    private void UpdateUI()
    {
        HPBar.GetComponent<Image>().fillAmount = HP/MaxHP;
        
    }
    private void flip()
    {
        if (isFacingRight && transform.position.x  > a  || !isFacingRight && rb.velocity.x < a)
        {
            Vector3 b = transform.localScale;
            isFacingRight = !isFacingRight;
            b.x *= -1f;
            transform.localScale = b;
        }
    }

    private void flip1()
    {
        if (isFacingRight && transform.position.x > playerVector2.position.x || !isFacingRight && rb.velocity.x < playerVector2.position.x)
        {
            Vector3 b = transform.localScale;
            isFacingRight = !isFacingRight;
            b.x *= -1f;
            transform.localScale = b;
        }
    }

    private void move()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(a, transform.position.y), speed * Time.deltaTime);
    }

    /*private IEnumerator Move()
    {
        
        yield return new WaitForSeconds(10);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Random.Range(0, 40), 5.2f), speed * Time.deltaTime);
    }*/

    /*private bool IsPlayerComing()
    {
        return Physics2D.OverlapCircle(playerCheck.position, 10f, playerLayer);
    }*/

    void randomPos() 
    {        
        a = Random.Range(0, 40);
    }

    private bool IsPlayerComing()
    {
        return Physics2D.OverlapCircle(playerCheck.position, sigh, playerLayer);
    }

    private bool IsPlayerInAttackRange()
    {
        return Physics2D.OverlapCircle(playerCheck.position, attackRange, playerLayer);
    }

    private bool IsPlayerInSpinRange()
    {
        return Physics2D.OverlapCircle(playerCheck.position, spinRange, playerLayer);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerCheck.position, sigh);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spinPos.position, spinArea);
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireSphere(attackPos.position, attackRange+1.5f);
        Gizmos.DrawWireSphere(spinPos.position, spinRange);
        Gizmos.DrawWireSphere(playerCheck.position, sigh);


    }

}
    
