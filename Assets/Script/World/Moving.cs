using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class Moving : MonoBehaviour
{
    [SerializeField] private List<GameObject> movingPlatforms = new List<GameObject>();
    private List<Transform> movingPlatformsPos = new List<Transform>();
    private void Awake()
    {
        foreach (Transform child in transform.GetComponentInChildren<Transform>())
        {
            if (child.gameObject.CompareTag("Moveable"))
            {
                movingPlatforms.Add(child.gameObject);
                movingPlatformsPos.Add(child.transform);
            }
        }
        InvokeRepeating("MoveObject", 1, 12);
    }
    private void MoveObject()
    {
        for (int i = 0; i < movingPlatforms.Count; i++)
        {
            Transform firstLocation = movingPlatformsPos[i];
            Vector3 newPos = firstLocation.position + (firstLocation.transform.up * (Random.Range(-2,2)));
            LeanTween.move(movingPlatforms[i], newPos, 10f).setEase(LeanTweenType.easeInOutBack);
        }
    }
}
