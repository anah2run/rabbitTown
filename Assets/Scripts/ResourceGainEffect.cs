using System.Collections;
using TMPro;
using UnityEngine;

public class ResourceGainEffect : MonoBehaviour
{
    public Vector3 spawnPoint; // 자원이 획득된 지점의 위치 (예: 캐릭터 위치 등)
    public float moveSpeed = 50f; // 자원 UI가 움직이는 속도
    public float fadeOutTime = 1f; // 사라지는 시간
    public TextMeshProUGUI text;

    public void ShowResourceGain(int amount)
    {
        gameObject.SetActive(true);
        text.text = amount.ToString();

        // 자원 UI가 spawnPoint에서 Canvas로 움직이도록 설정합니다.
        StartCoroutine(MoveAndFadeOut());
    }

    IEnumerator MoveAndFadeOut()
    {
        Vector3 startPos = Camera.main.WorldToScreenPoint(spawnPoint + Vector3.up * 1);
        Vector3 endPos = startPos + Vector3.up * 50f; // Canvas 내에서 움직일 최종 위치

        float t = 0;
        while (t < fadeOutTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t / fadeOutTime);
            yield return null;
            t += Time.deltaTime;
        }
        gameObject.SetActive(false);
    }
}