using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    private Text text;
    
    private void Awake ()
    {
        text = GetComponent <Text> ();
        score = 0;
    }
    
    private void Update ()
    {
        text.text = score.ToString();
    }
}
