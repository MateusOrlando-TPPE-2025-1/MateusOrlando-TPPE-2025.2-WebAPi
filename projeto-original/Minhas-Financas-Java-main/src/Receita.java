public class Receita extends Transacao {

    public Receita(double valor, String data, String descricao) {
        super(valor, data, descricao); // Chama o construtor da classe pai (Transacao)
    }

    @Override
    public void exibirDetalhes() {
        System.out.println("Receita - Data: " + getData() + ", Descrição: " + getDescricao() + ", Valor: " + getValor());
    }

    public String toString() {
        return "Receita: " + getDescricao() + ", Valor: " + getValor() + ", Data: " + getData();
    }
}
