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
  // private GameObject attackForce;
  // public GameObject[] enemies;
  // private bool isShooting = true;

  public GameObject bulletPrefab;
  private GameManager gameManager;

  private AudioSource enemyAudio;
  public AudioClip deathNoise;

  // Start is called before the first frame update
  void Start()
    {
      Shoot();
      gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
      enemyAudio = GetComponent<AudioSource>();
      startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z); // Initial pos of Enemy
  }

    // Update is called once per frame
    void Update()
    {
      Move(startPosition);
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
    StartCoroutine(ShootContinuous());
  }

  public IEnumerator ShootContinuous() {
    while (true) {
      yield return new WaitForSeconds(1);
      Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
      //isShooting = true;
      yield return new WaitForSeconds(1);
      //isShooting = false;
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
    pos.z = -1.0f;

    if (pos.y < -10.0f) {
      pos.y = 1.0f;
    }

    var speed = 1.0f;
    pos.y = pos.y - Time.deltaTime * speed;
    pos.x = Mathf.Sin(pos.y) * 3;
    transform.position = pos;
  }
  /*
  private void countDown() {

    attackTime -= Time.deltaTime;
  }
  8/

  /*
  int GetEnemyCount() {
    int total = 0;
    foreach (GameObject enemy in enemies) {
      if (enemy != null) {
        total += 1;
      }
    }
    return total;
  }
  */

  /*
  IEnumerator ChooseAttackForce() {
    yield return new WaitForSeconds(1);

    if (!attackForce && GameObject.FindGameObjectWithTag("Galaga")) {
      var enemyCount = GetEnemyCount();
      if (enemyCount == 0) {
        yield return new WaitForSeconds(1);
      }
      int attackerIndex = Random.Range(0, enemyCount);
      foreach (GameObject enemy in enemies) {
        if (enemy) {
          enemyCount -= 1;
        }
        if (enemyCount == attackerIndex) {
          attackForce = enemy;
        }
      }
    }
  }
  */

}
