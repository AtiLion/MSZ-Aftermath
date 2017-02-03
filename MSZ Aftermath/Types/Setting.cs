using System;
using System.Collections.Generic;
using System.Text;

namespace MSZ_Aftermath.Types
{
    public class Setting
    {
        public string name;
        public object value;

        public Setting(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
