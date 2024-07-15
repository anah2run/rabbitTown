using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject buildingPrefab; // 건설할 건물의 프리팹

    public bool CreateBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            // 미리보기 오브젝트가 활성화된 상태에서 클릭되었을 때
            // if (hitInfo.collider.CompareTag("Ground")) // Ground 태그를 사용하여 땅인지 확인
            {
                // 건설할 위치에 실제 건물을 생성합니다.
                Instantiate(buildingPrefab, hitInfo.point, Quaternion.identity);
                return true;
            }
        }
        return false;
    }
}