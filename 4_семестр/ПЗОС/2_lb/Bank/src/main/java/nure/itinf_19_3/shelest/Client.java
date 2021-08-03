package nure.itinf_19_3.shelest;

import java.util.UUID;

public class Client {
    private UUID id;
    private final String name;
    private final BankAccount account;

    public Client(String name) {
        id = UUID.randomUUID();
        this.name = name;
        account = new BankAccount();
    }

    public Client(String name, double bill) {
        id = UUID.randomUUID();
        this.name = name;
        account = new BankAccount(bill);
    }

    public BankAccount getAccount() {
        return account;
    }

    public String getName() {
        return name;
    }

    public void withdrawCashFromAccount(double minus) {
        account.withdrawCash(minus);
    }

    public void putCashOnAccount(double plus) {
        account.putCash(plus);
    }

    public void transferCash(BankAccount account, double sum) {
        if (account != null) {
            this.account.withdrawCash(sum);
            account.putCash(sum);
        }
    }

    public void pay(double sum) {
        withdrawCashFromAccount(sum);
    }

    public void convertTo(double kof) {
        account.convertTo(kof);
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }

        if (obj == null || obj.getClass() != this.getClass()) {
            return false;
        }

        return this.id.equals(((Client) obj).id);
    }
}
