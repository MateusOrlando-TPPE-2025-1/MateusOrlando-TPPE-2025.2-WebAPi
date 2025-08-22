
public class Main {
    public static void main(String[] args) {

        Usuario vitor = new Usuario("Vitor Gomes Martins", 0);
        Categoria lazer = new Categoria("Lazer", 400);
        Categoria alimentacao = new Categoria("Lazer", 800);
        Categoria investimentos = new Categoria("Lazer", 300);
        Receita salario = new Receita(8000, "05/10/2024", "Salário recebido do mês de setembro");
        Despesas mcDonalds = new Despesas(48.80, "10/10/2024", "Lanche no horario de almoço", alimentacao);
        vitor.adicionarReceita(salario);
        vitor.adicionarDespesas(mcDonalds);
        vitor.gerarRelatorio();
        System.out.println("Saldo atual: "+vitor.calcularSaldo());
    }
}