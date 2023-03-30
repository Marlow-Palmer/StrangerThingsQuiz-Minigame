using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;

    public GameObject Quizpanel;
    public GameObject GOPanel;
    public GameObject StartPanel;

    public Text QuestionTxt;
    public Text ScoreTxt;

    public int totalQuestions = 0;
    public int score;


    private void Start()
    {
        Quizpanel.SetActive(false);
        GOPanel.SetActive(false);
        StartPanel.SetActive(true);
        
    }

    public void goButton()
    {
        Game();
    }

    public void exitButton()
    {
        //put the debug log so that i know the quit button is working
        //because it wont just quit the game while running in unity
        Debug.Log("Quit");
        Application.Quit();
    }

    private void Game()
    {
        Quizpanel.SetActive(true);
        totalQuestions = QnA.Count;
        GOPanel.SetActive(false);
        StartPanel.SetActive(false);
        generateQuestion();
    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameOver()
    {
        Quizpanel.SetActive(false);
        GOPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions;
    }

    public void correct()
    {
        score += 1;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    void generateQuestion()
    {
        if(QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else 
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}
