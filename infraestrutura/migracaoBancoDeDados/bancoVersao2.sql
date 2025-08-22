-- ============================================
-- VERSÃO 2.0 - Sistema de Finanças Pessoais
-- Modelagem Completa com Regras de Negócio
-- ============================================

-- Extensões necessárias
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ============= TABELAS PRINCIPAIS =============

-- Tabela de Usuários
CREATE TABLE usuario (
    id BIGSERIAL PRIMARY KEY,
    nome TEXT NOT NULL CONSTRAINT nome_nao_vazio CHECK (LENGTH(TRIM(nome)) > 0),
    email TEXT NOT NULL CONSTRAINT email_valido CHECK (email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$'),
    criado_em TIMESTAMP NOT NULL DEFAULT NOW(),
    atualizado_em TIMESTAMP DEFAULT NOW(),
    
    CONSTRAINT email_unico UNIQUE (email)
);

-- Tabela de Categorias
CREATE TABLE categoria (
    id BIGSERIAL PRIMARY KEY,
    usuario_id BIGINT NOT NULL,
    nome TEXT NOT NULL CONSTRAINT nome_categoria_nao_vazio CHECK (LENGTH(TRIM(nome)) > 0),
    limite DECIMAL(14,2) NOT NULL CONSTRAINT limite_positivo CHECK (limite >= 0),
    tipo VARCHAR(10) NOT NULL CONSTRAINT tipo_categoria_valido CHECK (tipo IN ('ESSENCIAL', 'SUPERFLUO')),
    criado_em TIMESTAMP NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_categoria_usuario FOREIGN KEY (usuario_id) REFERENCES usuario(id) ON DELETE CASCADE,
    CONSTRAINT categoria_unica_por_usuario UNIQUE (usuario_id, nome)
);

-- Tabela de Transações
CREATE TABLE transacao (
    id BIGSERIAL PRIMARY KEY,
    usuario_id BIGINT NOT NULL,
    categoria_id BIGINT NULL, -- NULL permitido para receitas
    tipo VARCHAR(8) NOT NULL CONSTRAINT tipo_transacao_valido CHECK (tipo IN ('RECEITA', 'DESPESA')),
    valor DECIMAL(14,2) NOT NULL CONSTRAINT valor_positivo CHECK (valor > 0),
    data DATE NOT NULL CONSTRAINT data_nao_futura CHECK (data <= CURRENT_DATE),
    descricao TEXT,
    criado_em TIMESTAMP NOT NULL DEFAULT NOW(),
    atualizado_em TIMESTAMP DEFAULT NOW(),
    
    CONSTRAINT fk_transacao_usuario FOREIGN KEY (usuario_id) REFERENCES usuario(id) ON DELETE CASCADE,
    CONSTRAINT fk_transacao_categoria FOREIGN KEY (categoria_id) REFERENCES categoria(id) ON DELETE SET NULL,
    
    -- Regra: Despesas devem ter categoria
    CONSTRAINT despesa_deve_ter_categoria CHECK (
        (tipo = 'RECEITA' AND categoria_id IS NULL) OR 
        (tipo = 'DESPESA' AND categoria_id IS NOT NULL)
    )
);

-- Tabela de Orçamento Mensal
CREATE TABLE orcamento_mensal (
    id BIGSERIAL PRIMARY KEY,
    usuario_id BIGINT NOT NULL,
    ano_mes CHAR(7) NOT NULL CONSTRAINT formato_ano_mes CHECK (ano_mes ~ '^[0-9]{4}-(0[1-9]|1[0-2])$'),
    valor DECIMAL(14,2) NOT NULL CONSTRAINT orcamento_positivo CHECK (valor >= 0),
    criado_em TIMESTAMP NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_orcamento_usuario FOREIGN KEY (usuario_id) REFERENCES usuario(id) ON DELETE CASCADE,
    CONSTRAINT orcamento_unico_por_mes UNIQUE (usuario_id, ano_mes)
);

-- Tabela de Metas Financeiras
CREATE TABLE meta_financeira (
    id BIGSERIAL PRIMARY KEY,
    usuario_id BIGINT NOT NULL,
    descricao TEXT NOT NULL CONSTRAINT descricao_meta_nao_vazia CHECK (LENGTH(TRIM(descricao)) > 0),
    valor_alvo DECIMAL(14,2) NOT NULL CONSTRAINT valor_alvo_positivo CHECK (valor_alvo > 0),
    prazo CHAR(7) NOT NULL CONSTRAINT formato_prazo CHECK (prazo ~ '^[0-9]{4}-(0[1-9]|1[0-2])$'),
    criado_em TIMESTAMP NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_meta_usuario FOREIGN KEY (usuario_id) REFERENCES usuario(id) ON DELETE CASCADE
);

-- Tabela de Fechamento Mensal
CREATE TABLE fechamento_mensal (
    id BIGSERIAL PRIMARY KEY,
    usuario_id BIGINT NOT NULL,
    ano_mes CHAR(7) NOT NULL CONSTRAINT formato_fechamento CHECK (ano_mes ~ '^[0-9]{4}-(0[1-9]|1[0-2])$'),
    fechado_em TIMESTAMP NOT NULL DEFAULT NOW(),
    
    CONSTRAINT fk_fechamento_usuario FOREIGN KEY (usuario_id) REFERENCES usuario(id) ON DELETE CASCADE,
    CONSTRAINT fechamento_unico_por_mes UNIQUE (usuario_id, ano_mes)
);

-- ============= ÍNDICES PARA PERFORMANCE =============

-- Índices para consultas frequentes por usuário e data
CREATE INDEX idx_transacao_usuario_data ON transacao(usuario_id, data DESC);
CREATE INDEX idx_transacao_categoria_data ON transacao(categoria_id, data DESC) WHERE categoria_id IS NOT NULL;
CREATE INDEX idx_categoria_usuario_tipo ON categoria(usuario_id, tipo);
CREATE INDEX idx_orcamento_usuario_mes ON orcamento_mensal(usuario_id, ano_mes);
CREATE INDEX idx_meta_usuario_prazo ON meta_financeira(usuario_id, prazo);
CREATE INDEX idx_fechamento_usuario_mes ON fechamento_mensal(usuario_id, ano_mes);

-- Índice para email (já único, mas para performance de login)
CREATE INDEX idx_usuario_email ON usuario(email);

-- ============= TRIGGERS E FUNÇÕES =============

-- Função para atualizar timestamp de atualização
CREATE OR REPLACE FUNCTION atualizar_timestamp()
RETURNS TRIGGER AS $$
BEGIN
    NEW.atualizado_em = NOW();
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Trigger para usuário
CREATE TRIGGER trigger_usuario_atualizado
    BEFORE UPDATE ON usuario
    FOR EACH ROW
    EXECUTE FUNCTION atualizar_timestamp();

-- Trigger para transação
CREATE TRIGGER trigger_transacao_atualizada
    BEFORE UPDATE ON transacao
    FOR EACH ROW
    EXECUTE FUNCTION atualizar_timestamp();

-- Função para validar se mês não está fechado
CREATE OR REPLACE FUNCTION validar_mes_nao_fechado()
RETURNS TRIGGER AS $$
BEGIN
    -- Verifica se existe fechamento para o mês da transação
    IF EXISTS (
        SELECT 1 FROM fechamento_mensal 
        WHERE usuario_id = NEW.usuario_id 
        AND ano_mes = TO_CHAR(NEW.data, 'YYYY-MM')
    ) THEN
        RAISE EXCEPTION 'Não é possível modificar transações de um mês já fechado: %', TO_CHAR(NEW.data, 'YYYY-MM');
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Trigger para impedir edição de transações em mês fechado
CREATE TRIGGER trigger_validar_mes_fechado
    BEFORE INSERT OR UPDATE ON transacao
    FOR EACH ROW
    EXECUTE FUNCTION validar_mes_nao_fechado();

-- ============= VIEWS ÚTEIS =============

-- View para resumo mensal por usuário
CREATE VIEW vw_resumo_mensal AS
SELECT 
    u.id as usuario_id,
    u.nome as usuario_nome,
    TO_CHAR(t.data, 'YYYY-MM') as ano_mes,
    SUM(CASE WHEN t.tipo = 'RECEITA' THEN t.valor ELSE 0 END) as total_receitas,
    SUM(CASE WHEN t.tipo = 'DESPESA' THEN t.valor ELSE 0 END) as total_despesas,
    SUM(CASE WHEN t.tipo = 'RECEITA' THEN t.valor ELSE -t.valor END) as saldo_mensal,
    COUNT(t.id) as total_transacoes
FROM usuario u
LEFT JOIN transacao t ON u.id = t.usuario_id
GROUP BY u.id, u.nome, TO_CHAR(t.data, 'YYYY-MM');

-- View para gastos por categoria
CREATE VIEW vw_gastos_categoria AS
SELECT 
    c.id as categoria_id,
    c.nome as categoria_nome,
    c.tipo as categoria_tipo,
    c.limite as categoria_limite,
    u.id as usuario_id,
    TO_CHAR(t.data, 'YYYY-MM') as ano_mes,
    COALESCE(SUM(t.valor), 0) as total_gasto,
    c.limite - COALESCE(SUM(t.valor), 0) as saldo_restante,
    ROUND((COALESCE(SUM(t.valor), 0) / c.limite * 100), 2) as percentual_utilizado
FROM categoria c
LEFT JOIN transacao t ON c.id = t.categoria_id AND t.tipo = 'DESPESA'
JOIN usuario u ON c.usuario_id = u.id
GROUP BY c.id, c.nome, c.tipo, c.limite, u.id, TO_CHAR(t.data, 'YYYY-MM');

-- ============= DADOS INICIAIS (OPCIONAL) =============

-- Inserir usuário de exemplo
INSERT INTO usuario (nome, email) VALUES 
    ('Admin Sistema', 'admin@financas.com'),
    ('João Silva', 'joao@email.com');

-- Inserir categorias padrão para o usuário João
INSERT INTO categoria (usuario_id, nome, limite, tipo) VALUES 
    (2, 'Alimentação', 800.00, 'ESSENCIAL'),
    (2, 'Transporte', 400.00, 'ESSENCIAL'),
    (2, 'Lazer', 300.00, 'SUPERFLUO'),
    (2, 'Educação', 500.00, 'ESSENCIAL'),
    (2, 'Compras', 200.00, 'SUPERFLUO');

-- Inserir orçamento mensal
INSERT INTO orcamento_mensal (usuario_id, ano_mes, valor) VALUES 
    (2, '2025-08', 2500.00);

-- ============= COMENTÁRIOS NA BASE =============

COMMENT ON TABLE usuario IS 'Tabela de usuários do sistema de finanças pessoais';
COMMENT ON TABLE categoria IS 'Categorias de gastos com limites mensais';
COMMENT ON TABLE transacao IS 'Todas as transações (receitas e despesas) dos usuários';
COMMENT ON TABLE orcamento_mensal IS 'Orçamentos mensais definidos pelos usuários';
COMMENT ON TABLE meta_financeira IS 'Metas financeiras com prazos definidos';
COMMENT ON TABLE fechamento_mensal IS 'Registro de meses fechados para auditoria';

COMMENT ON COLUMN categoria.tipo IS 'ESSENCIAL: gastos prioritários, SUPERFLUO: gastos opcionais';
COMMENT ON COLUMN transacao.tipo IS 'RECEITA: entrada de dinheiro, DESPESA: saída de dinheiro';
COMMENT ON COLUMN transacao.categoria_id IS 'NULL para receitas, obrigatório para despesas';

-- ============= COMMIT =============
COMMIT;
