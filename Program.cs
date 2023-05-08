using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projeto2
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();

        enum Menu { Listagem=1, Adicionar=2, Remover=3, Sair=4};
        static void Main(string[] args)
        {
            Carregar();
            bool escolherSair = false;

            while(!escolherSair)
            {
                Console.WriteLine("Sistemas de Clientes");
                Console.WriteLine("1-listagem\n2-adicionar\n3-remover\n4-sair");
                int intopcao = int.Parse(Console.ReadLine());

                Menu opcao = (Menu)intopcao;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolherSair = true; 
                        break;
                }
                Console.Clear();
            }

            
        }

        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de cliente:");
            Console.WriteLine("Nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("Cpf do cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("cadastro concluido, aperte enter para sair.");
            Console.ReadLine();
        }

        static void Listagem()
        {
            if(clientes.Count > 0) // se tem pelo menos um cliente cadastrado exiba
            {
                Console.WriteLine("Listagem de Clientes:");
                int i = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"Cpf: {cliente.cpf}");
                    i++;
                    Console.WriteLine("===========================");
                }
            }
            else
            {
                Console.WriteLine("nenhum cliente cadastrado.");
            }
            
            Console.WriteLine("aperte enter para sair.");
            Console.ReadLine();
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("digite o id do cliente que voce quer remover: ");
            int id = int.Parse(Console.ReadLine());
            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("id digitado é inválido, tente novamente");
                Console.ReadLine();
            }
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);

                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }
            }
            catch(Exception e)
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }

    }
}
