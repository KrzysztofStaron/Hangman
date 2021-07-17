using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hangmanRenderer : MonoBehaviour
{
    public bool victory=false;
    public Sprite[] stages;
    public Color[] buttonColors;
    public Text passworldText;
    public Text txt;
    public int mistakes=0;
    public string passworld;
    [HideInInspector]public string passwordToShow="";
    [HideInInspector]public char[] charArrayPassworld;
    [HideInInspector]public string[] arrayPassworld;
    [HideInInspector]public List<string> guessedLetters;
    public GameObject vinMenu;
    public GameObject defeatMenu;
    public TextAsset jsonFile;

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space)) {
        retry();
      }
      GetComponent<SpriteRenderer>().sprite=stages[mistakes];
    }

    public void Start()
    {
    retry();
    }

    void createPasswoldToShow(){
      passworld=passworld.ToLower();
      passwordToShow="";
      victory=true;
      foreach (string letter in arrayPassworld) {
        if (letter==" ") {
          passwordToShow+=" ";
        }else if(guessedLetters.ToArray().Contains(letter)){
          passwordToShow+=letter;
        }else{
          passwordToShow+="_ ";
          victory=false;
        }
      }
      txt.text=passwordToShow;
      if (mistakes==8) {
        defeatMenu.SetActive(true);
        passworldText.text=passworld;
      }else if(victory){
        vinMenu.SetActive(true);
      }
    }


    public void checkLetter(string letter, Button butonComponent, Image imgComponent){
      if (arrayPassworld.Contains(letter)) {
        //good letter
        imgComponent.color=buttonColors[2];
      }else{
        //bad letter
        mistakes++;
        imgComponent.color=buttonColors[1];
      }
      butonComponent.interactable = false;
      guessedLetters.Add(letter);
      createPasswoldToShow();
    }

    public void retry(){
      defeatMenu.SetActive(false);
      vinMenu.SetActive(false);
      string[] passworlds = JsonUtility.FromJson<passwordClass>(jsonFile.text).passworlds;
      passworld=passworlds[Random.Range(0, passworlds.Length)];
      GameObject[] buttons = GameObject.FindGameObjectsWithTag("LetterButton");
      foreach (GameObject buton in buttons) {
        buton.GetComponent<Image>().color=buttonColors[0];
        buton.GetComponent<Button>().interactable=true;
      }

      guessedLetters.Clear();
      mistakes=0;
      charArrayPassworld=passworld.ToCharArray();
      arrayPassworld=new string[charArrayPassworld.Length];
      for (int i=0; i<charArrayPassworld.Length; i++) {
        arrayPassworld[i]=charArrayPassworld[i].ToString();
      }
      createPasswoldToShow();
    }

    public void hint(){
      if (victory) {
        return;
      }
      string charToHint=" ";
        while (guessedLetters.ToArray().Contains(charToHint) || charToHint==" ") {
          charToHint=arrayPassworld[Random.Range(0, arrayPassworld.Length)];
        }
      guessedLetters.Add(charToHint);
      GameObject[] buttons = GameObject.FindGameObjectsWithTag("LetterButton");
      foreach (GameObject buton in buttons) {
        if (buton.GetComponent<letter>().quesedChar==charToHint) {
          checkLetter(charToHint, buton.GetComponent<Button>(),buton.GetComponent<Image>());
        }
      }
      createPasswoldToShow();
    }
}
