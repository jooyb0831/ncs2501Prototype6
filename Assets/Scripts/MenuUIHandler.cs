using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

// 기본 스크립트가 모두 실행된 이후에 이 스크립트가 실행되도록 설정 
// UI를 설정하기 전에 다른 항목을 초기화해야 할 수 있으므로 UI에 유용
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;

    public void NewColorSelected(Color color)
    {
        //여기에 선택한 색상을 처리하는 코드 추가
        MainManager.Instance.TeamColor = color;
    }
    
    private void Start()
    {
        ColorPicker.Init();
        // 컬러 피커에 클릭된 색상 버튼이 있으면 NewColorSelected 함수 호출
        ColorPicker.onColorChanged += NewColorSelected;
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
    

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadScene(int value)
    {
        SceneManager.LoadScene(value);
    }

    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }

    public void Exit()
    {
        MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
