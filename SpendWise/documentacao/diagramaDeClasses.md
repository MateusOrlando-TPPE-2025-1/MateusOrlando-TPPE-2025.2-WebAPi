#  DIAGRAMA DE CLASSES - DOMAIN MODEL
# Sistema de Finanças Pessoais

##  AGGREGATES E ENTITIES

```mermaid
classDiagram
    %% ============= AGGREGATE: USUARIO =============
    class Usuario {
        +Long id
        +String nome
        +Email email
        +DateTime criadoEm
        +DateTime atualizadoEm
        --
        +criarCategoria(nome, limite, tipo) Categoria
        +criarTransacao(valor, data, descricao, categoriaId) Transacao
        +definirOrcamentoMensal(anoMes, valor) OrcamentoMensal
        +criarMeta(descricao, valorAlvo, prazo) MetaFinanceira
        +calcularSaldoTotal() Money
        +gerarRelatorioMensal(anoMes) RelatorioMensal
        +podeAdicionarDespesa(valor, categoriaId) Boolean
        +validarRegrasNegocio(transacao) ValidationResult
    }
    
    %% ============= ENTITIES =============
    class Categoria {
        +Long id
        +Long usuarioId
        +String nome
        +Money limite
        +TipoCategoria tipo
        +DateTime criadoEm
        --
        +calcularGastoMensal(anoMes) Money
        +verificarLimite(valorAdicional) StatusLimite
        +podeReceberDespesa(valor) Boolean
        +calcularPercentualUtilizado(anoMes) Decimal
    }
    
    class Transacao {
        <<abstract>>
        +Long id
        +Long usuarioId
        +Long categoriaId
        +TipoTransacao tipo
        +Money valor
        +Date data
        +String descricao
        +DateTime criadoEm
        --
        +validarData() Boolean
        +podeSerEditada() Boolean
        +podeSerExcluida() Boolean
    }
    
    class Receita {
        +String fonte
        +Boolean recorrente
        --
        +aplicarNoSaldo() Money
    }
    
    class Despesa {
        +String categoria
        +Boolean essencial
        --
        +validarLimiteCategoria() Boolean
        +validarOrcamentoMensal() Boolean
        +reduzirSaldo() Money
    }
    
    class OrcamentoMensal {
        +Long id
        +Long usuarioId
        +Periodo anoMes
        +Money valor
        +DateTime criadoEm
        --
        +calcularGastoAtual() Money
        +calcularSaldoRestante() Money
        +verificarDisponibilidade(valor) Boolean
        +calcularPercentualUtilizado() Decimal
    }
    
    class MetaFinanceira {
        +Long id
        +Long usuarioId
        +String descricao
        +Money valorAlvo
        +Periodo prazo
        +DateTime criadoEm
        --
        +calcularProgresso() Money
        +calcularPercentualAlcancado() Decimal
        +projetarDataAlcance() Date
        +verificarAlcancada() Boolean
    }
    
    class FechamentoMensal {
        +Long id
        +Long usuarioId
        +Periodo anoMes
        +DateTime fechadoEm
        --
        +bloquearEdicoes() Boolean
        +permitirReabertura() Boolean
    }
    
    %% ============= VALUE OBJECTS =============
    class Money {
        +Decimal valor
        +String moeda
        --
        +somar(Money) Money
        +subtrair(Money) Money
        +multiplicar(Decimal) Money
        +dividir(Decimal) Money
        +isPositivo() Boolean
        +isZero() Boolean
    }
    
    class Email {
        +String endereco
        --
        +validar() Boolean
        +toString() String
    }
    
    class Periodo {
        +Int ano
        +Int mes
        --
        +toString() String
        +proximoMes() Periodo
        +mesAnterior() Periodo
        +isAtual() Boolean
        +isFuturo() Boolean
    }
    
    %% ============= ENUMS =============
    class TipoCategoria {
        <<enumeration>>
        ESSENCIAL
        SUPERFLUO
    }
    
    class TipoTransacao {
        <<enumeration>>
        RECEITA
        DESPESA
    }
    
    class StatusLimite {
        <<enumeration>>
        NORMAL
        ALERTA_80_PORCENTO
        LIMITE_ULTRAPASSADO
    }
    
    %% ============= DOMAIN SERVICES =============
    class OrcamentoService {
        +validarDespesa(usuario, despesa) ValidationResult
        +calcularSaldoDisponivel(usuario, anoMes) Money
        +verificarLimitesCategoria(usuario, despesa) StatusLimite
        +aplicarRegrasPrioridade(usuario, despesa) Boolean
    }
    
    class RelatorioService {
        +gerarRelatorioMensal(usuario, anoMes) RelatorioMensal
        +calcularTotaisPorCategoria(usuario, anoMes) List~CategoriaResumo~
        +identificarAlertas(usuario, anoMes) List~Alerta~
    }
    
    class AlertaService {
        +verificarLimitesCategoria(usuario) List~Alerta~
        +verificarOrcamentoMensal(usuario) List~Alerta~
        +verificarMetasFinanceiras(usuario) List~Alerta~
    }
    
    %% ============= RELACIONAMENTOS =============
    Usuario ||--o{ Categoria : possui
    Usuario ||--o{ Transacao : realiza
    Usuario ||--o{ OrcamentoMensal : define
    Usuario ||--o{ MetaFinanceira : estabelece
    Usuario ||--o{ FechamentoMensal : executa
    
    Categoria ||--o{ Transacao : categoriza
    
    Transacao <|-- Receita
    Transacao <|-- Despesa
    
    Usuario -- Email : tem
    Transacao -- Money : valor
    Categoria -- Money : limite
    OrcamentoMensal -- Money : valor
    MetaFinanceira -- Money : valorAlvo
    
    Categoria -- TipoCategoria : tipo
    Transacao -- TipoTransacao : tipo
    
    OrcamentoMensal -- Periodo : anoMes
    MetaFinanceira -- Periodo : prazo
    FechamentoMensal -- Periodo : anoMes
```

## REGRAS DE NEGÓCIO MAPEADAS

### 1. **Limite Mensal por Categoria**
- `Categoria.calcularGastoMensal()`
- `Categoria.verificarLimite()`
- `OrcamentoService.verificarLimitesCategoria()`

### 2. **Orçamento Mensal**
- `OrcamentoMensal.verificarDisponibilidade()`
- `OrcamentoService.validarDespesa()`

### 3. **Validação Temporal**
- `Transacao.validarData()`
- `FechamentoMensal.bloquearEdicoes()`

### 4. **Prioridade Essencial/Supérfluo**
- `TipoCategoria` enum
- `OrcamentoService.aplicarRegrasPrioridade()`

### 5. **Metas Financeiras**
- `MetaFinanceira.calcularProgresso()`
- `MetaFinanceira.projetarDataAlcance()`

### 6. **Relatórios e Alertas**
- `RelatorioService.gerarRelatorioMensal()`
- `AlertaService.verificarLimitesCategoria()`
