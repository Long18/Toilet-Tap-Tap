using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScaleUI : MonoBehaviour
{
    [SerializeField] private float maxScale = 1;
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float duration = 0.5f;

    private IEnumerator Scaling()
    {
        transform.localScale = new Vector3(minScale, minScale, minScale);
        yield return true;
        transform.DOScale(new Vector3(maxScale, maxScale, maxScale), duration).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        StartCoroutine(Scaling());
    }

    private void OnDisable()
    {
        DOTween.Kill(transform);
        StopCoroutine(Scaling());
    }
}