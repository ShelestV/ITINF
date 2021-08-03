package nure.itinf_19_3.shelest;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class BankTests {

    @Test
    public void getBalanceTest() {
        Bank bank = new Bank(1000);
        Assertions.assertEquals(1000, bank.getBalance());
    }

    @Test
    public void addToStorageTest() {
        Bank bank = new Bank(1000);
        bank.addToStorage(200);
        Assertions.assertEquals(1200, bank.getBalance());
    }

    @Test
    public void getFromStorageTest() {
        Bank bank = new Bank(1000);
        bank.getFromStorage(200);
        Assertions.assertEquals(800, bank.getBalance());
    }

    @Test
    public void addClientTest() {
        Bank bank = new Bank(1000);
        Client client = new Client("Test", 1000);
        bank.addClient(client);

        Assertions.assertTrue(bank.isContainedClient(client));
    }

    @Test
    public void removeClientTest() {
        Bank bank = new Bank(1000);
        Client client = new Client("Test", 1000);
        bank.addClient(client);
        bank.removeClient(client);

        Assertions.assertFalse(bank.isContainedClient(client));
    }
}
