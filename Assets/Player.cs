using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    int coins;

    [SerializeField] TextMeshProUGUI tmpro;

    [SerializeField] AudioClip coin;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        tmpro.text = "Coins: " + coins;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coins += 1;
            audioSource.PlayOneShot(coin);
            Destroy(collision.gameObject);
        }
    }
}
