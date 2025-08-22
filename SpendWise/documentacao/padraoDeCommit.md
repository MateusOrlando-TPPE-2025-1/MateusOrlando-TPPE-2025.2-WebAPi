# CONVENÇÕES DE COMMIT - SpendWise

## **Padrão Conventional Commits**

### **Formato:**
```
<tipo>(escopo): CP-XX - <descrição>

[corpo opcional explicando o que e por que]

[rodapé com referências ou breaking changes]
```

### **Exemplo:**
```
feat(infra): CP-01 - configuração inicial Docker e PostgreSQL

- Adiciona docker-compose.yml com PostgreSQL 16
- Configura Adminer para interface web
- Define variáveis de ambiente
- Cria network isolada para containers

Refs: #1
```

---

## **TIPOS DE COMMIT**

| Tipo | Descrição | Exemplo |
|------|-----------|---------|
| **feat** | Nova funcionalidade | `feat(domain): CP-02 - implementa entidade Usuario` |
| **fix** | Correção de bug | `fix(db): CP-01 - corrige constraint de email` |
| **docs** | Documentação | `docs(specs): CP-01 - adiciona regras de negócio` |
| **style** | Formatação/estilo | `style(api): ajusta indentação Controllers` |
| **refactor** | Refatoração | `refactor(domain): extrai Value Objects` |
| **test** | Testes | `test(unit): CP-03 - testes para entidade Categoria` |
| **chore** | Manutenção | `chore(deps): atualiza packages NuGet` |
| **ci** | CI/CD | `ci(github): CP-03 - adiciona workflow Actions` |
| **perf** | Performance | `perf(db): otimiza queries de relatório` |

---

## **ESCOPOS DO PROJETO**

### **Infraestrutura:**
- **infra** - Docker, containers, orquestração
- **db** - Banco de dados, migrations, schemas

### **Backend:**
- **domain** - Domain Layer (entities, VOs, rules)
- **app** - Application Layer (use cases, CQRS)
- **api** - API Layer (controllers, middlewares)
- **config** - Configurações e setup

### **Frontend:**
- **frontend** - Setup Next.js e configurações
- **ui** - Componentes e interface
- **pages** - Páginas e rotas

### **Qualidade:**
- **test** - Testes em geral
- **docs** - Documentação
- **ci** - CI/CD e automação

---

## **PONTOS DE CONTROLE (CHECKPOINTS)**

### **CP-01: INFRAESTRUTURA E MODELAGEM** 
**Objetivo:** Base sólida com banco de dados e documentação completa

#### **Infraestrutura:**
```bash
feat(infra): CP-01 - setup inicial Docker e PostgreSQL
feat(infra): CP-01 - configuração Adminer e network
docs(infra): CP-01 - documentação de setup
```

#### **Modelagem:**
```bash
docs(domain): CP-01 - diagrama de classes domain model
docs(db): CP-01 - modelo entidade-relacionamento (MER/DER)
feat(db): CP-01 - DDL completo com regras de negócio
docs(specs): CP-01 - especificações das regras de negócio
docs(conventions): CP-01 - convenções de commit
```

#### **Organização:**
```bash
chore(structure): CP-01 - organização projeto SpendWise
docs(readme): CP-01 - atualização documentação principal
```

---

### **CP-02: APLICAÇÃO COMPLETA**
**Objetivo:** Sistema full-stack funcional com todas as funcionalidades

#### **Backend (ASP.NET Core):**
```bash
feat(config): CP-02 - estrutura Clean Architecture
feat(domain): CP-02 - implementa entidades (Usuario, Categoria, Transacao)
feat(domain): CP-02 - implementa Value Objects (Money, Email, Periodo)
feat(domain): CP-02 - implementa Domain Services e regras de negócio
feat(app): CP-02 - implementa Commands e Queries (CQRS)
feat(app): CP-02 - implementa Handlers com MediatR
feat(app): CP-02 - implementa validações e DTOs
feat(api): CP-02 - implementa Controllers e endpoints
feat(api): CP-02 - configura Swagger/OpenAPI
feat(api): CP-02 - implementa middlewares e CORS
feat(infra): CP-02 - implementa Entity Framework Core
feat(infra): CP-02 - implementa Repositories e UoW
```

#### **Frontend (Next.js + React):**
```bash
feat(frontend): CP-02 - setup Next.js 14 com App Router
feat(frontend): CP-02 - configura Tailwind CSS e TypeScript
feat(ui): CP-02 - componentes base (Button, Input, Layout)
feat(ui): CP-02 - navegação e estrutura principal
feat(pages): CP-02 - páginas de usuários e autenticação
feat(pages): CP-02 - páginas de categorias e limites
feat(pages): CP-02 - páginas de transações (receitas/despesas)
feat(pages): CP-02 - páginas de orçamento mensal
feat(pages): CP-02 - páginas de metas financeiras
feat(pages): CP-02 - páginas de relatórios e dashboard
```

#### **Integração:**
```bash
feat(integration): CP-02 - conecta frontend com API
feat(integration): CP-02 - implementa autenticação
feat(integration): CP-02 - implementa gestão de estado
```

---

### **CP-03: QUALIDADE E DEPLOY**
**Objetivo:** Aplicação testada, documentada e deployada

#### **Testes:**
```bash
test(unit): CP-03 - testes unitários Domain Layer
test(unit): CP-03 - testes unitários Application Layer
test(unit): CP-03 - testes unitários API Controllers
test(integration): CP-03 - testes de integração API
test(integration): CP-03 - testes de integração banco de dados
test(e2e): CP-03 - testes end-to-end frontend
test(performance): CP-03 - testes de carga e performance
```

#### **Qualidade:**
```bash
docs(api): CP-03 - documentação completa da API
docs(frontend): CP-03 - documentação dos componentes
docs(deploy): CP-03 - guia de deploy e produção
style(code): CP-03 - padronização de código
refactor(optimization): CP-03 - otimizações de performance
```

#### **CI/CD e Deploy:**
```bash
ci(github): CP-03 - workflow build e testes automáticos
ci(github): CP-03 - workflow deploy staging
ci(docker): CP-03 - build images para produção
feat(docker): CP-03 - docker-compose produção
feat(deploy): CP-03 - configuração produção (Azure/AWS)
ci(prod): CP-03 - pipeline deploy produção
```

#### **Monitoramento:**
```bash
feat(monitoring): CP-03 - logs estruturados
feat(monitoring): CP-03 - health checks
feat(monitoring): CP-03 - métricas de aplicação
```

---

## 🔄 **WORKFLOW DE COMMITS**

### **1. Commits Pequenos e Frequentes:**
- Cada feature/fix em commit separado
- Máximo 50 caracteres no título
- Corpo explicativo quando necessário

### **2. Sequência Lógica por Checkpoint:**
```bash
# Exemplo CP-01 (Infraestrutura e Modelagem):
git add infraestrutura/docker-compose.yml
git commit -m "feat(infra): CP-01 - setup inicial Docker e PostgreSQL"

git add documentacao/diagrama-classes.md
git commit -m "docs(domain): CP-01 - diagrama de classes domain model"

git add infraestrutura/migracaoBancoDeDados/bancoVersao2.sql
git commit -m "feat(db): CP-01 - DDL completo com regras de negócio"

# Exemplo CP-02 (Aplicação Completa):
git add backend/src/SpendWise.Domain/
git commit -m "feat(domain): CP-02 - implementa entidades e Value Objects"

git add frontend/src/components/
git commit -m "feat(ui): CP-02 - componentes base React"
```

### **3. Tags para Checkpoints:**
```bash
# Ao completar cada checkpoint:
git tag -a CP-01 -m "CP-01: Infraestrutura e Modelagem Completa"
git push origin CP-01

git tag -a CP-02 -m "CP-02: Aplicação Full-Stack Funcional"  
git push origin CP-02

git tag -a CP-03 -m "CP-03: Qualidade, Testes e Deploy"
git push origin CP-03
```

---

## **MÉTRICAS DE COMMIT**

### **Qualidade:**
- Mensagem clara e descritiva
- Escopo bem definido
- Checkpoint identificado
- Mudanças atômicas

### **Rastreabilidade:**
- Histórico linear e limpo
- Checkpoints marcados com tags
- Fácil rollback se necessário
- Changelog automático

---


