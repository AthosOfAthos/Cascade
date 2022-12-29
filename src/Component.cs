using Microsoft.Xna.Framework;

namespace Cascade;

abstract class Component {
	public Vector2 RelativeLocation { get; set; }

	public Component() {
		RelativeLocation = Vector2.Zero;
	}

	public Component(Vector2 location) {
		RelativeLocation = location;
	}
}
