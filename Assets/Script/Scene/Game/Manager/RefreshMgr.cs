using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Scene.Game.Manager
{
    public class RefreshMgr : MonoBehaviour
    {
        public static RefreshMgr instance;

        private List<TimeTask> taskTimeList = new List<TimeTask>();

        private List<TimeTask> tmpTimeList = new List<TimeTask>();

        private List<int> tidList = new List<int>();

        private void Awake()
        {
            instance = this;
        }

        private void FixedUpdate()
        {
            //加入缓存区的定时任务
            for (int i = 0; i < tmpTimeList.Count; i++)
            {
                taskTimeList.Add(tmpTimeList[i]);
            }

            tmpTimeList.Clear();

            //遍历满足条件的任务
            for (int i = 0; i < taskTimeList.Count; i++)
            {
                TimeTask task = taskTimeList[i];
                if (Time.realtimeSinceStartup * 1000 < task.destTime)
                {
                    continue;
                }
                else
                {
                    Action cb = task.CallBack;
                    cb?.Invoke();
                    if (task.count == 1)
                    {
                        ReMoveTaskId(task.id);
                        taskTimeList.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        if (task.count != 0)
                        {
                            task.count -= 1;
                        }

                        task.destTime += task.delay;
                    }
                }
            }
        }

        public int AddTimeTask(Action callBack, float delay, TimeUnitEnum timeUnitEnum = TimeUnitEnum.Millisecond,
            int count = 1)
        {
            if (timeUnitEnum != TimeUnitEnum.Millisecond)
            {
                switch (timeUnitEnum)
                {
                    case TimeUnitEnum.Second:
                        delay = delay * 1000;
                        break;
                    case TimeUnitEnum.Minute:
                        delay = delay * 1000 * 60;
                        break;
                    case TimeUnitEnum.Hour:
                        delay = delay * 1000 * 60 * 60;
                        break;
                    case TimeUnitEnum.Day:
                        delay = delay * 1000 * 60 * 60 * 24;
                        break;
                    default:
                        Debug.Log("Add Task TimeUnit Type Error...");
                        break;
                }
            }

            int id = GetTid();
            //realtimesincestartup表示的是从程序开始以来的真实时间
            float destTime = Time.realtimeSinceStartup * 1000 + delay;
            tmpTimeList.Add(new TimeTask(id, destTime, callBack, count, delay));
            tidList.Add(id);
            return id;
        }

        public bool DeleteTask(int taskId)
        {
            bool exist = false;
            for (int i = 0; i < taskTimeList.Count; i++)
            {
                if (taskTimeList[i].id == taskId)
                {
                    taskTimeList.RemoveAt(i);
                    for (int j = 0; j < tidList.Count; j++)
                    {
                        if (tidList[j] == taskId)
                        {
                            tidList.RemoveAt(j);
                            break;
                        }
                    }

                    exist = true;
                    break;
                }
            }

            if (!exist)
            {
                for (int i = 0; i < tmpTimeList.Count; i++)
                {
                    if (tmpTimeList[i].id == taskId)
                    {
                        tmpTimeList.RemoveAt(i);
                        for (int j = 0; j < tidList.Count; j++)
                        {
                            if (tidList[j] == taskId)
                            {
                                tidList.RemoveAt(j);
                                break;
                            }
                        }

                        exist = true;
                        break;
                    }
                }
            }

            return exist;
        }

        public bool ReplaceTask(int taskId, Action callBack, float delay,
            TimeUnitEnum timeUnitEnum = TimeUnitEnum.Millisecond,
            int count = 1)
        {
            if (timeUnitEnum != TimeUnitEnum.Millisecond)
            {
                switch (timeUnitEnum)
                {
                    case TimeUnitEnum.Second:
                        delay = delay * 1000;
                        break;
                    case TimeUnitEnum.Minute:
                        delay = delay * 1000 * 60;
                        break;
                    case TimeUnitEnum.Hour:
                        delay = delay * 1000 * 60 * 60;
                        break;
                    case TimeUnitEnum.Day:
                        delay = delay * 1000 * 60 * 60 * 24;
                        break;
                    default:
                        Debug.Log("Add Task TimeUnit Type Error...");
                        break;
                }
            }

            float destTime = Time.realtimeSinceStartup * 1000 + delay;
            TimeTask task = new TimeTask(taskId, destTime, callBack, count, delay);
            bool isRep = false;
            for (int i = 0; i < taskTimeList.Count; i++)
            {
                if (taskTimeList[i].id == taskId)
                {
                    taskTimeList[i] = task;
                    isRep = true;
                    break;
                }
            }

            if (!isRep)
            {
                for (int i = 0; i < tmpTimeList.Count; i++)
                {
                    if (tmpTimeList[i].id == taskId)
                    {
                        tmpTimeList[i] = task;
                        isRep = true;
                        break;
                    }
                }
            }

            return isRep;
        }

        private void ReMoveTaskId(int taskId)
        {
            for (int i = 0; i < tidList.Count; i++)
            {
                if (tidList[i] == taskId)
                {
                    tidList.RemoveAt(i);
                    break;
                }
            }
        }

        private int tid;

        private int GetTid()
        {
            tid += 1;
            //安全代码，以防万一
            while (true)
            {
                if (tid == int.MaxValue)
                {
                    tid = 0;
                }

                bool used = false;
                for (int i = 0; i < tidList.Count; i++)
                {
                    if (tid == tidList[i])
                    {
                        used = true;
                        break;
                    }
                }

                if (!used)
                {
                    tidList.Add(tid);
                    break;
                }
                else
                {
                    tid += 1;
                }
            }

            return tid;
        }
    }
}

public class TimeTask
{
    public int id;
    public float destTime; //单位：毫秒
    public Action CallBack;
    public float delay;
    public int count;

    public TimeTask(int id, float destTime, Action callBack, int count, float delay)
    {
        this.destTime = destTime;
        this.count = count;
        this.delay = delay;
        this.CallBack = callBack;
        this.id = id;
    }
}

public enum TimeUnitEnum
{
    Millisecond,
    Second,
    Minute,
    Hour,
    Day
}