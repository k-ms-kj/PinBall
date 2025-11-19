using UnityEngine;
using UnityEngine.InputSystem;

public class FripperController : MonoBehaviour
{
    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;
    //初期の傾き
    [SerializeField]
    private float defaultAngle = 20f;
    //弾いた時の傾き
    [SerializeField]
    private float flickAngle = -20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();
        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_IOS
        MoveFripperIOS();
#else
        //Aキーor左矢印キーを押した時左フリッパーを動かす
        if ((Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame) && this.gameObject.CompareTag("LeftFripperTag"))
        {
            SetAngle(this.flickAngle);
        }
        //Dキーor右矢印キーを押した時左フリッパーを動かす
        else if ((Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame) && this.gameObject.CompareTag("RightFripperTag"))
        {
            SetAngle(this.flickAngle);
        }
        //Sキーor下矢印キーを押した時に左右符リッパーを動かす
        else if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            SetAngle(this.flickAngle);
        }

        //A,D,S,左,右,下キーが離された時フリッパーを元に戻す
        if (Keyboard.current.aKey.wasReleasedThisFrame || Keyboard.current.leftArrowKey.wasReleasedThisFrame ||
            Keyboard.current.dKey.wasReleasedThisFrame || Keyboard.current.rightArrowKey.wasReleasedThisFrame ||
            Keyboard.current.sKey.wasReleasedThisFrame || Keyboard.current.downArrowKey.wasReleasedThisFrame)
        {
            SetAngle(this.defaultAngle);
        }
#endif
    }

    //フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }

    //iOSのフリッパー制御
    private void MoveFripperIOS()
    {
        if (Input.touchCount > 0)
        {
            //タッチをループ処理
            foreach (Touch touch in Input.touches)
            {
                //タッチ開始時のみに反応
                if (touch.phase == UnityEngine.TouchPhase.Began)
                {
                    //タッチのX座標を取得
                    float touchPosX = touch.position.x;
                    //画面の幅の半分を取得
                    float screenHalfWidth = Screen.width / 2.0f;
                    //X座標が画面の左半分にある場合左フリッパーを動かす
                    if (touchPosX < screenHalfWidth && this.CompareTag("LeftFripperTag"))
                    {
                        SetAngle(this.flickAngle);
                    }//X座標が画面の右半分にある場合右フリッパーを動かす
                    else if (touchPosX > screenHalfWidth && this.CompareTag("RightFripperTag"))
                    {
                        SetAngle(this.flickAngle);
                    }
                }
                //タッチ終了時にフリッパーを戻す
                else if (touch.phase == UnityEngine.TouchPhase.Ended)
                {
                    SetAngle(this.defaultAngle);
                }
            }
        }
    }
}