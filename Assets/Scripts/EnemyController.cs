/*///////////////////////////////////////////////////////////////////////
 *
 * Note: Never start a coroutine in Update(). Else continuous.
 //////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  // Class Variables
  public float speed;
  //private float shootInterval = 10.0f;
 // private float attackTime = 5.0f;
  public int direction = 1;
  private int pointValue = 100;
  private Vector3 startPosition;
  private float travelDistance = 0.5f;
  public float shootInterval = 10.0f;
  public bool canShoot = true;
  private bool startAttackVector = false;

  public GameObject bulletPrefab;
  private GameManager gameManager;

  private AudioSource enemyAudio;
  public AudioClip deathNoise;

  // Start is called before the first frame update
  void Start()
    {
      //Shoot();
      gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      enemyAudio = GetComponent<AudioSource>();
      startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); // Initial pos of Enemy
  }

    // Update is called once per frame
    void Update()
    {
      Move(startPosition);
      if (startAttackVector) {
        attackVector();
        Shoot();
      }
    }

  void Move(Vector3 startPosition) {
    // Move enemy bee left and right

    var pos = transform.position; // Get current pos.

    pos.x = pos.x + direction * Time.deltaTime * speed;
    pos.x = Mathf.Clamp(pos.x, startPosition.x - travelDistance, startPosition.x + travelDistance);

    // Update Pos next call.
    transform.position = pos;

    // Slide side to side
    if (pos.x == startPosition.x - travelDistance || pos.x == startPosition.x + travelDistance) {
      direction = -1 * direction; // Switch direction
    }
  }

  public void Shoot() {

    if (canShoot) {
      canShoot = false;
      Invoke(nameof(CanShootNow), shootInterval); // Prevent enemy from spamming shoot in Gamanager Update() method.
      Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
    }
  }


  private void OnTriggerEnter2D( Collider2D other ) {
    // If Enemy Bee hit
    if (other.gameObject.CompareTag("Bullet")) {

      // Update Score
      enemyAudio.PlayOneShot(deathNoise);
      gameManager.UpdateScore(pointValue);
    }
  }

  public void attackVector() {
    Vector3 pos = transform.position;
    //pos.z = -1.0f;

    if (pos.y < -10.0f) {
      pos.y = 0.0f;
    }

    var speed = 1.0f;
    pos.y = pos.y - Time.deltaTime * speed;
    if (pos.x < 0) {
      pos.x = Mathf.Sin(pos.y) * 2 * -1;
    }
    else {
      pos.x = Mathf.Sin(pos.y) * 2;
    }
    transform.position = pos;
  }

  public void startAttack() {
    startAttackVector = true;
  }

  private void CanShootNow() {
    canShoot = true;
  }

}
