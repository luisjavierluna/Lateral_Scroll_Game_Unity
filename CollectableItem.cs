using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    coins,
    healthPotion,
    manaPotion
}

public class CollectableItem : MonoBehaviour
{
    [SerializeField] CollectableType type;

    public int value = 1;
    [SerializeField] bool hasBeenCollected = false;

    CircleCollider2D itemCollider;
    SpriteRenderer sprite;

    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        
        itemCollider = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void HideItem()
    {
        itemCollider.enabled = false;
        sprite.enabled = false;
    }

    void Collected()
    {
        HideItem();
        hasBeenCollected = true;

        switch (type)
        {
            case CollectableType.coins:
                GameManager.instance.CollectCoin(this);
                break;
            case CollectableType.healthPotion:
                player.GetComponent<PlayerController>().HealthControl(value);
                break;
            case CollectableType.manaPotion:
                player.GetComponent<PlayerController>().ManaControl(value);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!hasBeenCollected)
            {
                Collected();
            }
        }
    }
}
