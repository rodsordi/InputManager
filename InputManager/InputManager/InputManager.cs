using InputManager.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InputManager
{
    class InputManager
    {
        public List<InputPlayer> Load()
        {
            List<InputPlayer> inputPlayers = new List<InputPlayer>();
            XElement xml = XElement.Load("../../InputManager.xml");
            foreach (XElement x in xml.Elements())
            {
                List<InputButton> inputButtons = new List<InputButton>();
                foreach (XElement e in x.Elements())
                {
                    InputButton button = new InputButton() {
                        code = e.Attribute("code").Value,
                        value = e.Attribute("value").Value,
                        defaultValue = e.Attribute("default").Value,
                        alt = e.Attribute("alt").Value,
                        defaultAlt = e.Attribute("default-alt").Value
                    };

                    inputButtons.Add(button);
                }

                InputPlayer p = new InputPlayer()
                {
                    id = x.Attribute("id").Value,
                    buttons = inputButtons
                };
                inputPlayers.Add(p);
            }
            return inputPlayers;
        }
    }
}
