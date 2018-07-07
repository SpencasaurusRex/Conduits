using UnityEngine;

public class GridSprite : MonoBehaviour
{
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite s)
    {
        sr.sprite = s;
    }
}
