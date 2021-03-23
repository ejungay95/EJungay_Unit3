using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float jumpForce = 10f;
  public float gravityModifier;
  public bool gameOver = false;

  public ParticleSystem expSystem;
  public ParticleSystem dirtSystem;

  public AudioClip jumpSound;
  public AudioClip crashSound;

  private bool onGround = true;
  private Rigidbody rb;
  private Animator animPlayer;
  private AudioSource asPlayer;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    Physics.gravity *= gravityModifier;
    animPlayer = GetComponent<Animator>();
    asPlayer = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    bool spaceDown = Input.GetKeyDown(KeyCode.Space);
    if(spaceDown && onGround && !gameOver)
    {
      // player jumps
      rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      onGround = false;
      animPlayer.SetTrigger("Jump_trig");
      asPlayer.PlayOneShot(jumpSound, 1.0f);
      dirtSystem.Stop();
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if(collision.gameObject.CompareTag("Ground"))
    {
      onGround = true;

      // Prevents dirt to play when player lands on the ground after a death.
      if(!gameOver)
      {
        dirtSystem.Play();
      }
    }
    else if (collision.gameObject.CompareTag("Obstacle"))
    {
      // End the game and play death animation
      Debug.Log("Game Over");
      gameOver = true;
      animPlayer.SetBool("Death_b", true);
      animPlayer.SetInteger("DeathType_int", 1);
      expSystem.Play();
      dirtSystem.Stop();
      asPlayer.PlayOneShot(crashSound, 1.0f);
    }
  }
}
