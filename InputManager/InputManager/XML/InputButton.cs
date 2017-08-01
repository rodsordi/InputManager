using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputManager.XML
{
    class InputButton
    {
        public string code;
        public string value;
        public string defaultValue;
        public string alt;
        public string defaultAlt;

        public override string ToString()
        {
            return string.Format("InputButton [code={0}, value={1}, defaultValue={2}, alt={3}, defaultAlt={4}]", code, value, defaultValue, alt, defaultAlt);
        }
    }
}
