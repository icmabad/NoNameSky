using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] waypoints;
    private const float kSpeed = 30f;
    private float WPradius = .2f;
    private int current=0;
    public int MaxHealth=100;
    private int health;
    public healthbar bar;
    private GameManager manager = null;
    private float xscale;

    enum State{
        Patrol,
        Chase
    }

    private State currentState = State.Patrol;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameObject A = GameObject.Find("waypoint1");
        GameObject B = GameObject.Find("waypoint2");
        waypoints = new GameObject[] {A,B};
        health=MaxHealth;
        //bar.SetMaxHealth(MaxHealth);
        manager=FindObjectOfType<GameManager>();
        Vector3 characterScale = transform.localScale;
        xscale = characterScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //for testing
        if (Input.GetKeyDown(KeyCode.T))
        {
            health-=25;
            bar.SetHealth(health);
            if(health<=0){
                Destroy(gameObject);
            }
        }

        switch (currentState)
        {
            case State.Patrol:
                ServicePatrolState();
                break;
            case State.Chase:
                ServiceChaseState();
                break;
        }

    }
    
        private void PointAtPosition(Vector3 p)
    {
        Vector3 characterScale = transform.localScale;
        float v = p.x - transform.position.x;
        if (v<0) {
            characterScale.x = xscale;
        } else {
            characterScale.x = -xscale;
        }
        transform.localScale = characterScale;
    }

    private void ServicePatrolState()
    {
        GameObject hero = GameObject.Find("Hero");
        float dist = Vector3.Distance(hero.transform.position, transform.position);
        if (dist < 60)
        {
            currentState = State.Chase;
        }
        else
        {
            if (Vector2.Distance(waypoints[current].transform.position, transform.position) < WPradius)
            {
                current++;
                if (current >= waypoints.Length){
                    current = 0;
                }
            }
            PointAtPosition(waypoints[current].transform.position);
            transform.position = Vector2.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * kSpeed);
        }
    }

    private void ServiceChaseState()
    {
        GameObject hero = GameObject.Find("Hero");
        float dist = Vector3.Distance(hero.transform.position, transform.position);
        if (dist > 90)
        {
            currentState = State.Patrol;
        }
        else
        {
            PointAtPosition(hero.transform.position);
            transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, Time.deltaTime*kSpeed);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Bullet"){
            health-=10;
            bar.gameObject.SetActive(true);
            bar.SetHealth(health);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Bullet2"){
            health-=25;
            bar.gameObject.SetActive(true);
            bar.SetHealth(health);
            Destroy(collision.gameObject);
        }
        if(health<=0){
            manager.dropRocket(transform.position);
            Destroy(gameObject);
        }
    }
/*

    private void ThisEnemyIsHit()
    {
        sEnemySystem.OneEnemyDestroyed();
        if (sEnemySystem.currentCameraTarget == gameObject)
        {
            sEnemySystem.currentCameraTarget = null;
            Camera camera = GameObject.Find("GameManager").GetComponent<GameManager>().enemyCamera.GetComponent<Camera>();
            camera.gameObject.SetActive(false);
            GameObject.Find("GameManager").GetComponent<GameManager>().labelEnemyChaseCam.GetComponent<UnityEngine.UI.Text>().text = "Enemy Chase Cam: Shut Off";
        }
        Destroy(gameObject);
    }
    #endregion
    */
}
