using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketpiece : MonoBehaviour
{
    private GameManager manager= null;
    public int piece;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void Use() {
        manager.updateRocket(piece);
        Destroy(gameObject);
    }
}
