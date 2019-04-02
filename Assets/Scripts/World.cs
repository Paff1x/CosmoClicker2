using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }

    private Camera cam;

    private void Awake()
    {
        Instance = this;
        cam = Camera.main;

        float width = cam.pixelWidth;
        float height = cam.pixelHeight;

        BotBorder = cam.ScreenToWorldPoint(new Vector2(0, 0)).y;
        TopBorder = cam.ScreenToWorldPoint(new Vector2(0, height)).y;
        LeftBorder = cam.ScreenToWorldPoint(new Vector2(0, 0)).x;
        RightBorder = cam.ScreenToWorldPoint(new Vector2(width, 0)).x;
    }

    
    public float BotBorder { get; private set; }
    public float TopBorder { get; private set; }
    public float LeftBorder { get; private set; }
    public float RightBorder { get; private set; }
}
