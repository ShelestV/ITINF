package nure.itinf_19_3.Domashina;

import javax.swing.*;
import java.awt.*;

public class GUI extends JFrame {

    public GUI() {
        super("Domashina");
        this.setBounds(50, 50, 1300, 700);
        this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        Container container = this.getContentPane();
        container.setLayout(new GridLayout(1, 2));

        JPanel components = new JPanel();
        components.setLayout(new BoxLayout(components, BoxLayout.Y_AXIS));
        container.add(components);

        Panel diagram = new Panel(600, 600);
        container.add(diagram);

        JLabel label = new JLabel("Значение столбца: ");
        label.setMaximumSize(new Dimension(Integer.MAX_VALUE, label.getMinimumSize().height));
        components.add(label);

        JTextField text = new JTextField("");
        text.setMaximumSize(new Dimension(280, text.getMinimumSize().height));
        components.add(text);

        JButton button = new JButton("Добавить");
        button.setMaximumSize(new Dimension(120, button.getMinimumSize().height));
        button.addActionListener(new ButtonEventListener(this, diagram, text));
        components.add(button);
    }
}
