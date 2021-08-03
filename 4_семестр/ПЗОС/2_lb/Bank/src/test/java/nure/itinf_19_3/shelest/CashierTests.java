package nure.itinf_19_3.shelest;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class CashierTests {

    @Test
    public void c_tor_Test() {
        Bank bank = new Bank(500000);
        Cashier cashier = new Cashier(bank);
        Assertions.assertEquals(490000, bank.getBalance());
    }

    @Test
    public void addToKassaTest() {
        Cashier cashier = new Cashier(new Bank(500000));
        cashier.pay(new Client("Test", 1000), 5000);
        double expected = cashier.getBalance();
        cashier.addToKassa();
        expected += 10000;
        Assertions.assertEquals(expected, cashier.getBalance());
    }

    @Test
    public void addToKassaWithStorageNotEnoughMoneyTest() {
        Cashier cashier = new Cashier(new Bank(9000));
        cashier.addToKassa();
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void addToStorageTest() {
        Bank bank = new Bank(510000);
        Cashier cashier = new Cashier(bank);

        cashier.putCashOnAccount(new Client("Test", 1000000), 100000);
        cashier.addToStorage();

        Assertions.assertEquals(20000, cashier.getBalance());
        Assertions.assertEquals(590000, bank.getBalance());
    }

    @Test
    public void withdrawCashFromAccount() {
        Client client = new Client("Test", 10000);
        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.withdrawCashFromAccount(client, 1000);

        Assertions.assertEquals(9000, client.getAccount().getBill());
        Assertions.assertEquals(9000, cashier.getBalance());
    }

    @Test
    public void withdrawCashFromAccount_ClientMayNotPay_Test() {
        Client client = new Client("Test", 500);
        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.withdrawCashFromAccount(client, 1000);

        Assertions.assertEquals(500, client.getAccount().getBill());
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void withdrawCashFromAccount_NotEnoughMoneyInKassa_Test() {
        Client client = new Client("Test", 20000);
        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.withdrawCashFromAccount(client, 15000);

        Assertions.assertEquals(20000, client.getAccount().getBill());
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void withdrawCashFromAccount_NullClient_Test() {
        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.withdrawCashFromAccount(null, 15000);

        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void putCashOnAccountTest() {
        Client client = new Client("Test", 20000);
        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.putCashOnAccount(client, 5000);

        Assertions.assertEquals(25000, client.getAccount().getBill());
        Assertions.assertEquals(15000, cashier.getBalance());
    }

    @Test
    public void putCashOnAccount_NullClient_Test() {
        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.putCashOnAccount(null, 5000);
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void transferCashTest() {
        Client client = new Client("Test", 10000);
        Client happyClient = new Client("Happy", 10000);

        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.transferCash(client, happyClient.getAccount(), 5000);

        Assertions.assertEquals(15000, happyClient.getAccount().getBill());
        Assertions.assertEquals(5000, client.getAccount().getBill());
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void transferCash_NullAccount_Test() {
        Client client = new Client("Test", 10000);

        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.transferCash(client, null, 5000);

        Assertions.assertEquals(10000, client.getAccount().getBill());
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void transferCash_NullClient_Test() {
        Client sadClient = new Client("Happy", 10000);

        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.transferCash(null, sadClient.getAccount(), 5000);

        Assertions.assertEquals(10000, sadClient.getAccount().getBill());
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void transferCash_NotEnoughMoney_Test() {
        Client client = new Client("Test", 10000);
        Client happyClient = new Client("Happy", 10000);

        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.transferCash(client, happyClient.getAccount(), 15000);

        Assertions.assertEquals(10000, happyClient.getAccount().getBill());
        Assertions.assertEquals(10000, client.getAccount().getBill());
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void payTest() {
        Client client = new Client("Test", 1000);
        Cashier cashier = new Cashier(new Bank(1000000));

        cashier.pay(client, 100);

        Assertions.assertEquals(900, client.getAccount().getBill());
        Assertions.assertEquals(10100, cashier.getBalance());
    }

    @Test
    public void pay_NullClient_Test() {
        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.pay(null, 100);
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void pay_ClientMayNotToPay_Test() {
        Client client = new Client("Test", 1000);
        Cashier cashier = new Cashier(new Bank(1000000));

        cashier.pay(client, 2000);

        Assertions.assertEquals(1000, client.getAccount().getBill());
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void convertToTest() {
        Client client = new Client("Test", 575);
        Cashier cashier = new Cashier(new Bank(1000000));

        cashier.convertTo(client, 0.3);

        Assertions.assertEquals(172.50, client.getAccount().getBill());
        Assertions.assertEquals(10000, cashier.getBalance());
    }

    @Test
    public void convertTo_NullClient_Test() {
        Cashier cashier = new Cashier(new Bank(1000000));
        cashier.convertTo(null, 0.3);
        Assertions.assertEquals(10000, cashier.getBalance());
    }
}
