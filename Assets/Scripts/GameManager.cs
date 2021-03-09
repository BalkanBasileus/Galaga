using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  // Variables
  // public List<GameObject> targets;
  private float spawnRate = 0.1f;
  private float distance = 0.5f;
  private float attackTime = 2.0f;
  private int score;
  public bool isGameActive;
  private int maxColumn = 10;
  public Text scoreText;
  public Text pointsText;
  public TextMeshProUGUI gameOverText;
  public Button restartButton;
  public GameObject enemy;
  public GameObject specificEnemy;
  private EnemyController enemyController;
  private Vector3 startPos = new Vector3(-3, -4.5f, -1);

  // Start is called before the first frame update
  void Start() {
    isGameActive = true;
    StartCoroutine( SpawnTarget() );
    UpdateScore(0);
  }

  // Update is called once per frame
  void Update() {

    countDown();
 
    // Initiate attack every n seconds.
    if (attackTime == 0) {

     // StartCoroutine(ChooseAttackForce());
      Attack();
    }
  }
  
  public IEnumerator SpawnTarget() {

    yield return new WaitForSeconds(spawnRate);
      
    // Spawn Brigade of Enemy Bees. Update pos.
    for(int i = 0; i< maxColumn; i++) {
      
      // Provide space for two main columns
      if(i % 5 == 0) {
        startPos.x += distance;
        continue;
      }

      Instantiate(enemy, startPos, enemy.transform.rotation);
      startPos.x += distance;
    }
  }
  

  // Update Score
  public void UpdateScore( int scoreToAdd ) {
    score += scoreToAdd;
    pointsText.text = score.ToString();
  }

  public void GameOver() {
    gameOverText.gameObject.SetActive(true);
    restartButton.gameObject.SetActive(true);
    isGameActive = false;
  }

  public void RestartGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  // Display time countdown
  private void countDown() {

    attackTime -= Time.deltaTime;
  }
  /*
  void Attack() {
    //enemy.transform.parent = null;
   // specificEnemy = GameObject.FindGameObjectWithTag("EnemyBee");

    EnemyController enemyBee = specificEnemy.GetComponent<EnemyController>();
    enemyBee.attackVector();
  }
  */

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
  IEnumerator ChooseAttackForce() {
    yield return new WaitForSeconds(1);

    if (!specificEnemy && GameObject.FindGameObjectWithTag("Galaga")) {
      //var enemyCount = GetEnemyCount();
      //if (enemyCount == 0) {
       // yield return new WaitForSeconds(1);
    //  }
      int attackerIndex = Random.Range(0, enemyCount);
      foreach (GameObject enemy in enemies) {
        if (enemy) {
          enemyCount -= 1;
        }
        if (enemyCount == attackerIndex) {
          specificEnemy = enemy;
        }
      }
    }
  }
  */

}
