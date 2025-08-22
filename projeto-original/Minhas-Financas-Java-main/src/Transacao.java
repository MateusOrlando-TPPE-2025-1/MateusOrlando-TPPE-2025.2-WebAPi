public abstract class Transacao {
    private double valor;
    private String data;
    private String descricao;


    public Transacao(double valor, String data, String descricao) {
        this.valor = valor;
        this.data = data;
        this.descricao = descricao;
    }
    public abstract void exibirDetalhes();


    public double getValor() {
        return valor;
    }

    public void setValor(double valor) {
        this.valor = valor;
    }

    public String getDescricao() {
        return descricao;
    }

    public void setDescricao(String descricao) {
        this.descricao = descricao;
    }

    public String getData() {
        return data;
    }

    public void setData(String data) {
        this.data = data;
    }

}
