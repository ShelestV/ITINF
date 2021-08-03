package nure.itinf_19_3.shelest;

public class Cashier {
    private Bank bank;
    private double kassa;

    private final double MIN_SUM_IN_KASSA = 10000.0;
    private final double MAX_SUM_IN_KASSA = 100000.0;

    public Cashier(Bank bank) {
        kassa = MIN_SUM_IN_KASSA;
        this.bank = bank;
        this.bank.getFromStorage(MIN_SUM_IN_KASSA);
    }

    public synchronized double getBalance() {
        return kassa;
    }

    public synchronized void addToKassa() {
        if (bank.getBalance() > MIN_SUM_IN_KASSA) {
            kassa += MIN_SUM_IN_KASSA;
            bank.getFromStorage(MIN_SUM_IN_KASSA);
        }
    }

    public synchronized void addToStorage() {
        kassa -= MAX_SUM_IN_KASSA - MIN_SUM_IN_KASSA;
        bank.addToStorage(MAX_SUM_IN_KASSA - MIN_SUM_IN_KASSA);
    }

    private boolean mayClientPay(Client client, double pay) {
        if (client != null) {
            return client.getAccount().getBill() - pay >= 0;
        }
        return false;
    }

    private boolean isEnoughMoneyInKassa(double minus) {
        return kassa - minus >= 0;
    }

    public void withdrawCashFromAccount(Client client, double minus) {
        if (mayClientPay(client, minus) &&
                isEnoughMoneyInKassa(minus)) {
            client.withdrawCashFromAccount(minus);
            kassa -= minus;
        }
    }

    public void putCashOnAccount(Client client, double plus) {
        if (client != null) {
            client.putCashOnAccount(plus);
            kassa += plus;
        }
    }

    public void transferCash(Client client, BankAccount account, double sum) {
        if (account != null &&
                mayClientPay(client, sum)) {
            client.withdrawCashFromAccount(sum);
            account.putCash(sum);
        }
    }

    public void pay(Client client, double sum) {
        if (mayClientPay(client, sum)) {
            client.pay(sum);
            kassa += sum;
        }
    }

    public void convertTo(Client client, double kof) {
        if (client != null) {
            client.convertTo(kof);
        }
    }
}
