package nure.itinf_19_3.shelest.Client;

import nure.itinf_19_3.shelest.Resources.Status;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public record StartButtonEventListener(Status clientStatus, IKeySendListener keySendListener)
        implements ActionListener {

    @Override
    public void actionPerformed(ActionEvent e) {
        clientStatus.work();
        keySendListener.send();
    }
}
