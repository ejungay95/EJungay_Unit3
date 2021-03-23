﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float jumpForce = 10f;
  public float gravityModifier;
  public bool gameOver = false;

  private bool onGround = true;
  private Rigidbody rb;
  private Animator animPlayer;

  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    Physics.gravity *= gravityModifier;
    animPlayer = GetComponent<Animator>();
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
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if(collision.gameObject.CompareTag("Ground"))
    {
      onGround = true;
    }
    else if (collision.gameObject.CompareTag("Obstacle"))
    {
      // End the game and play death animation
      gameOver = true;
      animPlayer.SetBool("Death_b", true);
      animPlayer.SetInteger("DeathType_int", 1);
      Debug.Log("Game Over");
    }
  }
}
