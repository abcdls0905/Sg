using Entitas;
using UnityEngine;
namespace Game
{
    [Game]
    public class TransformComponent : IComponent
    {
        public Vector3 position;
        public Vector3 lastPosition;

        public Quaternion rotation;
        public Quaternion lastRotation;

        public Vector3 serverPos;

        public Vector2 position2 { get { return new Vector2(position.x, position.z); } }
        public void Reset()
        {
            position = new Vector3(-1000, -1000, -1000);
            lastPosition = position;
            rotation = Quaternion.Euler(Vector3.forward);
            lastRotation = Quaternion.Euler(Vector3.forward);
        }
    }
}