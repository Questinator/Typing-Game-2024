using System;
using System.Collections.Generic;
using UnityEngine;
namespace Items
{
    public class ItemController
    {
        /// <summary>
        /// All possible types of Items.
        /// </summary>
        public enum ItemType
        {
            Basic,
            Quest,
            Weapon,
            Material,
            Currency
        }

        internal static ItemController controllerReference;
        public static ItemController GlobalItemController
        {
            get
            {
                if (controllerReference==null)
                {
                    controllerReference = new ItemController();
                }

                return controllerReference;
            }
        }
        
        /// <summary>
        /// A List of Items that actually exist in the world.
        /// </summary>
        public List<GameObject> existingItems;
        /// <summary>
        /// A List of all possible Items.
        /// </summary>
        private List<PossibleItem> possibleItems;
        
        /// <summary>
        /// Gets the <see cref="ItemType"/> of the Item with the given ID.
        /// </summary>
        /// <param name="id">The ID of the item to get the <see cref="ItemType"/> of.</param>
        /// <returns>The <see cref="ItemType"/> of the Item with the given ID.</returns>
        /// <exception cref="ArgumentException">There is no item with the given ID.</exception>
        public ItemType GetItemTypeFromID(int id)
        {
            for (int i = 0; i < possibleItems.Count; i++)
            {
                if (possibleItems[i].id == id) return possibleItems[i].type;
            }

            throw new ArgumentException("There is no item with id " + id);
        }
        
        /// <summary>
        /// Gets the name of the Item with the given ID.
        /// </summary>
        /// <param name="id">The ID of the item to get the name of.</param>
        /// <returns>The name of the item with the given ID.</returns>
        /// <exception cref="ArgumentException">There is no item with the given ID.</exception>
        public string GetNameFromID(int id)
        {
            for (int i = 0; i < possibleItems.Count; i++)
            {
                if (possibleItems[i].id == id) return possibleItems[i].name;
            }

            throw new ArgumentException("There is no item with id " + id);
        }
        
        /// <summary>
        /// Creates a new possible Item.
        /// </summary>
        /// <param name="id">The ID of the new Possible Item.</param>
        /// <param name="name">The name of the new Possible Item.</param>
        /// <param name="type">The <see cref="ItemType"/> of the new Possible Item.</param>
        public void CreateItem(int id, string name, ItemType type)
        {
            for (int i = 0; i < possibleItems.Count; i++)
            {
                if (possibleItems[i].id == id || possibleItems[i].name == name) throw new ArgumentException();
            }
            possibleItems.Add(new PossibleItem(id, name, type));
        }
        
        /// <summary>
        /// Instantiates an Item into the world.
        /// </summary>
        /// <param name="id">The ID of the item to add.</param>
        public void InstantiateItem(int id)
        {
            string name = GetNameFromID(id);
            ItemType type = GetItemTypeFromID(id);

            GameObject item = new GameObject();
            item.AddComponent<Item>().Init(id, type, name);
            existingItems.Add(item);
        }
        
        /// <summary>
        /// Removes an Item from the world.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <exception cref="ArgumentException">Cannot find the item in the list of existing items.</exception>
        public void DeleteItem(GameObject item)
        {
            for (int i = 0; i < existingItems.Count; i++)
            {
                if (existingItems[i] == item)
                {
                    GameObject.Destroy(item);
                    existingItems.Remove(item);
                    return;
                }
            }

            throw new ArgumentException("Cannot find item in list of existing items.");
        }
        
        /// <summary>
        /// A Possible Item.
        /// </summary>
        private struct PossibleItem
        {
            public readonly int id;
            public readonly string name;
            public readonly ItemType type;

            public PossibleItem(int id, string name, ItemType type)
            {
                this.id = id;
                this.name = name;
                this.type = type;
            }
        }
    }
}