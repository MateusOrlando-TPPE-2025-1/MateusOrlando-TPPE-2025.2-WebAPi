# SpendWise - Sistema Inteligente de Finanças Pessoais

## **Visão Geral**

Evolução completa de um projeto Java simples para uma aplicação enterprise robusta demonstrando as melhores práticas de desenvolvimento.

### **Transformação:**
- **De:** Console app Java básico
- **Para:** Sistema enterprise completo (.NET + React + PostgreSQL)

## **Estrutura do Projeto**

```
├── projeto-original/       # Projeto Java original (referência)
├── SpendWise/              # Novo sistema enterprise
│   ├── backend/            # ASP.NET Core API
│   ├── infraestrutura/     # Docker + PostgreSQL
│   └── documentacao/       # Diagramas + Specs
└── README.md
```

## **Como Executar**

### **Pré-requisitos:**
- Docker Desktop
- .NET 8 SDK
- Next.js


### **1. Infraestrutura:**
```bash
cd SpendWise/infraestrutura
cp .env.example .env
docker compose up -d
```

### **2. Acesso:**
- **Adminer:** http://localhost:8080
- **API:** http://localhost:5000 (CP-02)
- **Frontend:** http://localhost:3000 (CP-02)

### **3. Backend (CP-02):**
```bash
cd SpendWise/backend
dotnet run
```

### **4. Frontend (CP-02):**
```bash
cd SpendWise/frontend
npm install
npm run dev
```

## **Roadmap & Commits (3 Checkpoints)**

### **CP-01: INFRAESTRUTURA E MODELAGEM**
- Docker + PostgreSQL + Adminer
- Diagrama de Classes (Domain Model)
- MER/DER detalhado com todas as entidades
- DDL completo com regras de negócio
- Especificações das 6 regras de negócio
- Convenções de commit e documentação

### **CP-02: APLICAÇÃO COMPLETA**
- **Backend:** ASP.NET Core + Clean Architecture + CQRS
- **Frontend:** Next.js + React + Tailwind CSS
- **Integração:** API completa + UI funcional
- **Funcionalidades:** Todas as 6 regras implementadas

### **CP-03: QUALIDADE E DEPLOY**
- **Testes:** Unitários + Integração + E2E
- **CI/CD:** GitHub Actions + Deploy automatizado
- **Produção:** Aplicação deployada e funcional
- **Documentação:** Guias completos de uso

## **Desenvolvido por**

**Mateus Orlando** - TPPE 2025.2 - UnB

## **Licença**

Projeto acadêmico - Técnicas de Programação em Plataformas Emergentes
