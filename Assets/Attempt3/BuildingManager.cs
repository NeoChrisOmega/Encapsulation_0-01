using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] Building[] buildings;
    [SerializeField] Sprite[] resources;
    [SerializeField] List<ProductionPair> inventory;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory.Clear();
        buildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Building building in buildings)
        {
            building.isActive = building.production.sprite != resources[0];

        }
    }

    public void AddToInventory(Sprite production, int amount)
    {
        foreach(ProductionPair pair in inventory)
        {
            if(pair.production == production)
            {
                pair.amount += amount;
                return;
            }
        }

        ProductionPair newPair = new ProductionPair(production, amount);
        inventory.Add(newPair);
    }
}

[Serializable]
public class ProductionPair
{
    public string key;
    public Sprite production;
    public int amount;

    public ProductionPair(Sprite production, int amount)
    {
        this.production = production;
        key = production.name;
        this.amount = amount;
    }
}