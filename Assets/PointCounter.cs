using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    //ポイント用のテキスト
    [SerializeField]
    private Text pointText;
    //得点
    private int num;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //得点を０点に初期化
        num = 0;
        pointText.text = num.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "SmallStarTag")
        {
            num += 10;
        }
        else if (collision.transform.tag == "SmallCloudTag")
        {
            num += 20;
        }
        else if (collision.transform.tag == "LargeStarTag")
        {
            num += 30;
        }
        else if (collision.transform.tag == "LargeCloudTag")
        {
            num += 50;
        }
        else return;

            pointText.text = num.ToString();
    }
}
