package nure.itinf_19_3.Domashina;

import javax.swing.JPanel;
import java.awt.Graphics;
import java.awt.Color;
import java.util.ArrayList;
import java.util.List;

public class Panel extends JPanel {
    private final List<Rectangle> columns;

    private final int width;
    private final int height;

    private int maxValue;

    public Panel(int width, int height) {
        this.width = width;
        this.height = height;

        columns = new ArrayList<>();
        maxValue = 0;
    }

    public void addColumn(int value) {
        Color color;
        if (columns.size() % 2 == 0)
            color = Color.RED;
        else
            color = Color.BLUE;

        if (value > maxValue)
            maxValue = value;

        for (var rect : columns) {
            rect.setSize(
                    (width - 15) / (columns.size() + 1),
                    ((rect.getValue() * height) / maxValue) - 15);
        }

        int rectangleWidth = (width - 15) / (columns.size() + 1);
        int rectangleHeight = ((value * height) / maxValue) - 15;
        columns.add(new Rectangle(color, value, rectangleWidth, rectangleHeight));
    }

    public void paintComponent(Graphics g) {
        g.clearRect(0, 0, this.width, this.height);

        // Draw columns
        int x = 5;
        for (Rectangle column : columns) {
            g.setColor(column.getColor());
            g.fillRect(x, this.height - column.getHeight() - 5, column.getWidth(), column.getHeight());
            g.setColor(Color.BLACK);
            g.drawString(String.valueOf(column.getValue()),
                    x + (column.getWidth() / 2),
                    this.height - column.getHeight() - 5 + (column.getHeight() / 2));
            x += column.getWidth();
        }

        // Draw coordinate arrows
        g.setColor(Color.BLACK);

        g.drawLine(5, 0, 5, height);
        g.drawLine(0, 10, 5, 0);
        g.drawLine(10, 10, 5, 0);

        g.drawLine(0, height - 5, width, height - 5);
        g.drawLine(width - 10, height - 10, width, height - 5);
        g.drawLine(width - 10, height, width, height - 5);
    }
}
