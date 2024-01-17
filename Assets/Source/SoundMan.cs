using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

public class SoundMan : MonoBehaviour
{
    [SerializeField] private string m_strCueSheetName = "CueSheet_0";
    [SerializeField] private string m_strSelector = "Selector_00";

    private CriAtomEx.CueInfo[] cueInfoList;
    private CriAtomExPlayer atomExPlayer;
    private CriAtomExAcb atomExAcb;

    IEnumerator Start()
    {
        // キューシートファイルのロード待ち
        while ( CriAtom.CueSheetsAreLoading )
        {
            yield return null;
        }

        // AtomExPlayerの生成
        atomExPlayer = new CriAtomExPlayer();

        // Cue情報の取得
        atomExAcb = CriAtom.GetAcb( m_strCueSheetName );
        cueInfoList = atomExAcb.GetCueInfoList();
    }
    private void OnDestroy()
    {
        atomExPlayer.Dispose();
    }

    void OnGUI()
    {
        // キュー名再生ボタンの生成
        for ( int i = 0; i < cueInfoList.Length; i++ )
        {
            if ( GUI.Button( new Rect( Screen.width - 150, ( Screen.height / cueInfoList.Length ) * i, 150, Screen.height / cueInfoList.Length ), cueInfoList[i].name ) )
            {
                // 再生中の場合は停止
                if ( atomExPlayer.GetStatus() == CriAtomExPlayer.Status.Playing )
                {
                    atomExPlayer.Stop();
                }
                atomExPlayer.SetCue( atomExAcb, cueInfoList[i].name );

                // 今回はテストなので物理的に
                if( i == 4 )
                {
                    atomExPlayer.SetSelectorLabel( "sumple", m_strSelector );
                    Debug.Log( m_strSelector );
                }

                atomExPlayer.Start();
                Debug.Log( cueInfoList[i].name );
            }
        }
    }
}
