using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public static event Action<int> sendPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sendPoints?.Invoke(1);
            Destroy(gameObject);
        }
    }
}