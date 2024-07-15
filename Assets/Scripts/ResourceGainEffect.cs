using System.Collections;
using TMPro;
using UnityEngine;

public class ResourceGainEffect : MonoBehaviour
{
    public Vector3 spawnPoint; // �ڿ��� ȹ��� ������ ��ġ (��: ĳ���� ��ġ ��)
    public float moveSpeed = 50f; // �ڿ� UI�� �����̴� �ӵ�
    public float fadeOutTime = 1f; // ������� �ð�
    public TextMeshProUGUI text;

    public void ShowResourceGain(int amount)
    {
        gameObject.SetActive(true);
        text.text = amount.ToString();

        // �ڿ� UI�� spawnPoint���� Canvas�� �����̵��� �����մϴ�.
        StartCoroutine(MoveAndFadeOut());
    }

    IEnumerator MoveAndFadeOut()
    {
        Vector3 startPos = Camera.main.WorldToScreenPoint(spawnPoint + Vector3.up * 1);
        Vector3 endPos = startPos + Vector3.up * 50f; // Canvas ������ ������ ���� ��ġ

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