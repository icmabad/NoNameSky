using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null;
    public Text rocketText = null;
    public GameObject[] rocketPieces;
    private int rocketCount = 0;
    private int rocketUsed = 0;
    public GameObject rocket;
    public Sprite[] rocketStages;
    public GameObject[] enemies;

    public Queue<GameObject> gachaSpawn = new Queue<GameObject>();
	void Update () {
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }
        if (rocketUsed == 4)
        {
            SceneManager.LoadScene("GameOver-ShipRepaired"); // RENAME FOR OUR SCENE
            rocketUsed = 0;
        }
    }

    public void useWish() {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "Gacha")
        {   
            SceneManager.LoadScene("Gacha", LoadSceneMode.Additive);
        }
    }

    public void dropRocket(Vector3 place){
        if(rocketCount<rocketPieces.Length){
            Instantiate(rocketPieces[rocketCount], place, Quaternion.identity);
            rocketCount++;
        }
    }

    public void updateRocket(int piece){
        rocketUsed++;
        rocketText.text = rocketUsed.ToString()+"/4";
        rocket.GetComponent<SpriteRenderer>().sprite = rocketStages[piece];
        //new enemy
        if(piece<3){
            GameObject B = GameObject.Find("waypoint2");
            Instantiate(enemies[piece], B.transform.position, Quaternion.identity);
        }
    }
}