using InputManager.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InputManager
{
    class Program
    {
        static void Main(string[] args)
        {
            TestLoad();
            /*foreach (InputPlayer ip in imputManager.Load())
            {
                Console.WriteLine(ip);
                foreach (InputButton ib in ip.buttons)
                    Console.WriteLine(ib);
            }*/

            while (true) ;
        }

        public static void TestLoad()
        {
            InputManager inputManager = new InputManager();
            InputPlayer player = inputManager.Load("Joystick1", "pc");
            Console.WriteLine(player);
            if (player != null)
                foreach (InputButton ib in player.buttons)
                    Console.WriteLine(ib);
        }

        public static void TestSave()
        {
            InputManager inputManager = new InputManager();

            InputButton inputButton = new InputButton()
            {
                code = "context",
                value = "space"
            };

            InputButton inputButton2 = new InputButton()
            {
                code = "special",
                value = "return",
                defaultValue = "backspace",
                defaultAlt = "backspace"
            };

            List<InputButton> buttons = new List<InputButton>();
            buttons.Add(inputButton);
            buttons.Add(inputButton2);

            InputPlayer inputPlayer = new InputPlayer()
            {
                id = "Joystick1",
                type = "pc",
                buttons = buttons
            };

            inputManager.Save(inputPlayer);
        }

        public static List<Pessoa> ListarPessoas()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            XElement xml = XElement.Load("../../Pessoas.xml");
            foreach (XElement x in xml.Elements())
            {
                Pessoa p = new Pessoa()
                {
                    codigo = int.Parse(x.Attribute("codigo").Value),
                    nome = x.Attribute("nome").Value,
                    telefone = x.Attribute("telefone").Value
                };
                pessoas.Add(p);
            }
            return pessoas;
        }

        public static void AdicionarPessoa(Pessoa p)
        {
            XElement x = new XElement("pessoa");
            x.Add(new XAttribute("codigo", p.codigo.ToString()));
            x.Add(new XAttribute("nome", p.nome));
            x.Add(new XAttribute("telefone", p.telefone));
            XElement xml = XElement.Load("../../Pessoas.xml");
            xml.Add(x);
            xml.Save("../../Pessoas.xml");
        }

        public static void ExcluirPessoa(int codigo)
        {
            XElement xml = XElement.Load("../../Pessoas.xml");
            XElement x = xml.Elements().Where(p => p.Attribute("codigo").Value.Equals(codigo.ToString())).First();
            if (x != null)
            {
                x.Remove();
            }
            xml.Save("Pessoas.xml");
        }

        public static void EditarPessoa(Pessoa pessoa)
        {
            XElement xml = XElement.Load("../../Pessoas.xml");
            XElement x = xml.Elements().Where(p => p.Attribute("codigo").Value.Equals(pessoa.codigo.ToString())).First();
            if (x != null)
            {
                x.Attribute("nome").SetValue(pessoa.nome);
                x.Attribute("telefone").SetValue(pessoa.telefone);
            }
            xml.Save("../../Pessoas.xml");
        }
        
    }
}
