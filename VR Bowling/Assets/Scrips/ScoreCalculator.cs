using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour {

    private int scoreTurn1;
    public PinMachine frameSet;
    public int scoreTotal;
    public GameObject[] playerNr;
    private bool spare = false;
    private bool strike = false;

    public void ScoreCal(int score, bool turn, int player, int frame)
    {
        if(turn == false) //Check turn in frame | false = turn 1
        {
            scoreTurn1 = score; //Save score from turn 1
            if (score == 10) //If player throws strike
            {
                if (strike == true) //if last throw was strike also
                {
                    scoreTotal = scoreTotal + 30;
                    playerNr[player].GetComponent<ScoreScreen>().frames[frame - 1].GetComponent<ScoreFrame>().totalScore.text = scoreTotal.ToString(); //Add score to last frame
                }
                scoreTotal += 10;
                playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = "X";
                strike = true;
                frameSet.frame++; //Next frame
                return;//Stop Code
            }
            else
            {
                if(spare == true) //if last throw was spare
                {
                    scoreTotal = scoreTotal + score; 
                    playerNr[player].GetComponent<ScoreScreen>().frames[frame - 1].GetComponent<ScoreFrame>().totalScore.text = scoreTotal.ToString(); //add score of current turn to total of last frame
                }
                else
                {
                    if (score == 0) //if miss
                    {
                        playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[0].text = "-"; //Miss
                    }
                    else
                    {
                        scoreTotal = scoreTotal + score; //Add score to scoreTotal
                        playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[0].text = score.ToString(); //Normal business
                    }                  
                }
            }            
        }

        else //If turn 2 in frame
        {
            score -= scoreTurn1; //remove points from last turn
            scoreTotal = scoreTotal + score; //Add score to scoreTotal

            if (scoreTurn1 + score == 10) //Spare has been thrown
            {
                playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = "/";
                spare = true;
                frameSet.frame++; //Next frame
                return;//Stop Code
            }
            if (strike == true) //last frame was strike 
            {
                strike = false;
                playerNr[player].GetComponent<ScoreScreen>().frames[frame - 1].GetComponent<ScoreFrame>().totalScore.text = scoreTotal.ToString(); //Update totatScore of last frame
                scoreTotal += (scoreTurn1);
                scoreTotal += (score);
                playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().totalScore.text = scoreTotal.ToString(); //Update totatScore of current frame
                playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = score.ToString();
            }
            else //if no strike or spare
            {
                if (score == 0) //if miss
                {
                    playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = "-";
                }
                else
                {
                    playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().subFrame[1].text = score.ToString();
                    playerNr[player].GetComponent<ScoreScreen>().frames[frame].GetComponent<ScoreFrame>().totalScore.text = scoreTotal.ToString(); //Update totatScore of last frame
                }
            }
            frameSet.frame++; //Next frame
        }
    }
}
