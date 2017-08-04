using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputManager.XML
{
    class InputPlayer
    {
        public string id;
        public string type;
        public List<InputButton> buttons;

        public override string ToString()
        {
            return string.Format("InputPlayer [id={0}, buttons={1}]", id, buttons != null ? buttons.Count : 0);
        }
    }
}
