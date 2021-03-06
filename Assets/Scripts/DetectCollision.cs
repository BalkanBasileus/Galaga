/*///////////////////////////////////////////////////////////////////////
 * Detect Collision script attached to all bullet 
 * prefabs. If galaga bullet hit enemy, destroy.
 * If enemy bullet hit Galaga, destory.
 *
 //////////////////////////////////////////////////////////////////////*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{

  // Start is called before the first frame update
  void Start() {
    
  }

  // Update is called once per frame
  void Update() {

  }

  private void OnTriggerEnter2D( Collider2D other ) {

    // If Galaga Bullet hits enemy bee
    if( other.gameObject.CompareTag("EnemyBee") && gameObject.CompareTag("Bullet") ) {
     // Debug.Log("Hit!");
      Destroy(gameObject);
      // Destroy(other.gameObject); call destroy in player controller to destroy enemy
    }
    // If enemy bullet hits Galaga
    else if (other.gameObject.CompareTag("Galaga") && gameObject.CompareTag("EnemyBullet") ) {
     // Debug.Log("Galaga Hit!");
      Destroy(gameObject);
      //Destroy(other.gameObject); call destroy in player controller to destroy galga
    }
  }
}
