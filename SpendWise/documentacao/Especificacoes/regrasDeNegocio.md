# ESPECIFICAÇÕES DAS REGRAS DE NEGÓCIO
# SpendWise - Sistema de Finanças Pessoais

## VISÃO GERAL

Este documento detalha todas as regras de negócio implementadas no SpendWise, servindo como especificação para desenvolvimento e validação dos testes.

---

## REGRA 1: LIMITE MENSAL POR CATEGORIA (ACUMULADO)

### **Descrição:**
Controle de gastos por categoria com limite mensal acumulativo e sistema de alertas.

### **Comportamento Esperado:**
1. **Cálculo Acumulado:** Somar todas as despesas da categoria no mês corrente + nova despesa
2. **Alerta aos 80%:** Emitir aviso quando atingir 80% do limite
3. **Bloqueio aos 100%:** Impedir nova despesa que ultrapasse o limite

### **Critérios de Aceitação:**
- Usuario pode definir limite mensal para cada categoria
- Sistema calcula gasto acumulado do mês automaticamente
- Alerta é exibido quando gasto ≥ 80% do limite
- Despesa é bloqueada quando gasto + nova despesa > limite
- Alertas são enviados em tempo real

### **Cenários de Teste:**
```gherkin
Cenário: Alerta aos 80% do limite
  Dado que tenho uma categoria "Alimentação" com limite de R$ 1000
  E já gastei R$ 790 no mês atual
  Quando tento adicionar uma despesa de R$ 50
  Então o sistema deve permitir a despesa
  E exibir alerta "Categoria próxima do limite: 84% utilizado"

Cenário: Bloqueio ao ultrapassar limite
  Dado que tenho uma categoria "Lazer" com limite de R$ 500
  E já gastei R$ 480 no mês atual
  Quando tento adicionar uma despesa de R$ 50
  Então o sistema deve bloquear a operação
  E exibir mensagem "Despesa ultrapassa limite da categoria"
```

---

## REGRA 2: ORÇAMENTO MENSAL POR USUÁRIO

### **Descrição:**
Controle global de gastos mensais por usuário, impedindo que o total de despesas ultrapasse o orçamento definido.

### **Comportamento Esperado:**
1. **Orçamento Único:** Um orçamento por usuário por mês
2. **Validação Global:** Verificar total de despesas antes de permitir nova transação
3. **Bloqueio Preventivo:** Impedir despesa que exceda orçamento restante

### **Critérios de Aceitação:**
- Usuario define orçamento mensal único
- Sistema calcula total de despesas do mês
- Despesa é bloqueada se exceder saldo restante
- Relatório mostra % de utilização do orçamento
- Alertas aos 90% de utilização

### **Cenários de Teste:**
```gherkin
Cenário: Bloqueio por falta de orçamento
  Dado que defini orçamento mensal de R$ 3000
  E já gastei R$ 2950 no mês atual
  Quando tento adicionar despesa de R$ 100
  Então o sistema deve bloquear a operação
  E exibir "Orçamento mensal insuficiente: restam R$ 50"
```

---

## REGRA 3: VALIDAÇÃO TEMPORAL

### **Descrição:**
Controles de integridade temporal para garantir consistência dos dados financeiros.

### **Comportamento Esperado:**
1. **Data Não Futura:** Transações não podem ter data posterior ao dia atual
2. **Fechamento Mensal:** Após fechamento, proibir alterações no período
3. **Auditoria:** Manter histórico de todas as alterações

### **Critérios de Aceitação:**
- Data da transação ≤ data atual
- Transações de mês fechado são bloqueadas para edição
- Fechamento mensal é irreversível
- Log de auditoria registra todas as tentativas

### **Cenários de Teste:**
```gherkin
Cenário: Bloqueio de data futura
  Quando tento criar transação com data de amanhã
  Então o sistema deve rejeitar
  E exibir "Data não pode ser futura"

Cenário: Proteção de mês fechado
  Dado que fechei o mês de julho/2025
  Quando tento editar transação de 15/07/2025
  Então o sistema deve bloquear
  E exibir "Mês já fechado para alterações"
```

---

## REGRA 4: PRIORIDADE ESSENCIAL X SUPÉRFLUO

### **Descrição:**
Sistema inteligente de priorização que bloqueia gastos supérfluos quando há comprometimento financeiro.

### **Comportamento Esperado:**
1. **Classificação:** Categorias marcadas como ESSENCIAL ou SUPERFLUO
2. **Validação Preventiva:** Bloquear supérfluos se essenciais comprometidos
3. **Projeção Inteligente:** Considerar tendências mensais

### **Critérios de Aceitação:**
- Categorias classificadas por tipo
- Gastos supérfluos bloqueados se orçamento essencial > 100%
- Alerta quando projeção essencial indica problema
- Exceções podem ser autorizadas manualmente

### **Cenários de Teste:**
```gherkin
Cenário: Bloqueio de supérfluo por comprometimento essencial
  Dado que categorias essenciais já consumiram 105% do orçamento
  Quando tento adicionar despesa em categoria supérflua
  Então o sistema deve bloquear
  E sugerir "Quite primeiro os gastos essenciais"
```

---

## REGRA 5: RELATÓRIOS E ALERTAS

### **Descrição:**
Sistema proativo de monitoramento e reporting para educação financeira.

### **Comportamento Esperado:**
1. **Relatório Mensal:** Consolidação automática de receitas, despesas e saldos
2. **Alertas Automáticos:** Notificações preventivas
3. **Análise por Categoria:** Breakdown detalhado dos gastos

### **Critérios de Aceitação:**
- Relatório mensal gerado automaticamente
- Alertas em tempo real para limites
- Comparação mês anterior
- Gráficos e visualizações

---

## REGRA 6: METAS FINANCEIRAS

### **Descrição:**
Sistema de definição e acompanhamento de objetivos financeiros com projeções inteligentes.

### **Comportamento Esperado:**
1. **Definição de Metas:** "Juntar X até YYYY-MM"
2. **Cálculo de Progresso:** Receitas - Despesas - Compromissos
3. **Projeção de Alcance:** Estimativa baseada em média mensal

### **Critérios de Aceitação:**
- Múltiplas metas simultâneas
- Progresso calculado automaticamente
- Projeção de data de alcance
- Alertas de marcos (25%, 50%, 75%, 100%)

### **Cenários de Teste:**
```gherkin
Cenário: Projeção de meta
  Dado que defini meta de R$ 10000 até dez/2025
  E tenho histórico de economia média de R$ 1000/mês
  Quando consulto o progresso
  Então sistema projeta alcance em outubro/2025
  E sugere "Meta será alcançada 2 meses antes do prazo"
```

---

## TESTES DE INTEGRAÇÃO DAS REGRAS

### **Cenário Complexo: Múltiplas Regras**
```gherkin
Cenário: Validação integrada de todas as regras
  Dado usuário com orçamento mensal de R$ 3000
  E categoria "Alimentação" (essencial) com limite R$ 800
  E categoria "Lazer" (supérfluo) com limite R$ 400
  E já gastou R$ 2800 no mês (R$ 750 alimentação, R$ 2050 outras)
  Quando tenta adicionar R$ 100 em "Lazer"
  Então sistema deve:
    - Verificar limite categoria (OK: 0/400)
    - Verificar orçamento global (OK: restam R$ 200)
    - Verificar prioridade (OK: essenciais controlados)
    - Permitir transação
    - Emitir alerta "Orçamento 93% utilizado"
```

---

