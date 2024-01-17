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
        // �L���[�V�[�g�t�@�C���̃��[�h�҂�
        while ( CriAtom.CueSheetsAreLoading )
        {
            yield return null;
        }

        // AtomExPlayer�̐���
        atomExPlayer = new CriAtomExPlayer();

        // Cue���̎擾
        atomExAcb = CriAtom.GetAcb( m_strCueSheetName );
        cueInfoList = atomExAcb.GetCueInfoList();
    }
    private void OnDestroy()
    {
        atomExPlayer.Dispose();
    }

    void OnGUI()
    {
        // �L���[���Đ��{�^���̐���
        for ( int i = 0; i < cueInfoList.Length; i++ )
        {
            if ( GUI.Button( new Rect( Screen.width - 150, ( Screen.height / cueInfoList.Length ) * i, 150, Screen.height / cueInfoList.Length ), cueInfoList[i].name ) )
            {
                // �Đ����̏ꍇ�͒�~
                if ( atomExPlayer.GetStatus() == CriAtomExPlayer.Status.Playing )
                {
                    atomExPlayer.Stop();
                }
                atomExPlayer.SetCue( atomExAcb, cueInfoList[i].name );

                // ����̓e�X�g�Ȃ̂ŕ����I��
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
