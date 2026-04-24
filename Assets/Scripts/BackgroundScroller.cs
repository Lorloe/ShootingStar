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

        offset.x = Mathf.Repeat(offset.x, 1f);
        offset.y = Mathf.Repeat(offset.y, 1f);

        backgroundMaterial.mainTextureOffset = offset; 
    }
}
