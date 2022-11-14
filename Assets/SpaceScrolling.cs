using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceScrolling : MonoBehaviour
{
    public enum ScrollType
    {
        Up,
        Angle,
        Side,
        None
    };

    public ScrollType currentScroll = ScrollType.Side;
    public float scrollSpeed = 0.02f;

    // Update is called once per frame
    void Update()
    {
        MoveTexture();
    }

    void MoveTexture()
    {
        RawImage _img = GetComponent<RawImage>();
        switch (currentScroll)
        {
            case ScrollType.Up:
                _img.uvRect = new Rect(_img.uvRect.position + new Vector2(0f, 20 * scrollSpeed) * Time.deltaTime, _img.uvRect.size);
                break;
            case ScrollType.Angle:
                _img.uvRect = new Rect(_img.uvRect.position + new Vector2(8 * scrollSpeed, 8 * scrollSpeed) * Time.deltaTime, _img.uvRect.size);
                break;
            case ScrollType.Side:
                _img.uvRect = new Rect(_img.uvRect.position + new Vector2(scrollSpeed, 0f) * Time.deltaTime, _img.uvRect.size);
                break;
        }
    }
}
