using System.Collections.Generic;
using UnityEngine;
namespace ShooterProject.Scripts.General
{
	public class GameObjectsPool
	{
		#region Fields

		private List<GameObject> _objects = new List<GameObject>();

		private bool _autoExpand;
		private bool _activeByDefault;
		private Transform _container;
		private GameObject _objectPrefab;

		#endregion

		#region Properties

		public int MaxSize { get; }

		#endregion

		#region Constructors

		/// <summary>
		/// Конструктор класса
		/// </summary>
		/// <param name="maxSize">Максимальный размер пула</param>
		/// <param name="autoExpand">Использовать ли авторасширение</param>
		/// <param name="activeByDefault">Параметр active объектов при инициализации или добавлении в пул</param>
		/// <param name="prefab">Префаб объекта</param>
		/// <param name="container">Контейнер пула</param>
		public GameObjectsPool(int maxSize, bool autoExpand, bool activeByDefault, GameObject prefab, Transform container)
		{
			MaxSize = maxSize;
			_autoExpand = autoExpand;
			_activeByDefault = activeByDefault;
			_objectPrefab = prefab;
			_container = container;

			InitPool();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Проверяет наличие свободного элемента и возвращает его
		/// </summary>
		/// <param name="element">Свободный элемент пула</param>
		/// <param name="allowLongTimeNotUsed">Если нет свободного элемента и нельзя создать новый, то возвращает давно не использовавшийся элемент</param>
		/// <returns>Возвращает true если удалось найти свободный элемент, иначе возвращает false</returns>
		public bool TryGetFreeElement(out GameObject element, bool allowLongTimeNotUsed)
		{
			foreach (var poolObject in _objects)
			{
				if (!poolObject.activeSelf)
				{
					element = poolObject;
					ActivateElement
					(element);
					return true;
				}
			}

			if (_autoExpand)
			{
				element = AddObject();
				element.SetActive(true);
				return true;
			}

			if (allowLongTimeNotUsed)
			{
				element = _objects[0];
				ActivateElement
				(element);
				return true;
			}

			element = null;
			return false;

		}

		#endregion

		#region Private Methods

		private void InitPool()
		{
			for (var i = 0; i < MaxSize; i++)
			{
				AddObject();
			}
		}

		private GameObject AddObject()
		{
			var newObject = GameObject.Instantiate(_objectPrefab, _container);
			newObject.gameObject.SetActive(_activeByDefault);
			_objects.Add(newObject);
			return newObject;
		}

		private void ActivateElement
		(GameObject element)
		{
			element.SetActive(true);
			_objects.Remove(element);
			_objects.Add(element);
		}

		#endregion
	}
}
