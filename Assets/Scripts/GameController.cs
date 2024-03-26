using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private LevelCreator levelCreator;

    private int levelNumber = 1;
    private string correctAnswer;

    private Transform parent;

    private void Start()
    {
        levelCreator = GetComponent<LevelCreator>();
    }

    void Update()
    {
        correctAnswer = levelCreator.CorrectAnswer;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                parent = hit.transform.parent;
                if(parent.name == correctAnswer)
                {
                    Correct();
                }
                else
                {
                    UnCorrect();
                }
            }
        }
    }

    private void Correct()
    {
        switch(levelNumber++)
        {
            case 1:
                levelCreator.CreateVariant(6);
                correctAnswer = levelCreator.CorrectAnswer;
                break;
            case 2:
                levelCreator.CreateVariant(9);
                correctAnswer = levelCreator.CorrectAnswer;
                break;
            case 3:
                levelCreator.FinishGame();
                break;
        }
    }
    private void UnCorrect()
    {
        parent.DOShakePosition(0.2f, 0.1f, 2);
    }
}
