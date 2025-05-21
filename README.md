Simulação de Clínica – Projeto APS
Este projeto é uma aplicação de console escrita em C# que simula o atendimento de uma clínica médica, com funcionalidades para registro de pacientes, triagem com coleta de dados clínicos, classificação por prioridade (cores), visualização de pacientes atendidos e ordenação conforme gravidade.

//Descrição do Código
A aplicação segue a lógica de Fila para pacientes aguardando triagem e Pilha para armazenar os pacientes já atendidos. Cada paciente é classificado com base em três parâmetros clínicos:

  * Pressão arterial (sistólica)

  * Temperatura corporal

  * Nível de oxigenação

Com base nesses dados, o paciente recebe uma cor de prioridade:

   * VERMELHO: Alta prioridade (emergência)

   * AMARELO: Média prioridade

   * VERDE: Baixa prioridade

O programa possui uma interface de menu para facilitar a navegação do usuário.

  //Como Rodar o Sistema
  //Pré-requisitos
.NET SDK instalado (versão 6.0 ou superior recomendada)

Editor de código como Visual Studio, Visual Studio Code, ou outro de sua preferência

Sistema operacional: Windows, Linux ou macOS

  //Passo a Passo
Clone ou baixe o repositório:
  git clone https://github.com/seu-usuario/nome-do-repositorio.git

Navegue até a pasta do projeto:
  cd PorjetoAPS

Compile o projeto:
  dotnet build

Execute o sistema:
  dotnet run

  //Exemplos de Uso
1. Chegada de Paciente
Escolha [1] Chegada de paciente
Confirme com Y
Insira os dados clínicos solicitados
O sistema classificará o paciente automaticamente e o colocará na pilha de atendidos.

2. Ver Pacientes Atendidos
Escolha [2] Ver pacientes atendidos
O sistema exibirá os pacientes já atendidos com seus dados e prioridade.

3. Ver Prioridade na Clínica
Escolha [3] Ver a prioriedade na clinica
Lista os pacientes já atendidos ordenados por prioridade: VERMELHO → AMARELO → VERDE

  //Estrutura do Menu
---SIMULACAO DE CLINICA---

[1] Chegada de paciente
[2] Ver pacientes atendidos
[3] Ver a prioriedade na clinica
[4] Limpar Terminal
[0] Sair      

//Comandos Disponíveis
1	= Registra a chegada de um novo paciente e realiza triagem
2	= Mostra a lista de pacientes que já passaram pela triagem
3	= Lista os pacientes atendidos por ordem de gravidade (prioridade)
4	= Limpa o terminal
0	= Encerra o programa

  //Lógica de Priorização
A classificação é baseada nos seguintes critérios:

//Cor	Critérios
   * Vermelho	Pressão sistólica > 18 ou Temperatura > 39°C ou Oxigenação < 90%
   * Amarelo	Pressão entre 14-18 ou Temperatura entre 37.5-39°C ou Oxigenação entre 90-95%
   * Verde	Fora dos critérios acima (condição estável)

   //Estrutura de Código (Classes)
Program: Ponto de entrada e menu principal

Paciente: Modelo de dados do paciente

shared: Armazena filas e pilhas compartilhadas

chegadapaciente: Lida com a chegada de novos pacientes

triagem: Coleta dados e executa triagem

CorPrioridade: Avalia e retorna a cor de prioridade

visualizacao: Exibe pacientes já atendidos

ordem: Organiza pacientes atendidos por prioridade
