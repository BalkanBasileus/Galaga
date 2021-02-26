using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{

	public int left = 3;
	public bool spawning = false;
	public GameObject galagaPref;
	//PlayerController PlayerController;

	IEnumerator SpawnGalaga() {
		spawning = true;
		yield return new WaitForSeconds(1f);
		GameObject newGalaga = (GameObject)Instantiate(galagaPref);
		newGalaga.transform.position = new Vector3(0.29f, -9.17f, -1);
		newGalaga.tag = "Galaga";
		newGalaga.name = "Galaga";
		spawning = false;
		//newGalaga.AddComponent<SpriteR>();
	}
	// Update is called once per frame
	void Update() {
		GameObject galaga = GameObject.FindGameObjectWithTag("Galaga");
		if (!galaga && !spawning && left > 0) {
			left -= 1;
			StartCoroutine(SpawnGalaga());
		}

		//TextMesh mesh = GetComponent<TextMesh>();
		//mesh.text = "" + left;
/*
		if (left == 0 && !galaga && !spawning) {
			Instantiate(gameoverPref);
		}
*/
	}
}