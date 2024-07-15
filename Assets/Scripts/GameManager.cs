using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int _carrotCount = 0;
    public int _carrotFarmPrice = 10;
    private BuildingManager _buildingManager;

    private void Awake()
    {
        _buildingManager = GetComponent<BuildingManager>();
        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        _carrotCount = _carrotFarmPrice;
        UpdateCarrotCountText();
    }

    void Update()
    {
        if (_carrotCount >= _carrotFarmPrice && Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 시
        {
            if (_buildingManager.CreateBuilding())
            {
                _carrotCount -= _carrotFarmPrice;
                UpdateCarrotCountText();
            }
        }
    }

    private void UpdateCarrotCountText()
    {
        UIManager.Instance.SetCarrotCountText(_carrotCount);
    }

    public void AddCarrot(int count)
    {
        _carrotCount += count;
        UpdateCarrotCountText();
    }
}
