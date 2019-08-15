using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UIElements.CounterElements
{
    public abstract class CounterElementAbstract : MonoBehaviour
    {
        [SerializeField] private Image _itemPrefab;

        private RectTransform _container;
        private ObjectsPool<Image> _itemsPool;
        private List<Image> _shownImages;

        public void Awake()
        {
            _container = GetComponent<RectTransform>();
            _itemPrefab.gameObject.SetActive(false);
            _itemsPool = new ObjectsPool<Image>(() =>
            {
                var item = Instantiate(_itemPrefab);
                item.gameObject.SetActive(true);
                return item;
            }, _ => { });
        }

        public void SetCount(int count)
        {
            _shownImages?.ForEach(image => _itemsPool.Release(image));

            var spritesToShow = SelectSprites(count);
            _shownImages = spritesToShow.Select(sprite =>
            {
                var item = _itemsPool.Borrow();
                item.sprite = sprite;
                item.transform.SetParent(_container);
                return item;
            }).ToList();
        }

        protected abstract IEnumerable<Sprite> SelectSprites(int count);
    }
}