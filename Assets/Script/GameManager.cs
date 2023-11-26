using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int playerScore;
    public int PlayerScore { get; set; }
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject[] ballPosition;

    [SerializeField] private GameObject cueBall;
    [SerializeField] private GameObject ballLine;

    [SerializeField] private float XInput;
    [SerializeField] private float force;

    [SerializeField] private GameObject camera;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text developertext;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        camera = Camera.main.gameObject;
        CameraBehindBall();
        
        UpdateScoreText();


        //set ball on the table
        //Setballs(BallColors.White, 0);
        Setballs(BallColors.Red, 1);
        Setballs(BallColors.Yellow, 2);
        Setballs(BallColors.Green, 3);
        Setballs(BallColors.Brown, 4);
        Setballs(BallColors.Blue, 5);
        Setballs(BallColors.Pink, 6);
        Setballs(BallColors.Black, 7);
    }

    public void UpdateScoreText()
    {
       scoreText.text =  $"Player Score:{PlayerScore}";
    }
    

    void Update()
    {
        RotateBall();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBall();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StopBall();
        }



    }

    void Setballs(BallColors colors, int pos)
    {
        GameObject ball = Instantiate(ballPrefab, ballPosition[pos].transform.position, Quaternion.identity);
        Ball b = ball.GetComponent<Ball>();
        b.SetColorAndPoint(colors);
    }

    void RotateBall()
    {
        XInput = Input.GetAxis("Horizontal");
        cueBall.transform.Rotate(new Vector3(0f, XInput / 10, 0f));
    }


    void ShootBall()
    {
        camera.transform.parent = null;
        Rigidbody rd = cueBall.GetComponent<Rigidbody>();
        rd.AddRelativeForce(Vector3.forward * force, ForceMode.Impulse);
        ballLine.SetActive(false);
    }

    void CameraBehindBall()
    {
        camera.transform.parent = cueBall.transform;
        camera.transform.position = cueBall.transform.position + new Vector3(0f, 15f, -15f);
    }

    void StopBall()
    {
        Rigidbody rd = cueBall.GetComponent<Rigidbody>();
        rd.velocity = Vector3.zero;
        rd.angularVelocity = Vector3.zero;

        cueBall.transform.eulerAngles = Vector3.zero;
        CameraBehindBall();
        camera.transform.eulerAngles = new Vector3(40f, 0f, 0f);
        ballLine.SetActive(true);
    }

}    