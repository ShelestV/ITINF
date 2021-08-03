package nure.itinf_19_3.Domashina;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class ButtonEventListener implements ActionListener {
    private final JFrame frame;
    private final JTextField text;
    private final Panel diagram;

    public ButtonEventListener(JFrame frame, Panel diagram, JTextField text) {
        this.text = text;
        this.diagram = diagram;
        this.frame = frame;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        try {
            diagram.addColumn(getInputValue(text.getText()));
            frame.repaint();
        }
        catch (NumberFormatException exception) {
            JOptionPane.showMessageDialog(
                    null,
                    "Input must be number and this number must be greater then 0",
                    "Error",
                    JOptionPane.PLAIN_MESSAGE);
        }
    }

    private int getInputValue(String input) throws NumberFormatException {
        int number = -1;

        try {
            number = Integer.parseInt(input);
        } catch (NumberFormatException exception) {
            throw new NumberFormatException();
        }

        if (number < 0) {
            throw new NumberFormatException();
        }

        return number;
    }
}
