using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    Dictionary<Type, object> _listDict;
    static GameFlowManager _instance;
    public static GameFlowManager Instance => _instance;

    public void Init()
    {
        if (_instance == null)
        {
            _instance = this;
            _listDict = new Dictionary<Type, object>();
        }
    }

    public void RegisterList<T>(T data) where T : IGameFlow
    {
        var type = typeof(T);
        if (!_listDict.ContainsKey(type)) _listDict[type] = new ListClass<T>();
        ((ListClass<T>)_listDict[type]).RegisterData(data);
    }

    public void RemoveData<T>(T data) where T : IGameFlow
    {
        var type = typeof(T);
        if (!_listDict.ContainsKey(type)) return;
        ((ListClass<T>)_listDict[type]).RemoveData(data);
    }

    public void Pause()
    {
        foreach (var item in ((ListClass<IPause>)_listDict[typeof(IPause)]).List)
        {
            item.Pause();
        }
    }

    public void Resume()
    {
        foreach (var item in ((ListClass<IResume>)_listDict[typeof(IResume)]).List)
        {
            item.Resume();
        }
    }

    public void GameOver()
    {
        foreach (var item in ((ListClass<IGameOver>)_listDict[typeof(IGameOver)]).List)
        {
            item.GameOver();
        }
        SceneManager.LoadScene("Result");
        Debug.Log("GameOver");
    }
}

public class ListClass<T> where T : IGameFlow
{
    List<T> _list;
    public List<T> List => _list;

    public void RegisterData(T data)
    {
        if (_list == null) _list = new List<T>();
        _list.Add(data);
    }

    public void RemoveData(T data)
    {
        if (_list == null) return;
        _list.Remove(data);
    }
}

public interface IGameFlow { }

public interface IPause : IGameFlow
{
    void Pause();
}

public interface IResume : IGameFlow
{
    void Resume();
}

public interface IGameOver : IGameFlow
{
    void GameOver();
}
