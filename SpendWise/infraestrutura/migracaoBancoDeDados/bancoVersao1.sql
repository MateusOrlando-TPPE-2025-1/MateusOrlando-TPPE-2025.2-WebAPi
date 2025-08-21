-- V1__init.sql
-- Estruturas iniciais para Minhas Finanças

CREATE TABLE usuario (
  id BIGSERIAL PRIMARY KEY,
  nome TEXT NOT NULL,
  email TEXT UNIQUE
);

CREATE TABLE categoria (
  id BIGSERIAL PRIMARY KEY,
  usuario_id BIGINT NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
  nome TEXT NOT NULL,
  limite DECIMAL(14,2) NOT NULL CHECK (limite >= 0),
  tipo VARCHAR(10) NOT NULL CHECK (tipo IN ('ESSENCIAL','SUPERFLUO')),
  UNIQUE (usuario_id, nome)
);

CREATE TABLE transacao (
  id BIGSERIAL PRIMARY KEY,
  usuario_id BIGINT NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
  categoria_id BIGINT REFERENCES categoria(id) ON DELETE SET NULL,
  tipo VARCHAR(8) NOT NULL CHECK (tipo IN ('RECEITA','DESPESA')),
  valor DECIMAL(14,2) NOT NULL CHECK (valor > 0),
  data DATE NOT NULL,
  descricao TEXT
);
CREATE INDEX idx_transacao_usuario_data ON transacao(usuario_id, data);

CREATE TABLE orcamento_mensal (
  id BIGSERIAL PRIMARY KEY,
  usuario_id BIGINT NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
  ano_mes CHAR(7) NOT NULL,
  valor DECIMAL(14,2) NOT NULL CHECK (valor >= 0),
  UNIQUE (usuario_id, ano_mes)
);

CREATE TABLE fechamento_mensal (
  id BIGSERIAL PRIMARY KEY,
  usuario_id BIGINT NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
  ano_mes CHAR(7) NOT NULL,
  fechado_em TIMESTAMP NOT NULL DEFAULT NOW(),
  UNIQUE (usuario_id, ano_mes)
);

CREATE TABLE meta_financeira (
  id BIGSERIAL PRIMARY KEY,
  usuario_id BIGINT NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
  descricao TEXT NOT NULL,
  valor_alvo DECIMAL(14,2) NOT NULL CHECK (valor_alvo > 0),
  prazo CHAR(7) NOT NULL,
  criado_em TIMESTAMP NOT NULL DEFAULT NOW()
);
