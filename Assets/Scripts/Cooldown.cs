using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float value;
        private float timesUp;
        public bool IsReady => timesUp <= Time.time;

        public float RemainedTime =>(float)Math.Max(timesUp-Time.time, 0);


        public Cooldown(float _value)
        {
             value = _value;
        }
        public void Reset()
        {
            timesUp = Time.time + value;
        }
        public void SetResetTime(float _value)
        {
            value = _value;
        }
    }

