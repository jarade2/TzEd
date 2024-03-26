using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private float space;

    private GameObject[] cells;

    public GameObject[] SpawnCells(int cellNumber)
    {
        cells = new GameObject[cellNumber];

        int cellsPerRow = 3; 
        float startX = -space * (cellsPerRow - 1) / 2; 
        float startY = 0; 

        for (int i = 0; i < cellNumber; i++)
        {
            GameObject cell = Instantiate(cellPrefab, transform);

           
            int row = i / cellsPerRow; 
            int col = i % cellsPerRow; 

            cell.transform.localPosition = new Vector3(startX + col * space, startY - row * space, 0);

            cell.transform.localScale = Vector3.zero; 
            cell.transform.DOScale(1, 3).SetEase(Ease.OutBounce); 

            cells[i] = cell;
        }

        return cells;
    }


}
