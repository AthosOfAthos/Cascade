using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Cascade;

abstract class Actor {
	public Vector2 Location { get; set; }

	private Dictionary<System.Type, List<Component>> components;
	
	public Actor() {
		Location = Vector2.Zero;
		components = new Dictionary<System.Type, List<Component>>();
	}
	
	public Actor(Vector2 location) {
		Location = location;
		components = new Dictionary<System.Type, List<Component>>();
	}

	public void AddComponent<T>(T component) where T: Component {
		System.Type type = typeof(T);
		if (components.ContainsKey(type)) {
			components[type].Add(component as Component);
		} else {
			List<Component> array = new List<Component>();
			array.Add(component as Component);
			components.Add(type, array);
		}
	}

	public bool HasComponentOfType<T>() where T: Component {
		return components.ContainsKey(typeof(T));
	}

	public T[]? GetComponents<T>() where T: Component {
		System.Type type = typeof(T);
		if (components.ContainsKey(type)) {
			return System.Array.ConvertAll<Component, T>(components[type].ToArray(), item => (T)item);
		} else {
			return null;
		}
	}

	public bool RemoveComponent<T>(T component) where T: Component {
		System.Type type = typeof(T);
		bool didRemoveComponent = false;
		if (components.ContainsKey(type)) {
			List<Component> componentList = components[type];
			didRemoveComponent = componentList.Remove((Component)component);
			if (componentList.Count == 0) {
				components.Remove(type);
			}
		}
		return didRemoveComponent;
	}
}