using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace clock
{
    public class Clock
    {
        public int Hour{get;set;}
        public int Minute{get;set;}
        public int Second { get;set;}
        private int alarmHour;
        private int alarmMinute;
        private int alarmSecond;
        public void setAlarmTime(int h,int m,int s)
        {
            alarmHour = h;
            alarmMinute = m;
            alarmSecond = s;
        }
        //创建委托
        public delegate void ClockHander(int i,int j,int k );
        //public event ClockHander Tick;
        //public event ClockHander Alarm;
        public void tick(int i, int j, int k)
        {
            Console.WriteLine("tick~" + i + ":" + j + ":" + k);
        }
        public void alarm(int i, int j, int k)
        {
            Console.WriteLine("alarm!" + i + ":" + j + ":" + k);
        }
        public void run()
        {
            Hour = Minute =Second= 0;
            for(int i = 0; i < 23; i++)
            {
                Hour = i;
                for(int j = 0; j < 59; j++)
                {
                    Minute = j;
                    for(int k = 0; k < 59; k++)
                    {
                        Second = k;
                        Thread.Sleep(1000);
                        if (Hour == alarmHour && Minute == alarmMinute && Second == alarmSecond)
                        {
                            alarm(i, j, k);
                        }
                        else
                        {
                            tick(i, j, k);
                        }
                    }
                }
            }
        }
        
    }
    //public class ClockEvent
    //{
    //    public void tick(int i, int j, int k)
    //    {
    //        Console.WriteLine("tick~" + i + ":" + j + ":" + k);
    //    }
    //    public void alarm(int i, int j, int k)
    //    {
    //        Console.WriteLine("alarm!" + i + ":" + j + ":" + k);
    //    }
    //}
    internal class Program
    {
        static void Main(string[] args)
        {
             Clock clock = new Clock();
            clock.setAlarmTime(0, 0, 10);
             //ClockEvent a = new ClockEvent();
             //clock.Alarm += a.alarm;
             //clock.Tick += a.alarm;
             clock.run();
        }
    }
}
