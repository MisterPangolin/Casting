using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour {

    public GameObject[] Menu;

    public Text Titre;
    public Text Questions;
    public Text Propostion1;
    public Text Propostion2;
    public Text Propostion3;
    public Text Propostion4;

    private int etape = 0;
    private int menuChoisi = 0;
    private int QuestActuelle = 0;

    private int QuestionActuelle;

    /// <summary>
    /// fonction gérant la partie des menus
    /// </summary>
    public void Affiche()
    {
        switch (etape)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.M))
                {
                    etape = 1;
                    GetComponent<Animation>().Play("AfficherMenu1");
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.M))
                {
                    etape = 2;
                    GetComponent<Animation>().Play("AfficherMenu2");
                }
                break;
            case 2:
                if (Input.GetKeyDown(KeyCode.M))
                {
                    etape = 3;
                    GetComponent<Animation>().Play("AfficherMenu3");
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    GetComponent<Animation>().Play("RetirerToutMenus");
                    Titre = Menu[0].GetComponent<Text>();
                    menuChoisi = 0;
                    etape = 4;
                }
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    GetComponent<Animation>().Play("RetirerTout");
                    Titre = Menu[1].GetComponent<Text>();
                    menuChoisi = 1;
                    etape = 4;
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    GetComponent<Animation>().Play("RetirerTout");
                    Titre = Menu[2].GetComponent<Text>();
                    menuChoisi = 2;
                    etape = 4;
                }
                break;
            case 4:
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    GetComponent<Animation>().Play("AfficherTout");
                    etape = 3;
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    GetComponent<Animation>().Play("AfficherNomMenu");
                    etape = 5;
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    GetComponent<Animation>().Play("AfficherTout");
                    etape = 3;
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    Menu[menuChoisi].GetComponent<Text>().color = Color.gray;
                    GetComponent<Animation>().Play("AfficherNomMenu");
                    etape = 6;
                }
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.M))
                {
                    if(QuestActuelle < Menu[menuChoisi].transform.childCount)
                    {
                        if(Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().prop)
                        {
                            Questions.text = Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().question;
                            Propostion1.text = Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().propositions[0];
                            Propostion2.text = Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().propositions[1];
                            Propostion3.text = Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().propositions[2];
                            Propostion4.text = Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().propositions[3];
                            Propostion4.color = Color.white;
                            Propostion3.color = Color.white;
                            Propostion2.color = Color.white;
                            Propostion1.color = Color.white;
                            Questions.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(Questions.gameObject.GetComponent<RectTransform>().position.x, -0.3f, Questions.gameObject.GetComponent<RectTransform>().position.z);
                            etape = 8;
                            GetComponent<Animation>().Play("AfficherQuestion");
                        }
                        if (!Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().prop)
                        {
                            Questions.text = Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().question;
                            Questions.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(Questions.gameObject.GetComponent<RectTransform>().position.x, -76.15f, Questions.gameObject.GetComponent<RectTransform>().position.z);
                            etape = 7;
                            GetComponent<Animation>().Play("AfficherQuestion");
                        }
                    }
                    if (QuestActuelle >= Menu[menuChoisi].transform.childCount)
                    {
                        GetComponent<Animation>().Play("RetirerNomMenu");
                        etape = 14;
                    }

                }
                break;
            case 7:
                if (Input.GetKeyDown(KeyCode.L))
                {
                    GetComponent<Animation>().Play("RetirerQuestion");
                    etape = 6;
                    QuestActuelle += 1;
                }
                break;
            case 8:
                if (Input.GetKeyDown(KeyCode.L))
                {
                    GetComponent<Animation>().Play("AfficherQ1");
                    etape = 9;
                }
                break;
            case 9:
                if (Input.GetKeyDown(KeyCode.L))
                {
                    GetComponent<Animation>().Play("AfficherQ2");
                    etape = 10;
                }
                break;
            case 10:
                if (Input.GetKeyDown(KeyCode.L))
                {
                    GetComponent<Animation>().Play("AfficherQ3");
                    etape = 11;
                }
                break;
            case 11:
                if (Input.GetKeyDown(KeyCode.L))
                {
                    GetComponent<Animation>().Play("AfficherQ4");
                    etape = 12;
                }
                break;
            case 12:
                if (Input.GetKeyDown(KeyCode.Keypad1))
                {
                    Question.PassageJaunePolice(Propostion1);
                }
                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    Question.PassageJaunePolice(Propostion2);
                }
                if (Input.GetKeyDown(KeyCode.Keypad3))
                {
                    Question.PassageJaunePolice(Propostion3);
                }
                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    Question.PassageJaunePolice(Propostion4 );
                }
                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    AfficherBonneRep();
                    etape = 13;
                }
                break;
            case 13:
                if (Input.GetKeyDown(KeyCode.L))
                {
                    GetComponent<Animation>().Play("RetirerQ");
                    etape = 6;
                    QuestActuelle += 1;
                }
                break;
            case 14:
                GetComponent<Animation>().Play("AfficherTout");
                etape = 3;
                GameObject.Find("Gameplay").GetComponent<Gameplay>().ChangementBord();
                break;
        }
    }

    private void AfficherBonneRep()
    {
        for (int i = 0; i < Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().réponse.Length; i++)
        {
            switch (Menu[menuChoisi].transform.GetChild(QuestActuelle).GetComponent<Question>().réponse[i])
            {
                case 1:
                    Propostion1.color = Color.green;
                    break;
                case 2:
                    Propostion2.color = Color.green;
                    break;
                case 3:
                    Propostion3.color = Color.green;
                    break;
                case 4:
                    Propostion4.color = Color.green;
                    break;
            }
        }
    }
    public void GestionAfficher()
    {
        switch(etape)
        {
            case 0:
                GetComponent<Animation>().Play("AfficherAl'AFf");
                break;
            case 3:
                GetComponent<Animation>().Play("AfficherToutMenus");
                break;
            case 5:
                GetComponent<Animation>().Play("AfficherNomMenu");
                break;
            case 6:
                GetComponent<Animation>().Play("AfficherQuestionetMenuPouet");
                break;
            case 7:
                GetComponent<Animation>().Play("AfficherQuestionetMenuPouet");
                break;
            case 12:
                GetComponent<Animation>().Play("AfficherToutQuestionetMenu");
                break;
            case 13:
                GetComponent<Animation>().Play("AfficherToutQuestionetMenu");
                break;
        }
    }

    public void GestionRetirerAff()
    {
        switch (etape)
        {
            case 0:
                GetComponent<Animation>().Play("RetirerTitreAl'aff");
                break;
            case 3:
                GetComponent<Animation>().Play("RetirerToutMenus");
                break;
            case 5:
                GetComponent<Animation>().Play("RetirerNomMenu");
                break;
            case 6:
                GetComponent<Animation>().Play("RetirerQuestionetMenu");
                break;
            case 7:
                GetComponent<Animation>().Play("RetirerQuestionetMenu");
                break;
            case 12:
                GetComponent<Animation>().Play("RetirerToutQuestionetMenu");
                break;
            case 13:
                GetComponent<Animation>().Play("RetirerToutQuestionetMenu");
                break;
        }
    }

    private void PassageJaunePolice(Text pouet)
    {
        if (pouet.color == Color.yellow)
        {
            pouet.color = Color.white;
            return;
        }
        else
        {
            pouet.color = Color.yellow;
            return;
        }
    }
}
