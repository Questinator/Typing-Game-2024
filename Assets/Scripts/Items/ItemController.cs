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
        public static ItemType GetItemTypeFromID(int id)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Creates a new possible Item.
        /// </summary>
        /// <param name="item">The new possible Item.</param>
        public void CreateItem(int id, string name)
        {
            for (int i = 0; i < possibleItems.Count; i++)
            {
                if (possibleItems[i].id == id || possibleItems[i].name == name) throw new ArgumentException();
            }
            possibleItems.Add(new PossibleItem(id, name));
        }

        private struct PossibleItem
        {
            public readonly int id;
            public readonly string name;

            public PossibleItem(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }
    }
}