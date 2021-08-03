package nure.itinf_19_3.Domashina;

import javax.swing.JComponent;
import java.awt.Color;
import java.awt.Graphics;
import java.io.Serial;

public class Rectangle extends JComponent {
    @Serial
    private static final long serialVersionUID = 1L;
    private final Color color;

    private int width;
    private int height;

    private final int value;

    public Rectangle(Color color, int value, int width, int height) {
        this.color = color;
        this.value = value;
        this.width = width;
        this.height = height;
    }

    public Color getColor() {
        return color;
    }

    public int getWidth() {
        return width;
    }

    public int getHeight() {
        return height;
    }

    public int getValue() {
        return value;
    }

    public void setSize(int width, int height) {
        this.width = width;
        this.height = height;
    }
}
