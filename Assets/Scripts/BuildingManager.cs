using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject buildingPrefab; // �Ǽ��� �ǹ��� ������

    public bool CreateBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            // �̸����� ������Ʈ�� Ȱ��ȭ�� ���¿��� Ŭ���Ǿ��� ��
            // if (hitInfo.collider.CompareTag("Ground")) // Ground �±׸� ����Ͽ� ������ Ȯ��
            {
                // �Ǽ��� ��ġ�� ���� �ǹ��� �����մϴ�.
                Instantiate(buildingPrefab, hitInfo.point, Quaternion.identity);
                return true;
            }
        }
        return false;
    }
}