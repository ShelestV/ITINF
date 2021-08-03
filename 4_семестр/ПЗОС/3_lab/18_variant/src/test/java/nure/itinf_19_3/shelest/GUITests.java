package nure.itinf_19_3.shelest;

import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

public class GUITests {
    @Test
    public void getRadiusX_0_Test() {
        int expected = 200;
        int actual = GUI.getRadiusX(0);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void getRadiusX_90_Test() {
        int expected = 400;
        int actual = GUI.getRadiusX(90);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void getRadiusX_180_Test() {
        int expected = 200;
        int actual = GUI.getRadiusX(180);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void getRadiusX_270_Test() {
        int expected = 0;
        int actual = GUI.getRadiusX(270);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void getRadiusY_0_Test() {
        int expected = 0;
        int actual = GUI.getRadiusY(0);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void getRadiusY_90_Test() {
        int expected = 200;
        int actual = GUI.getRadiusY(90);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void getRadiusY_180_Test() {
        int expected = 400;
        int actual = GUI.getRadiusY(180);

        Assertions.assertEquals(expected, actual);
    }

    @Test
    public void getRadiusY_270_Test() {
        int expected = 200;
        int actual = GUI.getRadiusY(270);

        Assertions.assertEquals(expected, actual);
    }
}
