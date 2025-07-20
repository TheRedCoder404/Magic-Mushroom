using System;
using UnityEngine;

public class Shroom : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] GameObject mainObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerStats>(out var playerStats))
        {
            Debug.Log("Shroom collected: " + value);
            playerStats.EarnShroom(value);
            Destroy(mainObject);
        }
    }
}
