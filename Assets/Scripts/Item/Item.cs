using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int id;
    // private int value;
    // private int damage;

    public Item(int id)
    {
        this.id = id;
    }

    public static Item GetItemWithID(int id)
    {
        throw new NotImplementedException();
    }
}
