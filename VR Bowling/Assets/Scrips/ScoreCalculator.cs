using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour {

    public List<int> scoreTurn1 = new List<int>();
    public GameObject scoreObjectPrefab;
    public GameObject scoreInstantiatePos;
    public PinMachine frameSet;
    public List<int> scoreTotal = new List<int>();
    public List<GameObject> players = new List<GameObject>();
    private bool spare = false;
    private bool strike = false;
    public int playerNumber;
    public int player = 0;
    public SCR_object colors;
    private SpriteRenderer playerFrame;

    public void GameStart(int playersAmount)
    {
        playerNumber = playersAmount;
        float tempPos = -0.2f;
        for(int i = 0; i < playersAmount; i++)
        {
            GameObject scoreObject = Instantiate(scoreObjectPrefab, 
                new Vector3(scoreInstantiatePos.transform.position.x,
                    scoreInstantiatePos.transform.position.y - tempPos,
                    scoreInstantiatePos.transform.position.z),
                scoreInstantiatePos.transform.rotation,
                scoreInstantiatePos.transform);
            tempPos = tempPos + .2f;
            players.Add(scoreObject);
            scoreTotal.Add(0);
            scoreTurn1.Add(0);
        }
        TurnPlayer();
    }

    public void ScoreCal(int score, bool turn, int frame)
    {
        if(turn == false) //Check turn in frame | false = turn 1
        {
            scoreTurn1[player] = score; //Save score from turn 1
            if (score == 10) //If player throws strike
            {
                if (strike == true) //if last throw was strike also
                {
                    scoreTotal[player] = scoreTotal[player] + 30;
                    players[player].GetComponent<ScoreScreen>().frames[frame - 1].GetComponent<ScoreFrame>().totalScore.text = scoreTotal.ToString(); //Add score to last frame
                }
                scoreTotal[player] += 10;
                players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = "X";
                strike = true;
                player++; //Next Player
                if(player == playerNumber)
                {
                    frameSet.frame++; //Next Frame
                    player = 0;
                    TurnPlayer();
                }
                else
                {
                    TurnPlayer();
                }
                return;//Stop Code
            }
            else
            {
                if(spare == true) //if last throw was spare
                {
                    scoreTotal[player] = scoreTotal[player] + score;
                    players[player].GetComponent<ScoreScreen>().frames[frame - 1].GetComponent<ScoreFrame>().totalScore.text = scoreTotal.ToString(); //add score of current turn to total of last frame
                    spare = false;
                }
                if (score == 0) //if miss
                {
                    players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[0].text = "--"; //Miss
                }
                else
                {
                    scoreTotal[player] = scoreTotal[player] + score; //Add score to scoreTotal
                    players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[0].text = score.ToString(); //Normal business
                }                  
            }            
        }

        else //If turn 2 in frame
        {
            scoreTurn1[player] -= scoreTurn1[player]; //remove points from last turn
            scoreTotal[player] = scoreTotal[player] + score; //Add score to scoreTotal

            if (scoreTurn1[player] + score == 10) //Spare has been thrown
            {
                players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = "/ ";
                spare = true;
                frameSet.frame++; //Next frame
                if (player == playerNumber)
                {
                    frameSet.frame++; //Next Frame
                    player = 0;
                    TurnPlayer();
                }
                else
                {
                    TurnPlayer();
                }
                return;//Stop Code
            }
            if (strike == true) //last frame was strike 
            {
                strike = false;
                players[player].GetComponent<ScoreScreen>().frames[frame - 1].GetComponent<ScoreFrame>().totalScore.text = scoreTotal[player].ToString(); //Update totatScore of last frame
                scoreTotal[player] += (scoreTurn1[player]);
                scoreTotal[player] += (score);
                players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().totalScore.text = scoreTotal[player].ToString(); //Update totatScore of current frame
                players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = score.ToString();
            }
            else //if no strike or spare
            {
                if (score == 0) //if miss
                {
                    players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = "--";
                }
                else
                {
                    players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = score.ToString();                   
                }
                players[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().totalScore.text = scoreTotal[player].ToString(); //Update totatScore of current frame
            }
            player++; //Next Player
            if (player == playerNumber)
            {
                frameSet.frame++; //Next Frame
                player = 0;
                TurnPlayer();
            }
            else
            {
                TurnPlayer();
            }
        }
    }
    public void TurnPlayer()
    {
        if (playerFrame != null)
        {
            playerFrame.color = colors.nonPlayerTurn;
        }
        playerFrame = players[player].GetComponent<ScoreScreen>().scoreSprite;
        playerFrame.color = colors.PlayerTurn;
    }
}
