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
    [SerializeField] int speed;
    private int count = 0;
    public TextMeshProUGUI CountText;
    public Vector3 respawnPoint;
    public bool checkpointReached;
    public int HP;
    public TextMeshProUGUI HPText;
    public GameObject GameOverPanel;
    int horizontalSpeed;

    private void Start()
    {
        PlayerPrefs.SetInt("LevelIndex", SceneManager.GetActiveScene().buildIndex);
        respawnPoint = transform.position;
        LoadData();
        HPText.text = "Hp:" + HP.ToString();
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
            rb.velocity = Vector2.up * jumpForse;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && isGround == true)
        {
            rb.velocity = Vector2.up * jumpForse;
        }
        if (horizontalSpeed != 0)
        {
            rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
            animator.SetBool("IsWalk", true);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("IsWalk", false);
        }
    }
    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatISgroun);
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
                PlayerPrefs.SetInt("Hp", 3);
                PlayerPrefs.SetInt("NewHighscore", 1);
                SceneManager.LoadScene(8);
            }
            else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    void LoadData()
    {
            count = PlayerPrefs.GetInt("Count",0);
            HP= PlayerPrefs.GetInt("Hp",3);
    }
    public void LeftMoveButton()
    {
        horizontalSpeed = -speed;
        sprite.flipX = true;
    }
    public void RightMoveButton()
    {
        horizontalSpeed = speed;
        sprite.flipX = false;
    }
    public void Stop()
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
            rb.velocity = Vector2.up * jumpForse;
        }
        else if (extraJump == 0 && isGround == true)
        {
            rb.velocity = Vector2.up * jumpForse;
        }
    }
}
