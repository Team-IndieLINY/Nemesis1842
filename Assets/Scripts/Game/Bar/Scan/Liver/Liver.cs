using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Liver : MonoBehaviour
{
    [SerializeField]
    private GameObject _squareLeavenPrefab;

    [SerializeField]
    private GameObject _circleLeavePrefab;
    
    [SerializeField]
    private GameObject _starLeavePrefab;

    [SerializeField]
    private GameObject _triangleLeavenPrefab;

    private PolygonCollider2D _polygonCollider2D;
    private List<GameObject> _leavenGOs = new();

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    public void ResetLiver()
    {
        for (int i = 0; i < _leavenGOs.Count; i++)
        {
            Destroy(_leavenGOs[i]);
        }
        
        _leavenGOs.Clear();
        gameObject.SetActive(true);
    }
    
    public void SetLiver(int squareLeavenCount, int circleLeavenCount, int starLeavenCount, int triangleLeavenCount)
    {
        SpawnLeavens(squareLeavenCount, _squareLeavenPrefab);
        SpawnLeavens(circleLeavenCount, _circleLeavePrefab);
        SpawnLeavens(starLeavenCount, _starLeavePrefab);
        SpawnLeavens(triangleLeavenCount, _triangleLeavenPrefab);
        
        gameObject.SetActive(false);
    }

    private void SpawnLeavens(int leavenCount, GameObject leavenPrefab)
    {
        for (int i = 0; i < leavenCount; i++)
        {
            Vector3 rndPoint3D = RandomPointInBounds(_polygonCollider2D.bounds, 1f);
            Vector2 rndPoint2D = new Vector2(rndPoint3D.x, rndPoint3D.y);
            Vector3 rndPointInside = _polygonCollider2D.ClosestPoint(new Vector2(rndPoint2D.x, rndPoint2D.y));
            
            _leavenGOs.Add(Instantiate(leavenPrefab, rndPointInside, Quaternion.identity, transform));
        }
    }
    
    private Vector3 RandomPointInBounds(Bounds bounds, float scale)
    {
        return new Vector3(
            Random.Range(bounds.min.x * scale, bounds.max.x * scale),
            Random.Range(bounds.min.y * scale, bounds.max.y * scale),
            Random.Range(bounds.min.z * scale, bounds.max.z * scale)
        );
    }


}
