# 💰 SpendWise - Sistema de Finanças Pessoais

> **Transformação de um projeto Java simples em uma aplicação web moderna e completa**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)](https://www.docker.com/)
[![Clean Architecture](https://img.shields.io/badge/Clean-Architecture-00D084?style=for-the-badge)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

---

## 🎯 **Visão Geral**

O **SpendWise** é uma evolução moderna do projeto original de finanças pessoais em Java, transformado em uma aplicação web completa utilizando as melhores práticas de desenvolvimento de software.

### **📊 Comparativo: Antes vs Depois**

| Aspecto | Projeto Original (Java) | SpendWise (Moderno) |
|---------|------------------------|---------------------|
| **Arquitetura** | Monolítica simples | Clean Architecture |
| **Persistência** | Memória (temporário) | PostgreSQL + EF Core |
| **Interface** | Console | Web API + Frontend React |
| **Testes** | Nenhum | Unitários + Integração |
| **Validações** | Básicas | FluentValidation + Domain |
| **Padrões** | POO básica | CQRS + MediatR + DDD |
| **Deploy** | Manual | Docker + CI/CD |

---

## 🏗️ **Arquitetura**

```
SpendWise/
├── 📚 documentacao/           # Especificações e diagramas
├── 🐳 infraestrutura/        # Docker e banco de dados
├── ⚙️ backend/               # API .NET Core
│   ├── 🏛️ Domain/            # Entidades e regras de negócio
│   ├── 📋 Application/       # Casos de uso (CQRS)
│   ├── 🔧 Infrastructure/    # Persistência e serviços
│   ├── 🌐 API/              # Controllers e middleware
│   └── 🧪 Tests/            # Testes automatizados
└── 🎨 frontend/              # React + Next.js (em desenvolvimento)
```

### **🧱 Princípios Aplicados**

- ✅ **Clean Architecture** - Separação clara de responsabilidades
- ✅ **Domain-Driven Design** - Modelagem rica do domínio
- ✅ **CQRS + MediatR** - Segregação de comandos e consultas
- ✅ **Repository Pattern** - Abstração da persistência
- ✅ **Value Objects** - Tipos ricos para dados críticos
- ✅ **Rich Domain Model** - Lógica de negócio nas entidades

---

## 🚀 **Funcionalidades**

### **👤 Gestão de Usuários**
- [x] Cadastro e autenticação
- [x] Perfil de usuário
- [ ] Recuperação de senha
- [ ] Autenticação JWT

### **💳 Controle Financeiro**
- [x] Registro de receitas e despesas
- [x] Categorização de gastos
- [x] Limites por categoria
- [x] Orçamento mensal
- [ ] Relatórios e gráficos
- [ ] Metas financeiras

### **📊 Relatórios**
- [ ] Dashboard financeiro
- [ ] Análise de gastos por categoria
- [ ] Projeções e tendências
- [ ] Exportação de dados

---

## 🛠️ **Tecnologias**

### **Backend**
- **ASP.NET Core 8** - Framework web
- **Entity Framework Core** - ORM
- **PostgreSQL** - Banco de dados
- **MediatR** - Padrão mediator
- **FluentValidation** - Validações
- **AutoMapper** - Mapeamento de objetos
- **Serilog** - Logs estruturados

### **Frontend** *(em desenvolvimento)*
- **Next.js 14** - Framework React
- **TypeScript** - Tipagem estática
- **Tailwind CSS** - Estilização
- **Zustand** - Gerenciamento de estado
- **React Hook Form** - Formulários
- **Chart.js** - Gráficos

### **DevOps**
- **Docker** - Containerização
- **GitHub Actions** - CI/CD *(planejado)*
- **Azure** - Deploy *(planejado)*

---

## 🚦 **Quick Start**

### **Pré-requisitos**
```bash
- Docker Desktop
- .NET 8 SDK
- Node.js 18+ (para frontend futuro)
```

### **1. Clone e navegue**
```bash
git clone [repo-url]
cd SpendWise
```

### **2. Inicie a infraestrutura**
```bash
cd infraestrutura
docker-compose up -d
```

### **3. Execute o backend**
```bash
cd backend
dotnet restore
dotnet run --project src/SpendWise.API
```

### **4. Acesse a aplicação**
- **API**: http://localhost:5000
- **Swagger**: http://localhost:5000/swagger
- **Adminer**: http://localhost:8080

---

## 📋 **Roadmap de Desenvolvimento**

### **🎯 Fase 1: Fundação Sólida** *(em andamento)*
- [x] ~~Clean Architecture implementada~~
- [x] ~~Entidades do domínio~~
- [x] ~~Value Objects~~
- [x] ~~CQRS + MediatR~~
- [ ] **Testes unitários completos** 🔄
- [ ] **Autenticação JWT**
- [ ] **Validações robustas**
- [ ] **Logs estruturados**

### **🎨 Fase 2: Frontend Moderno**
- [ ] Setup Next.js 14
- [ ] Design system com Tailwind
- [ ] Componentes reutilizáveis
- [ ] Integração com API
- [ ] Estado global

### **🚀 Fase 3: DevOps & Deploy**
- [ ] Pipeline CI/CD
- [ ] Testes automatizados
- [ ] Deploy em Azure
- [ ] Monitoramento
- [ ] Performance optimization

### **📈 Fase 4: Recursos Avançados**
- [ ] Relatórios avançados
- [ ] Metas financeiras
- [ ] Alertas inteligentes
- [ ] Export/Import
- [ ] API mobile

---

## 🧪 **Testes**

```bash
# Executar todos os testes
dotnet test

# Executar com cobertura
dotnet test --collect:"XPlat Code Coverage"

# Testes específicos
dotnet test --filter "Category=Unit"
dotnet test --filter "Category=Integration"
```

### **Cobertura Atual**
- **Unitários**: 0% → Meta: 90%
- **Integração**: 0% → Meta: 70%
- **E2E**: 0% → Meta: 50%

---

## 🏛️ **Regras de Negócio**

### **💰 Limite Mensal por Categoria**
- Controle acumulativo de gastos
- Alerta aos 80% do limite
- Bloqueio aos 100%

### **📊 Orçamento Mensal**
- Limite global por usuário
- Validação antes de criar despesas
- Relatórios de utilização

### **📅 Transações**
- Data não pode ser futura
- Valor sempre positivo
- Despesas devem ter categoria
- Receitas são opcionais para categoria

---

## 👥 **Contribuição**

### **Padrão de Commits**
```
feat: adiciona nova funcionalidade
fix: corrige bug
docs: atualiza documentação
test: adiciona ou corrige testes
refactor: refatora código sem alterar funcionalidade
style: ajustes de formatação
chore: tarefas de manutenção
```

### **Workflow**
1. Fork do projeto
2. Criar branch feature
3. Implementar com testes
4. Pull request com descrição detalhada

---

## 📞 **Contato**

- **Desenvolvedor**: Mateus Orlando
- **Curso**: TPPE - UNB
- **Período**: 2025.2

---

## 📄 **Licença**

Este projeto é desenvolvido para fins acadêmicos como parte do curso de Técnicas de Programação em Plataformas Emergentes.

---

<div align="center">

**⭐ Se este projeto te ajudou, deixe uma estrela!**

Made with ❤️ and ☕ by [Mateus Orlando](https://github.com/MateusOrlando-TPPE-2025-1)

</div>
