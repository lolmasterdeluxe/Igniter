using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IG
{
    public class Item : ScriptableObject
    {
        [Header("Item Information")]
        public Sprite itemIcon;
        public string itemName;
    }
}
