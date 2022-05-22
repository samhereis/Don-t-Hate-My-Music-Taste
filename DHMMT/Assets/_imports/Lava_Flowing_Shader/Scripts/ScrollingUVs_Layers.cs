using UnityEngine;

namespace UI
{
    public class ScrollingUVs_Layers : MonoBehaviour
    {
        [SerializeField] private string _textureName = "_MainTex";
        [SerializeField] private Vector2 _uvAnimationRate = new Vector2(1.0f, 0.0f);
        [SerializeField] private Vector2 _uvOffset = Vector2.zero;

        [Header("Components")]
        [SerializeField] private Renderer _renderer;

        private void Awake()
        {
            if (_renderer == null) _renderer = GetComponent<Renderer>();
        }

        void LateUpdate()
        {
            _uvOffset += (_uvAnimationRate * Time.deltaTime);

            if (_renderer.enabled) _renderer.sharedMaterial.SetTextureOffset(_textureName, _uvOffset);
        }
    }
}