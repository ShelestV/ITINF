package nure.itinf_19_3.shelest;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class BankAccountTests {

    @Test
    public void withdrawCashTest() {
        BankAccount acc = new BankAccount(1000);
        acc.withdrawCash(200);
        Assertions.assertEquals(800, acc.getBill());
    }

    @Test
    public void putCashTest() {
        BankAccount acc = new BankAccount(1000);
        acc.putCash(200);
        Assertions.assertEquals(1200, acc.getBill());
    }

    @Test
    public void convertToTest() {
        BankAccount acc = new BankAccount(575);
        acc.convertTo(0.3);
        Assertions.assertEquals(172.50, acc.getBill());
    }
}
