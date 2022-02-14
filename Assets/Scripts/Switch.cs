using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [Header("Object References")]
    public GameObject environment1;
    public GameObject environment2;
    public GameObject surroundEnvironment;
    public SpriteRenderer player;

    // Extra Variables
    private bool isLight = false;

    void Update()
    {
        Switcher();
    }

    public void Switcher()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isLight != true)
        {
            Camera.main.backgroundColor = Color.white;
            player.color = Color.black;
            environment1.SetActive(false);
            environment2.SetActive(true);
            surroundEnvironment.GetComponentInChildren<SpriteRenderer>().color = Color.black;
            isLight = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isLight == true)
        {
            Camera.main.backgroundColor = Color.black;
            player.color = Color.white;
            environment1.SetActive(true);
            environment2.SetActive(false);
            surroundEnvironment.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            isLight = false;
        }
    }
}
