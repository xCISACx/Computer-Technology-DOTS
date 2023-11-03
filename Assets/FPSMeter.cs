using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSMeter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _fpsText;

    [SerializeField] private float _hudRefreshRate = 1f;

    private float _timer;
    
    // Start is called before the first frame update
    void Start()
    {
        _fpsText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int) (1f / Time.unscaledDeltaTime);
            _fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}
