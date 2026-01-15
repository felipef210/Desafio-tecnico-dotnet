# 🏥 Gestão de Pacientes

Esta aplicação é uma **API para Gestão de Pacientes**. Essa aplicação consiste num CRUD de pacientes.

O diferencial dessa API é que ela trabalha em conjunto com uma API pública externa, o *ViaCEP*, onde o endereço do paciente é enriquecido por essa aplicação externa ao preencher o CEP do endereço.

## 🚀 Funcionalidades

- ✅ Criar paciente;
- ✅ Editar paciente pelo Id;
- ✅ Excluir paciente pelo Id;
- ✅ Consultar paciente pelo Id;


## 🛠️ Tecnologias utilizadas
- C#;
- .NET 8;
- Entity Framework;
- PostgreSQL;
- ViaCEP (API externa);
- Swagger.

## 💡 Destaques Técnicos
- **Validação de Negócio:** Tratamento de nomes completos (capitalização inteligente ignorando preposições).
- **Consumo de API:** Integração resiliente com ViaCEP para enriquecimento automático de endereço.
- **Robustez:** Tratamento global de exceções e uso de tipos anuláveis (Nullable Reference Types) para evitar erros em tempo de execução.

## 🏗 Estrutura do Projeto

O backend foi estruturado em repository pattern, tendo assim a camada de repository (onde as consultas ao banco de dados são feitas), camada de services (onde ficam todas as regras de negócio da aplicação) e finalmente a controller (endpoints que se comunicam com o frontend).

```
GestaoDePacientesAPI.sln  # Arquivo de solução do projeto
GestaoDePacientesAPI/
│
├─ Data                   # Contexto do banco de dados e migrações
├─ Models/                # Entidades (Paciente e Endereço)
├─ DTOs/                  # Objetos de transferência (PacienteDTO, etc...)
├─ Profiles               # Configurações de mapeamento das models com os DTOs
├─ Repositories/          # Acesso ao banco de dados (PacienteRepository)
├─ Services/              # Regras de negócio (PacienteService e ViaCep)
├─ Controllers/           # Endpoints REST
├─ Exceptions/            # Exceções customizadas com os devidos status codes
└─ Program.cs             # Configurações de serviços, injeções de dependências e middlewares
```

## 📸 Screenshot da API no Swagger
<img width="1863" height="930" alt="swagger" src="https://github.com/user-attachments/assets/ce33b0a7-3fb4-41b1-baec-5ea30208ef67" />

## ▶️ Como executar o projeto

Antes de tudo é importante que você tenha uma SDK do .NET instalada em sua máquina, caso não tenha entre no link: https://dotnet.microsoft.com/pt-br/download/dotnet baixe e instale na sua máquina.

Após isso execute o passo a passo abaixo:

### VsCode

1 - Clone o repositório:
```
git clone https://github.com/seu-usuario/seu-repositorio.git
```

2 - Entre na GestaoDePacientesAPI, abra o local do arquivo com o **VsCode** e em seguida habilite o uso de secrets no projeto para garantir a conexão com o banco de dados:
```
dotnet user-secrets init
```

3 - Configure o seu secrets da seguinte forma:
```
dotnet user-secrets set "DbConnection" "Server=localhost;Port=5432;Database=HealthCare;User Id=seuUsuarioDoBancoDeDados;Password=suaSenhaDoBancoDeDados;"
```

4 - Com o secrets configurado compile o código com:
```
dotnet build
```

5 - Após a compilação, baixe as configurações dos relacionamentos entre as tabelas para o seu SGDB com:
```
dotnet ef database update
```

6 - Após isso rode o projeto com o comando:
```
dotnet watch run
```

### Visual Studio
1 - Clone o repositório:
```
git clone https://github.com/seu-usuario/seu-repositorio.git
```

2 - Clique no arquivo de solução chamado `GestaoDePacientesAPI.sln`

3 - Dentro da pasta da aplicação inicialize e configure o seu secrets com os mesmos comandos do tutorial anterior
```
dotnet user-secrets init
dotnet user-secrets set "DbConnection" "Server=localhost;Port=5432;Database=HealthCare;User Id=seuUsuarioDoBancoDeDados;Password=suaSenhaDoBancoDeDados;"
```

4 - Baixe as configurações dos relacionamentos entre as tabelas para o seu SGDB com:
```
dotnet ef database update
```

5 - Clique no ícone de *play* presente no menu no topo da tela, assim o projeto compilará e abrirá com o swagger.

## 👤 Créditos

👨‍💻 **Felipe Miranda**  
Desenvolvedor Full Stack

- 💼 LinkedIn: https://www.linkedin.com/in/felipe-m-945a6a116/
- 💻 GitHub: https://github.com/felipef210
- ✉️ E-mail: rfelipe321@live.com
