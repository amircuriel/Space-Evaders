using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionManager : MonoBehaviour
{
    public static bool isGameRunning = true;
    public TMP_Text fps;
    public RectTransform ScrollingBackground;
    public float backgroundScrollSpeed = 5f;


    // Update is called once per frame
    void Update()
    {
        ScrollingBackground.offsetMin = new Vector2(ScrollingBackground.offsetMin.x, ScrollingBackground.offsetMin.y - backgroundScrollSpeed);
    }

    private float count;

    private IEnumerator Start()
    {
        Application.targetFrameRate = 60;
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnGUI()
    {
        fps.text = Mathf.Round(count).ToString();
    }
}
