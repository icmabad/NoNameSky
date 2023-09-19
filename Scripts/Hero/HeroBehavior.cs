// the one to update
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HeroBehavior : MonoBehaviour {

    public float health;
    public int healthRounded;
    public Text healthDisplay; // for displaying health.... later

    private float speed = 30f;  // 20-units in a second
   	private bool canMoveRight = true;
    public Rigidbody2D rb;
    private Vector2 moveVelocity;
    public Animator animator;
    private float horizontalMove;
    private float verticalMove;

    private Transform playerPos;
    public GameObject fruit;
    public GameObject wish;

    public GameObject object1;
    public GameObject object2;
    public GameObject rocks1;
    public GameObject rocks2;
    public GameObject gun1;
    private float distanceToTree;

    private int fruitLimit = 4;
    private int wishLimit = 3;
    private int gunLimit = 1;
    public int currentWeapon = 1;
    private int shootwait=2;

    private bool inContactWithEnemy = false;

    void Start ()
    { 
    	rb.freezeRotation = true;
        healthRounded = 20;
    }
	
	// Update is called once per frame
	void Update () {
        if (inContactWithEnemy) {
          health = health - 0.01f;
          healthRounded = (int) health;
        }
         
        if(healthRounded < 10) {
           GameObject.Find("HealthDisplay").GetComponent<UnityEngine.UI.Text>().text = " " + healthRounded; 
        } else {
            GameObject.Find("HealthDisplay").GetComponent<UnityEngine.UI.Text>().text = "" + healthRounded;
        }
        if (healthRounded < 0 || healthRounded == 0) // oh noes hero died
        {
            SceneManager.LoadScene("GameOver-HPZero"); // rename as needed
        }
        Vector3 pos = transform.position;

        if (Input.GetKeyUp(KeyCode.Space)){
            switch (currentWeapon) {
                case 1:
                    animator.SetInteger("weapon",1);
                    animator.SetBool("shoot", true);
                    GameObject red = Instantiate(Resources.Load("Prefabs/redLaser") as GameObject);
                    red.transform.localPosition = transform.localPosition;
                    break;
                case 2:
                    animator.SetInteger("weapon",2);
                    animator.SetBool("shoot", true);
                    GameObject purple = Instantiate(Resources.Load("Prefabs/purpleLaser") as GameObject);
                    purple.transform.localPosition = transform.localPosition;
                    break;
            }
        }

        if(animator.GetBool("shoot")==true){
            shootwait--;
            if(shootwait<0){
                shootwait=2;
                animator.SetBool("shoot", false);
            }
        }

        //Animation
        horizontalMove = Input.GetAxisRaw("Horizontal")*speed;
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));
        if(Mathf.Abs(horizontalMove)<.01){
            verticalMove = Input.GetAxisRaw("Vertical")*speed;
            animator.SetFloat("speed", Mathf.Abs(verticalMove));
        }

		// Flip the Character:
		Vector3 characterScale = transform.localScale;

		// "shift" can be replaced with any key
        // this section moves the character faster
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 60f;
            animator.SetBool("shift", true);
        } else {
        	speed = 30f;
            animator.SetBool("shift", false);
        }

        // "w" can be replaced with any key
        // this section moves the character up
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        	&& pos.y < 19)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        // "s" can be replaced with any key
        // this section moves the character down
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        	&& pos.y > -66)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }

        // "d" can be replaced with any key
        // this section moves the character right
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        	&& canMoveRight && pos.x < 153)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            characterScale.x = -3;
        }

        // "a" can be replaced with any key
        // this section moves the character left
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			&& pos.x > -153)

        {
        	canMoveRight = true;
            transform.position += Vector3.left * speed * Time.deltaTime;
            characterScale.x = 3;
        }
        transform.localScale = characterScale;	

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            &&  50 >= Vector3.Distance(object1.transform.position, object2.transform.position)) {
            playerPos = GameObject.FindGameObjectWithTag("Tree").GetComponent<Transform>();
            int z = Random.Range(1,4);
            if(z==3&&wishLimit>0){
                Instantiate(wish, playerPos.position, Quaternion.identity);
                wishLimit--;
            } else if (fruitLimit > 0){
                Instantiate(fruit, playerPos.position, Quaternion.identity);
                fruitLimit--;
            }
        }

        if (((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && gunLimit > 0)
            &&  (50 >= Vector3.Distance(object1.transform.position, rocks1.transform.position)
                || 50 >= Vector3.Distance(object1.transform.position, rocks2.transform.position))) {
            playerPos = GameObject.FindGameObjectWithTag("rocks").GetComponent<Transform>();
            Instantiate(gun1, playerPos.position, Quaternion.identity);
            gunLimit--;
        }
    }


    private void OnTriggerEnter2D (Collider2D targetObj) {
    	if (targetObj.gameObject.tag == "BigLog") {
    		canMoveRight = false;
    	}

        if (targetObj.gameObject.tag == "enemy") {
            inContactWithEnemy = true;
        }
	}

    private void OnTriggerExit2D (Collider2D targetObj) {
    	if (targetObj.gameObject.tag == "BigLog") {
    		canMoveRight = true;
    	}
        if (targetObj.gameObject.tag == "enemy") {
            inContactWithEnemy = false;
        }
	}
}