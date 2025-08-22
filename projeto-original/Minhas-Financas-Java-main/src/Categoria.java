public class Categoria {

    private String nome;
    private double limite;
    private double saldo;


    public Categoria(String nome, double limite) {
        this.nome = nome;
        this.limite = limite;
        saldo = limite;
    }

    public boolean ultrapassouLimite(double valorGasto) {
        return valorGasto > limite; // Retorna true se o valorGasto for maior que o limite
    }

    public double getLimite() {
        return limite;
    }

    public void setLimite(double limite) {
        this.limite = limite;
    }

    public String getNome() {
        return nome;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }

}
