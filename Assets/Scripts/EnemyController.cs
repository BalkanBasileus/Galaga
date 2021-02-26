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
  private float shootInterval = 10.0f;
  public int direction = 1;
  // private GameObject attackForce;
  // public GameObject[] enemies;
  // private bool isShooting = true;

  public GameObject bulletPrefab;

  // Start is called before the first frame update
  void Start()
    {
      Shoot();
      //ChooseAttackForce();
    }

    // Update is called once per frame
    void Update()
    {
      Move();
    }

  void Move() {
    // Move enemy bee left and right

    var pos = transform.position;
    pos.x = pos.x + direction * Time.deltaTime * speed;
    pos.x = Mathf.Clamp(pos.x, -1, 1);

    transform.position = pos;

    if (pos.x == -1 || pos.x == 1) {
      direction = -1 * direction;
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

  /*
  private void OnCollisionTrigger( Collision collision ) {
    // If Galaga hit
    if (collision.gameObject.CompareTag("Bullet")) {

      Debug.Log("Hit!");
    }
  }
  */

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
