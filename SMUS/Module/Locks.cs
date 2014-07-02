using System;
using System.Collections.Generic;

namespace SMUS.Module
{
    class Locks
    {
        public delegate void LockDelegate();
        private readonly Dictionary<string, bool> locks = new Dictionary<string, bool>();

        public Locks()
        {
            
        }

        public void Add(string str)
        {
            string s = str.ToLower();

            if (!locks.ContainsKey(s))
                locks.Add(s, false);
        }

        public bool Use(string lockname, LockDelegate del)
        {
            string s = lockname.ToLower();
            if (!locks.ContainsKey(s) || locks[s] != false) 
                return false;

            del.Invoke();
            return true;
        }

        public void Release(string lockname)
        {
            string s = lockname.ToLower();
            if (!locks.ContainsKey(s))
                throw new Exception("Key does not exist:" + lockname);

            locks[s] = false;
        }
    }
}
