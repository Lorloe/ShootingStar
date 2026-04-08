using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private Vector2 scrollSpeed;

    private Vector2 offset;
    private Material backgroundMaterial;

    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        backgroundMaterial.mainTextureOffset = offset; 
    }
}
