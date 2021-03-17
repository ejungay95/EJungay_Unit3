using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float jumpForce = 10f;
  public float gravityModifier;
  public bool gameOver = false;

  private bool onGround = true;
  private Rigidbody rb;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    Physics.gravity *= gravityModifier;
  }

  // Update is called once per frame
  void Update()
  {
    bool spaceDown = Input.GetKeyDown(KeyCode.Space);
    if(spaceDown && onGround)
    {
      // player jumps
      rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      onGround = false;
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if(collision.gameObject.CompareTag("Ground"))
    {
      onGround = true;
    }
    else if (collision.gameObject.CompareTag("Obstacle"))
    {
      gameOver = true;
      Destroy(gameObject);
      Debug.Log("Game Over");
    }
  }
}
