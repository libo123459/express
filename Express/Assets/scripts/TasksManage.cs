using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksManage : MonoBehaviour {
    public Task _task;
    
    public Transform grid;

    TasksData _taskData;
    ItemData _itemData;
    int RefreshTime;
	// Use this for initialization
	void Start () {
        _taskData = this.GetComponent<TasksData>();
        _itemData = this.GetComponent<ItemData>();
        RefreshTask();
	}
    void RefreshTask()
    {
        for (int i = 0; i < 4; i++)
        {
            Task mytask = Instantiate(_task);
            mytask.transform.SetParent(grid.transform);
            mytask.transform.localPosition = new Vector3(0, 0, 0);
            mytask.transform.localScale = new Vector3(1, 1, 1);

            _taskData.TasksList.Add(mytask);
            mytask._item = _itemData.ItemsList[Random.Range(0, 5)];//临时
            mytask.destination = mytask.transform.GetChild(0).GetComponent<Text>();
            mytask.destination.text = mytask._item.gameObject.name;

            Item myitem = Instantiate(mytask._item);
            myitem.transform.SetParent(mytask.transform);
            myitem.transform.localPosition = new Vector3(0,0,0);
            myitem.transform.localScale = new Vector3(1,1,1);
        }
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
