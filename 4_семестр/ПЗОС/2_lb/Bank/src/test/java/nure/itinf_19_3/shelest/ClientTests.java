package nure.itinf_19_3.shelest;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class ClientTests {

    @Test
    public void withdrawCashFromAccountTest() {
        Client client = new Client("Test", 1000);
        client.withdrawCashFromAccount(200);
        Assertions.assertEquals(800, client.getAccount().getBill());
    }

    @Test
    public void putCashOnAccountTest() {
        Client client = new Client("Test", 1000);
        client.putCashOnAccount(200);
        Assertions.assertEquals(1200, client.getAccount().getBill());
    }

    @Test
    public void payTest() {
        Client client = new Client("Test", 1000);
        client.pay(200);
        Assertions.assertEquals(800, client.getAccount().getBill());
    }

    @Test
    public void transferCashNullTest() {
        Client client = new Client("Test", 1000);

        client.transferCash(null, 200);

        Assertions.assertEquals(1000, client.getAccount().getBill());
    }

    @Test
    public void transferCashTest() {
        Client client = new Client("Test", 1000);
        Client happyClient = new Client("Happy", 0);

        client.transferCash(happyClient.getAccount(), 200);

        Assertions.assertEquals(800, client.getAccount().getBill());
        Assertions.assertEquals(200, happyClient.getAccount().getBill());
    }

    @Test
    public void convertToTest() {
        Client client = new Client("Test", 575);
        client.convertTo(0.3);
        Assertions.assertEquals(172.50, client.getAccount().getBill());
    }

    @Test
    public void equalsTest() {
        Client client = new Client("Test",1000);
        Client another = new Client("Test", 500);

        Assertions.assertFalse(client.equals(another));
    }

    @Test
    public void equalsNullTest() {
        Client client = new Client("Test", 1000);
        Assertions.assertFalse(client.equals(null));
    }

    @Test
    public void equalsSameTest() {
        Client client = new Client("Test", 1000);
        Assertions.assertTrue(client.equals(client));
    }
}
