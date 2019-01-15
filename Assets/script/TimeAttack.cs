using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAttack : MonoBehaviour {
    /// <summary>
    /// affichages des minutes restantes à l'équipe Rouge
    /// </summary>
    public Text MinutesRouge;
    /// <summary>
    /// affichages des minutes restantes à l'équipe Bleue
    /// </summary>
    public Text MinutesBleue;
    /// <summary>
    /// affichages des Seconeds restantes à l'équipe Rouge
    /// </summary>
    public Text SecondesDRouge;
    public Text SecondesURouge;
    /// <summary>
    /// affichages des Seconeds restantes à l'équipe Bleue
    /// </summary>
    public Text SecondesDBleue;
    public Text SecondesUBleue;

    private int _minutesRouge;
    private int _minutesBleue;
    private int _secondesRouge;
    private int _secondesBleue;

    private int scoreBl;
    private int scoreRo;

    private bool End= false;
    private float timer;
    private int etape = 0;
    private bool Pause = true;

    /// <summary>
    /// Vraie si c'est au tour de l'équipe bleue
    /// </summary>
    private bool MainB;
    private bool MainBTampon;

    private void Start()
    {
        _minutesRouge = 1;
        _minutesBleue = 1;
        _secondesBleue = 0;
        _secondesRouge = 0;
    }

    // Update is called once per frame
    public void TA()
    {
        switch(etape)
        {
            case 0:
                HighScore();
                etape = 1;
                SetUp();
                GetComponent<Animation>().Play("Lancement");
                break;
            case 1:
                GestionRebourd();
                break;
        }
        MAJAffichage();
    }

    private void GestionRebourd()
    {
        if(MainB)
        {
            if(Pause)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Pause = false;
                    return;
                }
            }
            if(!Pause)
            {
                Rebourd(_minutesBleue, _secondesBleue);
            }
        }
    }

    /// <summary>
    /// sert à récupérer qui à pris la main au TOS en cas d'égalitée
    /// </summary>
    /// <param name="_main">MainequipeBleue</param>
    public void RecupMain(bool _main)
    {
        MainB = _main;
        MainBTampon = _main;
    }

    public void RecupScores(int ScoreBleu, int scoreRouge)
    {
        scoreBl = ScoreBleu;
        scoreRo = scoreRouge;
    }

    private void HighScore()
    {
        if(scoreBl > scoreRo)
        {
            MainB = true;
        }
        if(scoreRo > scoreBl)
        {
            MainB = false;
        }
    }

    /// <summary>
    /// transforme les scores en temps 
    /// </summary>
    /// <param name="_ScoreB">score equipe B</param>
    /// <param name="_ScoreR">score equipe R</param>
    public void ReadScore(int _ScoreB, int _ScoreR)
    {
        int scoreB, scoreR;
        scoreB = _ScoreB;
        scoreR = _ScoreR;
    }

    private void SetUp()
    {
        RecursifScore(scoreBl, _minutesBleue);
        RecursifScore(scoreRo, _minutesRouge);
        _secondesRouge = scoreRo;
        _secondesBleue = scoreBl;
    }

    private void RecursifScore(int _score, int minute)
    {
        if (_score > 59)
        {
            minute += 1;
            _score -= 60;
            RecursifScore(_score, minute);
        }
        else
        {
            return;
        }
    }

    private void MAJAffichage()
    {
        MinutesRouge.text = "" + _minutesRouge;
        MinutesBleue.text = "" + _minutesBleue;
        SecondesDRouge.text = "" + _secondesRouge / 10;
        SecondesURouge.text = "" + _secondesRouge%10;
        SecondesDBleue.text = "" + _secondesBleue / 10;
        SecondesUBleue.text = "" + _secondesBleue % 10;
    }

    private bool Rebourd(int minute, int seconde)
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            if(seconde == 0)
            {
                if(minute > 0)
                {
                    minute -= 1;
                    timer = 0;
                    return false;
                }
                if(minute ==0)
                {
                    return true;
                }
            }
            if(seconde > 0)
            {
                seconde += 1;
                timer = 0;
                return false;
            }
        }
        return false;
    }
}
