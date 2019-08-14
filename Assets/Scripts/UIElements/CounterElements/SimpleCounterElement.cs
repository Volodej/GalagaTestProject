using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIElements.CounterElements
{
    public class SimpleCounterElement : CounterElementAbstract
    {
        [SerializeField] private Sprite _sprite;
        
        protected override IEnumerable<Sprite> SelectSprites(int count)
        {
            return Enumerable.Repeat(_sprite, count);
        }
    }
}