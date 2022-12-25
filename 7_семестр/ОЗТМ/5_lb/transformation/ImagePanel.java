package transformation;

import java.awt.Color;
import java.awt.Graphics;

import javax.swing.JPanel;

public class ImagePanel extends JPanel {
    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);
        g.setColor(Color.white);
        g.fillRect(0, 0, 900, 500);
    }
}
