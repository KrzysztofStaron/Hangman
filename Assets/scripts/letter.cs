using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class letter : MonoBehaviour
{
    [SerializeField]public string quesedChar;
    public Button butonComponent;
    public Image imgComponent;

    public void quess()
    {
      GameObject.FindGameObjectWithTag("GameController").GetComponent<hangmanRenderer>().checkLetter(quesedChar,butonComponent,imgComponent);
    }
}
