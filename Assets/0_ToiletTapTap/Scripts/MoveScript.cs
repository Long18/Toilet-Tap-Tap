using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class MoveScript : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float moveSpeed = 1.0f;

    [SerializeField] private float checkPosition;
    [SerializeField] private Vector3 defaultPosition;

    public void Update()
    {
        if (transform.localPosition.x < checkPosition)
        {
            transform.localPosition = defaultPosition;
        }

        transform.Translate(-Time.deltaTime * moveSpeed, 0, 0);
    }
}