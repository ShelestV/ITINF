package nure.itinf_19_3.shelest.Client;

import nure.itinf_19_3.shelest.Resources.Iteration;
import nure.itinf_19_3.shelest.Resources.Status;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public record StopButtonEventListener(Status clientStatus, Iteration iteration)
        implements ActionListener {

    @Override
    public void actionPerformed(ActionEvent e) {
        clientStatus.doesNotWork();
        JOptionPane.showMessageDialog(null, "Iterations : " + iteration.getIteration());
    }
}