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

    public Text fin;

    public Text victoire;

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

    public void TA()
    {
        Debug.Log(etape);
        switch(etape)
        {
            case 0:
                SetUp();
                etape = 1;
                break;
            case 1:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    HighScore();
                    GameObject.Find("Gameplay").GetComponent<Animation>().Play("RetirerScores");
                    etape = 2;
                }
                break;

            case 2:
                GetComponent<Animation>().Play("Affichage");
                etape = 3;
                break;
            case 3:
                GestionRebourd();
                if(End)
                {
                    etape = 4;
                }
                break;
            case 4:
                if (MainB != MainBTampon)
                {
                    AfficherVainqueur(MainB);
                    etape = 5;
                }
                if (MainB == MainBTampon )
                {
                    if(MainB)
                    {
                        GetComponent<Animation>().Play("transitionBordBR");
                    }
                    if (!MainB)
                    {
                        GetComponent<Animation>().Play("transitionBordRB");
                    }
                    MainB = !MainB;
                    Pause = true;
                    etape = 6;
                    End = false;
                }
                break;
            case 5:
                if (Input.GetKey(KeyCode.Space))
                {
                    fin.text = "Final Cut";
                }
                break;
            case 6:
                if (Input.GetKey(KeyCode.P))
                {
                    if(MainB)
                    {
                        fin.text = "Victoire Bleue";
                        GetComponent<Animation>().Play("Vic");
                        etape = 5;
                    }
                    if(!MainB)
                    {
                        fin.text = "Victoire Rouge";
                        GetComponent<Animation>().Play("Vic");
                        etape = 5;
                    }
                    GestionRebourd();
                    if(End)
                    {
                        fin.text = "Focus";
                        GetComponent<Animation>().Play("Vic");
                        etape = 7;
                    }
                }
                break;
            case 7:
                if (Input.GetKey(KeyCode.B))
                {
                    fin.text = "Victoire Bleue";
                    etape = 5;
                }
                if (Input.GetKey(KeyCode.N))
                {
                    fin.text = "Victoire Rouge";
                    etape = 5;
                }
                break;
        }
        MAJAffichage();
    }
    public void SetMainTampon( bool pouet)
    {
        MainBTampon = pouet;
    }
    private void AfficherVainqueur(bool win)
    {
        if(win == false)
        {
            fin.text = "Victoire Rouge";
            GetComponent<Animation>().Play("VicR");
        }
        if (win == true)
        {
            fin.text = "Victoire Bleue";
            GetComponent<Animation>().Play("VicB");
        }
    }

    private void GestionRebourd()
    {
        if (Pause)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Pause = false;
                return;
            }
        }
        if (!Pause)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Pause = true;
                return;
            }
            if (MainB)
            {
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    if (_secondesBleue == 0)
                    {
                        if (_minutesBleue > 0)
                        {
                            _minutesBleue -= 1;
                            _secondesBleue = 60;
                            timer = 0;
                            End = false;
                        }
                        if (_minutesBleue == 0)
                        {
                            End = true;
                        }
                    }
                    if (_secondesBleue > 0)
                    {
                        _secondesBleue -= 1;
                        timer = 0;
                        End = false;
                    }
                }
            }
            if (!MainB)
            {
                timer += Time.deltaTime;
                if (timer >= 1f)
                {
                    if (_secondesRouge == 0)
                    {
                        if (_minutesRouge > 0)
                        {
                            _minutesRouge -= 1;
                            _secondesRouge = 60;
                            timer = 0;
                            End = false;
                        }
                        if (_minutesRouge == 0)
                        {
                            End = true;
                        }
                    }
                    if (_secondesRouge > 0)
                    {
                        _secondesRouge -= 1;
                        timer = 0;
                        End = false;
                    }
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Pause = true;
            MainB = !MainB;
            if(MainB)
            {
                GetComponent<Animation>().Play("transitionBordRB");
            }
            if (!MainB)
            {
                GetComponent<Animation>().Play("transitionBordBR");
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
        if(scoreBl == scoreRo)
        {
            MainB = MainBTampon;
        }
        if(MainB == true)
        {
            GetComponent<Animation>().Play("bordB");
        }
        if(MainB == false)
        {
            GetComponent<Animation>().Play("bordR");
        }
    }

    /// <summary>
    /// li le score
    /// </summary>
    /// <param name="_ScoreB">score equipe B</param>
    /// <param name="_ScoreR">score equipe R</param>
    public void ReadScore(int _ScoreB, int _ScoreR)
    {
        scoreBl = _ScoreB;
        scoreRo = _ScoreR;
    }

    private void SetUp()
    {
        ScoreToTime(scoreBl,out _minutesBleue,out _secondesBleue);
        ScoreToTime(scoreRo,out _minutesRouge,out _secondesRouge);
    }

    private void ScoreToTime(int _score, out int _minute,out int _sec)
    {
        _minute = 1;
        if (_score > 59)
        {
            _minute = _minute + 1;
            _score -= 60;
            _sec = _score;
            return;
        }
        else
        {
            _sec = _score;
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
}
