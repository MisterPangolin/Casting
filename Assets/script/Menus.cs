using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour {

    public GameObject[] Menu;

    public Text Titre;
    public Text Question;
    public Text Propostion1;
    public Text Propostion2;
    public Text Propostion3;
    public Text Propostion4;

    private int etape = 0;
    private int menuChoisi = 0;
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
                    GetComponent<Animation>().Play("RetirerTout");
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
                    etape = 3;
                    Menu[menuChoisi].GetComponent<Text>().color = Color.gray;
                    GetComponent<Animation>().Play("AfficherNomMenu");
                    etape = 5;
                }
                break;
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
        }
    }
}
