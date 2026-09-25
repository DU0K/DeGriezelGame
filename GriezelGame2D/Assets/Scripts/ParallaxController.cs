using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxFactor;
    [SerializeField] private float PixelsPerUnit;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxFactor);
        float distance = cam.transform.position.x * parallaxFactor;

        Vector3 newPosition = new Vector3(startpos + distance, GameObject.FindGameObjectWithTag("Player").transform.position.y, transform.position.z);

        transform.position = PixelPerfectClamp(newPosition, PixelsPerUnit);

        if (temp > startpos + (length / 2)) startpos += length;
        else if (temp < startpos - (length / 2)) startpos -= length;
    }

    private Vector3 PixelPerfectClamp(Vector3 locationVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(Mathf.CeilToInt(locationVector.x * pixelsPerUnit), Mathf.CeilToInt(locationVector.y * pixelsPerUnit), Mathf.CeilToInt(locationVector.z * pixelsPerUnit));
        return vectorInPixels / pixelsPerUnit;
    }
}