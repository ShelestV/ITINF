package nure.itinf_19_3.shelest;

public class BankAccount {
    private double bill;

    public BankAccount() {
        bill = 1000000.0;
    }

    public BankAccount(double bill) {
        this.bill = bill;
    }

    public void withdrawCash(double minus) {
        bill -= minus;
    }

    public void putCash(double plus) {
        bill += plus;
    }

    public double getBill() {
        return bill;
    }

    public void convertTo(double kof) {
        bill = Math.ceil(bill * kof * 100) / 100;
    }
}
