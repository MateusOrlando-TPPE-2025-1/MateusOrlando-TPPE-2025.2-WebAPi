public class Despesas extends Transacao {
    private Categoria categoria;

    public Despesas(double valor, String data, String descricao, Categoria categoria) {
        super(valor, data, descricao);
        this.categoria = categoria;
    }


    public String toString() {
        return "Despesa: " + getDescricao() + ", Valor: " + getValor() + ", Data: " + getData();
    }


    public Categoria getCategoria() {
        return categoria;
    }

    public void setCategoria(Categoria categoria) {
        this.categoria = categoria;
    }

    @Override
    public void exibirDetalhes() {
        System.out.println("Despesa - Data: " + getData() + ", Descrição: " + getDescricao() + ", Valor: " + getValor() + ", Categoria: "+getCategoria());

    }
}
