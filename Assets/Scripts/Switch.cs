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

    [SerializeField] private Sprite regularSprite;
    [SerializeField] private Sprite invertedSprite;

    // Extra Variables
    public static bool IsInverted = false;

    void Update()
    {
        Switcher();
    }

    public void Switcher()
    {
        if (Input.GetKeyDown(KeyCode.Q) && IsInverted == false)
        {
            Camera.main.backgroundColor = Color.white;
            player.sprite = invertedSprite;
            environment1.SetActive(false);
            environment2.SetActive(true);
            surroundEnvironment.GetComponentInChildren<SpriteRenderer>().color = Color.black;
            IsInverted = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && IsInverted == true)
        {
            Camera.main.backgroundColor = Color.black;
            player.sprite = regularSprite;
            environment1.SetActive(true);
            environment2.SetActive(false);
            surroundEnvironment.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            IsInverted = false;
        }
    }
}
