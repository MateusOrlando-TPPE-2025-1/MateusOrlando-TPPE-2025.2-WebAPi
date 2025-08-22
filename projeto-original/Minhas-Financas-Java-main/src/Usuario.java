import java.util.ArrayList;

public class Usuario {
    private String nome;
    private double saldo;
    private ArrayList<Transacao> transacoes;

    public Usuario(String nome, double saldo) {
        this.nome = nome;
        this.saldo = saldo;
        this.transacoes = new ArrayList<>();
    }

    public void adicionarReceita(Receita receita){
        transacoes.add(receita);
        saldo = saldo+ receita.getValor();
    }

    public void adicionarDespesas(Despesas despesas){
        transacoes.add(despesas);
        saldo = saldo - despesas.getValor();

        if (despesas.getCategoria().ultrapassouLimite(despesas.getValor())){
            System.out.println("A despesa ultrapassou o valor do limite: "+ despesas.getCategoria().getNome());
        }

    }


    public double calcularSaldo(){
        double totalDespesas = 0;
        double totalReceita = 0;

        for (Transacao transacao : transacoes){
            if (transacao instanceof Receita){
                totalReceita = totalReceita + transacao.getValor();
            }else {
                totalDespesas = totalDespesas + transacao.getValor();
            }
        }
        return totalReceita - totalDespesas;
    }

    public void gerarRelatorio(){
        double totalDespesas = 0;
        double totalReceita = 0;
        for (Transacao transacao : transacoes){
            if (transacao instanceof Receita){
                totalReceita = totalReceita + transacao.getValor();
                System.out.println(transacao.toString());
            }else {
                totalDespesas = totalDespesas + transacao.getValor();
                System.out.println(transacao.toString());
            }
        }
        System.out.println("Total de despesas: "+totalDespesas);
        System.out.println("Total de receitas: "+totalReceita);
    }










    public String getNome() {
        return nome;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }

    public ArrayList<Transacao> getTransacoes() {
        return transacoes;
    }

    public void setTransacoes(ArrayList<Transacao> transacoes) {
        this.transacoes = transacoes;
    }

    public double getSaldo() {
        return saldo;
    }

    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }
}
