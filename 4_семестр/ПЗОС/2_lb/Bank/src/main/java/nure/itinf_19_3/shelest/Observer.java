package nure.itinf_19_3.shelest;

import java.util.List;
import java.util.concurrent.Semaphore;

public class Observer extends Thread {
    private final double MILLION = 500000.0;
    private final double MIN_SUM_IN_KASSA = 1000.0;
    private final double MAX_SUM_IN_KASSA = 100000.0;

    private final Bank bank;
    private final List<Cashier> cashiers;

    Observer(String name, Bank bank, List<Cashier> cashiers) {
        super(name);
        this.bank = bank;
        this.cashiers = cashiers;
    }

    public void run() {
        while (bank.getBalance() < MILLION) {
            for (Cashier cashier : cashiers) {
                if (cashier.getBalance() < MIN_SUM_IN_KASSA) {
                    cashier.addToKassa();
                } else if (cashier.getBalance() > MAX_SUM_IN_KASSA) {
                    cashier.addToStorage();
                }
            }
        }
    }
}
