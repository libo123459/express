using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksManage : MonoBehaviour {
    TasksData _taskData;
    ItemData _itemData;
    int RefreshTime;
	// Use this for initialization
	void Start () {
        _taskData = this.GetComponent<TasksData>();
        _itemData = this.GetComponent<ItemData>();
        
	}
    void RefreshTask(Task _task,Item _item)
    {
        _taskData.TasksList.Add(_task);
        _item = _itemData.ItemsList[1];//临时
        _task._item = _item;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
