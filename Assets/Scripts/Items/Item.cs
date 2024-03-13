using System;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        
        /// <summary>
        /// The Item's ID.
        /// </summary>
        public readonly int id;
        
        /// <summary>
        /// The Item's <see cref="ItemType"/>.
        /// </summary>
        private ItemController.ItemType itemType;
        /// <summary>
        /// The Item's name.
        /// </summary>
        private readonly string name;
        
        /// <summary>
        /// Stores methods and values related to Items.
        /// </summary>
        /// <param name="id">The Item's ID.</param>
        public Item(int id)
        {
            this.id = id;
            
            // Set ItemType based on ID
            
            // Set name based on ID
        }

        public ItemController.ItemType GetItemType()
        {
            return itemType;
        }
    }
}