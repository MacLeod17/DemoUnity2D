using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float m_speed = 4.0f;
    public float m_jumpForce = 7.5f;

    public Animator m_animator;
    public Rigidbody2D m_body2d;
    public Sensor_Bandit m_groundSensor;
    public Health m_health;

    public bool m_grounded { get; set; } = false;
    public bool m_combatIdle { get; set; } = false;
    public bool m_isDead { get; set; } = false;

    // Update is called once per frame
    void Update()
    {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // -- Handle input and movement --
        float inputX = Input.GetAxis("Horizontal");

        if (!m_isDead)
        {
            // Swap direction of sprite depending on walk direction
            if (inputX > 0) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

            else if (inputX < 0) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Move
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        //Death
        //m_animator.SetTrigger("Death");
        //m_isDead = !m_isDead;

        //Hurt
        //m_animator.SetTrigger("Hurt");

        //Jump
        if (Input.GetKeyDown("space") && m_grounded && !m_isDead)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }

        //Run
        else if (Mathf.Abs(inputX) > Mathf.Epsilon && !m_isDead) m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle) m_animator.SetInteger("AnimState", 1);

        //Idle
        else m_animator.SetInteger("AnimState", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("Finish"))
        {
            //winScreen.SetActive(true);
            //Time.timeScale = 0;
            //return;
        }
        if (collision.CompareTag("DeathZone"))
        {
            m_health.Die();
            return;
        }
    }
}
