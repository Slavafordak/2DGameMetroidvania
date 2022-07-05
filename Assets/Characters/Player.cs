using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Player : Character
{
    public static Player Instance;
    private float runSpeed = 10.0f;
    private float walkSpeed = 5.0f;
    public int MaxHealth = 100;
    public int CurrentHealth;
    public int Exp1;
    FileInfo fi = new FileInfo("Points.txt");
    //StreamWriter sw = new StreamWriter("Points.txt");
    //bool activate; 

    public HealthBar healthBar;
    public override void Start()
    {
        Instance = this;
        base.Start();
        speed = runSpeed;
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        if (!fi.Exists)
        {
            fi.Create();
        }

    }
    public override void Update()
    {
        base.Update();
        direction = Input.GetAxisRaw("Horizontal");
        HandleJumping();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
        //if ((activate == true) && Input.GetKeyDown(KeyCode.F) && (tag == "NextLevel"))
        //{
        //    Debug.Log("NEXTLEVEL");
        //    //SceneManager.LoadScene(2);
        //}
        Exp1 = CurrentHealth;
    }

    protected override void HandleMovement()
    {
        base.HandleMovement();
        myAnimator.SetFloat("speed", Mathf.Abs(direction));
        TurnAround(direction);

    }

    protected override void HandleJumping()
    {
        if (grounded)
        {
            FindObjectOfType<AudioManager>().Play("JumpPL");
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("falling", false);
        }
        //use space and w to jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
            stoppedJumping = false;
            //tell the animator to play jump anim
            myAnimator.SetTrigger("jump");
        }

        if (Input.GetButton("Jump")&&!stoppedJumping && (jumpTimeCounter > 0))
        {
            Jump();
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }
        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
            myAnimator.SetBool("falling", true);
            myAnimator.ResetTrigger("jump");
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        healthBar.SetHealth(CurrentHealth);

        myAnimator.SetTrigger("Hit");

        if (CurrentHealth <= 0)
        {
            //Die();
            StartCoroutine(WaitDie());
        }
    }

    //void Die()
    //{
    //    rb.gravityScale = 0;
    //    Debug.Log("Enemy died");
    //    myAnimator.SetBool("Dead", true);// If the health is lessthan or equil to zeroplay death animation
    //    GetComponent<Collider2D>().enabled = false;// disables the boxcollider when enemy dies
    //    this.enabled = false;//disables the enemy
    //    SceneManager.LoadScene(1);
    //}

    IEnumerator WaitDie()
    {
        rb.gravityScale = 0;
        rb.drag = 100;
        Debug.Log("Enemy died");
        myAnimator.SetBool("Dead", true);// If the health is lessthan or equil to zeroplay death animation
        GetComponent<Collider2D>().enabled = false;// disables the boxcollider when enemy dies
        GetComponent<PlayerCombat>().enabled = false;
        this.enabled = false;//disables the enemy
        FindObjectOfType<AudioManager>().Play("DiePlayer");
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(2);
    }
    void Destroy()
    {
        Destroy(gameObject, 1f);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (/*(Input.GetKeyDown(KeyCode.F) == true) &&*/ (other.tag == "NextLevel"))
        {
            StreamWriter sw = new StreamWriter("Points.txt",false);
            sw.WriteLine("Points = "+Exp1);
            sw.Close();
            Debug.Log("NEXTLEVEL");
            SceneManager.LoadScene(3);
        }
    }




}
