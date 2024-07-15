using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    [SerializeField] private GameObject _previewObject;

    void Start()
    {
        _previewObject.SetActive(false);
    }

    void Update()
    {
        // 마우스 위치를 기준으로 미리보기 위치를 설정합니다.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            _previewObject.SetActive(true);
            _previewObject.transform.position = hitInfo.point;
        }
        else
        {
            _previewObject.SetActive(false);
        }
    }
}