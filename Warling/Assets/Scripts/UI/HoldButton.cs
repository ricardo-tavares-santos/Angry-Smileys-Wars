using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class HoldButton : MonoBehaviour, IEventSystemHandler
{
    GameControler _GameControler;
    SoundControler _SoundControler;
    SoundPlayerControler _SoundPlayer;
    void Start()
    {
        _GameControler = FindObjectOfType<GameControler>();
        _SoundControler = FindObjectOfType<SoundControler>();
        _SoundPlayer = FindObjectOfType<SoundPlayerControler>();
    }
    public void MoveRight_On(BaseEventData eventData)
    {
        if (_GameControler._StopTime) return;
        if (_GameControler._GameObj.tag == "Enemy" && _GameControler._TypeGame == 0) return;
        _SoundControler.PlayClickSound(true);
        Player player = _GameControler._GameObj.GetComponent<Player>();
        player._MovePlayer = Player.MovePlayer.MoveRigh;
    }
    public void MoveRight_Off(BaseEventData eventData)
    {
        Player player = _GameControler._GameObj.GetComponent<Player>();
        player._MovePlayer = Player.MovePlayer.None;
    }
    //=================
    public void MoveLeft_On(BaseEventData eventData)
    {
        if (_GameControler._StopTime) return;
        if (_GameControler._GameObj.tag == "Enemy" && _GameControler._TypeGame == 0) return;
        _SoundControler.PlayClickSound(true);
        Player player = _GameControler._GameObj.GetComponent<Player>();
        player._MovePlayer = Player.MovePlayer.MoveLeft;
    }
    public void MoveLeft_Off(BaseEventData eventData)
    {
        Player player = _GameControler._GameObj.GetComponent<Player>();
        player._MovePlayer = Player.MovePlayer.None;
    }
    //=================
    public void JumpLeft_On(BaseEventData eventData)
    {
        if (_GameControler._StopTime) return;
        if (_GameControler._GameObj.tag == "Enemy" && _GameControler._TypeGame == 0) return;
        _SoundControler.PlayClickSound(true);
        _SoundPlayer.PlayJump(true);
        Player player = _GameControler._GameObj.GetComponent<Player>();
        player._MovePlayer = Player.MovePlayer.JumpLeft;
    }
    //=================
    public void JumpRight_On(BaseEventData eventData)
    {
        if (_GameControler._StopTime) return;
        if (_GameControler._GameObj.tag == "Enemy" && _GameControler._TypeGame == 0) return;
        _SoundControler.PlayClickSound(true);
        _SoundPlayer.PlayJump(true);
        Player player = _GameControler._GameObj.GetComponent<Player>();
        player._MovePlayer = Player.MovePlayer.JumpRight;
    }
}