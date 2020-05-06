using com.mango.protocol.msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.mango.protocol
{
    public class AlarmManager
    {
        Dictionary<int, Alarm> all = new Dictionary<int, Alarm>();
        public void Add(Alarm[] list)
        {
            foreach (Alarm alarm in list)
            {
                Add(alarm);
            }
        }

        public void Clear()
        {
            this.all.Clear();
        }

        public void Add(Alarm alarm)
        {
            if (all.ContainsKey(alarm.id))
            {
                return;
            }
            all.Add(alarm.id, alarm);
        }

		public void Remove(int id)
		{
			if (all.ContainsKey(id))
            {
                all.Remove(id);
            }
		}

        public Alarm Get(int id)
        {
            if (all.ContainsKey(id))
            {
                return all[id];
            }
            return null;
        }


        public Alarm GetLastNew(string flag)
        {
            int max = int.MinValue;
            foreach (Alarm alarm in all.Values)
            {
                if (!alarm.flag.Equals(flag))
                {
                    continue;
                }
                if (alarm.id > max)
                {
                    max = alarm.id;
                }
            }

            if (all.ContainsKey(max))
            {
                return all[max];
            }
            return null;
        }

        public Alarm GetLastNew()
        {
            int max = int.MinValue;
            foreach(Alarm alarm in all.Values)
            {
                if(alarm.id > max)
                {
                    max = alarm.id;
                }
            }

            if (all.ContainsKey(max))
            {
                return all[max];
            }
            return null;
        }

        public List<Alarm> Get(string flag)
        {
            List<Alarm> list = new List<Alarm>();
            foreach (Alarm alarm in all.Values)
            {
                if (alarm.flag.Equals(flag))
                {
                    list.Add(alarm);
                }
            }
            return list;
        }

        public int Size()
        {
            return all.Count;
        }
    }
}
