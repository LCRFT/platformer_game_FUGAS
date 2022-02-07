using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;
    BoxCollider2D bc2d;
    public float jumpForse;
    bool isGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatISgroun;
    private int extraJump;
    public int extraJumpValue;
    public Rigidbody2D HeroRigidbody2D;
    [SerializeField]
    int speed;
    public int count = 0;
    public TextMeshProUGUI CountText;
    public Vector3 respawnPoint;
    public bool checkpointReached;
    public int HP;
    public TextMeshProUGUI HPText;
    public GameObject GameOverPanel;
    float horizontalSpeed;
    [SerializeField] float Speed_;

    private void Start()
    {
        PlayerPrefs.SetInt("LevelIndex", SceneManager.GetActiveScene().buildIndex);
        respawnPoint = transform.position;
        LoadData();
        HPText.text="Hp:"+HP.ToString();
        CountText.text = "Count:" + count.ToString();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        extraJump = extraJumpValue;
    }

    private void Update()
    {
        if (isGround == true)
        {
            extraJump = extraJumpValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            //animator.SetTrigger("Jump");
            rb.velocity = Vector2.up * jumpForse;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGround == true)
        {
            //animator.SetTrigger("Jump");
            rb.velocity = Vector2.up * jumpForse;
        }
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatISgroun);

        if (Input.GetKey(KeyCode.D) )
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            sprite.flipX = false;
            animator.SetBool("IsWalk", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            sprite.flipX = true;
            animator.SetBool("IsWalk", true);
        }
        else 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("IsWalk", false);
        }
        transform.Translate(horizontalSpeed, 0, 0);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Ladder")
        {
            HeroRigidbody2D.gravityScale = 0;
        }

    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Ladder")
        {
            HeroRigidbody2D.gravityScale = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Loot")
        {
            Destroy(other.gameObject);
            count++;
            CountText.text = "Count:" + count.ToString();
        }
        if (other.transform.tag == "Checkpoint")
        {
            checkpointReached = true;
            respawnPoint = other.transform.position;
        }
        if (other.transform.tag == "Fall")
        {
            transform.position = respawnPoint;
            --HP;
            HPText.text = "Hp:" + HP.ToString();
            if(HP==0)
            {
                Time.timeScale = 0;
                GameOverPanel.SetActive(true);
            }

        }
        if (other.transform.tag == "Exit")
        {
            PlayerPrefs.SetInt("Count", count);
            PlayerPrefs.SetInt("Hp", HP);
            if(SceneManager.GetActiveScene().buildIndex==7)
            {
                SceneManager.LoadScene(0);
            }
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    void LoadData()
    {
        if (PlayerPrefs.HasKey("Count")&& PlayerPrefs.HasKey("Hp"))
        {
            count = PlayerPrefs.GetInt("Count");
            HP= PlayerPrefs.GetInt("Hp");
        }
        else
            count =0;
            HP = 3;
    }
    public void LeftMoveButton()
    {
        FixedUpdate();
        horizontalSpeed = -Speed_;
        sprite.flipX = true;
    }
    public void RightMoveButton()
    {
        FixedUpdate();
        animator.SetBool("isRunning", true);
        horizontalSpeed = Speed_;
        sprite.flipX = false;
    }
    public void stop()
    {
        horizontalSpeed = 0;
    }
    public void JumpButton()
    {
        if (isGround == true)
        {
            extraJump = extraJumpValue;
        }
        if (extraJump > 0)
        {
            animator.SetTrigger("Jump");
            rb.velocity = Vector2.up * jumpForse;
        }
        else if (extraJump == 0 && isGround == true)
        {
            animator.SetTrigger("Jump");
            rb.velocity = Vector2.up * jumpForse;
        }
    }
}
