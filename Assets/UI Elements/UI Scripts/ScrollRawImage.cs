using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRawImage : MonoBehaviour
{
    public float horizontalSpeed;

    public float verticalSpeed;

    private RawImage _myRawImage;
    // Start is called before the first frame update
    void Start()
    {
        _myRawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Rect currentUV = _myRawImage.uvRect;
        currentUV.x -= Time.deltaTime*horizontalSpeed;
        currentUV.y -= Time.deltaTime * verticalSpeed;
        if (currentUV.x <= -1f ||currentUV.x >=1f)
        {
            currentUV.x = 0f;
        } if (currentUV.y <= -1f ||currentUV.y >=1f)
        {
            currentUV.y = 0f;
        }

        _myRawImage.uvRect = currentUV;
    }
}
