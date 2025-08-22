# GUIA DE SETUP - SpendWise
# Configuração Completa do Ambiente

## PRÉ-REQUISITOS

### Software Necessário:
- Docker Desktop (para Windows)
- .NET 8 SDK (para desenvolvimento futuro)
- Node.js 18+ (para desenvolvimento futuro)
- Git
- VS Code (recomendado)

### Verificação dos Pré-requisitos:
```powershell
# Verificar Docker
docker --version
# Output esperado: Docker version 26.x.x

# Verificar .NET (opcional para CP-01)
dotnet --version
# Output esperado: 8.x.x

# Verificar Node.js (opcional para CP-01)
node --version
# Output esperado: v18.x.x ou superior
```

---

## CONFIGURAÇÃO DA INFRAESTRUTURA

### 1. Iniciar Docker Desktop
- Abrir Docker Desktop no Windows
- Aguardar inicialização completa
- Verificar se está rodando: `docker ps`

### 2. Configurar Variáveis de Ambiente
O arquivo `.env` já está configurado em `SpendWise/infraestrutura/.env`:
```env
POSTGRES_DB=minhas_financas
POSTGRES_USER=financas_user
POSTGRES_PASSWORD=financas_pass
POSTGRES_PORT=5432
ADMINER_PORT=8080
```

### 3. Subir Containers
```powershell
cd SpendWise/infraestrutura
docker-compose up -d
```

### 4. Verificar Containers
```powershell
docker ps
# Deve mostrar:
# - mf_postgres (PostgreSQL)
# - mf_adminer (Adminer Web UI)
```

### 5. Acessar Adminer
- URL: http://localhost:8080
- Sistema: PostgreSQL
- Servidor: postgres
- Usuário: financas_user
- Senha: financas_pass
- Base de dados: minhas_financas

---

## ESTRUTURA DO BANCO DE DADOS

### Scripts de Migração:
Os scripts SQL são executados automaticamente na primeira inicialização:

1. `bancoVersao1.sql` - Schema original básico
2. `bancoVersao2.sql` - Schema completo com regras de negócio

### Tabelas Criadas:
- `usuario` - Usuários do sistema
- `categoria` - Categorias de gastos
- `transacao` - Receitas e despesas
- `orcamento_mensal` - Orçamentos por mês
- `meta_financeira` - Metas financeiras
- `fechamento_mensal` - Fechamentos para auditoria

### Views Criadas:
- `vw_resumo_mensal` - Resumo por usuário/mês
- `vw_gastos_categoria` - Gastos detalhados por categoria

---

## TESTES DA INFRAESTRUTURA

### 1. Teste de Conexão
```sql
-- No Adminer, executar:
SELECT 'Conexão OK' as status, NOW() as timestamp;
```

### 2. Teste de Dados Iniciais
```sql
-- Verificar usuários criados:
SELECT * FROM usuario;

-- Verificar categorias padrão:
SELECT * FROM categoria;

-- Verificar orçamento:
SELECT * FROM orcamento_mensal;
```

### 3. Teste de Regras de Negócio
```sql
-- Tentar inserir email duplicado (deve falhar):
INSERT INTO usuario (nome, email) VALUES ('Teste', 'admin@financas.com');

-- Tentar inserir categoria com limite negativo (deve falhar):
INSERT INTO categoria (usuario_id, nome, limite, tipo) 
VALUES (1, 'Teste', -100, 'ESSENCIAL');

-- Tentar inserir transação com data futura (deve falhar):
INSERT INTO transacao (usuario_id, tipo, valor, data, descricao)
VALUES (1, 'DESPESA', 100, '2025-12-31', 'Teste futuro');
```

---

## ESTRUTURA DE ARQUIVOS

```
SpendWise/
├── backend/                     # ASP.NET Core API (CP-02)
├── infraestrutura/              # Docker e DB
│   ├── docker-compose.yml      # Orquestração containers
│   ├── .env                    # Variáveis ambiente
│   └── migracaoBancoDeDados/   # Scripts SQL
│       ├── bancoVersao1.sql    # Schema básico
│       └── bancoVersao2.sql    # Schema completo
├── documentacao/               # Docs do projeto
│   ├── diagrama-classes.md     # Domain Model
│   ├── modelo-entidade-relacionamento.md # MER
│   ├── regrasDeNegocio.md      # Especificações
│   ├── commit-conventions.md   # Convenções de commit
│   └── setup-guide.md          # Este guia
└── README.md                   # Overview do projeto
```

---

## COMANDOS ÚTEIS

### Docker:
```powershell
# Subir containers
docker-compose up -d

# Ver logs
docker-compose logs -f

# Parar containers
docker-compose down

# Rebuild containers
docker-compose up -d --build

# Limpar volumes (CUIDADO: apaga dados)
docker-compose down -v
```

### PostgreSQL:
```powershell
# Conectar via linha de comando
docker exec -it mf_postgres psql -U financas_user -d minhas_financas

# Backup do banco
docker exec mf_postgres pg_dump -U financas_user minhas_financas > backup.sql

# Restore do banco
docker exec -i mf_postgres psql -U financas_user minhas_financas < backup.sql
```

---

## TROUBLESHOOTING

### Problema: Docker não inicia
```powershell
# Verificar se Docker Desktop está rodando
docker ps

# Se não funcionar:
# 1. Abrir Docker Desktop
# 2. Aguardar inicialização
# 3. Tentar novamente
```

### Problema: Porta 5432 em uso
```powershell
# Verificar qual processo usa a porta
netstat -ano | findstr :5432

# Matar processo se necessário
taskkill /PID <numero_do_pid> /F

# Ou alterar porta no .env
POSTGRES_PORT=5433
```

### Problema: Containers não sobem
```powershell
# Ver logs detalhados
docker-compose logs

# Rebuild forçado
docker-compose down
docker-compose up -d --build --force-recreate
```

---

## CHECKLIST DE VERIFICAÇÃO

### Infraestrutura:
- [ ] Docker Desktop rodando
- [ ] Containers postgres e adminer ativos
- [ ] Adminer acessível em localhost:8080
- [ ] Conexão com banco funcionando

### Banco de Dados:
- [ ] Todas as tabelas criadas
- [ ] Constraints funcionando
- [ ] Triggers ativados
- [ ] Views disponíveis
- [ ] Dados iniciais inseridos

### Documentação:
- [ ] Diagrama de classes atualizado
- [ ] MER/DER documentado
- [ ] Regras de negócio especificadas
- [ ] Setup guide criado

---

## CHECKPOINTS DE DESENVOLVIMENTO

### CP-01 - Infraestrutura e Modelagem (ATUAL)
- [x] Configuração Docker
- [x] Schema de banco  
- [x] Documentação técnica
- [x] Regras de negócio

### CP-02 - Aplicação Completa
- [ ] Backend ASP.NET Core + Clean Architecture
- [ ] Frontend Next.js + React + Tailwind
- [ ] Integração e testes

### CP-03 - Qualidadem Docker e Deploy
- [ ] Testes automatizados
- [ ] Docker
- [ ] CI/CD Pipeline
- [ ] Deploy em produção

**Status Atual:** CP-01 CONCLUÍDO!
