using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KACA_PlayerContoller : MonoBehaviour {

    public float speed;  

    public Text countText; 

    public Text winText;

    public Text endText;

    public Text startText;

    public float jumpforce;

    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    public AudioClip jumpClip;

    private float volLowRange = .5f;

    private float volHighRange = 1.0f;

    private Rigidbody2D rb2d; 



    private int count;              

    private float timer;

    private int wholetime;

    private Animator anim;

    private bool facingRight = true;

    private bool isOnGround;

    private AudioSource source;




    void Start () {

        rb2d = GetComponent<Rigidbody2D>();

        endText.text = "";

        winText.text = "";

        startText.text = "Collect The Gems!";

        count = 0;

        anim = GetComponent<Animator>();

        source = GetComponent<AudioSource>();

        SetCountText();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {

            anim.SetBool("isRunning", true);

        }
        else
        {

            anim.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("Jumping");
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void Fixedupdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");


        float moveVertical = Input.GetAxis("Vertical");


        Vector2 movement = new Vector2(moveHorizontal, moveVertical);


        rb2d.AddForce(movement * speed);


        timer = timer + Time.deltaTime;
        if (timer >= 12)
        {
            endText.text = "You Lose! :(";
            StartCoroutine(ByeAfterDelay(2));

        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {

            anim.SetBool("isRunning", true);

        }
        else
        {

            anim.SetBool("isRunning", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetTrigger("Jumping");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector2 Scaler = transform.localScale;

        Scaler.x = Scaler.x * -1;

        transform.localScale = Scaler;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);


            count = count + 1;


            GameLoader.AddScore(1);


            SetCountText();
        }
    }

    void SetCountText()
    {
        
        countText.text = "Count: " + count.ToString();


        if (count >= 10)
        {
            winText.text = "You win!";

            endText.text = "You win!";

            StartCoroutine(ByeAfterDelay(2));



        }
    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);


        GameLoader.gameOn = false;
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {

            if (Input.GetKey(KeyCode.UpArrow))
            {
             float vol = Random.Range(volLowRange, volHighRange);
                source.PlayOneShot(jumpClip);

                rb2d.velocity = Vector2.up * jumpforce;

            }
        }
        if (collision.collider.tag == "Enemy")
        {
            endText.text = "You Lose! :(";

            StartCoroutine(ByeAfterDelay(2));
        }

    }
}