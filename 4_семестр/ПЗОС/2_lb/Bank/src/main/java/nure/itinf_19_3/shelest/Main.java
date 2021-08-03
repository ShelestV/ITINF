package nure.itinf_19_3.shelest;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.concurrent.Semaphore;

public class Main {
    public static void main(String[] args) {
        List<Client> clients = new ArrayList<>();
        clients.add(new Client("Olya"));
        clients.add(new Client("Vova"));
        clients.add(new Client("Katya"));

        Bank bank = new Bank(200000, clients);
        List<Cashier> cashiers = new ArrayList<>();
        cashiers.add(new Cashier(bank));
        cashiers.add(new Cashier(bank));
        cashiers.add(new Cashier(bank));

        Observer observer = new Observer("Observer", bank, cashiers);
        observer.start();

        double previous = bank.getBalance();
        Random randomIndex = new Random();
        Random randomMethod = new Random();
        Random randomClient = new Random();
        while (bank.getBalance() < 1000000) {
            int indexKassa = randomIndex.nextInt(3);
            int method = randomMethod.nextInt(5);
            int indexClient = randomClient.nextInt(3);
            switch (method) {
                case 0: {
                    cashiers.get(indexKassa).withdrawCashFromAccount(
                            clients.get(indexClient), 10000);
                    System.out.println(clients.get(indexClient).getName() +
                            " withdraw 10000 from account");
                }
                case 1: {
                    cashiers.get(indexKassa).putCashOnAccount(
                            clients.get(indexClient), 100);
                    System.out.println(clients.get(indexClient).getName() +
                            " push 100 on account");
                }
                case 2: {
                    cashiers.get(indexKassa).pay(
                            clients.get(indexClient), 100);
                    System.out.println(clients.get(indexClient).getName() +
                            " pay 100");
                }
                case 3: {
                    cashiers.get(indexKassa).transferCash(
                            clients.get(indexClient), clients.get(indexKassa).getAccount(), 100);
                    System.out.println(clients.get(indexClient).getName() +
                            " transfer 100 to " + clients.get(indexKassa).getName());
                }
                case 4: {
                    cashiers.get(indexKassa).convertTo(
                            clients.get(indexClient), 0.9);
                    System.out.println(clients.get(indexClient).getName() + " convert money");
                }
            }
            if (bank.getBalance() != previous) {
                for (Client client : clients) {
                    System.out.println(client.getName() + " : " + client.getAccount().getBill());
                }
                System.out.println("Storage : " + bank.getBalance());
                for (int i = 0; i < cashiers.size(); ++i) {
                    System.out.println((i + 1) + "-cashier : " + cashiers.get(i).getBalance());
                }
                System.out.println();
                previous = bank.getBalance();
            }
        }
    }
}
