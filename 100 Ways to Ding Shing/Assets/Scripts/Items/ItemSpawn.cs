using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private Item[] items;
    [SerializeField] Transform prefab;

    private int spawnableItems;

    void Start()
    {
        spawnableItems = transform.childCount;
        SpawnItems();
    }

    private void SpawnItems()
    {
        for (int i = 0; i < spawnableItems; i++)
        {
            int itemIndex = Random.Range(0, items.Length);
            Transform item = Instantiate(prefab, transform.GetChild(i).position, Quaternion.identity);
            item.GetComponent<SpriteRenderer>().sprite = items[itemIndex].sprite;

            //ItemCharacteristics itemChar = item.GetComponent<ItemCharacteristics>();
            //itemChar.itemCharacteristics.name = items[itemIndex].name;
            //itemChar.itemCharacteristics.consumable = items[itemIndex].consumable;
        }
    }
}