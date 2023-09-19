using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBehavior : MonoBehaviour
{
    private const float kEggSpeed = 100f;
    //private const int kLifeTime=4000;
    //private int mLifeCount=0;
    //private GameController mGameController = null;
    // Start is called before the first frame update
    private Vector3 move;
    void Start()
    {
        //mLifeCount=kLifeTime;
        //mGameController=FindObjectOfType<GameController>();
        move = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        move.z=0;
        move.Normalize();
        PointAtPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position+ move * (Time.deltaTime * kEggSpeed);
        //transform.position+=transform.up*(kEggSpeed*Time.smoothDeltaTime);
    }

    private void PointAtPosition(Vector3 p)
    {
        //Vector3 v = p - transform.position;
        //transform.up = Vector3.LerpUnclamped(transform.up, v, kEggSpeed);
        Vector2 direction = new Vector2(p.x-transform.position.x, p.y-transform.position.y);
        transform.up=direction;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
