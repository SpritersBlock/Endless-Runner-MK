using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollToTheSide : MonoBehaviour
{
    float speed; //The speed at which objects will scroll to the left. Everything should be consistent so it will derive this value from the game manager

    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.instance.gameSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.gameActive)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    private void OnBecameInvisible() //Whenever objects go off-screen...
    {
        //Destroy(gameObject); //...they're destroyed, so that they're not hanging around in the game's memory
    }
}
