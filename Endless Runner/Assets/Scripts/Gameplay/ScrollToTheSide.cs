using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollToTheSide : MonoBehaviour
{
    float speed; //The speed at which objects will scroll to the left. Everything should be consistent so it will derive this value from the game manager

    void Start()
    {
        speed = GameManager.instance.gameSpeed;
    }

    void FixedUpdate()
    {
        if (GameManager.instance.gameActive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
