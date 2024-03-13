using System;
using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour
    {
        
        /// <summary>
        /// The Item's ID.
        /// </summary>
        public int id;
        
        /// <summary>
        /// The Item's ItemType.
        /// </summary>
        private ItemController.ItemType itemType;
        /// <summary>
        /// The Item's name.
        /// </summary>
        private string name;
        
        /// <summary>
        /// The ItemController.
        /// </summary>
        private ItemController controller;
        
        /// <summary>
        /// Stores methods and values related to Items.
        /// </summary>
        /// <param name="id">The Item's ID.</param>
        public void Init(int id, ItemController.ItemType itemType, string name)
        {
            this.id = id;
            this.itemType = itemType;
            this.name = name;
            this.controller = ItemController.GlobalController;
        }

        public ItemController.ItemType GetItemType()
        {
            return itemType;
        }
    }
}