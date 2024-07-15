using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI _carrotCountText;
    private void Awake()
    {
        Instance = this;
    }

    public void SetCarrotCountText(int count)
    {
        _carrotCountText.text = count.ToString();
    }
}
