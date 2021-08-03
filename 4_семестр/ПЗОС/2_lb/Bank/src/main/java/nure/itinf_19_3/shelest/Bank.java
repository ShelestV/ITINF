package nure.itinf_19_3.shelest;

import java.util.ArrayList;
import java.util.List;

class Bank {
    private final List<Client> clients;
    private double storage;

    public Bank(double storage) {
        clients = new ArrayList<>();
        this.storage = storage;
    }

    public Bank(double storage, List<Client> clients) {
        this.clients = clients;
        this.storage = storage;
    }

    public double getBalance() {
        return storage;
    }

    public void addToStorage(double sum) {
        storage += sum;
    }

    public void getFromStorage(double sum) {
        storage -= sum;
    }

    public void addClient(Client client) {
        if (clients.stream().noneMatch(c -> c.equals(client))) {
            clients.add(client);
        }
    }

    public void removeClient(Client client) {
        clients.remove(client);
    }

    public Client getClient(int index) {
        return clients.get(index);
    }

    public List<Client> getClients() {
        return clients;
    }

    public boolean isContainedClient(Client client) {
        return clients.stream().anyMatch(c -> c.equals(client));
    }
}