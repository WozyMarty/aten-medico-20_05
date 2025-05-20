using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PorjetoAPS
{
    public static class Program //programa principal
    {
        public static void Main(string[] args)
        {
            bool execucao = true;

            while (execucao)
            {
                Console.WriteLine("\n\t---SIMULACAO DE CLINICA---");
                Console.WriteLine("\n\n\t[1] Chegada de paciente");
                Console.WriteLine("\t[2] Ver pacientes atendidos");
                Console.WriteLine("\t[3] Ver a prioriedade na clinica");
                Console.WriteLine("\t[4] Limpar Terminal");
                Console.WriteLine("\t[0] Sair\n");
                Console.Write("\tEscolha uma opção: ");
                string opcao = Console.ReadLine();

                Console.Clear();

                switch (opcao)
                {
                    case "1":
                        chegadapaciente.Chegada();
                        break;
                    case "2":
                        visualizacao.MostrarAtendidos();
                        break;
                    case "3":
                        ordem.nafila();
                        break;
                    case "4":
                        Console.Clear();
                        break;
                    case "0":
                        execucao = false;
                        Console.WriteLine("\tEncerrando o programa...");
                        break;
                    default:
                        Console.WriteLine("\tOpção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }
    public class Paciente //deixar os dados na fila
    {
        public string Nome { get; set; }
        public string PressaoArterial { get; set; }
        public float TemperaturaCorporal { get; set; }
        public int NivelOxigenacao { get; set; }
    }
    public static class shared //objetos de fila e pilha
    {
        public static Queue<Paciente> Paciente = new Queue<Paciente>();
        public static Stack<Paciente> Atendidos = new Stack<Paciente>();
    }


    public static class chegadapaciente //implementação da fila e confirmação de chegada
    {
        public static void Chegada()
        {
            Console.Write("\n\tChegada do paciente: Y/N\n\t\t-> ");
            string escolha = Console.ReadLine()?.ToUpper();

            switch (escolha)
            {
                case "Y":
                    Console.Write("\n\tDigite o nome do paciente: ");
                    string nome = Console.ReadLine();

                    Paciente novoPaciente = new Paciente { Nome = nome };
                    shared.Paciente.Enqueue(novoPaciente);

                    Console.WriteLine($"\n\t\tO paciente {nome} foi registrado na fila de triagem");
                    Console.WriteLine("\t\tSera seguido o processo de coleta de informacoes, antes de vir outro paciente");
                    triagem.Dados();
                    break;

                case "N":
                    Console.WriteLine("\t\tNão há paciente");
                    break;
            }

        }
    }
    public static class triagem
    {
        public static void Dados()//coleta de dados input e utilização da fila e pilha
        {
            if (shared.Paciente.Count > 0)
            {
                Paciente pacienteAtual = shared.Paciente.Peek();
                object[] dadosClinicos = new object[3];

                Console.WriteLine($"\t\tInsira os dados do paciente para a triagem: \n");
                Console.Write($"\t\t  *Pressão arterial: ");
                dadosClinicos[0] = Console.ReadLine();


                Console.Write($"\t\t  *Temperatura corporal (em °C): ");
                dadosClinicos[1] = float.Parse(Console.ReadLine());


                Console.Write($"\t\t  *Nível de oxigenação (em %): ");
                dadosClinicos[2] = int.Parse(Console.ReadLine());


                pacienteAtual.PressaoArterial = (string)dadosClinicos[0];
                pacienteAtual.TemperaturaCorporal = (float)dadosClinicos[1];
                pacienteAtual.NivelOxigenacao = (int)dadosClinicos[2];

                Console.WriteLine("\n\tTriagem concluída.\n");

                string cor = CorPrioridade.Nivel(pacienteAtual);
                Console.WriteLine($"\tPaciente classificado com prioridade: {cor}\n");

                shared.Paciente.Dequeue();
                shared.Atendidos.Push(pacienteAtual);

            }
        }
    }

    public static class CorPrioridade //prioriedade das cores/pacientes, ao comparar com os dados coletados da "triagem", será verificado qual cor será enviado
    {
        public static string Nivel(Paciente paciente)
        {
            int sistolica = 0;
            if (!string.IsNullOrEmpty(paciente.PressaoArterial))
            {
                var partes = paciente.PressaoArterial.Split('/');
                if (int.TryParse(partes[0], out int sist))
                {
                    sistolica = sist;
                }
            }

            if (sistolica > 18 || paciente.TemperaturaCorporal > 39 || paciente.NivelOxigenacao < 90)
            {
                return "VERMELHO";
            }
            else if ((sistolica >= 14 && sistolica <= 18) ||
                     (paciente.TemperaturaCorporal >= 37.5 && paciente.TemperaturaCorporal <= 39) ||
                     (paciente.NivelOxigenacao >= 90 && paciente.NivelOxigenacao < 95))
            {
                return "AMARELO";
            }
            else
            {
                return "VERDE";
            }
        }
    }

    public static class visualizacao //historico com pilha
    {
        public static void MostrarAtendidos()
        {
            if (shared.Atendidos.Count == 0)
            {
                Console.WriteLine("\n Nenhum paciente foi atendido ainda.");
                return;
            }

            Console.WriteLine("\n Lista de pacientes atendidos:\n");

            int i = 1;
            foreach (var paciente in shared.Atendidos.Reverse())
            {
                Console.WriteLine($"  Paciente #{i++}");
                Console.WriteLine($"  Nome: {paciente.Nome}");
                Console.WriteLine($"  Pressão Arterial: {paciente.PressaoArterial}");
                Console.WriteLine($"  Temperatura: {paciente.TemperaturaCorporal}°C");
                Console.WriteLine($"  Oxigenação: {paciente.NivelOxigenacao}%");
                Console.WriteLine($"  Prioriedade: {CorPrioridade.Nivel(paciente)}");
                Console.WriteLine("-----------------------------------");
            }
        }
    }

    public static class ordem
    {
        public static void nafila()
        {
            if (shared.Atendidos.Count == 0)
            {
                Console.WriteLine("\n Nenhum paciente foi atendido ainda.");
                return;
            }

            // converte a pilha em lista para ordenação
            var pacientesOrdenados = shared.Atendidos.ToList();

            // ordena pela prioridade (VERMELHO > AMARELO > VERDE)
            pacientesOrdenados.Sort((p1, p2) =>
            {
                int prioridade1 = GetPrioridadeNumerica(CorPrioridade.Nivel(p1));
                int prioridade2 = GetPrioridadeNumerica(CorPrioridade.Nivel(p2));
                return prioridade1.CompareTo(prioridade2); // menor número = maior prioridade
            });

            Console.WriteLine("\nLista de pacientes atendidos por prioridade:\n");

            int i = 1;
            foreach (var paciente in pacientesOrdenados)
            {
                Console.WriteLine($"  Paciente #{i++}");
                Console.WriteLine($"  Nome: {paciente.Nome}");
                Console.WriteLine($"  Pressão Arterial: {paciente.PressaoArterial}");
                Console.WriteLine($"  Temperatura: {paciente.TemperaturaCorporal}°C");
                Console.WriteLine($"  Oxigenação: {paciente.NivelOxigenacao}%");
                Console.WriteLine($"  Prioriedade: {CorPrioridade.Nivel(paciente)}");
                Console.WriteLine("-----------------------------------");
            }
        }

        private static int GetPrioridadeNumerica(string cor)
        {
            if (cor == "VERMELHO")
                return 1;
            else if (cor == "AMARELO")
                return 2;
            else if (cor == "VERDE")
                return 3;
            else
                return 4; // padrão para valores inesperados
        }
    }

}



