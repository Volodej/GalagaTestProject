using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIElements.CounterElements
{
    public class LevelCounterElement : CounterElementAbstract
    {
        [SerializeField] private Sprite _level1;
        [SerializeField] private Sprite _level5;
        
        protected override IEnumerable<Sprite> SelectSprites(int count)
        {
            var level5count = count / 5;
            var level1count = count % 5;
            return Enumerable.Repeat(_level5, level5count)
                .Concat(Enumerable.Repeat(_level1, level1count));
        }
    }
}