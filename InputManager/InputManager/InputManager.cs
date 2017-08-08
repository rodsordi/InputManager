using InputManager.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace InputManager
{
    class InputManager
    {
        public string path = "../../InputManager.xml";

        public InputPlayer Load(string id, string type)
        {
            InputPlayer result = null;

            XElement root = null;

            if (File.Exists(path))
            {
                root = XElement.Load(path);

                if (root != null)
                {
                    XElement xInputPlayer = root.Elements().Where(t => t.Attribute("id").Value.Equals(id) && t.Attribute("type").Value.Equals(type)).FirstOrDefault();

                    if (xInputPlayer != null)
                    {
                        result = new InputPlayer()
                        {
                            id = xInputPlayer.Attribute("id").Value,
                            type = xInputPlayer.Attribute("type").Value
                        };

                        List<InputButton> buttons = new List<InputButton>(); 
                        foreach (XElement xInputButton in xInputPlayer.Elements().ToList())
                        {
                            buttons.Add(new InputButton()
                            {
                                code = LoadAttribute(xInputButton, "code"),
                                value = LoadAttribute(xInputButton, "value"),
                                defaultValue = LoadAttribute(xInputButton, "default"),
                                alt = LoadAttribute(xInputButton, "alt"),
                                defaultAlt = LoadAttribute(xInputButton, "default-alt")
                            });
                        }
                        result.buttons = buttons;
                    }
                }
            }
            
            if (root == null)
                Console.WriteLine("XML doesn't exist!");

            return result;
        }

        private string LoadAttribute(XElement element, string attributeName)
        {
            XAttribute attribute = element.Attribute(attributeName);
            if (attribute != null)
                return attribute.Value.ToString();
            return null;
        }

        public List<InputPlayer> LoadAll()
        {
            List<InputPlayer> inputPlayers = new List<InputPlayer>();
            XElement xml = XElement.Load(path);
            foreach (XElement xInputPlayer in xml.Elements())
            {
                InputPlayer inputPlayer = new InputPlayer()
                {
                    id = xInputPlayer.Attribute("id").Value,
                };

                List<InputButton> inputButtons = new List<InputButton>();
                foreach (XElement xInputButton in xInputPlayer.Elements())
                {
                    InputButton button = new InputButton() {
                        code = xInputButton.Attribute("code").Value,
                        value = xInputButton.Attribute("value").Value,
                        defaultValue = xInputButton.Attribute("default").Value,
                        alt = xInputButton.Attribute("alt").Value,
                        defaultAlt = xInputButton.Attribute("default-alt").Value
                    };

                    inputButtons.Add(button);
                }

                inputPlayer.buttons = inputButtons;
                inputPlayers.Add(inputPlayer);
            }
            return inputPlayers;
        }

        public void Save(InputPlayer inputPlayer)
        {
            XElement root = null;
            XElement xInputPlayer = null;

            //Update
            if (File.Exists(path))
            {
                root = XElement.Load(path);
                xInputPlayer = root.Elements().Where(p => p.Attribute("id").Value.Equals(inputPlayer.id) && 
                                                        p.Attribute("type").Value.Equals(inputPlayer.type)).FirstOrDefault();

                if (xInputPlayer != null)
                {
                    foreach (InputButton inputButton in inputPlayer.buttons)
                    {
                        XElement xInputButton = null;
                        if (inputButton.code != null)
                        {
                            xInputButton = xInputPlayer.Elements().Where(p => p.Attribute("code").Value.Equals(inputButton.code)).FirstOrDefault();
                            if (xInputButton != null)
                            {
                                PersistAttribute(xInputButton, "value", inputButton.value);
                                PersistAttribute(xInputButton, "default", inputButton.defaultValue);
                                PersistAttribute(xInputButton, "alt", inputButton.alt);
                                PersistAttribute(xInputButton, "default-alt", inputButton.defaultAlt);
                            }
                            else
                            {
                                xInputButton = new XElement("InputButton");
                                xInputButton.Add(new XAttribute("code", inputButton.code));

                                PersistAttribute(xInputButton, "value", inputButton.value);
                                PersistAttribute(xInputButton, "default", inputButton.defaultValue);
                                PersistAttribute(xInputButton, "alt", inputButton.alt);
                                PersistAttribute(xInputButton, "default-alt", inputButton.defaultAlt);

                                xInputPlayer.Add(xInputButton);
                            }
                        }
                    }
                }
            }

            //Create XML
            if (root == null)
                root = new XElement("Inputs");

            //Save new
            if (xInputPlayer == null)
            {
                xInputPlayer = new XElement("InputPlayer");
                xInputPlayer.Add(new XAttribute("id", inputPlayer.id));
                xInputPlayer.Add(new XAttribute("type", inputPlayer.type));

                foreach (InputButton inputButton in inputPlayer.buttons)
                {
                    XElement xInputButton = new XElement("InputButton");
                    xInputButton.Add(new XAttribute("code", inputButton.code));

                    PersistAttribute(xInputButton, "value", inputButton.value);
                    PersistAttribute(xInputButton, "default", inputButton.defaultValue);
                    PersistAttribute(xInputButton, "alt", inputButton.alt);
                    PersistAttribute(xInputButton, "default-alt", inputButton.defaultAlt);

                    xInputPlayer.Add(xInputButton);
                }
                root.Add(xInputPlayer);
            }

            //Finish
            root.Save(path);
        }

        void PersistAttribute(XElement element, string attributeName, object value)
        {
            XAttribute attribute = element.Attribute(attributeName);
            if (attribute == null)
            {
                if (value != null)
                    element.Add(new XAttribute(attributeName, value.ToString()));//Create
            }
            else
            {
                if (value != null)
                    attribute.SetValue(value.ToString());//Update
                else
                    attribute.Remove();//Delete
            }
        }
    }
}
