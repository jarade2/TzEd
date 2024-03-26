using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    [SerializeField]private Button button;
    [SerializeField] private Image fadeImage;

    private Spawner spawner;

    private HashSet<string> correctAnswers = new HashSet<string>();
    private string correctAnswer;
    public string CorrectAnswer { get { return correctAnswer; } private set { correctAnswer = value; } }

    private GameObject[] cells;


    private void Awake()
    {
        spawner = GetComponent<Spawner>();
        CreateVariant();

        button.onClick.AddListener(RestartGame);
        fadeImage.gameObject.SetActive(false);
    }

    public void CreateVariant(int variatnCount = 3)
    {
        List<string> numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        List<string> letters = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I",
            "J", "K", "L", "M", "N", "O", "P", "Q", "R",
            "S", "T", "U", "V", "W", "X", "Y", "Z"
        };

        List<string> dataSet = (Random.value > 0.5f) ? new List<string>(numbers) : new List<string>(letters);

        if(cells != null ) ClearCells();


        cells = spawner.SpawnCells(variatnCount);

        List<string> variants = new List<string>();

        foreach (GameObject cell in cells)
        {
            int index = Random.Range(0, dataSet.Count);
            string value = dataSet[index];
            dataSet.RemoveAt(index);
            cell.GetComponentInChildren<TMP_Text>().text = value;
            cell.name = value;

            variants.Add(value);  
        }

        do
        {
            correctAnswer = variants[Random.Range(0, variants.Count)];
        }
        while (correctAnswers.Contains(correctAnswer));

        correctAnswers.Add(correctAnswer);

        RenderText();
    }

    private void ClearCells()
    {
        foreach (GameObject g in cells) Destroy(g);
    }

    private void RenderText(bool render = true)
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        if (render)
        {

            text.text = $"Find {correctAnswer}";
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 1), 3);
        }
        else text.text = "";
    }

    public void FinishGame()
    {
       
        button.gameObject.SetActive(true);
        RenderText(false);
        

    }

    
    private void RestartGame()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.DOFade(0.8f, 1).OnComplete(() =>
        {
            // Перезагрузка сцены после затемнения
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

}
