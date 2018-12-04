using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using Game;

namespace Game
{
    [Game]
    public class ViewComponent : IComponent
    {
        
        [PrimaryMember]
        public string path;
        public GameObject gameObject;
        public Dictionary<string, Transform> cacheBone = new Dictionary<string, Transform>();

        public Projector projector;
        public List<Renderer> renderers = new List<Renderer>();
        public MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        public bool visible;
        public float hitAlpha;

        public void Reset()
        {
            path = string.Empty;
            gameObject = null;
            cacheBone.Clear();

            projector = null;
            renderers.Clear();
            propBlock.Clear();
            visible = true;
        }

        public Vector3 position
        {
            get
            {
                if (gameObject == null)
                    return Vector3.zero;
                return gameObject.transform.position;
            }
            set
            {
                if (gameObject == null)
                    return;
                gameObject.transform.position = value;
            }
        }

        public void SetHitAlpha(float alpha)
        {
            hitAlpha = Mathf.Max(alpha, 0);
            SetViewProperty("_AbsAlpha", hitAlpha);
        }

        public void UpdateHitAlpha()
        {
            if (hitAlpha > 0)
            {
                hitAlpha = Mathf.Max(hitAlpha - Time.deltaTime * 5f, 0);
                SetViewProperty("_AbsAlpha", hitAlpha);
            }
        }

        public void SetViewProperty(string propName, float propValue)
        {
            propBlock.SetFloat(propName, propValue);
            ApplyViewProperty();
        }

        public void SetViewProperty(string propName, Color propValue)
        {
            propBlock.SetColor(propName, propValue);
            ApplyViewProperty();
        }

        private void ApplyViewProperty()
        {
            foreach (Renderer renderer in renderers)
            {
                if (renderer != null)
                    renderer.SetPropertyBlock(propBlock);
            }
        }
    }
}