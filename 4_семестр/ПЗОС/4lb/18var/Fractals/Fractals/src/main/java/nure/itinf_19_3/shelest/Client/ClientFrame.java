package nure.itinf_19_3.shelest.Client;

import nure.itinf_19_3.shelest.Models.CircleFractal;
import nure.itinf_19_3.shelest.Models.TriangleFractal;
import nure.itinf_19_3.shelest.Resources.Iteration;
import nure.itinf_19_3.shelest.Resources.Status;
import nure.itinf_19_3.shelest.ViewComponents.DrawCircleFractalPanel;
import nure.itinf_19_3.shelest.ViewComponents.DrawTriangleFractalPanel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.KeyEvent;

public class ClientFrame
        extends JFrame {

    private final DrawTriangleFractalPanel triangleFractalPanel;
    private final DrawCircleFractalPanel circleFractalPanel;

    public ClientFrame(final Status status, final IKeySendListener sendListener, Iteration iteration) {
        setTitle("Client");

        setSize(900, 500);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null); // location : screen center

        JPanel mainPanel = new JPanel();
        mainPanel.setLayout(new GridLayout(1, 3));

        JPanel panel = new JPanel();
        JButton startButton = new JButton("Start");
        startButton.setMaximumSize(new Dimension(120, startButton.getMinimumSize().height));
        startButton.addActionListener(new StartButtonEventListener(status, sendListener));
        panel.add(startButton);

        JButton stopButton = new JButton("Stop");
        stopButton.setMaximumSize(new Dimension(120, stopButton.getMinimumSize().height));
        stopButton.addActionListener(new StopButtonEventListener(status, iteration));
        panel.add(stopButton);

        triangleFractalPanel = new DrawTriangleFractalPanel();
        circleFractalPanel = new DrawCircleFractalPanel();

        mainPanel.add(triangleFractalPanel);
        mainPanel.add(circleFractalPanel);
        mainPanel.add(panel);

        this.setVisible(true);

       KeyStroke ctrlAltCKeyStroke = KeyStroke.getKeyStroke(
                KeyEvent.VK_C,
                KeyEvent.ALT_DOWN_MASK | KeyEvent.CTRL_DOWN_MASK);
        String SPECIAL_KEY = "ALT_CTRL_C";
        mainPanel.getInputMap().put(ctrlAltCKeyStroke, SPECIAL_KEY);
        mainPanel.getActionMap().put(SPECIAL_KEY, new AbstractAction() {
            @Override
            public void actionPerformed(ActionEvent e) {
                JOptionPane.showMessageDialog(null, "Iterations : " + iteration.getIteration());
            }
        });
        this.add(mainPanel);
        mainPanel.setFocusable(true);
    }

    public void setTriangleFractalPanel(TriangleFractal fractal) {
        triangleFractalPanel.setFractal(fractal);
        repaint();
    }

    public void setCircleFractalPanel(CircleFractal fractal) {
        circleFractalPanel.setFractal(fractal);
        repaint();
    }
}
