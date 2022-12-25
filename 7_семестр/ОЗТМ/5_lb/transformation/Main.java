package transformation;

import javax.swing.JFrame;

public class Main {
    public static void main(String[] args) {
        var frame = new JFrame();
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setVisible(true);
        frame.setSize(900, 500);

        var panel = new ImagePanel(); // changed this line
        frame.add(panel);

        var transformation = new Transfomation();

        transformation.init("source.jpg");
        transformation.paint(panel.getGraphics());

        frame.validate();
        frame.repaint();
    }
}