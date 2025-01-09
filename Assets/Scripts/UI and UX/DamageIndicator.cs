using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    public float textSpeed = 1;
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
    public void SetDamageText(string damage)
    {
        text.text = damage;
    }
    public void FaceOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }
    private IEnumerator FadeOut(float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - timer / duration);
            text.transform.position += Vector3.up * Time.deltaTime*textSpeed;
            yield return null;
        }
        Destroy(gameObject);
    }
}
