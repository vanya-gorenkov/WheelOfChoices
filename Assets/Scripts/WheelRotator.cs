using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WheelRotator : MonoBehaviour
{
    [SerializeField] private int minRotationCycles;
    [SerializeField] private int maxRotationCycles;
    [SerializeField] private float minRotationTime;
    [SerializeField] private float maxRotationTime;
    [SerializeField] private float angleDispersion;

    [SerializeField] private List<Collider2D> sectorColliders;
    
    [SerializeField] private Button button;

    private int _targetSector;

    private void Start()
    {
        button.onClick.AddListener(delegate
        {
            TriggerRotation(Random.Range(0, sectorColliders.Count - 1),
                Random.Range(minRotationCycles, maxRotationCycles + 1),
                Random.Range(minRotationTime, maxRotationTime));
        });
    }

    private void Update()
    {
        _targetSector = 0;
        
        if (Input.GetMouseButtonDown(1))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);
            if (hit)
            {
                foreach (var sectorCollider in sectorColliders)
                {
                    if (sectorCollider == hit.collider)
                    {
                        _targetSector = sectorColliders.IndexOf(sectorCollider) + 1;
                        Debug.Log("Rotation to sector " + _targetSector + " - triggered by mouse");
                        TriggerRotation(_targetSector, Random.Range(minRotationCycles, maxRotationCycles + 1),
                            Random.Range(minRotationTime, maxRotationTime));
                        break;
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _targetSector = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _targetSector = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _targetSector = 3;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                _targetSector = 4;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                _targetSector = 5;
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                _targetSector = 6;
            }
        }
        
        if (_targetSector > 0)
        {
            Debug.Log("Rotation to sector " + _targetSector + " - triggered by keypad");
            TriggerRotation(_targetSector, Random.Range(minRotationCycles, maxRotationCycles + 1),
                Random.Range(minRotationTime, maxRotationTime));
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void TriggerRotation(int targetSector, int rotationCycles, float rotationTime)
    {
        Debug.Log("Rotation to sector " + targetSector + " - animation started");
        transform.DOKill();
        transform.DOLocalRotate(
            new Vector3(0, 0,
                360 * rotationCycles + 60 * (targetSector - 1) + Random.Range(-angleDispersion, angleDispersion)),
            rotationTime, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
    }
}